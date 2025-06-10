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
                if (MiseEnForme.NEstPasNull(value))
                    this.id = value;
                else throw new ArgumentException("L'id ne peut être null.");
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
                if (MiseEnForme.NEstPasNull(value))
                    this.unRole = value;
                else throw new ArgumentException("Le role ne peut être null.");
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

        public string Prenom
        {
            get
            {
                return this.prenom;
            }

            set
            {
                if (MiseEnForme.NEstPasNullOuWhitespace(value))
                    this.prenom = MiseEnForme.FormaterString(value);
                else throw new ArgumentException("Le prénom ne peut être null.");
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
                if (MiseEnForme.NEstPasNullOuWhitespace(value))
                    this.mdp = value;
                else throw new ArgumentException("Le mot de passe ne peut être null.");
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
                if (MiseEnForme.NEstPasNullOuWhitespace(value))
                    this.login = value;
                else throw new ArgumentException("Le login ne peut être null.");

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
