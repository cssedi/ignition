using System.ComponentModel.DataAnnotations;

namespace BMWIgnition_API.Model
{
    public class RewardRedemption
    {
        [Key]
       public int RewardRedemptionId { get; set; }
       public string QRCode { get; set; }
       public int RewardRedemptionStatusId { get; set; }

       public ICollection<PrizeOrder> PrizeOrders { get; set; }
       public RewardRedemptionStatus RewardRedemptionStatus { get; set; }

    }
}
