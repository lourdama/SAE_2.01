using PilotApp.Fenetre;
using PilotApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
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
        public AjouterCommandeUserControl acuc;
        public CommandesUserControl()
        {
            InitializeComponent();
            this.DataContext = MainWindow.Instance.Pilot.LesCommandes;

        }

        private void butAjouter_Click(object sender, RoutedEventArgs e)
        {

            Commande nouvelleCommande = new Commande();
            AjouterCommandeUserControl ajouterCommande = new AjouterCommandeUserControl(this, nouvelleCommande, Action.Creer);
            ajouterCommande.ValidationFaite += OnValidationFaiteAjouter;
            this.acuc = ajouterCommande;
            MainWindow.Instance.vueActuelle.Content = this.acuc;

            
            //var fenetre = new AjouterCommande(nouvelleCommande, AjouterCommande.Action.Créer);
            //bool? result = fenetre.ShowDialog();
        }

        private void OnValidationFaiteAjouter(bool estValide)
        {
            if (estValide)
            {
                try
                {
                    this.acuc.UneCommande.Id = this.acuc.UneCommande.Create();
                    MainWindow.Instance.Pilot.LesCommandes.Add(this.acuc.UneCommande);
                }
                catch
                {
                    MessageBox.Show("Erreur lors de l'ajout de la commande.");
                }
            }
            else
            {
                MessageBox.Show("Validation annulée.");
            }

        }

        private void butModifier_Click(object sender, RoutedEventArgs e)
        {

        }

        private void butSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (dgCommande.SelectedItem == null)
                MessageBox.Show("Veuillez sélectionner une commande", "Attention",
                MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                Commande commandeASuprimer = ((Commande)dgCommande.SelectedItem);

                MessageBoxResult result;
                result = MessageBox.Show($"Désirez-vous supprimer cette commande ? Cette action est définitive", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        commandeASuprimer.Delete();
                        MainWindow.Instance.Pilot.LesCommandes.Remove(commandeASuprimer);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("La commande n'a pas pu être supprimé.", "Attention",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            
            }
        }
    }
}
