using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            if (MainWindow.Instance.EstCommercial)
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


    }
}
