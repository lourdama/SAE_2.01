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

namespace PilotApp.Views
{
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
            
        }
    }
}
