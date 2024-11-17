using System.Diagnostics;
using System.Net.Http;
using System.Windows.Controls;
using CHPanel.Models;
using CHPanel.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace CHPanel.Views.Pages
{
    public partial class DataPage : INavigableView<DataViewModel>
    {
        public DataViewModel ViewModel { get; }
        
        public DataPage(DataViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }

        private void ClickedTab(object sender, SelectionChangedEventArgs e) {
            TabControl tabControl = (TabControl) sender;
            ServerData data = (ServerData) tabControl.SelectedItem;
            
            ViewModel.DisplayServer(data);
            ViewModel.TabControl = tabControl;
        }

        private void OnStartClicked(object sender, RoutedEventArgs e) {
            ViewModel.StartServer();
        }

        private void OnRestartClicked(object sender, RoutedEventArgs e) {
            ViewModel.RestartServer();
        }

        private void OnStopClicked(object sender, RoutedEventArgs e) {
            ViewModel.StopServer();
        }
    }
}
