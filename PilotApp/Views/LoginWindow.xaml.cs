using PilotApp.Services;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Wpf.Ui.Controls;
using PilotApp.Views.UserControls;


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

         
            Loaded += (s, e) => UsernameTextBox.Focus();
            var parametres = GestionnaireParametres.Charger();
            if (parametres.ResterConnecte)
            {
                UsernameTextBox.Text = parametres.NomUtilisateur;
                PasswordBoxMDP.Password = parametres.MotDePasse;
                ResterConnecteCheckBox.IsChecked = true;
                LoginButton_Click(LoginButton, null);
            }

        }





        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            
            var mainWindow = new MainWindow();
            new AuthenticationService(UsernameTextBox.Text, PasswordBoxMDP.Password);
            if (MainWindow.Instance.connexion)
            {
                bool reste = ResterConnecteCheckBox.IsChecked == true;
                var nouveau = new FichierParametres
                {
                    ResterConnecte = reste,
                    NomUtilisateur = reste ? UsernameTextBox.Text : "SAE201",
                    MotDePasse = reste ? PasswordBoxMDP.Password : "EASTER EGG"
                };
                
                GestionnaireParametres.Sauvegarder(nouveau);
                mainWindow.Show();
                MainWindow.Instance.vueActuelle.Content = new Accueil();
                this.Close();
            }
            else
            {
                messageErreur.Visibility = Visibility.Visible;
            }
                 
            
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();  
        }


    }
}