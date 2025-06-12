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
            DataContext = this;          // ce UserControl expose une propriété ‘Commande’
            this.pagePrec = pagePrec;

        }

        private void butEnregistrerr_Click(object sender, RoutedEventArgs e)
        {
            // LesSousCommandes a été mis à jour via TwoWay
            Commande.Update();
            MessageBox.Show("Produits mis à jour.", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
            // Retour à la liste des commandes
            MainWindow.Instance.vueActuelle.Content = new CommandesUserControl();
        }



        private void butAjouter_Click(object sender, RoutedEventArgs e)
        {
            //AjouterProduitCommande fenetre = new AjouterProduitCommande();
            //fenetre.Owner = this;

            //bool? result = fenetre.ShowDialog();
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

            // 3) Mettre à jour l’affichage
            dgLignes.Items.Refresh();

        }

        private void butRetour_Click(object sender, RoutedEventArgs e)
        {
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

            // Récupérer l'entrée sélectionnée dans le DataGrid
            var ligne = (KeyValuePair<Produit, decimal[]>)dgLignes.SelectedItem;
            Produit produit = ligne.Key;
            decimal quantite = ligne.Value[0];
            decimal prix = ligne.Value[1];

            // Créer le UserControl de modification
            var modifierProduitUC = new AjouterProduitCommandeUserControl(this, produit, quantite, prix);
            modifierProduitUC.ValidationFaite += OnValidationFaiteModifier;
            this.apcuc = modifierProduitUC;

            // Afficher l'interface de modification
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
            
            dgLignes.Items.Refresh();
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

            // 3) Mettre à jour l’affichage
            dgLignes.Items.Refresh();



        }
    }
}
