using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotApp.Model
{
    public class Entreprise
    {
        private string nom;
        private ObservableCollection<Role> lesRoles;
        private ObservableCollection<Couleur> lesCouleurs;

        public Entreprise(string nom)
        {
            this.Nom = nom;
            this.LesCouleurs = new ObservableCollection<Couleur>(new Couleur().FindAll());
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

        public ObservableCollection<Couleur> LesCouleurs
        {
            get
            {
                return this.lesCouleurs;
            }

            set
            {
                this.lesCouleurs = value;
            }
        }
    }
}
