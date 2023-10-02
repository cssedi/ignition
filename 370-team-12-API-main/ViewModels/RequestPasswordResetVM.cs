using System.ComponentModel.DataAnnotations;

namespace BMWIgnition_API.ViewModels
{
  public class RequestPasswordResetVM
  {
    [Required]
    [EmailAddress]
    public string email { get; set; }
  }
}
