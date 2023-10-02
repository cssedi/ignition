using System.ComponentModel.DataAnnotations;

namespace BMWIgnition_API.Model
{
    public class PrizeOrderStatus
    {
        [Key]
        public int PrizeOrderStatusId { get; set; }
        public string Status { get; set;  }


        public ICollection<PrizeOrder> PrizeOrders { get; set; }
    }
}
