using PilotApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
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
using static System.Collections.Specialized.BitVector32;

namespace PilotApp.Views.UserControls
{
    /// <summary>
    /// Logique d'interaction pour AjouterCommandeUserControl.xaml
    /// </summary>
    public partial class AjouterCommandeUserControl : UserControl
    {
        private Action action;
        private Commande commande;

        public AjouterCommandeUserControl(Commande commande, Action actions)
        {
            InitializeComponent();
            this.commande = commande;
            this.action = actions;
            this.DataContext = MainWindow.Instance.Pilot;

            if (action == Action.Creer)
                butValiderCommande.Content = "Créer";
            else
                butValiderCommande.Content = "Modifier";
        }

        private void butValiderCommande_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                commande.Create();
                MainWindow.Instance.Pilot.LesCommandes.Add(commande);
                MainWindow.Instance.vueActuelle.Content = new CommandesUserControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la création de la commande.");
            }

        }


    }
}
