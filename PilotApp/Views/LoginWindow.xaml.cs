using PilotApp.ViewModels;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Wpf.Ui.Controls;

namespace PilotApp.Views
{
    /// <summary>
    /// Logique d'interaction pour LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : FluentWindow
    {
        public LoginWindow()
        {
            InitializeComponent();
            var viewModel = new LoginViewModel();
            DataContext = viewModel;

            // S'abonner aux événements du ViewModel
            viewModel.LoginSuccessful += OnLoginSuccessful;
            viewModel.CloseRequested += OnCloseRequested;

            // Focus sur le champ username au démarrage
            Loaded += (s, e) => UsernameTextBox.Focus();
        }

        private void OnLoginSuccessful(object sender, EventArgs e)
        {
            try
            {
                // Ouvrir MainWindow et fermer LoginWindow
                var mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Erreur lors de l'ouverture de la fenêtre principale : {ex.Message}",
                              "Erreur", System.Windows.MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnCloseRequested(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as LoginViewModel;
            viewModel?.CloseCommand.Execute(null);
        }

        // Gérer la touche Enter dans les champs de texte
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var viewModel = DataContext as LoginViewModel;
                viewModel?.HandleEnterKey();
            }
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            // Ajouter les gestionnaires d'événements pour la touche Enter
            UsernameTextBox.KeyDown += OnKeyDown;
            PasswordBox.KeyDown += OnKeyDown;
        }

        protected override void OnClosed(EventArgs e)
        {
            // Désabonnement pour éviter les fuites mémoire
            if (DataContext is LoginViewModel viewModel)
            {
                viewModel.LoginSuccessful -= OnLoginSuccessful;
                viewModel.CloseRequested -= OnCloseRequested;
                viewModel.Dispose();
            }
            base.OnClosed(e);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}