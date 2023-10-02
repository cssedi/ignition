using System.ComponentModel.DataAnnotations;

namespace BMWIgnition_API.Model
{
    public class Like
    {
        // Composite Keys 
        [Key]
        public int PostId { get; set; }
        [Key]
        public string ChallengerId { get; set; }
        //Navigation properties
        public Post Post { get; set; }
        public Challenger Challenger { get; set; }
    }
}
