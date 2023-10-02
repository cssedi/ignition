using BMWIgnition_API.Model;

namespace Ignition_Mik_55_510.Model
{
    public class ChallengeInstanceStatus
    {
        public int ChallengeInstanceStatusId { get; set; }
        public string Name { get; set; }

        //public ChallengeInstance ChallengeInstance { get; set; }
        public ICollection<ChallengeInstance> ChallengeInstances { get; set; }
    }
}
