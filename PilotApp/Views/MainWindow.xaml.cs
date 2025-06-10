using PilotApp.Models;
using System;
using System.Reflection.Metadata;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Controls;
using System.Windows;
using System.ComponentModel;
using PilotApp.Services;
using PilotApp.Views.UserControls;

namespace PilotApp.Views
{
    public enum Action { Modifier, Creer }
    public partial class MainWindow : FluentWindow
    {
        public bool EstCommercial { get; set; }
        public bool EstResponsable { get; set; }
        public bool EstAdmin { get; set; }
        public static MainWindow Instance { get; private set; }
        public Entreprise Pilot { get; set; }

        public string login { get; set; }
        public string mdp { get; set; }
        public Employe EmployeConnecte { get; set; }
       
        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            DataContext = this;
        }


        public void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void DeconnexionButton_Click(object sender, RoutedEventArgs e)
        {
            EstCommercial = false;
            EstResponsable = false;
            EstAdmin = false;
            Pilot = null;
            login = null;
            mdp = null;
            EmployeConnecte = null;
            DataAccess.Instance.CloseConnection();
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void butCommande_Click(object sender, RoutedEventArgs e)
        {
            this.vueActuelle.Content= new CommandesUserControl();
        }

        private void butRevendeurs_Click(object sender, RoutedEventArgs e)
        {

        }

        private void butMesCommandes_Click(object sender, RoutedEventArgs e)
        {

        }

        private void butProduits_Click(object sender, RoutedEventArgs e)
        {

        }

        private void butGestionProduits_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
