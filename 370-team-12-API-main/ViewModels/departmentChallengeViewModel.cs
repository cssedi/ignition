using BMWIgnition_API.Model;

namespace BMWIgnition_API.ViewModels
{
    public class departmentChallengeViewModel
    {
        public string DepartmentCode { get; set; }
        public int Count { get; set; }
        public Department department { get; set; }
    }
}
