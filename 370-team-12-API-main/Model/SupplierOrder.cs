using System.ComponentModel.DataAnnotations;

namespace BMWIgnition_API.Model
{
    public class SupplierOrder
    {
        [Key]
        public int SupplierOrderId { get; set; }

        public DateTime DatePlaced { get; set; }
        public DateTime DateDelivered { get; set; }
        public int SupplierOrderStatusId { get; set; }


        public SupplierOrderStatus SupplierOrderStatus { get; set; }
        public ICollection<PrizeOrder> PrizeOrders { get; set; }

        
    }
}
