using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotApp.Model
{
    public class TypePointe
    {
        private int id;
        private string nom;

        public TypePointe()
        {
        }

        public TypePointe(int id, string nom)
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
            return obj is TypePointe pointe &&
                   this.Id == pointe.Id &&
                   this.Nom == pointe.Nom;
        }

        public List<TypePointe> FindAll()
        {
            List<TypePointe> lesTypesPointes = new List<TypePointe>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from typepointe ;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                    lesTypesPointes.Add(new TypePointe((Int32)dr["numtypepointe"], (String)dr["libelletypepointe"]));
            }
            return lesTypesPointes;
        }
    }
}
