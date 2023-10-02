using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BMWIgnition_API.Model
{
    public class Challenge 
    {
        [Key]
        public int ChallengeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime endDate { get; set; }
        public DateTime startDate { get; set; }
        public int? Tokens { get; set; }
        public bool IsArchived { get; set; }
        //Foreign Keys
        public string Id { get; set; }
        public int? ChallengeStatusID { get; set; }
        public int ChallengeTypeID  { get; set; }
        public int MedalId { get; set; }
        public string Image { get; set; }
        public int? PrizeId { get; set; }
        public string? countdown { get; set; }
        //Navigation properties
        public Challenger User { get; set; }
        public ICollection<DepartmentChallenge> DepartmentChallenges { get; set; }
        public ICollection<ChallengeInstance> ChallengeInstances { get; set; }
        public ChallengeType ChallengeType { get; set; }
        public Medal Medal { get; set; }
        public Prize? Prize { get; set; }
        public ChallengeStatus? ChallengeStatus { get; set; }
    }
}
