using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PokeWish.MVVM.View;
using System.Windows;
using System.Windows.Input;

namespace PokeWish.MVVM.ViewModel
{
    public class MainViewVM : BaseVM
    {
        private object? _currentVM;

        private string _username = "DefaultUser"; // Valeur par défaut pour le nom d'utilisateur.

        public MainViewVM()
        {
            LoadUsernameFromDatabase();
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                // Commandes liées à la navigation
                NavigateToFightSubview = new RelayCommand(() => CurrentVM = new FightSubview());
                NavigateToPokemonsSubview = new RelayCommand(() =>
                {
                    CurrentVM = new PokemonsSubview();
                });
                NavigateToLoginRegisterView = new RelayCommand(() => mainWindow.MainContent.Content = new LoginRegister());
                NavigateToSettingsView = new RelayCommand(() => mainWindow.MainContent.Content = new Settings());
            }
            else
            {
                // Initialisation des commandes pour éviter les valeurs nulles
                NavigateToFightSubview = new RelayCommand(() => { });
                NavigateToPokemonsSubview = new RelayCommand(() =>
                {
                    var app = (App)Application.Current;
                    if (app.CurrentPlayer != null)
                    {
                        CurrentVM = new PokemonsSubviewVM(app.CurrentPlayer.ID);
                        MessageBox.Show($"CurrentVM set to: {CurrentVM?.GetType().Name}");
                    }
                    else
                    {
                        MessageBox.Show("No current player found.");
                    }
                });
                NavigateToLoginRegisterView = new RelayCommand(() => { });
                NavigateToSettingsView = new RelayCommand(() => { });
            }
        }

        // Propriété pour le ViewModel actif dans la zone centrale
        public object? CurrentVM
        {
            get => _currentVM;
            set => SetProperty(ref _currentVM, value);
        }

        // Nom d'utilisateur affiché dans le menu de navigation
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        // Commandes de navigation
        public ICommand NavigateToFightSubview { get; }
        public ICommand NavigateToPokemonsSubview { get; }
        public ICommand NavigateToLoginRegisterView { get; }
        public ICommand NavigateToSettingsView { get; }

        private void LoadUsernameFromDatabase()
        {
            try
            {
                var app = (App)Application.Current;
                if (app.CurrentPlayer != null)
                {
                    var username = app.CurrentPlayer.Username;
                    Username = username ?? "Unknown Player"; // Si le nom est null, afficher "Unknown Player"
                }
                else
                {
                    Username = "Unknown Player 2";
                }
            }
            catch (Exception ex)
            {
                // Gérer les erreurs éventuelles (par exemple, problème de connexion à la base)
                MessageBox.Show("Erreur lors du chargement du nom d'utilisateur");
                MessageBox.Show(ex.Message);
                Username = "Error loading user";
                // Vous pouvez également logger l'erreur si nécessaire
            }
        }
    }
}
