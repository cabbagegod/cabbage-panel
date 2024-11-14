using CHPanel.Data;
using CHPanel.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace CHPanel.Views.Pages
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class LoginPage : INavigableView<LoginViewModel>
    {
        public LoginViewModel ViewModel { get; }

        public LoginPage(LoginViewModel viewModel) {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }

        private void OnKeyChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            AuthData.apiKey = ((System.Windows.Controls.TextBox)sender).Text;
        }
    }
}
