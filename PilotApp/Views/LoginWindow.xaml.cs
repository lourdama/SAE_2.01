using PilotApp.Services;
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

         
            // Focus sur le champ username au démarrage
            Loaded += (s, e) => UsernameTextBox.Focus();
        }




        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            
            var mainWindow = new MainWindow();
            new AuthenticationService(UsernameTextBox.Text, PasswordBoxMDP.Password);
            if (MainWindow.Instance.connexion)
            {
                mainWindow.Show();
                this.Close();
            }
                 
            
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}