using System.ComponentModel.DataAnnotations;

namespace BMWIgnition_API.Model
{
    public class ChallengerMedal
    {
       
       public string ChallenegerId { get; set; }
        
        public int MedalId { get; set; }

        public Medal Medal { get; set; }
        public Challenger Challenger { get; set; }

    }
}
