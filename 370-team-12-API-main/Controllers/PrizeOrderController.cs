using BMWIgnition_API.Data;
using BMWIgnition_API.Model;
using BMWIgnition_API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace BMWIgnition_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrizeOrderController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Challenger> _userManager;

        public PrizeOrderController(AppDbContext context, IConfiguration configuration, UserManager<Challenger> userManager)

        {
            _configuration = configuration;
            _context = context;
            _userManager = userManager;
        }

        // Seed Prize Orders EndPoint 
        [HttpGet("SeedPrizeOrders")]
        public async Task<ActionResult> SeedPrizeOrders()
        {
            try
            {
                var users = _userManager.Users.ToList();
                var prizes = _context.Prizes.ToList();
                Random random= new Random();
                Random randomUsereGen = new Random();
                DateTime dateTime= new DateTime();
                
                for(int i= 0; i < prizes.Count(); i++)
                {
                    for(int x = 0; x < random.Next(10); i++ )
                    {
                        PrizeOrder prizeOrder = new PrizeOrder
                        {
                            ChallengerId = users[random.Next(0, users.Count())].Id,
                            PrizeId = prizes[random.Next(prizes.Count())].PrizeID,
                            DatePlaced = DateTime.Now,
                            PrizeOrderStatusId = 1
                            
                        };

                        _context.PrizeOrders.Add(prizeOrder);

                    }
                }

                await _context.SaveChangesAsync();
                
                return Ok(new {Message = "Prize Orders have been seed sucessfully",});
            }
            catch (Exception ex)
            {

                return BadRequest(new {Message = ex.Message});
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "CHALLENGER")]
        [HttpPost("CreatePrizeOrder")]
        public async Task<IActionResult> CreatePrizeOrder(List<PrizeOrderVM> prizeOrderVM)
        {
            try
            {
                var httppUser = HttpContext.User;
                var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id   
                var user = _context.Challengers.FirstOrDefault(x => x.Id == userId);
                int totalPrice = 0;
                foreach (var vm in prizeOrderVM)
                {
                    //get prize
                    var prize = await _context.Prizes.Where(x => x.PrizeID == vm.PrizeId).FirstAsync();
                    //get total cost of order
                    totalPrice += prize.Price;
                    //check if user has enough tokens to place order
                    if(user.Tokens < totalPrice)
                    {
                        return BadRequest(new { Message = "You do not have enough tokens to place this order!" });
                    }
                    else
                    {
                        var prizeOrder = new PrizeOrder
                        {
                            ChallengerId = userId,
                            PrizeId = vm.PrizeId,
                            PrizeOrderStatusId = 1,
                            DatePlaced = DateTime.Now,
                        };
                        _context.PrizeOrders.Add(prizeOrder);

                        var auditTrail = new AuditTrail
                        {
                            UserId = user.Name + " " + user.Surname,
                            Action = "Order placed",
                            Timestamp = DateTime.Now,
                            Amount = prize.Price,
                            Quantity = 1

                        };
                       await _context.AuditTrails.AddAsync(auditTrail);
                       await _context.SaveChangesAsync();

                    }

                }

                //sutract tokens from user
                user.Tokens -= totalPrice;
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Order Placed Successfully", newTokens = user.Tokens });

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetOrdersAdmin")]
        public  ActionResult GetOrdersAdmin()
        {
            try
            {
                var res = _context.PrizeOrders.Include(x => x.Prize).Include(x => x.Challenger).Include(x => x.PrizeOrderStatus).Where( po => po.PrizeOrderStatusId == 1 ).ToList();
               

                return Ok(res);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var orders = await _context.PrizeOrders.ToListAsync();
                var prizes = await _context.Prizes.ToListAsync();
                var res = new List<object>();
                foreach (var order in orders)
                {
                    for (int i = 0; prizes.Count > i; i++)
                    {
                        if (order.PrizeId == prizes[i].PrizeID)
                        {
                            res.Add(new { prize = prizes[1], });
                        }
                    }
                }

                return Ok(res);

            }

            catch (Exception)
            {
                throw;
            }
        }

      
    }
}
