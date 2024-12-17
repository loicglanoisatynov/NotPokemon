using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;
using System.Windows;
using Microsoft.EntityFrameworkCore;


namespace PokeWish.Classes
{
    public class Player
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;

        public int? LoginID { get; set; }
        public Login? Login { get; set; }

        public ICollection<PlayerMonster> PlayerMonsters { get; set; } = new List<PlayerMonster>();

        public int GetNextAvailablePlayerID()
        {
            // Renvoie le prochain ID disponible dans la table Player de la base de données.
            using (var context = ((App)Application.Current).DbContext)
            {
                var connection = context.Database.GetDbConnection();
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT MAX(ID) FROM Player";
                var result = command.ExecuteScalar();
                connection.Close();
                return result == null ? 1 : (int)result + 1;
            }
        }

        public Player(string name)
        {
            Name = name;
            //ID = GetNextAvailablePlayerID();
            //LoginID = loginID;

        }
    }
}