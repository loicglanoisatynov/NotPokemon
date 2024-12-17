using System.ComponentModel.DataAnnotations;

namespace PokeWish.Classes
{
    public class Login
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Username { get; set; }

        [Required]
        [MaxLength(255)]
        public required string PasswordHash { get; set; }

    }
}
