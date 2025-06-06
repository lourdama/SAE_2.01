using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotApp.Model
{
    public class Commande
    {
        private int id;
        private Employe employe;
        private ModeTransport modeTransport;
        private Revendeur revendeur;
        private Dictionary<Produit, decimal[,]> sousCommandes;
        private DateTime dateCommande;
        private DateTime dateLivraison;
        private decimal prix;

        public Commande()
        {
        }

        public Commande(int id, Employe employe, ModeTransport modeTransport, Revendeur revendeur, Dictionary<Produit, decimal[,]> sousCommandes, DateTime dateCommande, DateTime dateLivraison, decimal prix)
        {
            this.Id = id;
            this.Employe = employe;
            this.ModeTransport = modeTransport;
            this.Revendeur = revendeur;
            this.SousCommandes = sousCommandes;
            this.DateCommande = dateCommande;
            this.DateLivraison = dateLivraison;
            this.Prix = prix;
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

        public Employe Employe
        {
            get
            {
                return employe;
            }

            set
            {
                employe = value;
            }
        }

        public ModeTransport ModeTransport
        {
            get
            {
                return modeTransport;
            }

            set
            {
                modeTransport = value;
            }
        }

        public Revendeur Revendeur
        {
            get
            {
                return revendeur;
            }

            set
            {
                revendeur = value;
            }
        }

        public Dictionary<Produit, decimal[,]> SousCommandes
        {
            get
            {
                return sousCommandes;
            }

            set
            {
                sousCommandes = value;
            }
        }

        public DateTime DateCommande
        {
            get
            {
                return dateCommande;
            }

            set
            {
                dateCommande = value;
            }
        }

        public DateTime DateLivraison
        {
            get
            {
                return dateLivraison;
            }

            set
            {
                dateLivraison = value;
            }
        }

        public decimal Prix
        {
            get
            {
                return this.prix;
            }

            set
            {
                this.prix = value;
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is Commande commande &&
                   this.Id == commande.Id &&
                   EqualityComparer<Employe>.Default.Equals(this.Employe, commande.Employe) &&
                   EqualityComparer<ModeTransport>.Default.Equals(this.ModeTransport, commande.ModeTransport) &&
                   EqualityComparer<Revendeur>.Default.Equals(this.Revendeur, commande.Revendeur);
        }

        public List<Commande> FindAll(Entreprise entreprise)
        {

            List<Commande> lesCommandes = new List<Commande>();
            using (NpgsqlCommand cmdSelectProduit = new NpgsqlCommand("select * from commande;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelectProduit);
                foreach (DataRow dr in dt.Rows)
                {
                    lesCommandes.Add(new Commande((int)dr["numcommande"], entreprise.LesEmployes.SingleOrDefault(c => c.Id == (int)dr["numemploye"]), entreprise.LesModesTransports.SingleOrDefault(c => c.Id == (int)dr["numtransport"]),
                        entreprise.LesRevendeurs.SingleOrDefault(c => c.Id == (int)dr["numrevendeur"]), new Dictionary<Produit, decimal[,]>(), (DateTime)dr["datecommande"], (DateTime)dr["datelivraison"], (decimal)dr["prixtotal"]));

                }
            }
            return lesCommandes;
        }
    }
}
