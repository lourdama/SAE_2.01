using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotApp.Models
{
    public class Categorie
    {
        private int id;
        private string nom;

        public Categorie()
        {
        }

        public Categorie(int id, string nom)
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
                if (MiseEnForme.NEstPasNull(value))
                    id = value;
                else throw new ArgumentException("L'id ne peut être null.");
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
                if(MiseEnForme.NEstPasNullOuWhitespace(value))
                    this.nom = MiseEnForme.FormaterString(value);
                else throw new ArgumentException("Le nom ne peut être null.");
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is Categorie categorie &&
                   this.Id == categorie.Id &&
                   this.Nom == categorie.Nom;
        }

        public List<Categorie> FindAll()
        {
            List<Categorie> lesCategories = new List<Categorie>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from categorie ;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                    lesCategories.Add(new Categorie((Int32)dr["numcategorie"], (String)dr["libellecategorie"]));
            }
            return lesCategories;
        }
    }
}