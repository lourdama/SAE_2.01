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
        private Dictionary<Produit, decimal[]> lesSousCommandes;
        private DateTime dateCommande;
        private DateTime dateLivraison;
        private decimal prix;

        public Commande()
        {
        }

        public Commande(int id, Employe employe, ModeTransport modeTransport, Revendeur revendeur, Dictionary<Produit, decimal[]> lesSousCommandes, DateTime dateCommande, DateTime dateLivraison, decimal prix)
        {
            this.Id = id;
            this.Employe = employe;
            this.ModeTransport = modeTransport;
            this.Revendeur = revendeur;
            this.LesSousCommandes = lesSousCommandes;
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

        public Dictionary<Produit, decimal[]> LesSousCommandes
        {
            get
            {
                return lesSousCommandes;
            }

            set
            {
                lesSousCommandes = value;
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
            using (NpgsqlCommand cmdSelectCommande = new NpgsqlCommand("select * from commande;"))
            using (NpgsqlCommand cmdSelectProduitCommande = new NpgsqlCommand("select * from produitcommande;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelectCommande);
                DataTable dtPC = DataAccess.Instance.ExecuteSelect(cmdSelectProduitCommande);
                foreach (DataRow dr in dt.Rows)
                {
                    Dictionary<Produit, decimal[]> lesSousCommandes = new Dictionary<Produit, decimal[]>();
                    foreach (DataRow drPC in dtPC.Rows)
                    {
                        if (drPC["numcommande"] == dr["numcommande"])
                        {
                            decimal[] coupleQuantitePrix = new decimal[2];
                            coupleQuantitePrix[0] = (decimal)drPC["quantitecommande"];
                            coupleQuantitePrix[1] = (decimal)drPC["prix"];
                            lesSousCommandes.Add(entreprise.LesProduits.SingleOrDefault(c => c.Id == (int)drPC["numproduit"]), coupleQuantitePrix);
                        }
                    }
                    lesCommandes.Add(new Commande((int)dr["numcommande"], entreprise.LesEmployes.SingleOrDefault(c => c.Id == (int)dr["numemploye"]), entreprise.LesModesTransports.SingleOrDefault(c => c.Id == (int)dr["numtransport"]),
                        entreprise.LesRevendeurs.SingleOrDefault(c => c.Id == (int)dr["numrevendeur"]), lesSousCommandes, (DateTime)dr["datecommande"], (DateTime)dr["datelivraison"], (decimal)dr["prixtotal"]));

                }
            }
            return lesCommandes;
        }
    }
}
