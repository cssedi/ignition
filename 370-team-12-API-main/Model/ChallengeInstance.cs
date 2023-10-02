using Ignition_Mik_55_510.Model;
using System.ComponentModel.DataAnnotations;

namespace BMWIgnition_API.Model
{
    public class ChallengeInstance
    {
        
        public int ChallengeInstanceId { get; set; }
        public string ChallengerId { get; set; }
        public int ChallengeID { get; set; }
        public string? Submition { get; set; }
        // Foreign Key
        public int ChallengeInstanceStatusId { get; set; }

        // Navigation properties
        public Challenge Challenge { get; set; }
        public Challenger Challenger { get; set; }

        public ChallengeInstanceStatus ChallengeInstanceStatus { get; set; }
    }
}
