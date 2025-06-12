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
        public ModifierProduitDetailUserControl mpdu;
        public Commande UneCommande;
        public Action uneaction;

        public AjouterCommandeUserControl(UserControl pagePrecedente, Commande commande, Action action)
        {
            InitializeComponent();
            this.pagePrecedente = pagePrecedente;
            AjouterCommandeViewModel vm = new AjouterCommandeViewModel(commande);
            this.uneaction = action;
            this.DataContext = vm;
            this.UneCommande = commande;
            butValiderCommande.Content = action;
            this.txtNbProduits.Text = $"{commande.LesSousCommandes.Count} produit(s) ajouté(s)";
        }
       
        private void butValiderCommande_Click(object sender, RoutedEventArgs e)
        {
            bool ok = true;
            foreach (UIElement uie in panelFormCommande.Children)
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

        private void butDetailProduit_Click(object sender, RoutedEventArgs e)
        {
            ModifierProduitDetailUserControl ajouterProduitCommande = new ModifierProduitDetailUserControl(this, this.UneCommande);
            this.mpdu = ajouterProduitCommande;
            MainWindow.Instance.vueActuelle.Content = this.mpdu;

        }

        private void butAjouterRevendeur_Click(object sender, RoutedEventArgs e)
        {
            Revendeur nouveauRevendeur = new Revendeur();
            var ajouterUC = new AjouterRevendeurUserControl(this, nouveauRevendeur, Action.Creer);
            ajouterUC.ValidationFaite += (ok) =>
            {
                if (ok)
                {
                    nouveauRevendeur.Id = nouveauRevendeur.Create();
                    MainWindow.Instance.Pilot.LesRevendeurs.Add(nouveauRevendeur);
                    this.UneCommande.UnRevendeur = nouveauRevendeur;
                }
            };
            MainWindow.Instance.vueActuelle.Content = ajouterUC;
        }

        private void butModifRevendeur_Click(object sender, RoutedEventArgs e)
        {
            var vm = (AjouterCommandeViewModel)this.DataContext;
            if (this.UneCommande.UnRevendeur == null)
            {
                MessageBox.Show("Veuillez sélectionner un revendeur à modifier.");
                return;
            }

            var copie = new Revendeur()
            {
                Id = this.UneCommande.UnRevendeur.Id,
                RaisonSociale = this.UneCommande.UnRevendeur.RaisonSociale,
                Rue = this.UneCommande.UnRevendeur.Rue,
                Ville = this.UneCommande.UnRevendeur.Ville,
                CodePostal = this.UneCommande.UnRevendeur.CodePostal
            };

            var modifierUC = new AjouterRevendeurUserControl(this, copie, Action.Modifier);
            modifierUC.ValidationFaite += (ok) =>
            {
                if (ok)
                {
                    var original = MainWindow.Instance.Pilot.LesRevendeurs.FirstOrDefault(r => r.Id == copie.Id);
                    if (original != null)
                    {
                        original.RaisonSociale = copie.RaisonSociale;
                        original.Rue = copie.Rue;
                        original.Ville = copie.Ville;
                        original.CodePostal = copie.CodePostal;
                    }
                    CollectionViewSource.GetDefaultView(vm.LesRevendeurs).Refresh();
                }
            };
            MainWindow.Instance.vueActuelle.Content = modifierUC;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.vueActuelle.Content = pagePrecedente;
        }
    }
}

    

