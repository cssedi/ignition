namespace BMWIgnition_API.Model
{
    public class Post
    {
        // Properties 
        public string Text { get; set; }


        public DateTime Date { get; set; } = DateTime.Now.Date;
       
        //Foreign Keys
  
        public string ChallengerId { get; set; }
        public int PostID { get; set; }

        // Navigation Porperties 
        public ICollection<Like> Likes { get; set; }
        /// </summary>
        //Navigation Properties
        public ICollection<Comment> Comments { get; set; }
        public Challenger Challenger { get; set; }
    }
}
