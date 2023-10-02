using BMWIgnition_API.Data;
using BMWIgnition_API.Model;
using BMWIgnition_API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BMWIgnition_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierOrderController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public SupplierOrderController(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        [HttpGet("GellSupplierOrder")]
        public IActionResult Get()
        {
            try
            {
               var res = _appDbContext.SupplierOrders.Include( x=> x.SupplierOrderStatus).Include(x => x.PrizeOrders).ThenInclude(x => x.Prize).ToList();
                return Ok(res);
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpPost("CreateSupplierOrder")]
        public async Task<ActionResult> CreateSupplierOrder(List<PrizeOrderVM> prizeOrders)
        {
            try
            {
                // Create Supplier Order 
                SupplierOrder supplierOrder = new SupplierOrder
                {
                    SupplierOrderStatusId = 1,
                    DatePlaced = DateTime.Now,

                };

                _appDbContext.SupplierOrders.Add(supplierOrder);
                await _appDbContext.SaveChangesAsync();

                 
                // Updae the Statues of PrizeOrders
                
                for(int i =0; i < prizeOrders.Count(); i++)
                {
                    var prize = _appDbContext.PrizeOrders.Find(prizeOrders[i].PrizeId);
                    prize.SupplierOrderId = supplierOrder.SupplierOrderId;
                    prize.PrizeOrderStatusId = 2;
                    await _appDbContext.SaveChangesAsync();

                }

                return Ok(new {Message = "supplier Order Succesfully Places"});
            }
            catch (Exception ex)
            {

                return BadRequest(new {Message = ex.Message});
            }
        }
    }
}
