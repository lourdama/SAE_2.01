using PilotApp.Models;
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

namespace PilotApp.Views.UserControls
{
    /// <summary>
    /// Logique d'interaction pour CommandesUserControl.xaml
    /// </summary>
    public partial class CommandesUserControl : UserControl
    {
        public CommandesUserControl()
        {
            ChargeData();
            InitializeComponent();
        }
        public void ChargeData()
        {
            try
            {
                Commande LaCommande = new Commande();
                this.DataContext = LaCommande;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problème lors de récupération des données, veuillez consulter votre admin");

                Application.Current.Shutdown();
            }
        }
    }
}
