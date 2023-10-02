namespace BMWIgnition_API.Model
{
    public class ChallengeStatus
    {
        public int ChallengeStatusID { get; set; }
        public string Name { get; set; }

        public ICollection<Challenge> Challenges { get; set; }
    }
}
