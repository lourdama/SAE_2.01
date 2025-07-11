﻿using PilotApp.Views;
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
        public ObservableCollection<ModeTransport> LesModesTransports => MainWindow.Instance.Pilot.LesModesTransports;

        public ObservableCollection<Revendeur> LesRevendeurs { get; set; }

        public AjouterCommandeViewModel(Commande commande)
        {
            Commande = commande;

            LesRevendeurs = MainWindow.Instance.Pilot.LesRevendeurs;

        }


    }

}
