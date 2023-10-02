using Microsoft.AspNetCore.Identity;

namespace BMWIgnition_API.Model
{
    public class User : IdentityUser
  {
        public string FirstName { get; set; }
        public string LastSurname { get; set; }
        
        public DateTime Birthdate { get; set; }


    /*  //Navigation properties
      public Challenger Challenger { get; set; }
      public RewardsArchitect RewardsArchitect { get; set; }
      public SuperArchitect SuperArchitect { get; set; }
      public Administrator Administrator { get; set; }*/
   // public SuperArchitect SuperArchitect { get; set; }
   /* public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }*/
   
  }
}
