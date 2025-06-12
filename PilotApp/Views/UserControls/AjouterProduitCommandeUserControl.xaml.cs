using PilotApp.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Collections.Specialized.BitVector32;

namespace PilotApp.Views.UserControls
{
    /// <summary>
    /// Logique d'interaction pour AjouterProduitCommandeUserControl.xaml
    /// </summary>
    public partial class AjouterProduitCommandeUserControl : UserControl
    {
        public Produit ProduitSelectionne { get; private set; }
        private UserControl pagePrecedente;
        public event Action<bool> ValidationFaite;
        public decimal Quantite { get; private set; }
        public decimal Prix { get; private set; }
        private List<Produit> lesProduitsDispos;

        // Constructeur pour l'ajout
        public AjouterProduitCommandeUserControl(UserControl pagePrecedente)
        {
            InitializeComponent();
            this.pagePrecedente = pagePrecedente;
            RechercheProduitDisponible();
            cmbProduits.ItemsSource = lesProduitsDispos;
            cmbProduits.DisplayMemberPath = "Nom";
        }


        public AjouterProduitCommandeUserControl(UserControl pagePrecedente, Produit produitAModifier, decimal quantite, decimal prix)
        {
            InitializeComponent();
            this.pagePrecedente = pagePrecedente;
            RechercheProduitDisponible();
            cmbProduits.ItemsSource = lesProduitsDispos;
            cmbProduits.DisplayMemberPath = "Nom";

            cmbProduits.SelectedItem = produitAModifier;
            txtQuantite.Text = quantite.ToString();
            txtPrix.Text = produitAModifier.PrixVente.ToString("F2");
            
            ProduitSelectionne = produitAModifier;
            Quantite = quantite;
            Prix = produitAModifier.PrixVente;

            // Désactiver le changement de produit pour éviter de changer la clé du dictionnaire
            cmbProduits.IsEnabled = false;
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
            ValidationFaite.Invoke(true);
            Prix = Prix * quantite;
            MainWindow.Instance.vueActuelle.Content = this.pagePrecedente;
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
        private void RechercheProduitDisponible()
        {
            lesProduitsDispos= new List<Produit>();
            foreach(Produit produit in MainWindow.Instance.Pilot.LesProduits)
            {
                if (produit.Disponible)
                {
                    lesProduitsDispos.Add(produit);
                }
            }
        }
    }
}
