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
        public ObservableCollection<Commande> listeCommandeDeEmploye = new ObservableCollection<Commande>();
        public Commande commandeSelectionne;
        public Commande copie;
        public CommandesUserControl()
        {
            InitializeComponent();
            ChercherLesCommandes();
            if(MainWindow.Instance.EstAdmin)
            {
                rechercherEmploye.Visibility= Visibility.Visible;
            }
            this.DataContext = listeCommandeDeEmploye;
            this.Loaded += CommandeUserControl_Loaded;

        }
        private void CommandeUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var vue = CollectionViewSource.GetDefaultView(dgCommande.ItemsSource);
            vue.Filter = RechercheMotClefProduit;
        }

        private bool RechercheMotClefProduit(object obj)
        {
            if (!(obj is Commande c))
                return false;

            if (rechercherRevendeur?.Text != null &&
                !string.IsNullOrWhiteSpace(rechercherRevendeur.Text) &&
                !c.UnRevendeur.RaisonSociale?.StartsWith(rechercherRevendeur.Text, StringComparison.OrdinalIgnoreCase) == true)
                return false;

            if (rechercherTransport?.Text != null &&
                !string.IsNullOrWhiteSpace(rechercherTransport.Text) &&
                !c.UnModeTransport.Nom?.StartsWith(rechercherTransport.Text, StringComparison.OrdinalIgnoreCase) == true)
                return false;

            if (rechercherEmploye?.Text != null &&
                !string.IsNullOrWhiteSpace(rechercherEmploye.Text) &&
                !c.UnEmploye.Nom?.StartsWith(rechercherEmploye.Text, StringComparison.OrdinalIgnoreCase) == true)
                return false;

            if (rechercherDate?.Text != null &&
                !string.IsNullOrWhiteSpace(rechercherDate.Text) &&
                !c.DateCommande.ToString("dd/MM/yyyy")?.Contains(rechercherDate.Text, StringComparison.OrdinalIgnoreCase) == true)
                return false;

            return true;

        }

        private void ChercherLesCommandes()
        {
            listeCommandeDeEmploye.Clear();
            foreach(Commande commande in MainWindow.Instance.Pilot.LesCommandes)
            {
                if(commande.UnEmploye == MainWindow.Instance.EmployeConnecte || MainWindow.Instance.EstAdmin)
                {
                    listeCommandeDeEmploye.Add(commande);
                }
            }
        }

        private void butAjouter_Click(object sender, RoutedEventArgs e)
        {

            Commande nouvelleCommande = new Commande();
            AjouterCommandeUserControl ajouterCommande = new AjouterCommandeUserControl(this, nouvelleCommande, Action.Creer);
            ajouterCommande.ValidationFaite += OnValidationFaiteAjouter;
            this.acuc = ajouterCommande;
            MainWindow.Instance.vueActuelle.Content = this.acuc;
        }

        private void OnValidationFaiteAjouter(bool estValide)
        {
            if (estValide)
            {
                try
                {
                    this.acuc.UneCommande.Id = this.acuc.UneCommande.Create();
                    MainWindow.Instance.Pilot.LesCommandes.Add(this.acuc.UneCommande);
                    ChercherLesCommandes();
                    dgReset();
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
        private void RefreshDg()
        {
            CollectionViewSource.GetDefaultView(dgCommande.ItemsSource).Refresh();
        }

        private void butModifier_Click(object sender, RoutedEventArgs e)
        {

            if (dgCommande.SelectedItem == null)
                MessageBox.Show("Veuillez sélectionner une commande", "Attention",
                MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                this.commandeSelectionne = (Commande)dgCommande.SelectedItem;
                 this.copie = new Commande(commandeSelectionne.Id, commandeSelectionne.UnEmploye,
                    commandeSelectionne.UnModeTransport, commandeSelectionne.UnRevendeur, commandeSelectionne.LesSousCommandes,
                    commandeSelectionne.DateCommande, commandeSelectionne.DateLivraison);
                AjouterCommandeUserControl ajouterCommande = new AjouterCommandeUserControl(this, copie, Action.Modifier);
                ajouterCommande.ValidationFaite += OnValidationFaiteModifier;
                this.acuc = ajouterCommande;
                MainWindow.Instance.vueActuelle.Content = this.acuc;
                
            }

        }


        private void OnValidationFaiteModifier(bool estValide)
        {
            if (estValide == true)
            {
                try
                {
                    commandeSelectionne.Update();
                    commandeSelectionne.UnEmploye = copie.UnEmploye;
                    commandeSelectionne.UnModeTransport = copie.UnModeTransport;
                    commandeSelectionne.UnRevendeur = copie.UnRevendeur;
                    commandeSelectionne.LesSousCommandes = copie.LesSousCommandes;
                    commandeSelectionne.DateCommande = copie.DateCommande;
                    commandeSelectionne.DateLivraison = copie.DateLivraison;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("La commande n'a pas pu être modifié.", "Attention",
               MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void dgReset()
        {
            ChercherLesCommandes();
            CollectionViewSource.GetDefaultView(dgCommande.ItemsSource).Refresh();
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
                        dgReset();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("La commande n'a pas pu être supprimé.", "Attention",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            
            }
        }

        private void rechercherTransport_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshDg();
        }

        private void rechercherRevendeur_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshDg();
        }

        private void rechercherEmploye_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshDg();
        }

        private void rechercherDate_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshDg();
        }
    }
}
