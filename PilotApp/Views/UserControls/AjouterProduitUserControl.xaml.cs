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
        public AjouterProduitUserControl(UserControl pagePrecedente, Produit unProduit, Action action)
        {
            InitializeComponent();
            AjouterProduitViewModel vm = new AjouterProduitViewModel(unProduit);
            this.action = action;
            this.pagePrecedente = pagePrecedente;
            this.DataContext = vm;
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
                else if (uie is ComboBox)
                {
                    ComboBox cmb = (ComboBox)uie;
                    cmb.GetBindingExpression(ComboBox.SelectedItemProperty)?.UpdateSource();
                }
                else if (uie is NumberBox)
                {
                    NumberBox nmb = (NumberBox)uie;
                    nmb.GetBindingExpression(NumberBox.TextProperty).UpdateSource();
                }
                else if (uie is ToggleSwitch)
                {
                    ToggleSwitch tgs = (ToggleSwitch)uie;
                    tgs.GetBindingExpression(ToggleSwitch.IsCheckedProperty).UpdateSource();
                }
                else if (uie is System.Windows.Controls.ListView)
                {
                    System.Windows.Controls.ListView lsv = (System.Windows.Controls.ListView)uie;
                    lsv.GetBindingExpression(System.Windows.Controls.ListView.SelectedItemsProperty).UpdateSource();
                }

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
                System.Windows.MessageBox.Show("Veuillez corriger les erreurs.");
            }
        }
    }
}
