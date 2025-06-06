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
        private ObservableCollection<Categorie> lesCategories;
        private ObservableCollection<TypePointe> lesTypesPointes;
        private ObservableCollection<Type> lesTypes;
        private ObservableCollection<Produit> lesProduits;

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

        public ObservableCollection<Type> LesTypes
        {
            get
            {
                return lesTypes;
            }

            set
            {
                lesTypes = value;
            }
        }

        public ObservableCollection<Produit> LesProduits
        {
            get
            {
                return this.lesProduits;
            }

            set
            {
                this.lesProduits = value;
            }
        }

        public ObservableCollection<TypePointe> LesTypesPointes
        {
            get
            {
                return this.lesTypesPointes;
            }

            set
            {
                this.lesTypesPointes = value;
            }
        }

        public ObservableCollection<Categorie> LesCategories
        {
            get
            {
                return this.lesCategories;
            }

            set
            {
                this.lesCategories = value;
            }
        }
    }
}
