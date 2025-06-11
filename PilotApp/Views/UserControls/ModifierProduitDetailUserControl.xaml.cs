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

        private void butAnnuler_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.vueActuelle.Content = new CommandesUserControl();
            
        }


    }
}
