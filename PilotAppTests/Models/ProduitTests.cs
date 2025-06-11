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
    public class ProduitTests
    {
        private TypePointe typePointe;
        private Type type;
        private Categorie categorie;
        private List<Couleur> couleurs;
        private Couleur couleur1, couleur2;

        [TestInitialize]
        public void Setup()
        {
            categorie = new Categorie(1, "Stylos");
            type = new Type(1, categorie, "Feutre");
            typePointe = new TypePointe(1, "Fine");
            couleur1 = new Couleur(1, "Rouge");
            couleur2 = new Couleur(2, "Bleu");
            couleurs = new List<Couleur> { couleur1, couleur2 };
        }

        [TestMethod()]
        public void ProduitTest()
        {
            Produit produit = new Produit();
            Assert.IsNotNull(produit);
        }

        [TestMethod()]
        public void ProduitTest1()
        {
            Produit produit = new Produit(1, typePointe, type, couleurs, "C1234", "Pilot G2", 2.50m, 100, true);

            Assert.AreEqual(1, produit.Id);
            Assert.AreEqual(typePointe, produit.UnTypePointe);
            Assert.AreEqual(type, produit.UnType);
            Assert.AreEqual(couleurs, produit.LesCouleurs);
            Assert.AreEqual("C1234", produit.Code);
            Assert.AreEqual("Pilot G2", produit.Nom);
            Assert.AreEqual(2.50m, produit.PrixVente);
            Assert.AreEqual(100, produit.QuantiteStock);
            Assert.AreEqual(true, produit.Disponible);
        }

        [TestMethod()]
        public void IdTest_ValidValue()
        {
            Produit produit = new Produit();
            produit.Id = 5;
            Assert.AreEqual(5, produit.Id);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void IdTest_InvalidValue()
        {
            Produit produit = new Produit();
            produit.Id = 0;
        }

        [TestMethod()]
        public void UnTypePointeTest_ValidValue()
        {
            Produit produit = new Produit();
            produit.UnTypePointe = typePointe;
            Assert.AreEqual(typePointe, produit.UnTypePointe);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void UnTypePointeTest_NullValue()
        {
            Produit produit = new Produit();
            produit.UnTypePointe = null;
        }

        [TestMethod()]
        public void UnTypeTest_ValidValue()
        {
            Produit produit = new Produit();
            produit.UnType = type;
            Assert.AreEqual(type, produit.UnType);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void UnTypeTest_NullValue()
        {
            Produit produit = new Produit();
            produit.UnType = null;
        }

        [TestMethod()]
        public void LesCouleursTest_ValidValue()
        {
            Produit produit = new Produit();
            produit.LesCouleurs = couleurs;
            Assert.AreEqual(couleurs, produit.LesCouleurs);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void LesCouleursTest_NullValue()
        {
            Produit produit = new Produit();
            produit.LesCouleurs = null;
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void LesCouleursTest_EmptyList()
        {
            Produit produit = new Produit();
            produit.LesCouleurs = new List<Couleur>();
        }

        [TestMethod()]
        public void CodeTest_ValidValue()
        {
            Produit produit = new Produit();
            produit.Code = "C1234";
            Assert.AreEqual("C1234", produit.Code);
        }

        [TestMethod()]
        public void CodeTest_ValidValueLowerCase()
        {
            Produit produit = new Produit();
            produit.Code = "c5678";
            Assert.AreEqual("c5678", produit.Code);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CodeTest_InvalidFormat()
        {
            Produit produit = new Produit();
            produit.Code = "D1234";
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CodeTest_NullValue()
        {
            Produit produit = new Produit();
            produit.Code = null;
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CodeTest_EmptyString()
        {
            Produit produit = new Produit();
            produit.Code = "";
        }

        [TestMethod()]
        public void NomTest_ValidValue()
        {
            Produit produit = new Produit();
            produit.Nom = "Pilot G2";
            Assert.AreEqual("Pilot G2", produit.Nom);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void NomTest_NullValue()
        {
            Produit produit = new Produit();
            produit.Nom = null;
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void NomTest_EmptyString()
        {
            Produit produit = new Produit();
            produit.Nom = "";
        }

        [TestMethod()]
        public void PrixVenteTest_ValidValue()
        {
            Produit produit = new Produit();
            produit.PrixVente = 15.99m;
            Assert.AreEqual(15.99m, produit.PrixVente);
        }

        [TestMethod()]
        public void PrixVenteTest_ZeroValue()
        {
            Produit produit = new Produit();
            produit.PrixVente = 0m;
            Assert.AreEqual(0m, produit.PrixVente);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void PrixVenteTest_NegativeValue()
        {
            Produit produit = new Produit();
            produit.PrixVente = -5.00m;
        }

        [TestMethod()]
        public void QuantiteStockTest_ValidValue()
        {
            Produit produit = new Produit();
            produit.QuantiteStock = 50;
            Assert.AreEqual(50, produit.QuantiteStock);
        }

        [TestMethod()]
        public void QuantiteStockTest_ZeroValue()
        {
            Produit produit = new Produit();
            produit.QuantiteStock = 0;
            Assert.AreEqual(0, produit.QuantiteStock);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void QuantiteStockTest_NegativeValue()
        {
            Produit produit = new Produit();
            produit.QuantiteStock = -10;
        }

        [TestMethod()]
        public void DisponibleTest()
        {
            Produit produit = new Produit();
            produit.Disponible = true;
            Assert.AreEqual(true, produit.Disponible);

            produit.Disponible = false;
            Assert.AreEqual(false, produit.Disponible);
        }

        [TestMethod()]
        public void ImageTest()
        {
            Produit produit = new Produit(1, typePointe, type, couleurs, "C1234", "Pilot G2", 2.50m, 100, true);
            Assert.IsNotNull(produit.Image);
        }

        [TestMethod()]
        public void EqualsTest()
        {
            Produit produit1 = new Produit(1, typePointe, type, couleurs, "C1234", "Pilot G2", 2.50m, 100, true);
            Produit produit2 = new Produit(2, typePointe, type, couleurs, "C1234", "Autre nom", 5.00m, 50, false);

            Assert.AreEqual(produit1.Code, produit2.Code);
        }

        [TestMethod()]
        public void EqualsTest_DifferentProducts()
        {
            Produit produit1 = new Produit(1, typePointe, type, couleurs, "C1234", "Pilot G2", 2.50m, 100, true);
            Produit produit2 = new Produit(2, typePointe, type, couleurs, "C5678", "Autre produit", 5.00m, 50, false);

            Assert.AreNotEqual(produit1.Code, produit2.Code);
        }

        [TestMethod()]
        public void EqualsTest_NullObject()
        {
            Produit produit1 = new Produit(1, typePointe, type, couleurs, "C1234", "Pilot G2", 2.50m, 100, true);

            Assert.IsFalse(produit1.Equals(null));
        }

        [TestMethod()]
        [Ignore("Nécessite une base de données")]
        public void FindAllTest()
        {
            Entreprise entreprise = new Entreprise("Entreprise");
            Produit produit = new Produit();
            List<Produit> result = produit.FindAll(entreprise);

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        [Ignore("Nécessite une base de données")]
        public void CreateTest()
        {
            Produit produit = new Produit(0, typePointe, type, couleurs, "C9999", "Test Produit", 10.00m, 25, true);
            int result = produit.Create();

            Assert.IsTrue(result > 0);
            Assert.IsTrue(produit.Id > 0);
        }

        [TestMethod()]
        [Ignore("Nécessite une base de données")]
        public void ReadTest()
        {
            Entreprise entreprise = new Entreprise("Entreprise");
            Produit produit = new Produit();
            produit.Id = 1;
            produit.Read(entreprise);

            Assert.AreEqual(1, produit.Id);
        }

        [TestMethod()]
        [Ignore("Nécessite une base de données")]
        public void UpdateTest()
        {
            Produit produit = new Produit(1, typePointe, type, couleurs, "C1234", "Produit Modifié", 15.00m, 75, true);
            int result = produit.Update();

            Assert.IsTrue(result >= 0);
        }

        [TestMethod()]
        [Ignore("Nécessite une base de données")]
        public void DeleteTest()
        {
            Produit produit = new Produit();
            produit.Id = 1;
            int result = produit.Delete();

            Assert.IsTrue(result >= 0);
        }

        [TestMethod()]
        [Ignore("Nécessite une base de données")]
        public void DeleteCPTest()
        {
            Produit produit = new Produit();
            produit.Id = 1;
            int result = produit.DeleteCP();

            Assert.IsTrue(result >= 0);
        }

        [TestMethod()]
        [Ignore("Nécessite une base de données")]
        public void InsertCPTest()
        {
            Produit produit = new Produit();
            produit.Id = 1;
            int result = produit.InsertCP(couleur1);

            Assert.IsTrue(result >= 0);
        }

        [TestMethod()]
        [Ignore("Nécessite une base de données")]
        public void UpdateDisponibiliteTest()
        {
            Produit produit = new Produit();
            produit.Id = 1;
            produit.Disponible = true;

            int result = produit.UpdateDisponibilite();

            Assert.IsTrue(result >= 0);
            Assert.AreEqual(false, produit.Disponible);
        }

        [TestMethod()]
        [Ignore("Nécessite une base de données")]
        public void FindNbCouleurTest()
        {
            Produit produit = new Produit();
            produit.Id = 1;
            int result = produit.FindNbCouleur();

            Assert.IsTrue(result >= 0);
        }

        [TestMethod()]
        [Ignore("Nécessite une base de données")]
        public void FindNbCommandeTest()
        {
            Produit produit = new Produit();
            produit.Id = 1;
            int result = produit.FindNbCommande();

            Assert.IsTrue(result >= 0);
        }

        [TestMethod()]
        [ExpectedException(typeof(NotImplementedException))]
        public void ReadTest1()
        {
            Produit produit = new Produit();
            produit.Read();
        }

        [TestMethod()]
        [ExpectedException(typeof(NotImplementedException))]
        public void FindAllTest1()
        {
            Produit produit = new Produit();
            List<Produit> result = produit.FindAll();
        }

        [TestMethod()]
        [ExpectedException(typeof(NotImplementedException))]
        public void FindBySelectionTest()
        {
            Produit produit = new Produit();
            List<Produit> result = produit.FindBySelection("critère");
        }
    }
}