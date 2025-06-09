using System;
using System.Threading.Tasks;
using System.Data;
using Npgsql;
using PilotApp.Models;

namespace PilotApp.Services
{
    public class AuthenticationService
    {
        private readonly DataAccess _dataAccess;
        private Employe _currentUser;

        public Employe CurrentUser => _currentUser;
        public bool IsAuthenticated => _currentUser != null;

        public AuthenticationService()
        {
            //_dataAccess = DataAccess.Instance;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            try
            {
                // Utilisation de votre DataAccess existant pour l'authentification
                _currentUser = await AuthenticateUserAsync(username, password);
                return _currentUser != null;
            }
            catch (Exception ex)
            {
                LogError.Log(ex, $"Erreur lors de l'authentification pour l'utilisateur: {username}");
                return false;
            }
        }

        private async Task<Employe> AuthenticateUserAsync(string username, string password)
        {
            return await Task.Run(() =>
            {
                try
                {
                    // Requête SQL pour authentifier l'utilisateur
                    string query = @"
                        SELECT e.numemploye, e.nom, e.prenom, e.login, 
                               r.numrole, r.nomrole
                        FROM employes e
                        INNER JOIN roles r ON e.numrole = r.numrole
                        WHERE e.login = @login ";

                    using (var cmd = new NpgsqlCommand(query))
                    {
                        cmd.Parameters.AddWithValue("@login", username);
                        cmd.Parameters.AddWithValue("@password", password); // En production, utilisez un hash

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
            });
        }

        public void Logout()
        {
            _currentUser = null;
        }

        public bool HasRole(Role role)
        {
            return _currentUser?.UnRole?.Id == role?.Id;
        }

        public bool HasRole(UserRole userRole)
        {
            if (_currentUser?.UnRole == null) return false;

            return userRole switch
            {
                UserRole.Commercial => _currentUser.UnRole.Nom.Equals("Commercial", StringComparison.OrdinalIgnoreCase),
                UserRole.ResponsableProduction => _currentUser.UnRole.Nom.Equals("Responsable de production", StringComparison.OrdinalIgnoreCase),
                _ => false
            };
        }

        public bool IsCommercial => HasRole(UserRole.Commercial);
        public bool IsResponsableProduction => HasRole(UserRole.ResponsableProduction);

        // Méthode pour vérifier si l'utilisateur existe (utile pour la validation)
        public async Task<bool> UserExistsAsync(string username)
        {
            return await Task.Run(() =>
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
            });
        }

        // Méthode pour obtenir tous les rôles disponibles
        public async Task<List<Role>> GetAllRolesAsync()
        {
            return await Task.Run(() =>
            {
                var roles = new List<Role>();

                try
                {
                    string query = "SELECT id_role, nom_role FROM roles ORDER BY nom_role";

                    using (var cmd = new NpgsqlCommand(query))
                    {
                        DataTable result = _dataAccess.ExecuteSelect(cmd);

                        foreach (DataRow row in result.Rows)
                        {
                            roles.Add(new Role(
                                Convert.ToInt32(row["id_role"]),
                                row["nom_role"].ToString()
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
            });
        }
    }
}

// Enum pour les rôles (compatible avec votre classe Role)
namespace PilotApp.Models
{
    public enum UserRole
    {
        Commercial = 1,
        ResponsableProduction = 2
    }
}