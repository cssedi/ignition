using Org.BouncyCastle.Bcpg.OpenPgp;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace BMWIgnition_API.Model
{
    public class PrizeOrder
    {
        [Key]
        public int PrizeOrderId { get; set; }
        public int PrizeId { get; set; }
        public string ChallengerId { get; set; }
        public DateTime DatePlaced { get; set; }
        public int PrizeOrderStatusId { get; set; }
        public int? SupplierOrderId { get; set; }

        public Prize Prize { get; set; }
        public Challenger Challenger { get; set; }
        public PrizeOrderStatus PrizeOrderStatus { get; set; }
        public SupplierOrder SupplierOrder { get; set; }

    }
}