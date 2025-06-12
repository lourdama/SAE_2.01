using PilotApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
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

namespace PilotApp.Views.UserControls
{
    /// <summary>
    /// Logique d'interaction pour ModifierProduitDetailUserControl.xaml
    /// </summary>
    public partial class ModifierProduitDetailUserControl : UserControl
    {
        public AjouterProduitCommandeUserControl apcuc ;
        public KeyValuePair<Produit, decimal[]> ligneSelectionnee;
        public KeyValuePair<Produit, decimal[]> copieLigne;


        public Commande Commande { get; }
        public UserControl pagePrec;
        public ModifierProduitDetailUserControl(UserControl pagePrec,Commande commande)
        {
            InitializeComponent();
            this.Commande = commande;
            DataContext = this;          
            this.pagePrec = pagePrec;

        }

        private void butEnregistrerr_Click(object sender, RoutedEventArgs e)
        {
            Commande.Update();
            MessageBox.Show("Produits mis à jour.", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
            MainWindow.Instance.vueActuelle.Content = new CommandesUserControl();
        }



        private void butAjouter_Click(object sender, RoutedEventArgs e)
        {
            AjouterProduitCommandeUserControl ajouterProduitCommande = new AjouterProduitCommandeUserControl(this);
            ajouterProduitCommande.ValidationFaite += OnValidationFaiteAjouter;
            this.apcuc = ajouterProduitCommande;
            MainWindow.Instance.vueActuelle.Content = this.apcuc;
        }


        private void OnValidationFaiteAjouter(bool estValide)
        {
            if (!estValide) return;

            Produit p = apcuc.ProduitSelectionne;
            decimal[] data = new decimal[] { apcuc.Quantite, apcuc.Prix * apcuc.Quantite };

            if (!Commande.LesSousCommandes.ContainsKey(p))
            {
                Commande.LesSousCommandes[p] = data;
                MessageBox.Show("Produit ajouté !");
            }
            else
            {
                MessageBox.Show("Ce produit est déjà dans la commande.");
            }

            CollectionViewSource.GetDefaultView(dgLignes.ItemsSource).Refresh();

        }

        private void butRetour_Click(object sender, RoutedEventArgs e)
        {
            if (pagePrec is AjouterCommandeUserControl pageAjout)
            {
                pageAjout.txtNbProduits.Text = $"{Commande.LesSousCommandes.Count} produit(s) ajouté(s)";
                pageAjout.txtPrixTotal.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
            }

            MainWindow.Instance.vueActuelle.Content = this.pagePrec;

        }

        private void butModifier_Click(object sender, RoutedEventArgs e)
        {
            if (dgLignes.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un produit", "Attention",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var ligne = (KeyValuePair<Produit, decimal[]>)dgLignes.SelectedItem;
            Produit produit = ligne.Key;
            decimal quantite = ligne.Value[0];
            decimal prix = ligne.Value[1];
            var modifierProduitUC = new AjouterProduitCommandeUserControl(this, produit, quantite, prix);
            modifierProduitUC.ValidationFaite += OnValidationFaiteModifier;
            this.apcuc = modifierProduitUC;
            MainWindow.Instance.vueActuelle.Content = modifierProduitUC;
        }


        private void OnValidationFaiteModifier(bool estValide)
        {
            if (!estValide || apcuc == null) return;

            Produit p = apcuc.ProduitSelectionne;
            decimal[] nouvelleValeur = new decimal[] { apcuc.Quantite, apcuc.Prix };

            if (Commande.LesSousCommandes.ContainsKey(p))
            {
                Commande.LesSousCommandes[p] = nouvelleValeur;
                MessageBox.Show("Produit modifié avec succès.");
            }

            CollectionViewSource.GetDefaultView(dgLignes.ItemsSource).Refresh();
        }



        private void dgReset()
        {
            CollectionViewSource.GetDefaultView(dgLignes.ItemsSource).Refresh();
        }
        private void butSupprimer_Click(object sender, RoutedEventArgs e)
        {
            var ligne = (KeyValuePair<Produit, decimal[]>)dgLignes.SelectedItem;
            Produit p = ligne.Key;


            if (Commande.LesSousCommandes.ContainsKey(p))
            {
                Commande.LesSousCommandes.Remove(p);
                MessageBox.Show("Produit supprimer !");
            }
            else
            {
                MessageBox.Show("Erreur");
            }
            CollectionViewSource.GetDefaultView(dgLignes.ItemsSource).Refresh();



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.vueActuelle.Content = pagePrec;
        }
    }
}
