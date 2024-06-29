using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kt12.Models
{
    public class UserProfile
    {
        [ForeignKey("User")]
        public int UserProfileId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        // Foreign key
        public int UserId { get; set; }

        // Navigation property
        public User User { get; set; }
    }
}
