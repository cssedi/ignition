using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMWIgnition_API.Model
{
    public class Prize
    {
        [Key]
        public int PrizeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FrontImgURL { get; set; }
        public string BackImgURL { get; set; }
        public int Price { get; set; }


        //relationships
        public int PrizeTypeID { get; set; }
        public PrizeType prizeType { get; set; }
        public ICollection<Challenge> Challenges { get; set; }
        public ICollection<PrizeOrder> PrizeOrders { get; set; }
    }
}