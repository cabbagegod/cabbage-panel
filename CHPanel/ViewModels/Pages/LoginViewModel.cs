using CHPanel.ViewModels.Windows;
using CHPanel.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;
using System.IO;
using System.Diagnostics;
using CHPanel.Data;
using System.Text.Json.Serialization;
using CHPanel.Helpers;
using CHPanel.Models.Dtos;
using MessageBox = System.Windows.MessageBox;

namespace CHPanel.ViewModels.Pages {
	public partial class LoginViewModel : ObservableObject {
		[ObservableProperty] private string _apiKey = string.Empty;
		[ObservableProperty] private string username = string.Empty;

		private MainWindowViewModel MainWindowViewModel { get; }

		[ObservableProperty] private RelayCommand _relayCommand;

		private string keyPath;

		public LoginViewModel(MainWindowViewModel mainWindowModel) {
			MainWindowViewModel = mainWindowModel;

			_relayCommand = new RelayCommand(SaveApiKeyClick);

			TryLoadCustomUrl();
            
			keyPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/chpanel";
			TryLoadApiKey();
		}

		private async void SaveApiKeyClick() {
			AccountInfo? account = await HttpUtil.GetAsync<AccountInfo>("api/client/account", ApiKey);

			if (account == null) return;
			
			Username = $"Logged in as: {account.attributes?.username}";

			AuthData.apiKey = ApiKey;
			WriteApiKey();
			RefreshNav();
		}

		private void RefreshNav() {
            MainWindowViewModel.MenuItems.Add(new NavigationViewItem() {
                Content = "Dashboard",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
                TargetPageType = typeof(Views.Pages.DashboardPage)
            });
            MainWindowViewModel.MenuItems.Add(new NavigationViewItem {
				Content = "Data",
				Icon = new SymbolIcon { Symbol = SymbolRegular.DataHistogram24 },
				TargetPageType = typeof(Views.Pages.DataPage)
			});

			MainWindow.Instance.RefreshNav();
		}

		private void TryLoadApiKey() {
            if (File.Exists(keyPath + "/chpanel.key")) {
                ApiKey = File.ReadAllText(keyPath + "/chpanel.key");
                SaveApiKeyClick();
            }
        }

		private void WriteApiKey() {
			if(!Directory.Exists(keyPath))
				Directory.CreateDirectory(keyPath);

            File.WriteAllText(keyPath + "/chpanel.key", ApiKey);
        }

		private static void TryLoadCustomUrl() {
			string customUrl = CustomUrlUtil.TryGetCustomUrl();
			if (string.IsNullOrEmpty(customUrl))
				return;

			if (customUrl[^1] != '/')
				MessageBox.Show("The Custom URL for this app does not seem to be configured correctly. Please contact an administrator.", "Critical Failure");
			
			HttpUtil.urlBase = customUrl;
		}
	}
}