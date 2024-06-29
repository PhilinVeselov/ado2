using System.ComponentModel.DataAnnotations;
using kt12.Models;

namespace kt12.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // Navigation property
        public UserProfile UserProfile { get; set; }
    }
}
