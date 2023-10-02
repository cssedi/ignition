using System.ComponentModel.DataAnnotations;

namespace BMWIgnition_API.Model
{
    public class Department
    
    {
        [Key]
        public int DepartmentId { get; set; }

        //RewardArchitct Id pu here 
        public string DepartmentCode { get; set; }
        public string Name { get; set; }
        //Foreign Keys
        public int FunctionId { get; set; }
        public string? Id { get; set; }
        //Navigation Properties
        public Function Function { get; set; }
        public ICollection<Challenger> Challengers { get; set; }
        public Challenger? AwardsArchitect { get; set; }
        //public ICollection<RewardsArchitect> RewardsArchitects { get; set; }
        public ICollection<DepartmentChallenge> Challenges { get; set; }

    }
}
