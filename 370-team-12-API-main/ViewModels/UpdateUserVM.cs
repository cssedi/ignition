namespace BMWIgnition_API.ViewModels
{
    public class UpdateUserVM
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string Surname { get; set; }
        public int DepartmentID { get; set; }
    }
}
