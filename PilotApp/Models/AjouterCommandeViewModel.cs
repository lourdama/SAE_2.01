using PilotApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotApp.Models
{
    public class AjouterCommandeViewModel
    {
        public Commande Commande { get; set; }
        public ObservableCollection<Employe> LesEmployes => MainWindow.Instance.Pilot.LesEmployes;
        public ObservableCollection<Revendeur> LesRevendeurs => MainWindow.Instance.Pilot.LesRevendeurs;
        public ObservableCollection<ModeTransport> LesModesTransports => MainWindow.Instance.Pilot.LesModesTransports;

        public AjouterCommandeViewModel(Commande commande)
        {
            Commande = commande;
        }
    }

}
