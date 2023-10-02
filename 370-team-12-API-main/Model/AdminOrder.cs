namespace BMWIgnition_API.Model
{
    public class AdminOrder
    {

        public int PrizeOrderId { get; set; }
        public int SupplierOrderId { get; set; }

        public ICollection<PrizeOrder> PrizeOrder { get; set; }
        public SupplierOrder SupplierOrder { get; set; }
    }
}
