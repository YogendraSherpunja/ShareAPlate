using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ShareAPlate.Models
{
    [Table("IndividualDonation")]
    public class IndividualDonation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DonorId { get; set; }

        [Required]
        public string FoodDetails { get; set; }

        public string? FoodCategory { get; set; } // Optional: Cooked, Raw, Packaged, Drinks

        public string? FoodQuantity { get; set; } // Optional: Quantity or number of servings

        [Required]
        public string Location { get; set; }

        [Required]
        public DateTime AvailableUntil { get; set; }

        public string? FoodPhotoPath { get; set; } // Optional: Path for uploaded food photo

        public string? SpecialNotes { get; set; } // Optional: Dietary notes or instructions

        public bool IsAnonymous { get; set; } = false; // Checkbox for anonymous donation

        // Add the UserId as a foreign key
        [ForeignKey("User")]
        public int UserId { get; set; }

        // Navigation property to reference the User (the creator of the donation)
        [ValidateNever]
        public virtual User User { get; set; }

        // Constructor for initializing the model
        public IndividualDonation() { }

        public IndividualDonation(
            int donorId,
            string foodDetails,
            string? foodCategory,
            string? foodQuantity,
            string location,
            DateTime availableUntil,
            string? foodPhotoPath,
            string? specialNotes,
            bool isAnonymous,
            int userId)
        {
            DonorId = donorId;
            FoodDetails = foodDetails;
            FoodCategory = foodCategory;
            FoodQuantity = foodQuantity;
            Location = location;
            AvailableUntil = availableUntil;
            FoodPhotoPath = foodPhotoPath;
            SpecialNotes = specialNotes;
            IsAnonymous = isAnonymous;
            UserId = userId;
        }
    }
}
