using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotApp.Model
{
    public class Couleur
    {
        private int id;
        private string nom;

        public Couleur()
        {
        }

        public Couleur(int id, string nom)
        {
            this.Id = id;
            this.Nom = nom;
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Nom
        {
            get
            {
                return this.nom;
            }

            set
            {
                this.nom = value;
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is Couleur couleur &&
                   this.Id == couleur.Id &&
                   this.Nom == couleur.Nom;
        }
    }
}
