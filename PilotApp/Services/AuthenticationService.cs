using System;
using System.Threading.Tasks;
using System.Data;
using Npgsql;
using PilotApp.Models;
using PilotApp.Views;
using System.Windows;

namespace PilotApp.Services
{
    public class AuthenticationService
    {
        private MainWindow mainWindow = MainWindow.Instance ;
        private readonly DataAccess _dataAccess;


        public AuthenticationService(string login, string mdp)
        {
            ChargeData(login, mdp);

        }
        public void ChargeData(string login, string mdp)
        {
            try
            {
                mainWindow.login = login;
                mainWindow.mdp = mdp;
                mainWindow.Pilot = new Entreprise("Pilot");
                


            }
            catch (Exception ex)
            {
                MessageBox.Show("Problème lors de récupération des données, veuillez consulter votre admin");
                LogError.Log(ex, "Erreur SQL");
                Application.Current.Shutdown();
            }
        }

        public void ChargeEmploye()
        {
            mainWindow.employeconnecte = new Employe();
            foreach (Employe employeTemp in mainWindow.Pilot.LesEmployes)
            {
                if (employeTemp.Login == mainWindow.login)
                {
                    mainWindow.employeconnecte = employeTemp;
                }
            }
        }

        public bool ARole(roleUtilisateur role)
        {
            return (roleUtilisateur)mainWindow.employeconnecte.UnRole.Id == roleUtilisateur.Administrateur ||(roleUtilisateur) mainWindow.employeconnecte.UnRole.Id == role;
        }

        public bool EstCommercial
        {
            get
            {
                return ARole(roleUtilisateur.Commercial);
            }

           
        }
        public bool EstResponsable
        {
            get
            {
                return ARole(roleUtilisateur.ResponsableProduction);
            }
        }       
    }
}

// Enum pour les rôles (compatible avec votre classe Role)
namespace PilotApp.Models
{
    public enum roleUtilisateur
    {
        Commercial = 1,
        ResponsableProduction = 2,
        Administrateur = 3
    }
}