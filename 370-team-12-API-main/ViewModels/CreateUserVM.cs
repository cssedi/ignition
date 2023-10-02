namespace BMWIgnition_API.ViewModels
{
    public class CreateUserVM
    {
       
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string Role { get; set; }
        public int? DepartmentId { get; set; }
        public int? FunctionId { get; set; }

    }
}
