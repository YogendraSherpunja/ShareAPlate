using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShareAPlate.Models
{
    public class DonorModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DonorId { get; set; }

        [Required]
        public string DonorName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FoodDetails { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public DateTime AvailableUntil { get; set; }

        public DonorModel() { }

        public DonorModel(int donorId, string donorName, string email, string foodDetails, string location, DateTime availableUntil)
        {
            DonorId = donorId;
            DonorName = donorName;
            Email = email;
            FoodDetails = foodDetails;
            Location = location;
            AvailableUntil = availableUntil;
        }
    }
}
