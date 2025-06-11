using PilotApp.Fenetre;
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
        public event Action<bool> ValidationFaite;
        private UserControl pagePrecedente;
        public AjouterProduitCommandeUserControl apcuc;
        public Commande UneCommande;


        public AjouterCommandeUserControl(UserControl pagePrecedente, Commande commande, Action action)
        {
            InitializeComponent();
            this.pagePrecedente = pagePrecedente;
            AjouterCommandeViewModel vm = new AjouterCommandeViewModel(commande);
            this.DataContext = vm;
            this.UneCommande = commande;
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
                ValidationFaite.Invoke(ok);
                MainWindow.Instance.vueActuelle.Content = this.pagePrecedente;
            }
            else
            {
                MessageBox.Show("Veuillez corriger les erreurs.");
            }
        }

        private void butAjouterProduit_Click(object sender, RoutedEventArgs e)
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
                this.txtNbProduits.Text = $"{commande.LesSousCommandes.Count} produit(s) ajouté(s)";

            }

        }
    }
}

    

