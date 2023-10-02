namespace BMWIgnition_API.Model
{
    public class Comment
    {
        public int CommentID { get; set; }
        public string Text { get; set; }
        //Foreign Keys
        public int PostID { get; set; }
        public string ChallengerId { get; set; }
        //Navigation properties
        public Post Post { get; set; }
        public Challenger Challenger { get; set; }

    }
}
