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
        private Action action;
        public AjouterProduitCommandeUserControl()
        {
            InitializeComponent();
            this.DataContext = MainWindow.Instance.Pilot.LesProduits;
            this.action = action;

            if (action == Action.Creer)
            {
                butAjouter.Content = "Créer";
            }
            else
            {
                butAjouter.Content = "Modifier";
            }
        }
    }
}
