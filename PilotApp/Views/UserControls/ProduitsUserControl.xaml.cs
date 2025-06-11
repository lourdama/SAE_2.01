using System;
using System.Collections.Generic;
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
        public ProduitsUserControl()
        {
            InitializeComponent();
            this.DataContext = MainWindow.Instance.Pilot.LesProduits;
            dgProduits.Items.Filter = RechercheMotClefProduit;
        }

        private bool RechercheMotClefProduit(object obj)
        {
            bool ok = true;
            if (String.IsNullOrWhiteSpace(textBoxFiltreCode.Text) && String.IsNullOrWhiteSpace(textBoxFiltreNom.Text) && comboBoxFiltreTypePointe.SelectedItem == null &&
                comboBoxFiltreType.SelectedItem == null && numberBoxFiltrePrixVente == null && numberBoxFiltreQuantite == null && checkBoxDisponibiliteFalse.IsChecked == true
                && checkBoxDisponibiliteTrue.IsChecked == true)
                return true;
            Produit unProduit = obj as Produit;
            if(checkBoxDisponibiliteTrue.IsChecked == true)
            {
                ok = ok && unProduit.Disponible;
            }
            if (checkBoxDisponibiliteFalse.IsChecked == true)
            {
                ok = ok && !unProduit.Disponible;
            }

            return (unProduit.Code.StartsWith(textBoxFiltreCode.Text, StringComparison.OrdinalIgnoreCase))
                && (unProduit.Nom.StartsWith(textBoxFiltreNom.Text, StringComparison.OrdinalIgnoreCase))
                && ((TypePointe)comboBoxFiltreTypePointe.SelectedItem == unProduit.UnTypePointe)
                && ((PilotApp.Models.Type)comboBoxFiltreType.SelectedItem == unProduit.UnType)
                && ((decimal)numberBoxFiltrePrixVente.Value >= unProduit.PrixVente)
                && ((int)numberBoxFiltreQuantite.Value >= unProduit.QuantiteStock)
                && ok;
        }

        private void butAjouter_Click(object sender, RoutedEventArgs e)
        {
            test();
            /*Produit unProduit = new Produit();
            AjouterProduitUserControl ajouterProduit = new AjouterProduitUserControl(this, unProduit,Action.Creer);
            ajouterProduit.ValidationFaite += OnValidationFaiteAjouter;
            this.apuc = ajouterProduit;
            MainWindow.Instance.vueActuelle.Content = this.apuc;*/

        }

        private void OnValidationFaiteAjouter(bool estValide)
        {
            if (estValide)
            {
                try
                {
                    this.apuc.unProduit.Id = this.apuc.unProduit.Create();
                    MainWindow.Instance.Pilot.LesProduits.Add(this.apuc.unProduit);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Le chien n'a pas pu être créé.",
                        "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
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

        }

        private void textBoxFiltreCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.RefreshDg();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.RefreshDg();
        }

        private void comboBoxFiltreTypePointe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.RefreshDg();
        }

        private void comboBoxFiltreType_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        private void RefreshDg()
        {
            CollectionViewSource.GetDefaultView(dgProduits.ItemsSource).Refresh();
        }

        private void test()
        {
            foreach (Produit produit in MainWindow.Instance.Pilot.LesProduits)
            {
                MessageBox.Show(produit.Id + produit.Nom);
            }
        }
    }
}
