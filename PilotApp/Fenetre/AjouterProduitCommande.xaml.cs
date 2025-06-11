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
    /// Logique d'interaction pour AjouterProduitCommande.xaml
    /// </summary>
    public partial class AjouterProduitCommande : Window
    {
        public Produit ProduitSelectionne { get; private set; }
        public decimal Quantite { get; private set; }
        public decimal Prix { get; private set; }

        public AjouterProduitCommande()
        {
            InitializeComponent();
            cmbProduits.ItemsSource = MainWindow.Instance.Pilot.LesProduits;
            cmbProduits.DisplayMemberPath = "Nom";
        }

        private void butValider_Click(object sender, RoutedEventArgs e)
        {
            if (cmbProduits.SelectedItem == null ||
                !decimal.TryParse(txtQuantite.Text, out decimal quantite) ||
                !decimal.TryParse(txtPrix.Text, out decimal prix))
            {
                MessageBox.Show("Veuillez remplir tous les champs correctement.");
                return;
            }

            ProduitSelectionne = (Produit)cmbProduits.SelectedItem;
            Quantite = quantite;
            Prix = prix;

            DialogResult = true;
            this.Close();
        }

        private void cmbProduits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbProduits.SelectedItem is Produit p)
            {
                txtPrix.Text = p.PrixVente.ToString("F2"); // format avec 2 décimales
            }
            else
            {
                txtPrix.Text = string.Empty;
            }
        }
    }
}

