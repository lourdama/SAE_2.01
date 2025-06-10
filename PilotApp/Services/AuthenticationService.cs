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


        public AuthenticationService(string login, string mdp)
        {
            MainWindow.Instance.connexion = ChargeData(login.ToLower(), mdp);
            if (MainWindow.Instance.connexion)
            {
                ChargeEmploye();
            }

        }
        public bool ChargeData(string login, string mdp)
        {
            try
            {
                MainWindow.Instance.login = login;
                MainWindow.Instance.mdp = mdp;
                MainWindow.Instance.Pilot = new Entreprise("Pilot");
                return true;
                


            }
            catch
            {
                MessageBox.Show("Login ou mot de passe incorrect ");
                return false;
            }
        }

        public void ChargeEmploye()
        {
            MainWindow.Instance.EmployeConnecte = new Employe();
            foreach (Employe employeTemp in MainWindow.Instance.Pilot.LesEmployes)
            {
                if (employeTemp.Login == MainWindow.Instance.login)
                {
                    MainWindow.Instance.EmployeConnecte = employeTemp;
                }
            }
            MainWindow.Instance.EstCommercial = ARole(roleUtilisateur.Commercial);
            MainWindow.Instance.EstResponsable = ARole(roleUtilisateur.ResponsableProduction);
            MainWindow.Instance.EstAdmin = ARole(roleUtilisateur.Administrateur);
            

        }

        public bool ARole(roleUtilisateur role)
        {
            return (roleUtilisateur)MainWindow.Instance.EmployeConnecte.UnRole.Id == roleUtilisateur.Administrateur ||(roleUtilisateur) MainWindow.Instance.EmployeConnecte.UnRole.Id == role;
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