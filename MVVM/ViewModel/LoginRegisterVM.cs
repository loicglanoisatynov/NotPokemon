using CommunityToolkit.Mvvm.Input;
using PokeWish.Classes;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using PokeWish;
using PokeWish.MVVM.View;

namespace PokeWish.MVVM.ViewModel
{
    public class LoginRegisterVM
    {
        public IRelayCommand LoginCommand { get; }
        public IRelayCommand RegisterCommand { get; }

        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public LoginRegisterVM()
        {
            //_dbContext = App.DbContext ?? throw new System.NullReferenceException("Database context is not initialized.");

            LoginCommand = new RelayCommand(OnLogin);
            RegisterCommand = new RelayCommand(OnRegister);
        }

        private void OnLogin()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Veuillez remplir tous les champs.");
                return;
            }

            // Hash le mot de passe pour comparaison
            string hashedPassword = HashPassword(Password);

            // Vérification dans la DB
            var user = ((App)Application.Current).DbContext?.Login?.FirstOrDefault(u => u.Username == Username && u.PasswordHash == hashedPassword);

            if (user != null)
            {
                MessageBox.Show("Connexion réussie !");

                ((App)Application.Current).CurrentPlayerId = user.ID;
                ((App)Application.Current).CurrentPlayer = user;


                RedirectToMainView();
            }
            else
            {
                MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.");
            }
        }

        private void OnRegister()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Veuillez remplir tous les champs.");
                return;
            }

            // Vérifier si l'utilisateur existe déjà
            if (((App)Application.Current).DbContext.Login?.Any(u => u.Username == Username) == true)
            {
                MessageBox.Show("Cet utilisateur existe déjà.");
                return;
            }

            // Création d'un nouvel utilisateur
            var newUser = new Login
            {
                Username = Username,
                PasswordHash = HashPassword(Password)
            };

            
            
            ((App)Application.Current).DbContext?.Login?.Add(newUser);
            ((App)Application.Current).DbContext?.SaveChanges();

            MessageBox.Show("Inscription réussie !");

            ((App)Application.Current).CurrentPlayerId = newUser.ID;
            ((App)Application.Current).CurrentPlayer = newUser;


            RedirectToMainView();
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        private void RedirectToMainView()
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainContent.Content = new MainView();
            }
        }


    }
}
