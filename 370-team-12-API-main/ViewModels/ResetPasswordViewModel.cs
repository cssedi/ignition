using System.ComponentModel.DataAnnotations;

namespace AuthenticationAndAutherazation.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

}
