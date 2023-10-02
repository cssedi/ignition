using System.ComponentModel.DataAnnotations;

namespace BMWIgnition_API.ViewModels
{
    public class MedalViewModel
    {
        [Required]
        public string MedalName { get; set; }
        [Required]
        public string ImageString { get; set; }
        [Required]
        public int ChallengeTypeId { get; set; }
    }
}
