using BMWIgnition_API.Data;
using BMWIgnition_API.Model;
using BMWIgnition_API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BMWIgnition_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrizeCategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Challenger> _userManager;


        public PrizeCategoryController(AppDbContext context, UserManager<Challenger> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/PrizeCategory
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "CHALLENGER,REWARDARCHITECT,ADMIN,SUPERARCHITECT")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrizeCategory>>> GetPrizeCategories()
        {
            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
            var user = await _userManager.FindByIdAsync(userId);

            var auditTrail = new AuditTrail
            {
                UserId = user.Name + " " + user.Surname, // Replace with the actual user ID
                Action = "Rewards Categories Viewed",
                Timestamp = DateTime.Now,
                Amount = 0,
                Quantity = 0

            };

            return await _context.PrizeCategories.ToListAsync();
        }

        // GET: api/PrizeCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PrizeCategory>> GetPrizeCategory(int id)
        {
            var prizeCategory = await _context.PrizeCategories.FindAsync(id);

            if (prizeCategory == null)
            {
                return NotFound();
            }

            return prizeCategory;
        }

        // POST: api/PrizeCategory
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [HttpPost]
        public async Task<ActionResult<PrizeCategory>> CreatePrizeType(PrizeCategoryDto prizeCategoryDto)
        {
            var prizeCategory = new PrizeType
            {
                Name = prizeCategoryDto.PrizeCategoryName,
                PrizeCategoryID = 1
            };
            _context.PrizeTypes.Add(prizeCategory);
            await _context.SaveChangesAsync();

            var name = prizeCategoryDto.PrizeCategoryName;
            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
            var user = await _userManager.FindByIdAsync(userId);

            var auditTrail = new AuditTrail
            {
                UserId = user.Name + " " + user.Surname, // Replace with the actual user ID
                Action = "Reward Category " + name + " Created",
                Timestamp = DateTime.Now,
                Amount = 0,
                Quantity = 0

            };

            _context.AuditTrails.Add(auditTrail);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetPrizeCategory), new { id = prizeCategory.PrizeCategoryID }, prizeCategory);
        }

        // PUT: api/PrizeCategory/5
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePrizeCategory([FromRoute]int id, [FromBody]PrizeCategoryDto prizeCategoryDto)
        {
            var prizeCategory = _context.PrizeTypes.Find(id);
            prizeCategory.Name = prizeCategoryDto.PrizeCategoryName;
            prizeCategory.PrizeCategoryID = 1;
            _context.SaveChanges();


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrizeCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            var name = prizeCategoryDto.PrizeCategoryName;
            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
            var user = await _userManager.FindByIdAsync(userId);

            var auditTrail = new AuditTrail
            {
                UserId = user.Name + " " + user.Surname, // Replace with the actual user ID
                Action = "Reward Category " + name + " Updated",
                Timestamp = DateTime.Now,
                Amount = 0,
                Quantity = 2

            };

            _context.AuditTrails.Add(auditTrail);
            _context.SaveChanges();


            return Ok( new { M = "Prize Catergoty updated Succesfully" });
        }

        // DELETE: api/PrizeCategory/5
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrizeCategory(int id)
        {
            var prizeCategory = await _context.PrizeTypes.FindAsync(id);
            if (prizeCategory == null)
            {
                return NotFound();
            }

            _context.PrizeTypes.Remove(prizeCategory);
            await _context.SaveChangesAsync();

            var name = prizeCategory.Name;

            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
            var user = await _userManager.FindByIdAsync(userId);

            var auditTrail = new AuditTrail
            {
                UserId = user.Name + " " + user.Surname, // Replace with the actual user ID
                Action = "Reward Category " + name + " Deleted",
                Timestamp = DateTime.Now,
                Amount = 0,
                Quantity = 2

            };

            _context.AuditTrails.Add(auditTrail);
            _context.SaveChanges();

            return NoContent();
        }

        private bool PrizeCategoryExists(int id)
        {
            return _context.PrizeCategories.Any(e => e.PrizeCategoryID == id);
        }
    }
}

