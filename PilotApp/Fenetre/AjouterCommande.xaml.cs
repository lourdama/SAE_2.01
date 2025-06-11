using PilotApp.Models;
using PilotApp.Views;
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
using System.Windows.Shapes;

namespace PilotApp.Fenetre
{
    /// <summary>
    /// Logique d'interaction pour AjouterCommande.xaml
    /// </summary>
    public partial class AjouterCommande : Window
    {
        public enum Action { Modifier, Créer };

        public AjouterCommande(Commande commande, Action action)
        {
            InitializeComponent();
            AjouterCommandeViewModel vm = new AjouterCommandeViewModel(commande);
            this.DataContext = vm;
            butValiderCommande.Content = action;
        }

        private void butValiderCommande_Click(object sender, RoutedEventArgs e)
        {
            bool ok = true;
            foreach (UIElement uie in panelFormChien.Children)
            {
                if (uie is TextBox txt)
                    txt.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
                else if (uie is ComboBox cmb)
                    cmb.GetBindingExpression(ComboBox.SelectedItemProperty)?.UpdateSource();
                else if (uie is DatePicker dp)
                    dp.GetBindingExpression(DatePicker.SelectedDateProperty)?.UpdateSource();

                if (Validation.GetHasError(uie))
                    ok = false;
            }

            if (ok)
            {
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Veuillez corriger les erreurs.");
            }
        }

        private void butAjouterProduit_Click(object sender, RoutedEventArgs e)
        {
            AjouterProduitCommande fenetre = new AjouterProduitCommande();
            fenetre.Owner = this;

            bool? result = fenetre.ShowDialog();

            if (result == true)
            {
                Produit p = fenetre.ProduitSelectionne;
                decimal[] data = new decimal[] { fenetre.Quantite, fenetre.Prix * fenetre.Quantite};

                // Récupérer la commande via le ViewModel
                var vm = (AjouterCommandeViewModel)this.DataContext;
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

                // Actualiser le compteur
                txtNbProduits.Text = $"{commande.LesSousCommandes.Count} produit(s) ajouté(s)";
            }
        }
    }
}
