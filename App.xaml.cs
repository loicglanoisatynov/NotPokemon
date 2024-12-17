using System.Windows;
using Microsoft.Data.SqlClient;
using PokeWish.Classes;
using PokeWish.MVVM.Services;

namespace PokeWish
{
    public partial class App : Application
    {

        public int? CurrentPlayerId { get; set; }
        // Connexion SQL actuelle (gérée dynamiquement)
        public SqlConnection? CurrentConnection { get; private set; }

        // Instance de DbContext actuelle (gérée par DbService)
        public AppDbContext? DbContext { get; private set; }

        // Objet Player pour l'utilisateur connecté
        public Login? CurrentPlayer { get; set; }



        // Service pour la gestion de la musique
        public static MusicService MusicService { get; } = new MusicService();

        /// <summary>
        /// Charge la base de données et initialise DbContext via DbService.
        /// </summary>
        /// <param name="dataSource">Adresse du serveur SQL.</param>
        /// <param name="initialCatalog">Nom de la base de données.</param>
        /// <returns>True si la connexion est réussie, sinon False.</returns>
        public bool LoadDatabase(string dataSource, string initialCatalog)
        {
            // Appel au service DbService pour ouvrir la connexion et créer DbContext
            var connection = DbService.OpenDb(CurrentConnection, dataSource, initialCatalog);

            if (connection != null)
            {
                CurrentConnection = connection;
                return true;
            }

            MessageBox.Show("Échec de la connexion à la base de données.");
            return false;
        }

        /// <summary>
        /// Libère les ressources à la fermeture de l'application.
        /// </summary>
        protected override void OnExit(ExitEventArgs e)
        {
            // Ferme la connexion SQL si elle est ouverte
            CurrentConnection?.Close();
            CurrentConnection?.Dispose();

            base.OnExit(e);
        }

        public void SetDbContext(AppDbContext? newDbContext)
        {
            // Libérer les ressources de l'ancien DbContext s'il existe
            DbContext?.Dispose();

            // Assigner le nouveau DbContext
            DbContext = newDbContext;
        }
    }
}

