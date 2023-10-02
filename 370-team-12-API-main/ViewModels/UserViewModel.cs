namespace BMWIgnition_API.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string ProfilePicture { get; set; }
        public string UserName { get; set; }    
        public string Password { get; set; }  
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public List<string> Roles { get; set; }
    }
}
