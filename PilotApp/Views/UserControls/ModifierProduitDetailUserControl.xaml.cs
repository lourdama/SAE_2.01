using PilotApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
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
        public Commande Commande { get; }
        public UserControl pagePrec;
        public ModifierProduitDetailUserControl(UserControl pagePrec,Commande commande)
        {
            InitializeComponent();
            Commande = commande;
            DataContext = this;
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
            if (estValide)
            {
                Produit p = this.apcuc.ProduitSelectionne;
                decimal[] data = new decimal[] { this.apcuc.Quantite, this.apcuc.Prix * this.apcuc.Quantite };

                // Récupérer la commande via le ViewModel
                var vm = (ModifierProduitDetailUserControl)this.DataContext;
                var commande = vm.Commande;

                // Ajouter le produit s'il n'existe pas encore
                if (!commande.LesSousCommandes.ContainsKey(p))
                {
                    commande.LesSousCommandes[p] = data;
                    MessageBox.Show("Produit ajouté !");
                }
                else
                {
                    MessageBox.Show("Ce produit est déjà dans la commande.");
                }

                if (!commande.LesSousCommandes.ContainsKey(p))
                {
                    commande.LesSousCommandes[p] = data;

                    MessageBox.Show("Produit ajouté !");
                }


            }

        }

        private void butRetour_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.vueActuelle.Content = this.pagePrec;

        }
    }
}
