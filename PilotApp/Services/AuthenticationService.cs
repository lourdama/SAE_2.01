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
        private Employe _currentUser;

        public Employe CurrentUser => _currentUser;
        public bool EstAuthentifie => _currentUser != null;

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
                //Accueil();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Problème lors de récupération des données, veuillez consulter votre admin");
                LogError.Log(ex, "Erreur SQL");
                Application.Current.Shutdown();
            }
        }


        private Employe AuthentifierUser(string username)
        {
                try
                {

                    // Requête SQL pour authentifier l'utilisateur
                    string query = @"
                        SELECT e.numemploye, e.nom, e.prenom, e.login, 
                               r.numrole, r.nomrole
                        FROM employe e
                        INNER JOIN role r ON e.numrole = r.numrole
                        WHERE e.login = @login ";

                    using (var cmd = new NpgsqlCommand(query))
                    {
                        cmd.Parameters.AddWithValue("@login", username);

                        DataTable result = _dataAccess.ExecuteSelect(cmd);

                        if (result.Rows.Count > 0)
                        {
                            DataRow row = result.Rows[0];

                            // Création de l'objet Role
                            var role = new Role(
                                Convert.ToInt32(row["id_role"]),
                                row["nom_role"].ToString()
                            );

                            // Création de l'objet Employe
                            var employe = new Employe
                            {
                                Id = Convert.ToInt32(row["id_employe"]),
                                Nom = row["nom"].ToString(),
                                Prenom = row["prenom"].ToString(),
                                Login = row["login"].ToString(),
                                Mdp = row["mdp"].ToString(),
                                UnRole = role
                            };

                            return employe;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError.Log(ex, "Erreur lors de l'authentification en base de données");
                    throw;
                }

                return null;
            
        }

        public void Logout()
        {
            _currentUser = null;
        }

        public bool ARole(Role role)
        {
            return _currentUser?.UnRole?.Id == role?.Id;
        }

        public bool ARole(UserRole userRole)
        {
            if (_currentUser?.UnRole == null) return false;

            return userRole switch
            {
                UserRole.Commercial => _currentUser.UnRole.Nom.Equals("Commercial"),
                UserRole.ResponsableProduction => _currentUser.UnRole.Nom.Equals("Responsable de production"),
                _ => false
            };
        }

        public bool IsCommercial => ARole(UserRole.Commercial);
        public bool IsResponsableProduction => ARole(UserRole.ResponsableProduction);

        // Méthode pour vérifier si l'utilisateur existe (utile pour la validation)
        public bool UserExists(string username)
        {
                try
                {
                    string query = "SELECT COUNT(*) FROM employes WHERE login = @login";

                    using (var cmd = new NpgsqlCommand(query))
                    {
                        cmd.Parameters.AddWithValue("@login", username);

                        object result = _dataAccess.ExecuteSelectUneValeur(cmd);
                        return Convert.ToInt32(result) > 0;
                    }
                }
                catch (Exception ex)
                {
                    LogError.Log(ex, $"Erreur lors de la vérification d'existence de l'utilisateur: {username}");
                    return false;
                }
        }

        // Méthode pour obtenir tous les rôles disponibles
        public List<Role> GetAllRoles()
        {

                var roles = new List<Role>();

                try
                {
                    string query = "SELECT numrole, nomrole FROM roles ORDER BY nom_role";

                    using (var cmd = new NpgsqlCommand(query))
                    {
                        DataTable result = _dataAccess.ExecuteSelect(cmd);

                        foreach (DataRow row in result.Rows)
                        {
                            roles.Add(new Role(
                                Convert.ToInt32(row["numrole"]),
                                row["nomrole"].ToString()
                            ));
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError.Log(ex, "Erreur lors de la récupération des rôles");
                    throw;
                }

                return roles;
        }
    }
}

// Enum pour les rôles (compatible avec votre classe Role)
namespace PilotApp.Models
{
    public enum UserRole
    {
        Commercial = 1,
        ResponsableProduction = 2,
        Administrateur = 3
    }
}