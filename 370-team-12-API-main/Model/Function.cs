using System.ComponentModel.DataAnnotations;

namespace BMWIgnition_API.Model
{
    public class Function
    {
        [Key]
        public int FunctionId { get; set; }
        public string FunctionCode { get; set; }
        public string Name { get; set; }
        // Foreing Keys
        public string Id { get; set; }
        // Navigation Properties
        public ICollection<Department> Departments { get; set; }
        public Challenger SuperArchitect { get; set; }
    }
}
