namespace BMWIgnition_API.Model
{
    public class ProfilePicture
    {
        public int ProfilePictureID { get; set; }
        public string PictureURl { get; set; }
        //Foreign Key
        public int UserID { get; set; }
        //Navigation 
        public User User { get; set; }
    }
}
