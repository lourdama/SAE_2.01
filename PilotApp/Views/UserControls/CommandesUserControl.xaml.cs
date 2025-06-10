using PilotApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour CommandesUserControl.xaml
    /// </summary>
    public partial class CommandesUserControl : UserControl
    {
        private MainWindow mainWindow = MainWindow.Instance;
        public CommandesUserControl(ObservableCollection<Commande> commandes)
        {
            InitializeComponent();
            this.DataContext = commandes;

        }

    }
}
