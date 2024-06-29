using System.ComponentModel.DataAnnotations;

namespace kt8.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]{3,}$", ErrorMessage = "Имя пользователя должно содержать не менее 3 символов и только буквы.")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d).{8,}$", ErrorMessage = "Пароль должен содержать не менее 8 символов, включая буквы и цифры.")]
        public string Password { get; set; }
    }
}
