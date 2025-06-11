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

namespace PilotApp.Views.UserControls
{
    /// <summary>
    /// Logique d'interaction pour RevendeursUserControl.xaml
    /// </summary>
    public partial class RevendeursUserControl : UserControl
    {
        public AjouterRevendeurUserControl aruc;
        public RevendeursUserControl()
        {
            InitializeComponent();
            this.DataContext = MainWindow.Instance.Pilot.LesRevendeurs;
        }

        private void butAjouter_Click(object sender, RoutedEventArgs e)
        {
            Revendeur nouveauRevendeur = new Revendeur();
            AjouterRevendeurUserControl ajouterRevendeur = new AjouterRevendeurUserControl(this, nouveauRevendeur, Action.Creer);
            ajouterRevendeur.ValidationFaite += OnValidationFaiteAjouter;
            this.aruc = ajouterRevendeur;
            MainWindow.Instance.vueActuelle.Content = this.aruc;
        }

        private void OnValidationFaiteAjouter(bool estValide)
        {
            if (estValide)
            {
                try
                {
                    this.aruc.UnRevendeur.Id = this.aruc.UnRevendeur.Create();
                    MainWindow.Instance.Pilot.LesRevendeurs.Add(this.aruc.UnRevendeur);
                }
                catch
                {
                    MessageBox.Show("Erreur lors de l'ajout du revendeur.");
                }
            }
            else
            {
                MessageBox.Show("Validation annulée.");
            }
        }
    }
}
