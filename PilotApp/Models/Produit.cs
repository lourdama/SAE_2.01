using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PilotApp.Models
{
    public class Produit : ICrud<Produit>, INotifyPropertyChanged
    {
        private int id;
        private TypePointe unTypePointe;
        private Type unType;
        private List<Couleur> lesCouleurs;
        private string code;
        private string nom;
        private decimal prixVente;
        private int quantiteStock;
        private bool disponible;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Produit()
        {
        }

        public Produit(int id, TypePointe unTypePointe, Type unType, List<Couleur> lesCouleurs, string code, string nom, decimal prixVente, int quantiteStock, bool disponible)
        {
            this.Id = id;
            this.UnTypePointe = unTypePointe;
            this.UnType = unType;
            this.LesCouleurs = lesCouleurs;
            this.Code = code;
            this.Nom = nom;
            this.PrixVente = prixVente;
            this.QuantiteStock = quantiteStock;
            this.Disponible = disponible;
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
                    this.id = value;
                else throw new ArgumentException("L'id ne peut être null.");
            }
        }

        public TypePointe UnTypePointe
        {
            get
            {
                return unTypePointe;
            }

            set
            {
                if (MiseEnForme.NEstPasNull(value))
                    unTypePointe = value;
                else throw new ArgumentException("Le typepointe ne peut être null.");
            }
        }

        public Type UnType
        {
            get
            {
                return unType;
            }

            set
            {
                if (MiseEnForme.NEstPasNull(value))
                    unType = value;
                else throw new ArgumentException("Le type ne peut être null.");
            }
        }

        public List<Couleur> LesCouleurs
        {
            get
            {
                return lesCouleurs;
            }

            set
            {
                if (MiseEnForme.NEstPasNull(value) && value.Count != 0)
                    lesCouleurs = value;
                else throw new ArgumentException("Un produit ne peut pas n'avoir aucune couleur");
            }
        }

        public string Code
        {
            get
            {
                return code;
            }

            set
            {
                Regex regexCodeProduit = new Regex(@"^C\d{4}$", RegexOptions.IgnoreCase);
                if (MiseEnForme.NEstPasNullOuWhitespace(value) && regexCodeProduit.IsMatch(value))
                    code = value;
                else throw new ArgumentException("Le code ne peut être null et doit respecter le format");
            }
        }

        public string Nom
        {
            get
            {
                return nom;
            }

            set
            {
                if (MiseEnForme.NEstPasNullOuWhitespace(value))
                    nom = MiseEnForme.FormaterString(value);
                else throw new ArgumentException("Le code ne peut être null.");
            }
        }

        public decimal PrixVente
        {
            get
            {
                return prixVente;
            }

            set
            {
                if (MiseEnForme.NEstPasNull(value) && MiseEnForme.EstEntre(value,0))
                    prixVente = value;
               else throw new ArgumentException("Le prix ne peut être null ou négatif");
            }
        }

        public int QuantiteStock
        {
            get
            {
                return quantiteStock;
            }

            set
            {
                if (MiseEnForme.NEstPasNull(value) && MiseEnForme.EstEntre(value, 0))
                    quantiteStock = value;
                else throw new ArgumentException("La quantité ne peut être négative");
            }
        }

        public bool Disponible
        {
            get
            {
                return this.disponible;
            }

            set
            {
                this.disponible = value;
            }
        }

        public BitmapImage Image
        {
            get
            {
                // return  new BitmapImage (new Uri($"pack://application:,,,/Images/{this.Code}.jpg"));
                BitmapImage image = new BitmapImage();
                Random random = new Random();
                int randomNbImage = random.Next(1, 7);
                image = new BitmapImage(new Uri($"pack://application:,,,/Images/image{randomNbImage}.jpg"));
                return image;
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is Produit produit &&
                   this.Code == produit.Code;
        }

        public List<Produit> FindAll(Entreprise entreprise)
        {

            List<Produit> lesProduits = new List<Produit>();
            using (NpgsqlCommand cmdSelectProduit = new NpgsqlCommand("select * from produit;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelectProduit);
                
                foreach (DataRow dr in dt.Rows)
                {
                    List<Couleur> lesCouleurs = new List<Couleur>();
                    using (NpgsqlCommand cmdSelectCouleurProduit = new NpgsqlCommand("select * from couleurproduit where numproduit =@id;"))
                    {
                        cmdSelectCouleurProduit.Parameters.AddWithValue("id", dr["numproduit"]);
                        DataTable dtCouleur = DataAccess.Instance.ExecuteSelect(cmdSelectCouleurProduit);
                        foreach (DataRow drCouleur in dtCouleur.Rows)
                        {
                                lesCouleurs.Add(entreprise.LesCouleurs.SingleOrDefault(c => c.Id == (int)drCouleur["numcouleur"]));
                        }
                    }
                        lesProduits.Add(new Produit((int)dr["numproduit"], entreprise.LesTypesPointes.SingleOrDefault(c => c.Id == (int)dr["numtypepointe"]), entreprise.LesTypes.SingleOrDefault(c => c.Id == (int)dr["numtype"]), 
                        lesCouleurs, (string)dr["codeproduit"], (string)dr["nomproduit"], (decimal)dr["prixvente"], (int)dr["quantitestock"], (bool)dr["disponible"]));
                
                }
            }
            return lesProduits;
        }

        public int Create()
        {
            int nb = 0;
            using (var cmdInsert = new NpgsqlCommand("insert into produit (numtypepointe,numtype,codeproduit,nomproduit,prixvente,quantitestock,disponible) " +
                "values (@numtypepointe,@numtype,@codeproduit,@nomproduit,@prixvente,@quantitestock,@disponible) RETURNING numproduit"))
            {
                cmdInsert.Parameters.AddWithValue("numtypepointe", this.UnTypePointe.Id);
                cmdInsert.Parameters.AddWithValue("numtype", this.UnType.Id);
                cmdInsert.Parameters.AddWithValue("codeproduit", this.Code);
                cmdInsert.Parameters.AddWithValue("nomproduit", this.Nom);
                cmdInsert.Parameters.AddWithValue("prixvente", this.PrixVente);
                cmdInsert.Parameters.AddWithValue("quantitestock", this.QuantiteStock);
                cmdInsert.Parameters.AddWithValue("disponible", this.Disponible);
                nb = DataAccess.Instance.ExecuteInsert(cmdInsert);
            }
            this.Id = nb;

                foreach (Couleur couleur in this.LesCouleurs)
                {
                    this.InsertCP(couleur);
                }
            return nb;
        }

        public void Read(Entreprise entreprise)
        {
            using (var cmdSelect = new NpgsqlCommand("select * from  produit  where numproduit =@id;"))
            using (var cmdSelectCP = new NpgsqlCommand("select * from  couleurproduit  where numproduit =@id;"))
            {
                cmdSelect.Parameters.AddWithValue("id", this.Id);
                cmdSelectCP.Parameters.AddWithValue("id", this.Id);
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                DataTable dtCouleur = DataAccess.Instance.ExecuteSelect(cmdSelectCP);
                List<Couleur> lesCouleurs = new List<Couleur>();
                foreach (DataRow drCouleur in dtCouleur.Rows)
                {
                        lesCouleurs.Add(entreprise.LesCouleurs.SingleOrDefault(c => c.Id == (int)drCouleur["numcouleur"]));
                }
                this.Id = (int)dt.Rows[0]["numproduit"];
                this.UnTypePointe = entreprise.LesTypesPointes.SingleOrDefault(c => c.Id == (int)dt.Rows[0]["numtypepointe"]);
                this.UnType = entreprise.LesTypes.SingleOrDefault(c => c.Id == (int)dt.Rows[0]["numtype"]);
                this.LesCouleurs = lesCouleurs;
                this.Code = (string)dt.Rows[0]["codeproduit"];
                this.Nom = (string)dt.Rows[0]["nomproduit"];
                this.PrixVente = (decimal)dt.Rows[0]["prixvente"];
                this.QuantiteStock = (int)dt.Rows[0]["quantitestock"];
                this.Disponible = (bool)dt.Rows[0]["disponible"];
            }

        }

        public int Update()
        {
            using (var cmdUpdate = new NpgsqlCommand("update produit set numtypepointe =@numtypepointe ,  numtype = @numtype,  codeproduit = @codeproduit, " +
                "nomproduit = @nomproduit, prixvente = @prixvente, quantitestock = @quantitestock, disponible = @disponible where numproduit =@id;"))
            {
                cmdUpdate.Parameters.AddWithValue("numtypepointe", this.UnTypePointe.Id);
                cmdUpdate.Parameters.AddWithValue("numtype", this.UnType.Id);
                cmdUpdate.Parameters.AddWithValue("codeproduit", this.Code);
                cmdUpdate.Parameters.AddWithValue("nomproduit", this.Nom);
                cmdUpdate.Parameters.AddWithValue("prixvente", this.PrixVente);
                cmdUpdate.Parameters.AddWithValue("quantitestock", this.QuantiteStock);
                cmdUpdate.Parameters.AddWithValue("disponible", this.Disponible);
                cmdUpdate.Parameters.AddWithValue("id", this.Id);

                this.DeleteCP();
                foreach (Couleur couleur in lesCouleurs)
                {
                        this.InsertCP(couleur);
                }

                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }

        public int Delete()
        {
            using (var cmdUpdate = new NpgsqlCommand("delete from produit  where numproduit =@id;"))
            {
                cmdUpdate.Parameters.AddWithValue("id", this.Id);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }

        public int DeleteCP()
        {
            using (var cmdUpdate = new NpgsqlCommand("delete from couleurproduit where numproduit =@id;"))
            {
                cmdUpdate.Parameters.AddWithValue("id", this.Id);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }

        public int InsertCP(Couleur couleur)
        {
            using (var cmdInsertCP = new NpgsqlCommand("insert into couleurproduit (numproduit,numcouleur) " +
            "values (@numproduit,@numcouleur)"))
            {
                cmdInsertCP.Parameters.AddWithValue("numproduit", this.Id);
                cmdInsertCP.Parameters.AddWithValue("numcouleur", couleur.Id);
                return DataAccess.Instance.ExecuteSet(cmdInsertCP);
            }
        }

        public int UpdateDisponibilite()
        {
            this.Disponible = !this.Disponible;
            using (var cmdUpdateDisponibilite = new NpgsqlCommand("update produit set  disponible = @disponible where numproduit =@id;"))

            {
                cmdUpdateDisponibilite.Parameters.AddWithValue("disponible", this.Disponible);
                cmdUpdateDisponibilite.Parameters.AddWithValue("id", this.Id);
                return DataAccess.Instance.ExecuteSet(cmdUpdateDisponibilite);
            }
        }

        public int FindNbCouleur()
        {
            int nb = 0;
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select count(*) from couleurproduit where numproduit = @id; "))
            {
                cmdSelect.Parameters.AddWithValue("id", this.Id);
                return (int)(Int64)DataAccess.Instance.ExecuteSelectUneValeur(cmdSelect);
            }
            return nb;
        }

        public int FindNbCommande()
        {
            int nb = 0;
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select count(*) from produitcommande where numproduit = @id; "))
            {
                cmdSelect.Parameters.AddWithValue("id", this.Id);
                return (int)(Int64)DataAccess.Instance.ExecuteSelectUneValeur(cmdSelect);
            }
            return nb;
        }

        public void Read()
        {
            //Méthode existante mais utilisée d'une autre manière pour des raisons de la modélisation de la BDD
            throw new NotImplementedException();
        }

        public List<Produit> FindAll()
        {
            //Méthode existante mais utilisée d'une autre manière pour des raisons de la modélisation de la BDD
            throw new NotImplementedException();
        }

        public List<Produit> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }
    }
}
