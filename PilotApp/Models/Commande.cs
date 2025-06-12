using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace PilotApp.Models
{
    public class Commande : ICrud<Commande>, INotifyPropertyChanged
    {
        private int id;
        private Employe unEmploye;
        private ModeTransport unModeTransport;
        private Revendeur unRevendeur;
        private Dictionary<Produit, decimal[]> lesSousCommandes;
        private DateTime dateCommande = DateTime.Now;
        private DateTime? dateLivraison;
        private decimal prix;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Commande()
        {
            this.LesSousCommandes = new Dictionary<Produit, decimal[]>();
        }

        public Commande(int id, Employe unEmploye, ModeTransport unModeTransport, Revendeur unRevendeur, Dictionary<Produit, decimal[]> lesSousCommandes, DateTime dateCommande, DateTime? dateLivraison):this() 
        {
            this.Id = id;
            this.UnEmploye = unEmploye;
            this.UnModeTransport = unModeTransport;
            this.UnRevendeur = unRevendeur;
            this.LesSousCommandes = lesSousCommandes;
            this.DateCommande = dateCommande;
            this.DateLivraison = dateLivraison;
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                if (MiseEnForme.NEstPasNull(value))
                    id = value;
                else throw new ArgumentException("L'id ne peut être null.");
            }
        }

        public Employe UnEmploye
        {
            get
            {
                return unEmploye;
            }

            set
            {
                if (MiseEnForme.NEstPasNull(value))
                    unEmploye = value;
                else throw new ArgumentException("L'employe ne peut être null.");
            }
        }

        public ModeTransport UnModeTransport
        {
            get
            {
                return unModeTransport;
            }

            set
            {
                if (MiseEnForme.NEstPasNull(value))
                    unModeTransport = value;
                else throw new ArgumentException("Le mode de transport ne peut être null.");
            }
        }

        public Revendeur UnRevendeur
        {
            get
            {
                return unRevendeur;
            }

            set
            {
                if (MiseEnForme.NEstPasNull(value))
                    unRevendeur = value;
                else throw new ArgumentException("Le revendeur ne peut être null.");
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
                 /*if (MiseEnForme.NEstPasNull(value))
                     if (value.Count != 0)
                     {
                         foreach (KeyValuePair<Produit, decimal[]> uneSousCommande in value)
                         {
                             if (!MiseEnForme.EstEntre(uneSousCommande.Value[0], 0) || !MiseEnForme.EstEntre(uneSousCommande.Value[1], 0) || uneSousCommande.Key == null)
                                 throw new ArgumentException("Une sous commande ne doit pas contenir de produit null ni de quantié ou prix négatif");
                         }*/
                lesSousCommandes = value;
                  /* }
                    else throw new ArgumentException("Les sous commandes ne peut être null ou ne rien contenir");*/
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
                if (MiseEnForme.NEstPasNull(value))
                    dateCommande = value;
                else throw new ArgumentException("La date de commande ne peut être null.");
            }
        }

        public DateTime? DateLivraison
        {
            get
            {
                return dateLivraison;
            }

            set
            {
                if (value == null || value > this.DateCommande)
                {
                    dateLivraison = value;
                }
                else throw new ArgumentException("La date de livraison doit être null ou supérieure à la datecommande");
            }
        }

        public decimal Prix
        {
            get
            {
                decimal prix = 0;
                foreach (KeyValuePair<Produit, decimal[]> uneSousCommande in this.LesSousCommandes)
                {
                    prix += uneSousCommande.Value[1];

                }
                return prix;
            }

        }


        public override bool Equals(object? obj)
        {
            return obj is Commande commande &&
                   this.Id == commande.Id &&
                   EqualityComparer<Employe>.Default.Equals(this.UnEmploye, commande.UnEmploye) &&
                   EqualityComparer<ModeTransport>.Default.Equals(this.UnModeTransport, commande.UnModeTransport) &&
                   EqualityComparer<Revendeur>.Default.Equals(this.UnRevendeur, commande.UnRevendeur);
        }

        public List<Commande> FindAll(Entreprise entreprise)
        {

            List<Commande> lesCommandes = new List<Commande>();
            using (NpgsqlCommand cmdSelectCommande = new NpgsqlCommand("select * from commande;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelectCommande);
                foreach (DataRow dr in dt.Rows)
                {
                    Dictionary<Produit, decimal[]> lesSousCommandes = new Dictionary<Produit, decimal[]>();
                    using (NpgsqlCommand cmdSelectProduitCommande = new NpgsqlCommand("select * from produitcommande where numcommande =@id ;"))    
                    {
                        cmdSelectProduitCommande.Parameters.AddWithValue("id", dr["numcommande"]);
                        DataTable dtPC = DataAccess.Instance.ExecuteSelect(cmdSelectProduitCommande);
                        foreach (DataRow drPC in dtPC.Rows)
                        {
                                decimal[] coupleQuantitePrix = new decimal[2];
                                coupleQuantitePrix[0] = Convert.ToDecimal(drPC["quantitecommande"]);
                                coupleQuantitePrix[1] = (decimal)drPC["prix"];
                                lesSousCommandes.Add(entreprise.LesProduits.SingleOrDefault(c => c.Id == (int)drPC["numproduit"]), coupleQuantitePrix);
                        }
                    }
                    DateTime? dateLivraison = null;
                    if (!(dr["datelivraison"] ==DBNull.Value))
                    {
                        dateLivraison = (DateTime)dr["datelivraison"];
                    }
                    lesCommandes.Add(new Commande((int)dr["numcommande"], entreprise.LesEmployes.SingleOrDefault(c => c.Id == (int)dr["numemploye"]), entreprise.LesModesTransports.SingleOrDefault(c => c.Id == (int)dr["numtransport"]),
                        entreprise.LesRevendeurs.SingleOrDefault(c => c.Id == (int)dr["numrevendeur"]), lesSousCommandes, (DateTime)dr["datecommande"], dateLivraison));

                }
            }
            return lesCommandes;
        }

        public int Create()
        {
            int nb = 0;
            using (var cmdInsert = new NpgsqlCommand("insert into commande (numemploye,numtransport,numrevendeur,datecommande,datelivraison,prixtotal) " +
                "values (@numemploye,@numtransport,@numrevendeur,@datecommande,@datelivraison,@prixtotal) RETURNING numcommande"))
            {
                cmdInsert.Parameters.AddWithValue("numemploye", this.UnEmploye.Id);
                cmdInsert.Parameters.AddWithValue("numtransport", this.UnModeTransport.Id);
                cmdInsert.Parameters.AddWithValue("numrevendeur", this.UnRevendeur.Id);
                cmdInsert.Parameters.AddWithValue("datecommande", this.DateCommande);
                if (this.DateLivraison.HasValue)
                {
                    cmdInsert.Parameters.AddWithValue("datelivraison", this.DateLivraison.Value);
                }
                else
                {
                    cmdInsert.Parameters.Add("datelivraison", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = DBNull.Value;
                }
                cmdInsert.Parameters.AddWithValue("prixtotal", this.Prix);
                nb = DataAccess.Instance.ExecuteInsert(cmdInsert);
            }
            this.Id = nb;


            foreach (KeyValuePair<Produit, decimal[]> uneSousCommande in this.LesSousCommandes)
            {
                this.InsertPC(uneSousCommande.Key, (int)uneSousCommande.Value[0], uneSousCommande.Value[1]);
            }


            return nb;
        }
        public int InsertPC(Produit produit, int quantite, decimal prix)
        {
            using (var cmdInsertPC = new NpgsqlCommand("insert into produitcommande (numcommande,numproduit,quantitecommande,prix) " +
                "values (@numcommande,@numproduit,@quantitecommande,@prix)"))
            {
                cmdInsertPC.Parameters.AddWithValue("numcommande", this.Id);
                cmdInsertPC.Parameters.AddWithValue("numproduit", produit.Id);
                cmdInsertPC.Parameters.AddWithValue("quantitecommande", quantite);
                cmdInsertPC.Parameters.AddWithValue("prix", prix);

                return DataAccess.Instance.ExecuteSet(cmdInsertPC);
            }
        }

        public void Read(Entreprise entreprise)
        {
            using (var cmdSelect = new NpgsqlCommand("select * from  commande  where numcommande =@id;"))
            using (var cmdSelectPC = new NpgsqlCommand("select * from  produitcommande  where numcommande =@id;"))
            {
                cmdSelect.Parameters.AddWithValue("id", this.Id);
                cmdSelectPC.Parameters.AddWithValue("id", this.Id);
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                DataTable dtSousCommande = DataAccess.Instance.ExecuteSelect(cmdSelectPC);
                Dictionary<Produit, decimal[]> lesSousCommandes = new Dictionary<Produit, decimal[]>();
                foreach (DataRow drSousCommande in dtSousCommande.Rows)
                {
                    decimal[] coupleQuantitePrix = new decimal[2];
                    coupleQuantitePrix[0] = (decimal)drSousCommande["quantitecommande"];
                    coupleQuantitePrix[1] = (decimal)drSousCommande["prix"];
                    lesSousCommandes.Add(entreprise.LesProduits.SingleOrDefault(c => c.Id == (int)drSousCommande["numProduit"]), coupleQuantitePrix);
                }
                this.Id = (int)dt.Rows[0]["numcommande"];
                this.UnEmploye = entreprise.LesEmployes.SingleOrDefault(c => c.Id == (int)dt.Rows[0]["numemploye"]);
                this.UnModeTransport = entreprise.LesModesTransports.SingleOrDefault(c => c.Id == (int)dt.Rows[0]["numtransport"]);
                this.UnRevendeur = entreprise.LesRevendeurs.SingleOrDefault(c => c.Id == (int)dt.Rows[0]["numrevendeur"]);
                this.LesSousCommandes = lesSousCommandes;
                this.DateCommande = (DateTime)dt.Rows[0]["datecommande"];
                this.DateLivraison = (DateTime)dt.Rows[0]["datelivraison"];
            }

        }

        public int Update()
        {
            using (var cmdUpdate = new NpgsqlCommand("update commande set numemploye =@numemploye ,  numtransport = @numtransport,  numrevendeur = @numrevendeur, " +
                "datecommande = @datecommande, datelivraison = @datelivraison, prixtotal = @prixtotal where numcommande =@id;"))
            {
                cmdUpdate.Parameters.AddWithValue("numemploye", this.UnEmploye.Id);
                cmdUpdate.Parameters.AddWithValue("numtransport", this.UnModeTransport.Id);
                cmdUpdate.Parameters.AddWithValue("numrevendeur", this.UnRevendeur.Id);
                cmdUpdate.Parameters.AddWithValue("datecommande", this.DateCommande);
                if (this.DateLivraison.HasValue)
                {
                    cmdUpdate.Parameters.AddWithValue("datelivraison", this.DateLivraison.Value);
                }
                else
                {
                    cmdUpdate.Parameters.Add("datelivraison", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = DBNull.Value;
                }
                cmdUpdate.Parameters.AddWithValue("prixtotal", this.Prix);
                cmdUpdate.Parameters.AddWithValue("id", this.Id);

                this.DeletePC();
                foreach (KeyValuePair<Produit, decimal[]> uneSousCommande in this.LesSousCommandes)
                {
                    this.InsertPC(uneSousCommande.Key, Convert.ToInt32(uneSousCommande.Value[0]), uneSousCommande.Value[1]);
                }

                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }


        public int Delete()
        {
            using (var cmdUpdate = new NpgsqlCommand("delete from commande where numcommande =@id;"))
            {
                cmdUpdate.Parameters.AddWithValue("id", this.Id);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }

        public int DeletePC()
        {
            using (var cmdUpdate = new NpgsqlCommand("delete from produitcommande where numcommande =@id;"))
            {
                cmdUpdate.Parameters.AddWithValue("id", this.Id);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }

        

        public int UpdateDateLivraison(DateTime dateLivraison)
        {
            this.DateLivraison = dateLivraison;
            using (var cmdUpdateDisponibilite = new NpgsqlCommand("update commande set  datelivraison = @datelivraison where numcommande =@id;"))

            {
                cmdUpdateDisponibilite.Parameters.AddWithValue("datelivraison", this.DateLivraison);
                cmdUpdateDisponibilite.Parameters.AddWithValue("id", this.Id);
                return DataAccess.Instance.ExecuteSet(cmdUpdateDisponibilite);
            }
        }

        public int FindNbProduit()
        {
            int nb = 0;
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select count(*) from produitcommande where numcommande = @id; "))
            {
                cmdSelect.Parameters.AddWithValue("id", this.Id);
                return (int)(Int64)DataAccess.Instance.ExecuteSelectUneValeur(cmdSelect);
            }
            return nb;
        }

        void ICrud<Commande>.Read()
        {
            //Méthode existante mais utilisée d'une autre manière pour des raisons de la modélisation de la BDD
            throw new NotImplementedException();
        }

        List<Commande> ICrud<Commande>.FindAll()
        {
            //Méthode existante mais utilisée d'une autre manière pour des raisons de la modélisation de la BDD
            throw new NotImplementedException();
        }

        List<Commande> ICrud<Commande>.FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }
    }
}
