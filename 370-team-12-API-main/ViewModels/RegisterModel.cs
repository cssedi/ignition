namespace BMWIgnition_API.ViewModels
{
  public class RegisterModel
  {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Bio { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Surname { get; set; }
        public int DepartmentId { get; set; }
    }
}
