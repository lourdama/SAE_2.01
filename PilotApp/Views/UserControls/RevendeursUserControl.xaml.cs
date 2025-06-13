using System;
using System.Collections.Generic;
using System.Data.Common;
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
using PilotApp.Models;

namespace PilotApp.Views.UserControls
{
    /// <summary>
    /// Logique d'interaction pour RevendeursUserControl.xaml
    /// </summary>
    public partial class RevendeursUserControl : UserControl
    {
        public AjouterRevendeurUserControl aruc;
        public Revendeur RevendeurSelectionne;
        public Revendeur Copie;
        public RevendeursUserControl()
        {
            InitializeComponent();
            this.DataContext = MainWindow.Instance.Pilot.LesRevendeurs;
            this.Loaded += RevendeursUserControl_Loaded;
        }
        private void RevendeursUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var vue = CollectionViewSource.GetDefaultView(dgRevendeur.ItemsSource);
            vue.Filter = RechercheMotClefProduit;
        }

        private bool RechercheMotClefProduit(object obj)
        {
            if (!(obj is Revendeur r))
                return false;

            if (rechercheRaison?.Text != null &&
                !string.IsNullOrWhiteSpace(rechercheRaison.Text) &&
                !r.RaisonSociale?.StartsWith(rechercheRaison.Text, StringComparison.OrdinalIgnoreCase) == true)
                return false;

            if (rechercheVille?.Text != null &&
                !string.IsNullOrWhiteSpace(rechercheVille.Text) &&
                !r.Ville?.StartsWith(rechercheVille.Text, StringComparison.OrdinalIgnoreCase) == true)
                return false;

            if (rechercheCP?.Text != null &&
                !string.IsNullOrWhiteSpace(rechercheCP.Text) &&
                !r.CodePostal?.StartsWith(rechercheCP.Text, StringComparison.OrdinalIgnoreCase) == true)
                return false;

            return true;

        }
    

        private void butAjouter_Click(object sender, RoutedEventArgs e)
        {
            Revendeur nouveauRevendeur = new Revendeur();
            AjouterRevendeurUserControl ajouterRevendeur = new AjouterRevendeurUserControl(this, nouveauRevendeur, Action.Creer);
            ajouterRevendeur.ValidationFaite += OnValidationFaiteAjouter;
            this.aruc = ajouterRevendeur;
            MainWindow.Instance.vueActuelle.Content = this.aruc;
        }

        private void OnValidationFaiteAjouter(bool estValide)
        {
            if (estValide)
            {
                try
                {
                    this.aruc.UnRevendeur.Id = this.aruc.UnRevendeur.Create();
                    MainWindow.Instance.Pilot.LesRevendeurs.Add(this.aruc.UnRevendeur);
                }
                catch
                {
                    MessageBox.Show("Erreur lors de l'ajout du revendeur.");
                }
            }
            else
            {
                MessageBox.Show("Validation annulée.");
            }
        }

        private void butModifier_Click(object sender, RoutedEventArgs e)
        {
            if (dgRevendeur.SelectedItem == null)
                MessageBox.Show("Veuillez sélectionner un revendeur", "Attention",
                MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                this.RevendeurSelectionne = (Revendeur)dgRevendeur.SelectedItem;
                this.Copie = new Revendeur(RevendeurSelectionne.Id,RevendeurSelectionne.RaisonSociale, RevendeurSelectionne.Rue,
                   RevendeurSelectionne.Ville, RevendeurSelectionne.CodePostal);
                AjouterRevendeurUserControl modifierRevendeur = new AjouterRevendeurUserControl(this, Copie, Action.Modifier);
                modifierRevendeur.ValidationFaite += OnValidationFaiteModifier;
                this.aruc = modifierRevendeur;
                MainWindow.Instance.vueActuelle.Content = this.aruc;

            }
        }

        private void OnValidationFaiteModifier(bool estValide)
        {
            if (estValide == true)
            {
                try
                {
                    
                    RevendeurSelectionne.RaisonSociale = Copie.RaisonSociale;
                    RevendeurSelectionne.Rue = Copie.Rue;
                    RevendeurSelectionne.Ville = Copie.Ville;
                    RevendeurSelectionne.CodePostal = Copie.CodePostal;
                    RevendeurSelectionne.Update();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Le revendeur n'a pas pu être modifié.", "Attention",
               MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void butSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (dgRevendeur.SelectedItem == null)
                MessageBox.Show("Veuillez sélectionner un revendeur", "Attention",
                MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                Revendeur revendeurASupprimer = ((Revendeur)dgRevendeur.SelectedItem);

                MessageBoxResult result;
                result = MessageBox.Show($"Désirez-vous supprimer ce revendeur ? Cette action est définitive", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        revendeurASupprimer.Delete();
                        MainWindow.Instance.Pilot.LesRevendeurs.Remove(revendeurASupprimer);
                        for (int i = MainWindow.Instance.Pilot.LesCommandes.Count - 1; i >= 0; i--)
                        {
                            Commande commande = MainWindow.Instance.Pilot.LesCommandes[i];
                
                            if (commande.UnRevendeur == revendeurASupprimer)
                            {
                                MainWindow.Instance.Pilot.LesCommandes.RemoveAt(i);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Le revendeur n'a pas pu être supprimé.", "Attention",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

            }
        }

        private void filtre_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshDg();
        }

        private void RefreshDg()
        {
            CollectionViewSource.GetDefaultView(dgRevendeur.ItemsSource).Refresh();
        }
    }
}
