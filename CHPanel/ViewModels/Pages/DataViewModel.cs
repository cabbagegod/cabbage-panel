using System.Diagnostics;
using System.Net.Http;
using System.Windows.Controls;
using CHPanel.Models;
using System.Windows.Media;
using CHPanel.Helpers;
using CHPanel.Models.Dtos;
using Wpf.Ui.Controls;

namespace CHPanel.ViewModels.Pages; 

public partial class DataViewModel : ObservableObject, INavigationAware {
	[ObservableProperty] private ServerData serverData = new(null, null);
	
	[ObservableProperty] private List<ServerData> selectedServers = new();
	
	public TabControl? TabControl { get; set; }

	private CancellationTokenSource serverUpdateToken = new();

	private const string powerStateEndpoint = "api/client/servers/{0}/power";
	
	~DataViewModel() {
		serverUpdateToken.Cancel();
	}
    
	public void DisplayServer(ServerData data) {
		if (ServerData == data)
			return;
        
		ServerData = data;

		TryAddCurrentServerToList();
		
		OnSelectedServersChanged();
	}
	
	private void OnSelectedServersChanged() {
		if (TabControl == null)
			return;

		foreach (object? item in TabControl.Items) {
			if(item != ServerData)
				continue;
			
			TabControl.SelectedIndex = TabControl.Items.IndexOf(item);
		}
	}

	private void StartNewLoop() {
		serverUpdateToken.Cancel();
		serverUpdateToken = new CancellationTokenSource();
		UpdateServerData();
	}
    
	//TODO: Update this with a socket connection
	private async void UpdateServerData() {
		CancellationToken token = serverUpdateToken.Token;
		
		while (true) {
			try {
				ServerResources? resources = await HttpUtil.GetAsync<ServerResources>($"api/client/servers/{ServerData.Info.attributes!.identifier}/resources");
				token.ThrowIfCancellationRequested();

				if (resources != null) {
					ServerData updatedServerData = ServerData;
					updatedServerData.Update(updatedServerData.Info, resources);
					
					ServerData = new ServerData(null, null);
					ServerData = updatedServerData;
				}

				//TODO: We could check the header for the remaining requests per minute and adjust the delay accordingly, but this is probably going to be replaced with a socket connection
				await Task.Delay(1000, token); //Only poll at most every 1 second (60rpm/240rpm)
				token.ThrowIfCancellationRequested();
			} catch (Exception e) {
				if (e is TaskCanceledException)
					return;
				
				Debug.WriteLine(e);
			}
		}
	}

	private void TryAddCurrentServerToList() {
		if (SelectedServers.Contains(ServerData)) //Dont run if the object is already in the list
			return;
		
		ServerData? duplicateData = SelectedServers.FirstOrDefault(selectedData => string.Equals(selectedData.Info.attributes?.identifier, ServerData.Info.attributes!.identifier));
		if (duplicateData != null) //If there is a different object in SelectedServers with the same identifier, replace it with the new one
			SelectedServers.Remove(duplicateData);
		else
			StartNewLoop();
		
		List<ServerData> newSelectedServers = new(SelectedServers) { ServerData };
		SelectedServers = newSelectedServers;
	}

	public void StartServer() {
		SendServerSignal(Signal.SignalType.start);
	}

	public void RestartServer() {
		SendServerSignal(Signal.SignalType.restart);
	}
	
	public void StopServer() {
		SendServerSignal(Signal.SignalType.stop);
	}

	private async void SendServerSignal(Signal.SignalType signal) {
		string formattedEndpoint = string.Format(powerStateEndpoint, ServerData.Info.attributes!.identifier);
		
		bool success = await HttpUtil.PostAsync(formattedEndpoint, new Signal() { ServerSignal = signal });
		
		Debug.WriteLine($"Signal {signal.ToString()} was {success}");
	}
	
	public void OnNavigatedTo() { }
	public void OnNavigatedFrom() { }
}