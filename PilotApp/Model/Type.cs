using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotApp.Model
{
    public class Type
    {
        private int id;
        private Categorie uneCategorie;
        private string nom;

        public Type()
        {
        }

        public Type(int id, Categorie uneCategorie, string nom)
        {
            this.Id = id;
            this.UneCategorie = uneCategorie;
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

        public Categorie UneCategorie
        {
            get
            {
                return uneCategorie;
            }

            set
            {
                uneCategorie = value;
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
            return obj is Type type &&
                   this.Id == type.Id &&
                   EqualityComparer<Categorie>.Default.Equals(this.UneCategorie, type.UneCategorie) &&
                   this.Nom == type.Nom;
        }
    }
}
