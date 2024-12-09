namespace ShareAPlate.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

    namespace ShareAPlate.Models
    {
        [Table("OrganizationDonation")]
        public class OrganizationDonation
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int DonationId { get; set; }

            // Add the UserId as a foreign key
            [ForeignKey("User")]
            public int UserId { get; set; }

            // Navigation property to reference the User (the creator of the donation)

            [ValidateNever]
            public virtual User User { get; set; }

            [Required]
            public string OrganizationName { get; set; } // Name of the organization

            [Required]
            public string ContactPerson { get; set; } // Name of the contact person

            [Required]
            [EmailAddress]
            public string ContactEmail { get; set; } // Email address for communication

            [Required]
            [Phone]
            public string ContactPhone { get; set; } // Phone number for communication

            public string? Website { get; set; } // Optional: Organization's website

            [Required]
            public string FoodDetails { get; set; } // Description of food items being donated

            public string? FoodCategory { get; set; } // Optional: Cooked, Raw, Packaged, Drinks

            public string? FoodQuantity { get; set; } // Optional: Total quantity or servings

            [Required]
            public string EventLocation { get; set; } // Location of the donation program

            [Required]
            public DateTime EventDate { get; set; } // Date of the donation event

            [Required]
            public DateTime AvailableUntil { get; set; } // Time until the food is available for pickup

            public string? EventPhotoPath { get; set; } // Optional: Path to uploaded photos of the event

            public string? SpecialNotes { get; set; } // Optional: Dietary notes or additional details

            public bool IsRecurring { get; set; } = false; // Indicates if the event is recurring

            public OrganizationDonation() { }

            public OrganizationDonation(
                int donationId,
                string organizationName,
                string contactPerson,
                string contactEmail,
                string contactPhone,
                string? website,
                string foodDetails,
                string? foodCategory,
                string? foodQuantity,
                string eventLocation,
                DateTime eventDate,
                DateTime availableUntil,
                string? eventPhotoPath,
                string? specialNotes,
                bool isRecurring)
            {
                DonationId = donationId;
                OrganizationName = organizationName;
                ContactPerson = contactPerson;
                ContactEmail = contactEmail;
                ContactPhone = contactPhone;
                Website = website;
                FoodDetails = foodDetails;
                FoodCategory = foodCategory;
                FoodQuantity = foodQuantity;
                EventLocation = eventLocation;
                EventDate = eventDate;
                AvailableUntil = availableUntil;
                EventPhotoPath = eventPhotoPath;
                SpecialNotes = specialNotes;
                IsRecurring = isRecurring;
            }
        }
    }

}
