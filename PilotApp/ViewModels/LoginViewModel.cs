using PilotApp.Helpers;
using PilotApp.Services;
using PilotApp.Views;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PilotApp.ViewModels
{
    /// <summary>
    /// ViewModel pour la fenêtre de connexion avec toute la logique métier
    /// </summary>
    public class LoginViewModel : INotifyPropertyChanged, IDisposable
    {
        #region Champs privés

        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _errorMessage = string.Empty;
        private bool _hasError;
        private bool _isLoading;
        private bool _isLoginEnabled = true;
        private bool _disposed;

        #endregion

        #region Propriétés publiques bindées

        public string Username
        {
            get => _username;
            set
            {
                if (SetProperty(ref _username, value))
                {
                    ClearError();
                    UpdateCanExecuteCommands();
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (SetProperty(ref _password, value))
                {
                    ClearError();
                    UpdateCanExecuteCommands();
                }
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            private set => SetProperty(ref _errorMessage, value);
        }

        public bool HasError
        {
            get => _hasError;
            private set => SetProperty(ref _hasError, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            private set
            {
                if (SetProperty(ref _isLoading, value))
                {
                    IsLoginEnabled = !value;
                    UpdateCanExecuteCommands();
                }
            }
        }

        public bool IsLoginEnabled
        {
            get => _isLoginEnabled;
            private set => SetProperty(ref _isLoginEnabled, value);
        }

        #endregion

        #region Commandes

        public ICommand LoginCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand ClearErrorCommand { get; }

        #endregion

        #region Événements

        public event EventHandler LoginSuccessful;
        public event EventHandler CloseRequested;

        #endregion

        #region Constructeur

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(ExecuteLoginAsync, CanExecuteLogin);
            CloseCommand = new RelayCommand(ExecuteClose);
            ClearErrorCommand = new RelayCommand(ExecuteClearError);
        }

        #endregion

        #region Méthodes publiques

        public void HandleEnterKey()
        {
            if (LoginCommand.CanExecute(null))
            {
                LoginCommand.Execute(null);
            }
        }

        #endregion

        #region Logique des commandes

        private bool CanExecuteLogin(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Username) &&
                   !string.IsNullOrWhiteSpace(Password) &&
                   IsLoginEnabled &&
                   !IsLoading;
        }

        private async void ExecuteLoginAsync(object parameter)
        {
            if (_disposed) return;

            try
            {
                IsLoading = true;
                ClearError();

                // Simulation d'un délai d'authentification (remplacez par votre logique)
                await Task.Delay(800);

                var authResult = await AuthenticateUserAsync(Username, Password);

                if (_disposed) return; // Vérifier si l'objet a été disposé pendant l'attente

                if (authResult.IsSuccess)
                {
                    // Stocker les informations utilisateur
                    CurrentUser.SetUser(authResult.User);

                    // Déclencher l'événement de connexion réussie
                    LoginSuccessful?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    ShowError(authResult.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                ShowError($"Erreur de connexion : {ex.Message}");
            }
            finally
            {
                if (!_disposed)
                {
                    IsLoading = false;
                }
            }
        }

        private void ExecuteClose(object parameter)
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }

        private void ExecuteClearError(object parameter)
        {
            ClearError();
        }

        #endregion

        #region Logique d'authentification

        private async Task<AuthenticationResult> AuthenticateUserAsync(string username, string password)
        {
            return await Task.Run(() =>
            {
                try
                {
                    // Comptes de test - À REMPLACER par votre logique d'authentification
                    var testAccounts = new[]
                    {
                        new { Username = "admin", Password = "admin", Role = "Administrator", FullName = "Administrateur Système" },
                        new { Username = "user", Password = "password", Role = "User", FullName = "Utilisateur Standard" },
                        new { Username = "pilot", Password = "pilot123", Role = "Manager", FullName = "Gestionnaire Pilot" },
                        new { Username = "demo", Password = "demo", Role = "Demo", FullName = "Compte de démonstration" }
                    };

                    var account = testAccounts.FirstOrDefault(a =>
                        string.Equals(a.Username, username?.Trim(), StringComparison.OrdinalIgnoreCase) &&
                        a.Password == password);

                    if (account != null)
                    {
                        return new AuthenticationResult
                        {
                            IsSuccess = true,
                            User = new UserInfo
                            {
                                Username = account.Username,
                                FullName = account.FullName,
                                Role = account.Role,
                                LoginTime = DateTime.Now
                            }
                        };
                    }

                    return new AuthenticationResult
                    {
                        IsSuccess = false,
                        ErrorMessage = "Nom d'utilisateur ou mot de passe incorrect"
                    };
                }
                catch (Exception ex)
                {
                    return new AuthenticationResult
                    {
                        IsSuccess = false,
                        ErrorMessage = $"Erreur d'authentification : {ex.Message}"
                    };
                }
            });
        }

        #endregion

        #region Méthodes utilitaires privées

        private void ShowError(string message)
        {
            if (!_disposed)
            {
                ErrorMessage = message ?? "Une erreur inconnue s'est produite";
                HasError = true;
            }
        }

        private void ClearError()
        {
            if (HasError && !_disposed)
            {
                HasError = false;
                ErrorMessage = string.Empty;
            }
        }

        private void UpdateCanExecuteCommands()
        {
            if (!_disposed)
            {
                CommandManager.InvalidateRequerySuggested();
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
        {
            if (System.Collections.Generic.EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                // Nettoyer les événements
                LoginSuccessful = null;
                CloseRequested = null;

                _disposed = true;
            }
        }

        #endregion
    }

    #region Classes de support

    /// <summary>
    /// Commande réutilisable pour le binding MVVM
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;
        public void Execute(object parameter) => _execute(parameter);
    }

    /// <summary>
    /// Résultat d'une tentative d'authentification
    /// </summary>
    public class AuthenticationResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public UserInfo User { get; set; }
    }

    /// <summary>
    /// Informations sur l'utilisateur connecté
    /// </summary>
    public class UserInfo
    {
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime LoginTime { get; set; }

        public override string ToString()
        {
            return $"{FullName} ({Username}) - {Role}";
        }
    }

    /// <summary>
    /// Singleton pour gérer l'utilisateur actuellement connecté
    /// </summary>
    public static class CurrentUser
    {
        private static UserInfo _user;

        public static UserInfo User
        {
            get => _user;
            private set => _user = value;
        }

        public static bool IsAuthenticated => User != null;

        public static string DisplayName => User?.FullName ?? "Non connecté";

        public static void SetUser(UserInfo user)
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
        }

        public static void Logout()
        {
            User = null;
        }

        public static bool HasRole(string role)
        {
            return IsAuthenticated &&
                   string.Equals(User.Role, role, StringComparison.OrdinalIgnoreCase);
        }
    }

    #endregion
}
