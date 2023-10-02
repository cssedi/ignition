using Microsoft.AspNetCore.Identity;
using System.Collections;

namespace BMWIgnition_API.Model
{
    public class Challenger : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Bio { get; set; }  
        public string ProfilePicture { get; set; }
        // Foreign keys
        public DateTime DateOfBirth { get; set; }
        public int? DepartmentId { get; set; }

        // Navigation Properties
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public ICollection<Like> Likes { get; set; }
        public int Tokens { get; set; }

        // Public SuperArchitect SuperArchitect { get; set; }
        public Department Department { get; set; }
        public Challenger AwardsArchitect { get; set; }
        public ICollection<Challenge> Challenges { get; set; }
        public ICollection<ChallengeInstance> ChallengeInstances { get; set; }
        public ICollection<ChallengerMedal> ChallengerMedals { get; set; }
        public ICollection<PrizeOrder> PrizeOrders { get; set; }

    }
}
