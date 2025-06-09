using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotApp.Models
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

        public List<Couleur> FindAll()
        {
            List<Couleur> lesCouleurs = new List<Couleur>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from couleur ;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                    lesCouleurs.Add(new Couleur((Int32)dr["numcouleur"], (String)dr["libellecouleur"]));
            }
            return lesCouleurs;
        }
    }
}
