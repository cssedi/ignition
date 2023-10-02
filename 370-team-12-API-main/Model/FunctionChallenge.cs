namespace BMWIgnition_API.Model
{
    public class FunctionChallenge
    {
        public int ChallengeID { get; set; }
        public int FunctionID { get; set; }
        public Challenge Challenge { get; set; }
        public Function Function { get; set; }
    }
}
