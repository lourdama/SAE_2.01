using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotApp.Model
{
    public class Produit
    {
        private int id;
        private TypePointe unTypePointe;
        private Type unType;
        private List<Couleur> lesCouleurs;
        private string code;
        private string nom;
        private double prixVente;
        private int quantiteStock;
        private bool disponible;

        public Produit()
        {
        }

        public Produit(int id, TypePointe unTypePointe, Type unType, List<Couleur> lesCouleurs, string code, string nom, double prixVente, int quantiteStock, bool disponible)
        {
            this.Id = id;
            this.UnTypePointe = unTypePointe;
            this.UnType = unType;
            this.LesCouleurs = lesCouleurs;
            this.Code = code;
            this.Nom = nom;
            this.PrixVente = prixVente;
            this.QuantiteStock = quantiteStock;
            this.Disponible = disponible;
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

        public TypePointe UnTypePointe
        {
            get
            {
                return unTypePointe;
            }

            set
            {
                unTypePointe = value;
            }
        }

        public Type UnType
        {
            get
            {
                return unType;
            }

            set
            {
                unType = value;
            }
        }

        public List<Couleur> LesCouleurs
        {
            get
            {
                return lesCouleurs;
            }

            set
            {
                lesCouleurs = value;
            }
        }

        public string Code
        {
            get
            {
                return code;
            }

            set
            {
                code = value;
            }
        }

        public string Nom
        {
            get
            {
                return nom;
            }

            set
            {
                nom = value;
            }
        }

        public double PrixVente
        {
            get
            {
                return prixVente;
            }

            set
            {
                prixVente = value;
            }
        }

        public int QuantiteStock
        {
            get
            {
                return quantiteStock;
            }

            set
            {
                quantiteStock = value;
            }
        }

        public bool Disponible
        {
            get
            {
                return this.disponible;
            }

            set
            {
                this.disponible = value;
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is Produit produit &&
                   this.Code == produit.Code;
        }
    }
}
