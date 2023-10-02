using System.ComponentModel.DataAnnotations;

namespace BMWIgnition_API.Controllers
{
    public class UpdateUserRoleVM
    {

        [Required]
        public string UserId { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
