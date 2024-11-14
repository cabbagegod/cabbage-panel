using CHPanel.ViewModels.Windows;
using CHPanel.Views.Windows;
using System.Net;
using System.Net.Http;
using System.Windows.Input;
using CHPanel.Data;
using CHPanel.Helpers;
using CHPanel.Models;
using CHPanel.Models.Dtos;
using Wpf.Ui.Controls;

namespace CHPanel.ViewModels.Pages {
	public partial class DashboardViewModel : ObservableObject {
        //[ObservableProperty] private List<ServerInfo> servers = new();
        [ObservableProperty] private List<ServerData> servers = new();
        
		public DashboardViewModel() {
			LoadServers();
		}

		private async void LoadServers() {
			ServerList? info = await HttpUtil.GetAsync<ServerList>("api/client", AuthData.apiKey);

			if (info == null)
				return;

			List<ServerData> newServers = new();
            
			foreach (ServerInfo server in info.data.Where(server => server.attributes != null)) {
				ServerResources? resources = await HttpUtil.GetAsync<ServerResources>($"api/client/servers/{server.attributes!.identifier}/resources", AuthData.apiKey);
				if (resources == null)
					continue;
				
				newServers.Add(new ServerData(server, resources));
			}
			
			Servers = newServers;
		}
	}
}