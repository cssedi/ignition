using System.ComponentModel.DataAnnotations;

namespace BMWIgnition_API.ViewModels
{
  public class ChallengeStatusViewModel
  {
    [Required]
    public string Name { get; set; }
  }
}
