using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareAPlate.Models
{
    public class User
    {
        // Auto-incremented primary key
        [Key] // Marks Id as the primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Enables auto-increment
        public int Id { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Location { get; set; }
        public long Number { get; set; }

        // Default constructor
        public User() { }

        // Constructor with parameters
        public User(string userFirstName, string userLastName, string email, string password, string location, long number)
        {   
            UserFirstName = userFirstName;
            UserLastName = userLastName;
            Email = email;
            Password = password;
            Location = location;
            Number = number;
        }
    }
}
