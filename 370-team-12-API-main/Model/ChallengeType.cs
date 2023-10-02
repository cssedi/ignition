using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMWIgnition_API.Model
{
    public class ChallengeType
    {
        //Props --//
        public int ChallengeTypeID { get; set; }
        public string Name { get; set; }

        //Rels --//
        //Challenge type is related to:
        //Challenge. And only challenge. The foreign key for Challenge type is found in challenge entity.

        //A challenge type can be realted to many different challenges, hence an ICollection of challenges.
        public ICollection<Challenge> challenges { get; set; }
        public ICollection<Medal> medals { get; set; }
    }
}
