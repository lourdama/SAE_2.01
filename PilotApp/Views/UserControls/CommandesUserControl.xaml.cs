using PilotApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            InitializeComponent();
            this.DataContext = MainWindow.Instance.Pilot.LesCommandes;

        }

        private void butAjouter_Click(object sender, RoutedEventArgs e)
        {

            Commande uneCommande = new Commande();
            MainWindow.Instance.vueActuelle.Content = new AjouterCommandeUserControl(uneCommande, Action.Creer);



        }

        private void butModifier_Click(object sender, RoutedEventArgs e)
        {

        }

        private void butSupprimer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
