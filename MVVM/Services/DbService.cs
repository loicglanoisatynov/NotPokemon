using System.Windows;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PokeWish.Classes;

namespace PokeWish.MVVM.Services
{
    public static class DbService
    {
        /// <summary>
        /// Ouvre une connexion � la base de donn�es et initialise DbContext.
        /// </summary>
        /// <param name="prevConn">Connexion SQL pr�c�dente.</param>
        /// <param name="dataSource">Adresse du serveur SQL.</param>
        /// <param name="initialCatalog">Nom de la base de donn�es.</param>
        /// <returns>Nouvelle connexion SQL ou null en cas d'�chec.</returns>
        public static SqlConnection? OpenDb(SqlConnection? prevConn, string dataSource, string initialCatalog)
        {
            // Fermer l'ancienne connexion si elle existe
            prevConn?.Close();

            // Construire la cha�ne de connexion
            var connectionString = $"Data Source={dataSource};Initial Catalog={initialCatalog};Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

            try
            {
                // Cr�er et ouvrir une nouvelle connexion SQL
                var newConnection = new SqlConnection(connectionString);
                newConnection.Open();

                // Cr�er une nouvelle instance de DbContext avec les options
                var options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseSqlServer(connectionString)
                    .Options;

                var newDbContext = new AppDbContext(options);

                // Utiliser une m�thode publique pour d�finir le DbContext
                ((App)Application.Current).SetDbContext(newDbContext);

                return newConnection; // Retourne la nouvelle connexion
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Erreur SQL : " + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Erreur d'op�ration invalide : " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur inconnue : " + ex.Message);
            }

            return null; // En cas d'�chec
        }
    }
}
