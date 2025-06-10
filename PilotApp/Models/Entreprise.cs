using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotApp.Models
{
    public class Entreprise
    {
        private string nom;
        private ObservableCollection<Role> lesRoles;
        private ObservableCollection<Employe> lesEmployes;
        private ObservableCollection<Couleur> lesCouleurs;
        private ObservableCollection<ModeTransport> lesModesTransports;
        private ObservableCollection<Revendeur> lesRevendeurs;
        private ObservableCollection<Categorie> lesCategories;
        private ObservableCollection<TypePointe> lesTypesPointes;
        private ObservableCollection<Type> lesTypes;
        private ObservableCollection<Produit> lesProduits;
        private ObservableCollection<Commande> lesCommandes;


        public Entreprise(string nom)
        {
            this.Nom = nom;
            this.LesRoles = new ObservableCollection<Role>(new Role().FindAll());
            this.LesEmployes = new ObservableCollection<Employe>(new Employe().FindAll(this));
            this.LesCouleurs = new ObservableCollection<Couleur>(new Couleur().FindAll());
            this.LesModesTransports = new ObservableCollection<ModeTransport>(new ModeTransport().FindAll());
            this.LesRevendeurs = new ObservableCollection<Revendeur>(new Revendeur().FindAll());
            this.LesCategories = new ObservableCollection<Categorie>(new Categorie().FindAll());
            this.LesTypesPointes = new ObservableCollection<TypePointe>(new TypePointe().FindAll());
            this.LesTypes = new ObservableCollection<Type>(new Type().FindAll(this));
            this.LesProduits = new ObservableCollection<Produit>(new Produit().FindAll(this));
            this.LesCommandes = new ObservableCollection<Commande>(new Commande().FindAll(this));
        }

        public string Nom
        {
            get
            {
                return this.nom;
            }

            set
            {
                if (MiseEnForme.NEstPasNullOuWhitespace(value))
                    this.nom = value;
                else throw new ArgumentException("Le nom ne peut être null.");
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

        public ObservableCollection<Role> LesRoles
        {
            get
            {
                return this.lesRoles;
            }

            set
            {
                this.lesRoles = value;
            }
        }

        public ObservableCollection<Employe> LesEmployes
        {
            get
            {
                return this.lesEmployes;
            }

            set
            {
                this.lesEmployes = value;
            }
        }

        public ObservableCollection<ModeTransport> LesModesTransports
        {
            get
            {
                return this.lesModesTransports;
            }

            set
            {
                this.lesModesTransports = value;
            }
        }

        public ObservableCollection<Revendeur> LesRevendeurs
        {
            get
            {
                return this.lesRevendeurs;
            }

            set
            {
                this.lesRevendeurs = value;
            }
        }

        public ObservableCollection<Commande> LesCommandes
        {
            get
            {
                return this.lesCommandes;
            }

            set
            {
                this.lesCommandes = value;
            }
        }
    }
}
