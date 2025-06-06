using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotApp.Model
{
    public class Revendeur
    {
        private int id;
        private string raisonSociale;
        private string rue;
        private string ville;
        private string codePostal;

        public Revendeur()
        {
        }

        public Revendeur(int id, string raisonSociale, string rue, string ville, string codePostal)
        {
            this.Id = id;
            this.RaisonSociale = raisonSociale;
            this.Rue = rue;
            this.Ville = ville;
            this.CodePostal = codePostal;
        }

        public int Id
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
            }
        }

        public string RaisonSociale
        {
            get
            {
                return this.raisonSociale;
            }

            set
            {
                this.raisonSociale = value;
            }
        }

        public string Rue
        {
            get
            {
                return this.rue;
            }

            set
            {
                this.rue = value;
            }
        }

        public string Ville
        {
            get
            {
                return this.ville;
            }

            set
            {
                this.ville = value;
            }
        }

        public string CodePostal
        {
            get
            {
                return this.codePostal;
            }

            set
            {
                this.codePostal = value;
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is Revendeur revendeur &&
                   this.Id == revendeur.Id &&
                   this.Rue == revendeur.Rue &&
                   this.Ville == revendeur.Ville &&
                   this.CodePostal == revendeur.CodePostal;
        }

        public List<Revendeur> FindAll()
        {
            List<Revendeur> lesRevendeurs = new List<Revendeur>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from revendeur ;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                    lesRevendeurs.Add(new Revendeur((int)dr["numrevendeur"], (string)dr["raisonsociale"], (string)dr["adresserue"], (string)dr["adressecp"], (string)dr["adresseville"]));
            }
            return lesRevendeurs;
        }
    }
}
