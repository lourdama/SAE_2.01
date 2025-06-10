using PilotApp.Models;
using System;
using System.Collections.Generic;
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
        private Action action;
        public AjouterCommandeUserControl(Commande uneCommande,Action action)
        {
            InitializeComponent();
            this.action = action;

            if (action == Action.Creer)
            {
                butValiderCommande.Content = "Créer";
            }
            else
            {
                butValiderCommande.Content = "Modifier";
            }
        }

        private void butValiderCommande_Click(object sender, RoutedEventArgs e)
        {

            // validation des champs
            bool ok = true;
            foreach (UIElement uie in panelFormCommande.Children)
            {
                if (uie is TextBox txt)
                    txt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                if (Validation.GetHasError(uie))
                    ok = false;
            }

            if (ok)   = true;
            else MessageBox.Show("Veuillez corriger les erreurs.");
        }

    }
    }
}
