using PilotApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PilotApp.View
{
    /// <summary>
    /// Logique d'interaction pour PageConnexion.xaml
    /// </summary>
    public partial class PageConnexion : UserControl
    {
        public MainWindow mainWindow;
        public PageConnexion()
        {
            InitializeComponent();
            
        }

        private void butConnexion_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Login : {login.Text} Mot de passe : {mdp.Text}");
            ChargeData();

        }
        public void ChargeData()
        {
            try
            {
                mainWindow.Pilot = new Entreprise("Pension dog");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Problème lors de récupération des données, veuillez consulter votre admin");
                LogError.Log(ex, "Erreur SQL");
                Application.Current.Shutdown();
            }
        }
}
