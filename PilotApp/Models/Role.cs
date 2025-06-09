using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotApp.Models
{
    public class Role
    {
        private int id;
        private string nom;

        public Role()
        {
        }

        public Role(int id, string nom)
        {
            this.Id = id;
            this.Nom = nom;
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
        public List<Role> FindAll()
        {
            List<Role> lesRoles = new List<Role>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from role ;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                    lesRoles.Add(new Role((Int32)dr["numrole"], (String)dr["libellerole"]));
            }
            return lesRoles;
        }
    }
}
