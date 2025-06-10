using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace PilotApp.Models
{
    public class ModeTransport
    {
        private int id;
        private string nom;

        public ModeTransport()
        {
        }

        public ModeTransport(int id, string nom)
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
                if (MiseEnForme.NEstPasNull(value))
                    this.id = value;
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
                if (MiseEnForme.NEstPasNullOuWhitespace(value))
                    this.nom = MiseEnForme.FormaterString(value);
                else throw new ArgumentException("Le nom ne peut être null.");
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is ModeTransport transport &&
                   this.Id == transport.Id &&
                   this.Nom == transport.Nom;
        }

        public List<ModeTransport> FindAll()
        {
            List<ModeTransport> lesModeTransports = new List<ModeTransport>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from modetransport ;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                    lesModeTransports.Add(new ModeTransport((Int32)dr["numtransport"], (String)dr["libelletransport"]));
            }
            return lesModeTransports;
        }
    }
}
