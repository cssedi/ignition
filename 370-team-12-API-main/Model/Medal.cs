namespace BMWIgnition_API.Model
{
    public class Medal
    {
        public int MedalId { get; set; }

        public string MedalName { get; set; }

        public string ImageString { get; set; }

        public int ChallengeTypeId { get; set; }

        public ICollection<Challenge> Challenges { get; set; }
        public ChallengeType ChallengeType { get; set; }
        public ICollection<ChallengerMedal> ChallengerMedals { get; set; }
    }
}
