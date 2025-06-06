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
        private MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        public PageConnexion()
        {
            InitializeComponent();

        }

        private void butConnexion_Click(object sender, RoutedEventArgs e)
        {
            ChargeData();
            MessageBox.Show(mainWindow.Pilot.LesCouleurs.Count.ToString());
            

        }
        public void ChargeData()
        {
            try
            {
                mainWindow.login = login.Text;
                mainWindow.mdp = mdp.Text;
                mainWindow.Pilot = new Entreprise("Pilot");
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Problème lors de récupération des données, veuillez consulter votre admin");
                LogError.Log(ex, "Erreur SQL");
                Application.Current.Shutdown();
            }
        }
    }
}
