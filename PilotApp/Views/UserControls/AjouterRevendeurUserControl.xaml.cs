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

namespace PilotApp.Views.UserControls
{
    /// <summary>
    /// Logique d'interaction pour AjouterRevendeurUserControl.xaml
    /// </summary>

    public partial class AjouterRevendeurUserControl : UserControl
    {
        public event Action<bool> ValidationFaite;
        private UserControl pagePrecedente;
        public Revendeur UnRevendeur;
        public AjouterRevendeurUserControl(UserControl pagePrecedente, Revendeur revendeur, Action action)
        {
            InitializeComponent();
            this.DataContext = revendeur;
            this.butAjouter.Content = action;
            this.pagePrecedente = pagePrecedente;
            this.UnRevendeur = revendeur;
        }

        private void butAjouter_Click(object sender, RoutedEventArgs e)
        {
            bool ok = true;
            foreach (UIElement uie in panelFormProduit.Children)
            {
                if (uie is TextBox txt)
                    txt.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.vueActuelle.Content = pagePrecedente;
        }
    }
}
