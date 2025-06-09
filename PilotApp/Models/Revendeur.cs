using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotApp.Models
{
    public class Revendeur : ICrud<Revendeur>, INotifyPropertyChanged
    {
        private int id;
        private string raisonSociale;
        private string rue;
        private string ville;
        private string codePostal;

        public event PropertyChangedEventHandler? PropertyChanged;

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

        public int Create()
        {
            int nb = 0;
            using (var cmdInsert = new NpgsqlCommand("insert into revendeur (raisonsociale,adresserue,adressecp,adresseville ) " +
                "values (@raisonsociale,@adresserue,@adressecp,@adresseville) RETURNING numrevendeur"))
            {
                cmdInsert.Parameters.AddWithValue("raisonsociale", this.RaisonSociale);
                cmdInsert.Parameters.AddWithValue("adresserue", this.Rue);
                cmdInsert.Parameters.AddWithValue("adressecp", this.CodePostal);
                cmdInsert.Parameters.AddWithValue("adresseville", this.Ville);
                nb = DataAccess.Instance.ExecuteInsert(cmdInsert);
            }
            this.Id = nb;
            return nb;
        }

        public void Read()
        {
            using (var cmdSelect = new NpgsqlCommand("select * from  revendeur  where numrevendeur =@id;"))
            {
                cmdSelect.Parameters.AddWithValue("id", this.id);

                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                this.RaisonSociale = (String)dt.Rows[0]["raisonsociale"];
                this.Rue = (String)dt.Rows[0]["adresserue"];
                this.CodePostal = (String)dt.Rows[0]["adressecp"];
                this.Ville = (String)dt.Rows[0]["adresseville"];

            }

        }

        public int Update()
        {
            using (var cmdUpdate = new NpgsqlCommand("update revendeur set raisonsociale =@raisonsociale ,  adresserue = @adresserue,  " +
                "adressecp = @adressecp, adresseville = @adresseville  where numrevendeur =@id;"))
            {
                cmdUpdate.Parameters.AddWithValue("raisonsociale", this.RaisonSociale);
                cmdUpdate.Parameters.AddWithValue("adresserue", this.Rue);
                cmdUpdate.Parameters.AddWithValue("adressecp", this.CodePostal);
                cmdUpdate.Parameters.AddWithValue("adresseville", this.Ville);
                cmdUpdate.Parameters.AddWithValue("id", this.Id);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }

        public List<Revendeur> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }

        public int Delete()
        {
            using (var cmdUpdate = new NpgsqlCommand("delete from chiens  where numrevendeur =@id;"))
            {
                cmdUpdate.Parameters.AddWithValue("id", this.Id);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }

        public int FindNbCommande()
        {
            int nb = 0;
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select count(*) from commande where numrevendeur = @id; "))
            {
                cmdSelect.Parameters.AddWithValue("id", this.Id);
                return (int)(Int64)DataAccess.Instance.ExecuteSelectUneValeur(cmdSelect);
            }
            return nb;
        }
    }
}
