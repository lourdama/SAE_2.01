using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Logique d'interaction pour ProduitsUserControl.xaml
    /// </summary>
    public partial class ProduitsUserControl : UserControl
    {
        public AjouterProduitUserControl apuc;
        public Produit produitSelectionne;
        public Produit copie;
        public ProduitsUserControl()
        {
            InitializeComponent();
            if (MainWindow.Instance.EstCommercial && !MainWindow.Instance.EstAdmin)
            {
                butAjouter.Visibility = Visibility.Collapsed;
                butModifier.Visibility = Visibility.Collapsed;
                butSupprimer.Visibility = Visibility.Collapsed;
            }

            dgProduits.ItemsSource = MainWindow.Instance.Pilot.LesProduits;
            var vue = CollectionViewSource.GetDefaultView(dgProduits.ItemsSource);
            vue.Filter = RechercheMotClefProduit;

            comboBoxFiltreTypePointe.Items.Clear();
            comboBoxFiltreTypePointe.Items.Add("Aucun");
            foreach (var tp in MainWindow.Instance.Pilot.LesTypesPointes)
                comboBoxFiltreTypePointe.Items.Add(tp);
            comboBoxFiltreTypePointe.SelectedIndex = 0;
            comboBoxFiltreTypePointe.SelectionChanged += (_, __) => RefreshDg();

            comboBoxFiltreType.Items.Clear();
            comboBoxFiltreType.Items.Add("Aucun");
            foreach (var t in MainWindow.Instance.Pilot.LesTypes)
                comboBoxFiltreType.Items.Add(t);
            comboBoxFiltreType.SelectedIndex = 0;
            comboBoxFiltreType.SelectionChanged += (_, __) => RefreshDg();
        }

        private bool RechercheMotClefProduit(object obj)
        {
            if (!(obj is Produit p))
                return false;


            if (!string.IsNullOrWhiteSpace(textBoxFiltreCode.Text) &&
                !p.Code.StartsWith(textBoxFiltreCode.Text, StringComparison.OrdinalIgnoreCase))
                return false;


            if (!string.IsNullOrWhiteSpace(textBoxFiltreNom.Text) &&
                !p.Nom.StartsWith(textBoxFiltreNom.Text, StringComparison.OrdinalIgnoreCase))
                return false;

            if (comboBoxFiltreTypePointe.SelectedItem is TypePointe tpFilter &&
                p.UnTypePointe != tpFilter)
                return false;


            if (comboBoxFiltreType.SelectedItem is PilotApp.Models.Type tFilter &&
                p.UnType != tFilter)
                return false;


            if (decimal.TryParse(numberBoxFiltrePrixVente.Text, out var prixMax) &&
                p.PrixVente > prixMax)
                return false;


            if (int.TryParse(numberBoxFiltreQuantite.Text, out var qteMax) &&
                p.QuantiteStock > qteMax)
                return false;


            bool showAvailable = checkBoxDisponibiliteTrue.IsChecked == true;
            bool showUnavailable = checkBoxDisponibiliteFalse.IsChecked == true;


            if (!(showAvailable && showUnavailable))
            {

                if (showAvailable && !p.Disponible)
                    return false;

                if (showUnavailable && p.Disponible)
                    return false;
            }

            return true;

        }

        private void butAjouter_Click(object sender, RoutedEventArgs e)
        {
            Produit unProduit = new Produit();
            AjouterProduitUserControl ajouterProduit = new AjouterProduitUserControl(this, unProduit,Action.Creer);
            ajouterProduit.ValidationFaite += OnValidationFaiteAjouter;
            this.apuc = ajouterProduit;
            MainWindow.Instance.vueActuelle.Content = this.apuc;

        }

        private void OnValidationFaiteAjouter(bool estValide)
        {
            if (estValide)
            {
                try
                {
                    if (this.apuc.unProduit.LesCouleurs == null || this.apuc.unProduit.LesCouleurs.Count == 0)
                    {
                        MessageBox.Show("Le produit doit avoir au moins une couleur.",
                            "Erreur de validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    int nouveauId = this.apuc.unProduit.Create();

                    if (nouveauId > 0)
                    {
                        this.apuc.unProduit.Id = nouveauId;
                        MainWindow.Instance.Pilot.LesProduits.Add(this.apuc.unProduit);
                        MessageBox.Show("Produit créé avec succès !",
                            "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la création du produit : ID non valide.",
                            "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Le produit n'a pas pu être créé.\nErreur : {ex.Message}",
                        "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);

                    System.Diagnostics.Debug.WriteLine($"Erreur création produit : {ex}");
                }
            }
            else
            {
                MessageBox.Show("Validation annulée.");
            }
        }

        private void butModifier_Click(object sender, RoutedEventArgs e)
        {
            if (dgProduits.SelectedItem == null)
                MessageBox.Show("Veuillez sélectionner un produit", "Attention",
                MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                this.produitSelectionne = (Produit)dgProduits.SelectedItem;
                this.copie = new Produit(produitSelectionne.Id, produitSelectionne.UnTypePointe, produitSelectionne.UnType, produitSelectionne.LesCouleurs,
                    produitSelectionne.Code, produitSelectionne.Nom, produitSelectionne.PrixVente, produitSelectionne.QuantiteStock, produitSelectionne.Disponible);
                AjouterProduitUserControl ajouterCommande = new AjouterProduitUserControl(this, copie, Action.Modifier);
                ajouterCommande.ValidationFaite += OnValidationFaiteModifier;
                this.apuc = ajouterCommande;
                MainWindow.Instance.vueActuelle.Content = this.apuc;

            }
        }

        private void OnValidationFaiteModifier(bool estValide)
        {
            if (estValide == true)
            {
                try
                {
                    
                    produitSelectionne.UnTypePointe = copie.UnTypePointe;
                    produitSelectionne.UnType = copie.UnType;
                    produitSelectionne.LesCouleurs = copie.LesCouleurs;
                    produitSelectionne.Code = copie.Code;
                    produitSelectionne.Nom = copie.Nom;
                    produitSelectionne.PrixVente = copie.PrixVente;
                    produitSelectionne.Disponible = copie.Disponible;
                    produitSelectionne.Update();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Le produit n'a pas pu être modifié.", "Attention",
               MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void butSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (dgProduits.SelectedItem == null)
                MessageBox.Show("Veuillez sélectionner une commande", "Attention",
                MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                Produit produitASupprimer = ((Produit)dgProduits.SelectedItem);

                MessageBoxResult result;
                result = MessageBox.Show($"Désirez-vous supprimer cette commande ? Cette action est définitive", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        produitASupprimer.Delete();
                        MainWindow.Instance.Pilot.LesProduits.Remove(produitASupprimer);
                        this.RefreshDg();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Le produit n'a pas pu être supprimé.", "Attention",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        private void RefreshDg()
        {
            CollectionViewSource.GetDefaultView(dgProduits.ItemsSource).Refresh();
        }

        private void textBoxFiltreCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.RefreshDg();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.RefreshDg();
        }


        private void numberBoxFiltrePrixVente_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.RefreshDg();
        }

        private void numberBoxFiltreQuantite_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.RefreshDg();
        }

        private void checkBoxDisponibiliteTrue_Checked(object sender, RoutedEventArgs e)
        {
            this.RefreshDg();
        }

        private void checkBoxDisponibiliteFalse_Checked(object sender, RoutedEventArgs e)
        {
            this.RefreshDg();
        }

        private void checkBoxDisponibilite_Checked(object sender, RoutedEventArgs e)
        {
            this.RefreshDg();
        }


        private void checkBoxDisponibiliteTrue_Unchecked(object sender, RoutedEventArgs e)
        {
            RefreshDg();
        }

        private void checkBoxDisponibiliteFalse_Unchecked(object sender, RoutedEventArgs e)
        {
            RefreshDg();
        }


        private void checkDisponible_Click(object sender, RoutedEventArgs e)
        {
            var cb = (CheckBox)sender;
            if (cb.DataContext is Produit produit)
            {
                produit.UpdateDisponibilite();
            }
        }
    }
}
