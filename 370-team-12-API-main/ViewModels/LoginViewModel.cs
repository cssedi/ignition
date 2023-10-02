using System.ComponentModel.DataAnnotations;

namespace BMWIgnition_API.ViewModels
{
  public class LoginViewModel
  {
    [Required]
    public string Email { get;  set; }
    [Required]
    public string Password { get;  set; }
  }
}
