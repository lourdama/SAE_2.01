using PilotApp.Helpers;
using PilotApp.Services;
using PilotApp.Models;
using PilotApp.Views;
using PilotApp.Views.UserControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Wpf.Ui.Controls;

namespace PilotApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly AuthenticationService _authService;
        private readonly NavigationService _navigationService;
        private UserControl _currentView;

        public MainViewModel(AuthenticationService authService)
        {
            _authService = authService;
            _navigationService = new NavigationService();

            
            NavigateCommand = new RelayCommand<string>(NavigateTo);

            // Navigation par défaut
            NavigateToDefault();
        }

        public Employe CurrentUser => _authService.CurrentUser;

        public bool IsCommercial = true;//=> _authService.IsCommercial;
        public bool IsResponsableProduction = true;//=> _authService.IsResponsableProduction;

        public UserControl CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        public ICommand LogoutCommand { get; }
        public ICommand NavigateCommand { get; }

        public void InitializeNavigation(ContentPresenter contentPresenter)
        {
            _navigationService.Initialize(contentPresenter);
        }

        private void NavigateTo(string tag)
        {
            UserControl view = tag switch
            {
                "commandes" when IsCommercial => new CommandesUserControl(),
                "mescommandes" when IsCommercial => new CommandesUserControl(), // Même UC avec paramètre différent
                "revendeurs" when IsCommercial => new RevendeursUserControl(),
                "produits" => new ProduitsUserControl(),
                "gestionproduits" when IsResponsableProduction => new GestionProduitsUserControl(),
                _ => null
            };

            if (view != null)
            {
                CurrentView = view;
                _navigationService.NavigateTo(view);
            }
        }

        private void NavigateToDefault()
        {
            if (IsCommercial)
            {
                NavigateTo("commandes");
            }
            else if (IsResponsableProduction)
            {
                NavigateTo("produits");
            }
        }

        public void Logout()
        {
            _authService.Logout();

            // Retourner à la fenêtre de login
            var loginWindow = new LoginWindow();
            loginWindow.DataContext = new LoginViewModel();

            // Fermer la fenêtre principale
            var mainWindow = Application.Current.Windows.OfType<FluentWindow>().FirstOrDefault(w => w is MainWindow);
            mainWindow?.Close();

            loginWindow.Show();
        }
    }
}
