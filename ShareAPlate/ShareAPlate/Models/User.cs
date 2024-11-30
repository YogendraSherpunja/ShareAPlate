using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareAPlate.Models
{
    [Table("Users")]
    public class User
    {
        // Auto-incremented primary key
        [Key] // Marks Id as the primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Enables auto-increment
        public int Id { get; set; }

        [Required]
        public string UserFirstName { get; set; }
        [Required]
        public string UserLastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(8)]
        public string Password { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public long Number { get; set; }

        // Default constructor
        public User() { }

        // Constructor with parameters
        public User(string userFirstName, string userLastName, string email, string password, string location, int number)
        {   
            UserFirstName = userFirstName;
            UserLastName = userLastName;
            Email = email;
            Password = password;
            Location = location;
            Number = number;
        }
        public override string ToString()
        {
            return $"UserId: {Id}, First Name: {UserFirstName}, Email: {Email}";
        }
    }
}
