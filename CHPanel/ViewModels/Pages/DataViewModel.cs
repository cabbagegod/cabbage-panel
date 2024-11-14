using CHPanel.Models;
using System.Windows.Media;
using Wpf.Ui.Controls;

namespace CHPanel.ViewModels.Pages; 

public partial class DataViewModel : ObservableObject, INavigationAware {
	[ObservableProperty] private ServerData serverData = new(null, null);
	
	public void DisplayServer(ServerData data) {
		ServerData = data;
	}
	
	public void OnNavigatedTo() { }
	public void OnNavigatedFrom() { }
}