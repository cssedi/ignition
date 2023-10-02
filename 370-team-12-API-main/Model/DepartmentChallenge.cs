namespace BMWIgnition_API.Model
{
    public class DepartmentChallenge
    {
        public int ChallengeID { get; set; }
        public int DepartmentId { get; set; }

        public Challenge Challenge { get; set; }
        public Department Department { get; set; }
    }
}
