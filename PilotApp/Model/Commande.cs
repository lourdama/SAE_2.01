using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotApp.Model
{
    public class Commande
    {
        private int id;
        private Employe employe;
        private ModeTransport modeTransport;
        private Revendeur revendeur;
        private DateTime dateCommande;
        private DateTime dateLivraison;
        private double prix;
    }
}
