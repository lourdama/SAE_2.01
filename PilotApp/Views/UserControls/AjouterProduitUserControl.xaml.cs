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
using PilotApp.Models;
using Wpf.Ui.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace PilotApp.Views.UserControls
{
    /// <summary>
    /// Logique d'interaction pour AjouterProduitUserControl.xaml
    /// </summary>
    public partial class AjouterProduitUserControl : UserControl
    {
        public event Action<bool> ValidationFaite;
        private UserControl pagePrecedente;
        public Produit unProduit;
        private Entreprise pilot = MainWindow.Instance.Pilot;
        private Action action;
        private AjouterProduitViewModel Vm;
        public AjouterProduitUserControl(UserControl pagePrecedente, Produit unProduit, Action action)
        {
            InitializeComponent();
            this.Vm = new AjouterProduitViewModel(unProduit);
            this.action = action;
            this.pagePrecedente = pagePrecedente;
            this.DataContext = Vm;
            this.unProduit = unProduit;

            if (action == Action.Creer)
            {
                butAjouter.Content = "Creer";
            }
            else
            {
                butAjouter.Content = "Modifier";
            }


        }

        private void butAjouter_Click(object sender, RoutedEventArgs e)
        {
            bool ok = true;
            foreach (UIElement uie in panelFormProduit.Children)
            {
                if (uie is System.Windows.Controls.TextBox txt)
                {
                    txt.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty)?.UpdateSource();
                }
                else if (uie is ComboBox cmb)
                {
                    cmb.GetBindingExpression(ComboBox.SelectedItemProperty)?.UpdateSource();
                }
                else if (uie is NumberBox nmb)
                {
                    nmb.GetBindingExpression(NumberBox.ValueProperty)?.UpdateSource();
                }
                else if (uie is ToggleSwitch tgs)
                {
                    tgs.GetBindingExpression(ToggleSwitch.IsCheckedProperty)?.UpdateSource();
                }

                if (Validation.GetHasError(uie))
                {
                    ok = false;
                }
            }

            if (this.Vm.Produit.LesCouleurs == null || this.Vm.Produit.LesCouleurs.Count == 0)
            {
                System.Windows.MessageBox.Show("Veuillez sélectionner au moins une couleur.");
                ok = false;
            }

            if (this.Vm.Produit.UnTypePointe == null)
            {
                System.Windows.MessageBox.Show("Veuillez sélectionner un type de pointe.");
                ok = false;
            }

            if (this.Vm.Produit.UnType == null)
            {
                System.Windows.MessageBox.Show("Veuillez sélectionner un type.");
                ok = false;
            }

            if (ok)
            {
                this.unProduit = this.Vm.Produit;
                ValidationFaite?.Invoke(ok);
                MainWindow.Instance.vueActuelle.Content = this.pagePrecedente;
            }
            else
            {
                System.Windows.MessageBox.Show("Veuillez corriger les erreurs.");
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listView = sender as System.Windows.Controls.ListView;

            if (this.Vm != null)
            {
                List<Couleur> listCouleurTemp = new List<Couleur>();
                foreach (var item in listView.SelectedItems)
                {
                    if (item is Couleur couleur)
                    {
                        listCouleurTemp.Add(couleur);
                    }
                }

                if (listCouleurTemp.Count > 0)
                {
                    this.Vm.Produit.LesCouleurs = listCouleurTemp;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.vueActuelle.Content = pagePrecedente;
        }
    }
}
