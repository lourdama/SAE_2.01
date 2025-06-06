using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotApp.Model
{
    public class Role
    {
        private int id;
        private string nom;

        public int IdRole
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

        public string NomRole
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
    }
}
