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
        private Action action;
        public AjouterRevendeurUserControl()
        {
            InitializeComponent();
            this.action = action;

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

        }
    }
}
