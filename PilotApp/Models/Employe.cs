using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotApp.Models
{
    public class Employe
    {
        private int id;
        private Role unRole;
        private string nom;
        private string prenom;
        private string mdp;
        private string login;

        public Employe()
        {
        }

        public Employe(int id, Role unRole, string nom, string prenom, string mdp, string login)
        {
            this.Id = id;
            this.UnRole = unRole;
            this.Nom = nom;
            this.Prenom = prenom;
            this.Mdp = mdp;
            this.Login = login;
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

        public Role UnRole
        {
            get
            {
                return this.unRole;
            }

            set
            {
                this.unRole = value;
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

        public string Prenom
        {
            get
            {
                return this.prenom;
            }

            set
            {
                this.prenom = value;
            }
        }

        public string Mdp
        {
            get
            {
                return this.mdp;
            }

            set
            {
                this.mdp = value;
            }
        }

        public string Login
        {
            get
            {
                return this.login;
            }

            set
            {
                this.login = value;
            }
        }
        public List<Employe> FindAll(Entreprise entreprise)
        {
            List<Employe> lesEmployes = new List<Employe>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from employe;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                    lesEmployes.Add(new Employe((int)dr["numemploye"], entreprise.LesRoles.SingleOrDefault(c => c.Id == (int)dr["numrole"]), (string)dr["nom"], (string)dr["prenom"], (string)dr["password"], (string)dr["login"]));
            }
            return lesEmployes;
        }
    }
}
