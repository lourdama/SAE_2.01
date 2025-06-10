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
    /// Logique d'interaction pour ProduitsUserControl.xaml
    /// </summary>
    public partial class ProduitsUserControl : UserControl
    {
        public ProduitsUserControl()
        {
            InitializeComponent();
            this.DataContext = MainWindow.Instance.Pilot.LesProduits;
        }

       /* private bool RechercheMotClefSejour(object obj)
        {
            if (String.IsNullOrEmpty(textMotClefChienSejour.Text) && String.IsNullOrEmpty(textMotClefBoxSejour.Text) && String.IsNullOrEmpty(textMotClefDateSejour.Text))
                return true;
            Sejour unSejour = obj as Sejour;
            return (unSejour.UnChien.Nom.StartsWith(textMotClefChienSejour.Text, StringComparison.OrdinalIgnoreCase))
                && (unSejour.UnBox.NumBox.ToString().StartsWith(textMotClefBoxSejour.Text, StringComparison.OrdinalIgnoreCase))
                && ((unSejour.DateDebut.ToString().Contains(textMotClefDateSejour.Text, StringComparison.OrdinalIgnoreCase)) || (unSejour.DateFin.ToString().Contains(textMotClefDateSejour.Text, StringComparison.OrdinalIgnoreCase)));
        }*/

        private void butAjouter_Click(object sender, RoutedEventArgs e)
        {

        }

        private void butModifier_Click(object sender, RoutedEventArgs e)
        {

        }

        private void butSupprimer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


    }
}
