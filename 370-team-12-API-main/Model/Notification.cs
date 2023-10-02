
using System.ComponentModel.DataAnnotations;

namespace BMWIgnition_API.Model
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }
        public string ChallengerId { get; set; }
        public string From { get; set; }  
        public string Message { get; set; }

    }
}
