using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotApp.Models
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
                if (MiseEnForme.NEstPasNull(value))
                    this.id = value;
                else throw new ArgumentException("L'id ne peut être null.");
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
                if (MiseEnForme.NEstPasNull(value))
                    uneCategorie = value;
                else throw new ArgumentException("La catégorie ne peut être null.");
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
                if (MiseEnForme.NEstPasNullOuWhitespace(value))
                    this.nom = MiseEnForme.FormaterString(value);
                else throw new ArgumentException("Le nom ne peut être null.");
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is Type type &&
                   this.Id == type.Id &&
                   EqualityComparer<Categorie>.Default.Equals(this.UneCategorie, type.UneCategorie) &&
                   this.Nom == type.Nom;
        }

        public List<Type> FindAll(Entreprise entreprise)
        {

            List<Type> lesTypes = new List<Type>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from type ;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    lesTypes.Add(new Type((int)dr["numtype"], entreprise.LesCategories.SingleOrDefault(c => c.Id == (int)dr["numcategorie"]), (string)dr["libelletype"]));
                }
            }
            return lesTypes;
        }
    }
}
