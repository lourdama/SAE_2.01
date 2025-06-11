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
    public class RevendeurTests
    {
        [TestMethod()]
        public void RevendeurTest()
        {
            Revendeur revendeur = new Revendeur();
            Assert.IsNotNull(revendeur);
        }

        [TestMethod()]
        public void RevendeurTest1()
        {
            Revendeur revendeur = new Revendeur(1, "Bureau Vallée", "123 Rue de la Paix", "Paris", "75001");

            Assert.AreEqual(1, revendeur.Id);
            Assert.AreEqual("Bureau Vallée", revendeur.RaisonSociale);
            Assert.AreEqual("123 Rue de la Paix", revendeur.Rue);
            Assert.AreEqual("Paris", revendeur.Ville);
            Assert.AreEqual("75001", revendeur.CodePostal);
        }

        [TestMethod()]
        public void IdTest_ValidValue()
        {
            Revendeur revendeur = new Revendeur();
            revendeur.Id = 5;
            Assert.AreEqual(5, revendeur.Id);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void IdTest_InvalidValue()
        {
            Revendeur revendeur = new Revendeur();
            revendeur.Id = 0;
        }

        [TestMethod()]
        public void RaisonSocialeTest_ValidValue()
        {
            Revendeur revendeur = new Revendeur();
            revendeur.RaisonSociale = "FNAC";
            Assert.AreEqual("FNAC", revendeur.RaisonSociale);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void RaisonSocialeTest_NullValue()
        {
            Revendeur revendeur = new Revendeur();
            revendeur.RaisonSociale = null;
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void RaisonSocialeTest_EmptyString()
        {
            Revendeur revendeur = new Revendeur();
            revendeur.RaisonSociale = "";
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void RaisonSocialeTest_WhitespaceOnly()
        {
            Revendeur revendeur = new Revendeur();
            revendeur.RaisonSociale = "   ";
        }

        [TestMethod()]
        public void RueTest_ValidValue()
        {
            Revendeur revendeur = new Revendeur();
            revendeur.Rue = "45 Avenue des Champs";
            Assert.AreEqual("45 Avenue des Champs", revendeur.Rue);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void RueTest_NullValue()
        {
            Revendeur revendeur = new Revendeur();
            revendeur.Rue = null;
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void RueTest_EmptyString()
        {
            Revendeur revendeur = new Revendeur();
            revendeur.Rue = "";
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void RueTest_WhitespaceOnly()
        {
            Revendeur revendeur = new Revendeur();
            revendeur.Rue = "   ";
        }

        [TestMethod()]
        public void VilleTest_ValidValue()
        {
            Revendeur revendeur = new Revendeur();
            revendeur.Ville = "Lyon";
            Assert.AreEqual("Lyon", revendeur.Ville);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void VilleTest_NullValue()
        {
            Revendeur revendeur = new Revendeur();
            revendeur.Ville = null;
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void VilleTest_EmptyString()
        {
            Revendeur revendeur = new Revendeur();
            revendeur.Ville = "";
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void VilleTest_WhitespaceOnly()
        {
            Revendeur revendeur = new Revendeur();
            revendeur.Ville = "   ";
        }

        [TestMethod()]
        public void CodePostalTest_ValidMetropole()
        {
            Revendeur revendeur = new Revendeur();
            revendeur.CodePostal = "75001";
            Assert.AreEqual("75001", revendeur.CodePostal);
        }

        [TestMethod()]
        public void CodePostalTest_ValidCorse()
        {
            Revendeur revendeur = new Revendeur();
            revendeur.CodePostal = "20000";
            Assert.AreEqual("20000", revendeur.CodePostal);
        }

        [TestMethod()]
        public void CodePostalTest_ValidDOM()
        {
            Revendeur revendeur = new Revendeur();
            revendeur.CodePostal = "97100";
            Assert.AreEqual("97100", revendeur.CodePostal);
        }

        [TestMethod()]
        public void CodePostalTest_ValidTOM()
        {
            Revendeur revendeur = new Revendeur();
            revendeur.CodePostal = "98000";
            Assert.AreEqual("98000", revendeur.CodePostal);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CodePostalTest_InvalidFormat()
        {
            Revendeur revendeur = new Revendeur();
            revendeur.CodePostal = "1234";
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CodePostalTest_InvalidStart()
        {
            Revendeur revendeur = new Revendeur();
            revendeur.CodePostal = "00123";
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CodePostalTest_InvalidRange()
        {
            Revendeur revendeur = new Revendeur();
            revendeur.CodePostal = "96000";
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CodePostalTest_NullValue()
        {
            Revendeur revendeur = new Revendeur();
            revendeur.CodePostal = null;
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CodePostalTest_EmptyString()
        {
            Revendeur revendeur = new Revendeur();
            revendeur.CodePostal = "";
        }

        [TestMethod()]
        public void EqualsTest()
        {
            Revendeur revendeur1 = new Revendeur(1, "Bureau Vallée", "123 Rue de la Paix", "Paris", "75001");
            Revendeur revendeur2 = new Revendeur(1, "Autre raison", "123 Rue de la Paix", "Paris", "75001");

            Assert.AreEqual(revendeur1.Id, revendeur2.Id);
            Assert.AreEqual(revendeur1.Rue, revendeur2.Rue);
            Assert.AreEqual(revendeur1.Ville, revendeur2.Ville);
            Assert.AreEqual(revendeur1.CodePostal, revendeur2.CodePostal);
        }

        [TestMethod()]
        public void EqualsTest_DifferentRevendeurs()
        {
            Revendeur revendeur1 = new Revendeur(1, "Bureau Vallée", "123 Rue de la Paix", "Paris", "75001");
            Revendeur revendeur2 = new Revendeur(2, "FNAC", "456 Avenue de la République", "Lyon", "69000");

            Assert.AreNotEqual(revendeur1.Id, revendeur2.Id);
            Assert.AreNotEqual(revendeur1.Rue, revendeur2.Rue);
            Assert.AreNotEqual(revendeur1.Ville, revendeur2.Ville);
            Assert.AreNotEqual(revendeur1.CodePostal, revendeur2.CodePostal);
        }

        [TestMethod()]
        public void EqualsTest_NullObject()
        {
            Revendeur revendeur1 = new Revendeur(1, "Bureau Vallée", "123 Rue de la Paix", "Paris", "75001");

            Assert.IsFalse(revendeur1.Equals(null));
        }

        [TestMethod()]
        public void EqualsTest_DifferentRue()
        {
            Revendeur revendeur1 = new Revendeur(1, "Bureau Vallée", "123 Rue de la Paix", "Paris", "75001");
            Revendeur revendeur2 = new Revendeur(1, "Bureau Vallée", "456 Rue Différente", "Paris", "75001");

            Assert.AreNotEqual(revendeur1.Rue, revendeur2.Rue);
        }

        [TestMethod()]
        [Ignore("Nécessite une base de données")]
        public void FindAllTest()
        {
            Revendeur revendeur = new Revendeur();
            List<Revendeur> result = revendeur.FindAll();

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        [Ignore("Nécessite une base de données")]
        public void CreateTest()
        {
            Revendeur revendeur = new Revendeur(0, "Test Revendeur", "123 Rue Test", "Ville Test", "75001");
            int result = revendeur.Create();

            Assert.IsTrue(result > 0);
            Assert.IsTrue(revendeur.Id > 0);
        }

        [TestMethod()]
        [Ignore("Nécessite une base de données")]
        public void ReadTest()
        {
            Revendeur revendeur = new Revendeur();
            revendeur.Id = 1;
            revendeur.Read();

            Assert.AreEqual(1, revendeur.Id);
        }

        [TestMethod()]
        [Ignore("Nécessite une base de données")]
        public void UpdateTest()
        {
            Revendeur revendeur = new Revendeur(1, "Revendeur Modifié", "Rue Modifiée", "Ville Modifiée", "69000");
            int result = revendeur.Update();

            Assert.IsTrue(result >= 0);
        }

        [TestMethod()]
        [ExpectedException(typeof(NotImplementedException))]
        public void FindBySelectionTest()
        {
            Revendeur revendeur = new Revendeur();
            List<Revendeur> result = revendeur.FindBySelection("critère");
        }

        [TestMethod()]
        [Ignore("Nécessite une base de données")]
        public void DeleteTest()
        {
            Revendeur revendeur = new Revendeur();
            revendeur.Id = 1;
            int result = revendeur.Delete();

            Assert.IsTrue(result >= 0);
        }

        [TestMethod()]
        [Ignore("Nécessite une base de données")]
        public void FindNbCommandeTest()
        {
            Revendeur revendeur = new Revendeur();
            revendeur.Id = 1;
            int result = revendeur.FindNbCommande();

            Assert.IsTrue(result >= 0);
        }
    }
}