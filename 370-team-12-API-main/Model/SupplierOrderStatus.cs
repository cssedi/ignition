using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace BMWIgnition_API.Model
{
    public class SupplierOrderStatus
    {
        [Key]
        public int SupplierOrderStatusId { get; set; }

        [Required]
        public string Name { get; set; }


        public ICollection<SupplierOrder> SupplierOrders { get;}
    }
}
