using PilotApp.Models;
using PilotApp.Views;
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
using PilotApp.ViewModels;
using Wpf.Ui.Controls;
using System.Windows;

namespace PilotApp.Views
{
    public partial class MainWindow : FluentWindow
    {
        public static MainWindow Instance { get; private set; }
        public Entreprise Pilot { get; set; }

        public string login { get; set; }
        public string mdp { get; set; }
        public Employe employeconnecte { get; set; }
       
        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Initialiser la navigation si le DataContext est un MainViewModel
           
            
        }

        private void DeconnexionButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
