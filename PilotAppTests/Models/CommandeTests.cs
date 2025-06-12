using Microsoft.VisualStudio.TestTools.UnitTesting;
using PilotApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotApp.Models.Tests
{
    [TestClass()]
    public class CommandeTests
    {
        private Employe employeTest;
        private ModeTransport modeTransportTest;
        private Revendeur revendeurTest;
        private Dictionary<Produit, decimal[]> sousCommandesTest;
        private Produit produitTest;
        private DateTime dateCommandeTest;
        private DateTime dateLivraisonTest;

        [TestInitialize]
        public void Setup()
        {
            employeTest = new Employe();
            modeTransportTest = new ModeTransport();
            revendeurTest = new Revendeur();

            produitTest = new Produit();

            sousCommandesTest = new Dictionary<Produit, decimal[]>();
            sousCommandesTest.Add(produitTest, new decimal[] { 5, 100.50m });

            dateCommandeTest = new DateTime(2024, 1, 15);
            dateLivraisonTest = new DateTime(2024, 1, 20);
        }

        [TestMethod()]
        public void CommandeTest_ConstructeurVide_DevraitCreerObjetAvecDictionnaireVide()
        {
            var commande = new Commande();

            Assert.IsNotNull(commande.LesSousCommandes);
            Assert.AreEqual(commande.LesSousCommandes.Count, 0);
        }

        [TestMethod()]
        public void CommandeTest_ConstructeurComplet_DevraitInitialiserToutesLesProprietes()
        {
            int id = 1;

            var commande = new Commande(id, employeTest, modeTransportTest, revendeurTest,
                                      sousCommandesTest, dateCommandeTest, dateLivraisonTest);

            Assert.AreEqual(commande.Id, id);
            Assert.AreEqual(commande.UnEmploye, employeTest);
            Assert.AreEqual(commande.UnModeTransport, modeTransportTest);
            Assert.AreEqual(commande.UnRevendeur, revendeurTest);
            Assert.AreEqual(commande.LesSousCommandes, sousCommandesTest);
            Assert.AreEqual(commande.DateCommande, dateCommandeTest);
            Assert.AreEqual(commande.DateLivraison, dateLivraisonTest);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Id_ValeurNulle_DevraitLeverException()
        {
            var commande = new Commande();

            commande.Id = 0;
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void UnEmploye_ValeurNulle_DevraitLeverException()
        {
            var commande = new Commande();

            commande.UnEmploye = null;
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void UnModeTransport_ValeurNulle_DevraitLeverException()
        {
            var commande = new Commande();

            commande.UnModeTransport = null;
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void UnRevendeur_ValeurNulle_DevraitLeverException()
        {
            var commande = new Commande();

            commande.UnRevendeur = null;
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void DateCommande_ValeurNulle_DevraitLeverException()
        {
            var commande = new Commande();
            commande.DateCommande = default(DateTime);
        }

        [TestMethod()]
        public void DateLivraison_ValeurNulle_DevraitEtreAcceptee()
        {
            var commande = new Commande();

            commande.DateLivraison = null;

            Assert.IsNull(commande.DateLivraison);
        }

        [TestMethod()]
        public void Prix_CalculerPrix_DevraitRetournerSommeDesSousCommandes()
        {
            var commande = new Commande();
            var produit1 = new Produit();
            var produit2 = new Produit();

            var sousCommandes = new Dictionary<Produit, decimal[]>();
            sousCommandes.Add(produit1, new decimal[] { 2, 50.00m });
            sousCommandes.Add(produit2, new decimal[] { 3, 75.50m });

            commande.LesSousCommandes = sousCommandes;

            decimal prixTotal = commande.Prix;

            Assert.AreEqual(prixTotal, 125.50m);
        }

        [TestMethod()]
        public void EqualsTest_CommandesIdentiques_DevraitRetournerTrue()
        {
            var commande1 = new Commande(1, employeTest, modeTransportTest, revendeurTest,
                                       sousCommandesTest, dateCommandeTest, dateLivraisonTest);
            var commande2 = new Commande(1, employeTest, modeTransportTest, revendeurTest,
                                       new Dictionary<Produit, decimal[]>(), dateCommandeTest, null);

            bool result = commande1.Equals(commande2);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void EqualsTest_CommandesDifferentes_DevraitRetournerFalse()
        {
            var autreEmploye = new Employe();
            var commande1 = new Commande(1, employeTest, modeTransportTest, revendeurTest,
                                       sousCommandesTest, dateCommandeTest, dateLivraisonTest);
            var commande2 = new Commande(1, autreEmploye, modeTransportTest, revendeurTest,
                                       sousCommandesTest, dateCommandeTest, dateLivraisonTest);

            bool result = commande1.Equals(commande2);

            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void EqualsTest_ObjetNull_DevraitRetournerFalse()
        {
            var commande = new Commande(1, employeTest, modeTransportTest, revendeurTest,
                                      sousCommandesTest, dateCommandeTest, dateLivraisonTest);

            bool result = commande.Equals(null);

            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void EqualsTest_ObjetDifferentType_DevraitRetournerFalse()
        {
            var commande = new Commande(1, employeTest, modeTransportTest, revendeurTest,
                                      sousCommandesTest, dateCommandeTest, dateLivraisonTest);
            var autreObjet = "pas une commande";

            bool result = commande.Equals(autreObjet);

            Assert.IsFalse(result);
        }

        [TestMethod()]
        [Ignore("Nécessite une base de données de test ")]
        public void FindAllTest_AvecEntreprise_DevraitRetournerListeCommandes()
        {
            var entreprise = new Entreprise("Entreprise");
            var commande = new Commande();

            var result = commande.FindAll(entreprise);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Commande>));
        }

        [TestMethod()]
        [Ignore("Nécessite une base de données de test ")]
        public void CreateTest_CommandeValide_DevraitRetournerIdNonZero()
        {
            var commande = new Commande(0, employeTest, modeTransportTest, revendeurTest,
                                      sousCommandesTest, dateCommandeTest, dateLivraisonTest);

            int result = commande.Create();

            Assert.IsTrue(result > 0);
            Assert.AreEqual(commande.Id, result);
        }

        [TestMethod()]
        [Ignore("Nécessite une base de données de test ")]
        public void InsertPCTest_ProduitEtQuantiteValides_DevraitRetournerNonZero()
        {
            var commande = new Commande();
            commande.Id = 1;
            var produit = new Produit();
            int quantite = 5;
            decimal prix = 25.99m;

            int result = commande.InsertPC(produit, quantite, prix);

            Assert.IsTrue(result >= 0);
        }

        [TestMethod()]
        [Ignore("Nécessite une base de données de test ")]
        public void ReadTest_IdExistant_DevraitChargerProprietes()
        {
            var entreprise = new Entreprise("Entreprise");
            var commande = new Commande();
            commande.Id = 1;

            commande.Read(entreprise);

            Assert.IsNotNull(commande.UnEmploye);
            Assert.IsNotNull(commande.UnModeTransport);
            Assert.IsNotNull(commande.UnRevendeur);
            Assert.IsNotNull(commande.LesSousCommandes);
        }

        [TestMethod()]
        [Ignore("Nécessite une base de données de test ")]
        public void UpdateTest_CommandeModifiee_DevraitRetournerNonZero()
        {
            var commande = new Commande(1, employeTest, modeTransportTest, revendeurTest,
                                      sousCommandesTest, dateCommandeTest, dateLivraisonTest);

            int result = commande.Update();

            Assert.IsTrue(result >= 0);
        }

        [TestMethod()]
        [Ignore("Nécessite une base de données de test ")]
        public void DeleteTest_CommandeExistante_DevraitRetournerNonZero()
        {
            var commande = new Commande();
            commande.Id = 1;

            int result = commande.Delete();

            Assert.IsTrue(result >= 0);
        }

        [TestMethod()]
        [Ignore("Nécessite une base de données de test ")]
        public void DeletePCTest_CommandeExistante_DevraitRetournerNonZero()
        {
            var commande = new Commande();
            commande.Id = 1;

            int result = commande.DeletePC();

            Assert.IsTrue(result >= 0);
        }

        [TestMethod()]
        public void UpdateDateLivraisonTest_NouvelleDateValide_DevraitMettreAJourPropriete()
        {
            var commande = new Commande();
            commande.Id = 1;
            var nouvelleDateLivraison = new DateTime(2024, 2, 15);

            commande.DateLivraison = nouvelleDateLivraison;

            Assert.AreEqual(commande.DateLivraison, nouvelleDateLivraison);
        }

        [TestMethod()]
        [Ignore("Nécessite une base de données de test ")]
        public void UpdateDateLivraisonTest_AvecBaseDeDonnees_DevraitRetournerNonZero()
        {
            var commande = new Commande();
            commande.Id = 1;
            var nouvelleDateLivraison = new DateTime(2024, 2, 15);

            int result = commande.UpdateDateLivraison(nouvelleDateLivraison);

            Assert.IsTrue(result >= 0);
            Assert.AreEqual(commande.DateLivraison, nouvelleDateLivraison);
        }

        [TestMethod()]
        [Ignore("Nécessite une base de données de test ")]
        public void FindNbProduitTest_CommandeAvecProduits_DevraitRetournerNombre()
        {
            var commande = new Commande();
            commande.Id = 1;

            int result = commande.FindNbProduit();

            Assert.IsTrue(result >= 0);
        }

        [TestMethod()]
        [ExpectedException(typeof(NotImplementedException))]
        public void ICrudRead_DevraitLeverNotImplementedException()
        {
            var commande = new Commande();
            ICrud<Commande> crudCommande = commande;

            crudCommande.Read();
        }

        [TestMethod()]
        [ExpectedException(typeof(NotImplementedException))]
        public void ICrudFindAll_DevraitLeverNotImplementedException()
        {
            var commande = new Commande();
            ICrud<Commande> crudCommande = commande;

            crudCommande.FindAll();
        }

        [TestMethod()]
        [ExpectedException(typeof(NotImplementedException))]
        public void ICrudFindBySelection_DevraitLeverNotImplementedException()
        {
            var commande = new Commande();
            ICrud<Commande> crudCommande = commande;

            crudCommande.FindBySelection("critere");
        }

        [TestMethod()]
        public void LesSousCommandes_DictionnaireValide_DevraitEtreAccepte()
        {
            var commande = new Commande();
            var sousCommandes = new Dictionary<Produit, decimal[]>();
            var produit = new Produit();
            sousCommandes.Add(produit, new decimal[] { 3, 45.99m });

            commande.LesSousCommandes = sousCommandes;

            Assert.AreEqual(commande.LesSousCommandes, sousCommandes);
            Assert.AreEqual(commande.LesSousCommandes.Count, 1);
        }

        [TestMethod()]
        public void Prix_DictionnaireVide_DevraitRetournerZero()
        {
            var commande = new Commande();

            decimal prix = commande.Prix;

            Assert.AreEqual(prix, 0m);
        }
    }
}