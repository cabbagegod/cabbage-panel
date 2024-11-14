using System.Data;
using System.Diagnostics;
using System.Net;
using CHPanel.Models;
using CHPanel.ViewModels.Pages;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace CHPanel.Views.Pages {
	public partial class DashboardPage : INavigableView<DashboardViewModel> {
		public DashboardViewModel ViewModel { get; }

		private INavigationService navService;
		private DataViewModel dataVm;
		
		public DashboardPage(DashboardViewModel viewModel, INavigationService navigationService, DataViewModel dataViewModel) {
			ViewModel = viewModel;
			DataContext = this;

			navService = navigationService;
			dataVm = dataViewModel;
			
			InitializeComponent();
		}
		
		private void OnServerClicked(object sender, RoutedEventArgs e) {
			Button button = (Button) sender;
			ServerData data = (ServerData) button.DataContext;
			
			//Show the data page
			bool success = navService.Navigate(typeof(DataPage));
			if (success) {
				dataVm.DisplayServer(data);
			}
		}
	}
}