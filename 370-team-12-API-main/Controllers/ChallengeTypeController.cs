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
    public class ChallengeTypesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Challenger> _userManager;

        public ChallengeTypesController(AppDbContext context, UserManager<Challenger> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "CHALLENGER,REWARDARCHITECT,ADMIN,SUPERARCHITECT")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChallengeType>>> GetChallengeTypes()
        {
            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
            var user = await _userManager.FindByIdAsync(userId);

            var auditTrail = new AuditTrail
            {
                UserId = user.Name + " " + user.Surname, // Replace with the actual user ID
                Action = "Challenge types Viewed",
                Timestamp = DateTime.Now,
                Amount = 0,
                Quantity = 0

            };

            _context.AuditTrails.Add(auditTrail);
            _context.SaveChanges();
            return await _context.ChallengeTypes.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ChallengeType>> GetChallengeType(int id)
        {
            var challengeType = await _context.ChallengeTypes.FindAsync(id);

            if (challengeType == null)
            {
                return NotFound();
            }

            return challengeType;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [HttpPost]
        public async Task<ActionResult<ChallengeType>> CreateChallengeType(ChallengeTypeDto challengeTypeDto)
        {
            var challengeType = new ChallengeType
            {
                Name = challengeTypeDto.name,

            };
            _context.ChallengeTypes.Add(challengeType);
            await _context.SaveChangesAsync();


            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
            var user = await _userManager.FindByIdAsync(userId);

            var auditTrail = new AuditTrail
            {
                UserId = user.Name + " " + user.Surname, // Replace with the actual user ID
                Action = "Challenge types" + challengeTypeDto.name + " Created",
                Timestamp = DateTime.Now,
                Amount = 0,
                Quantity = 0

            };

            _context.AuditTrails.Add(auditTrail);
            _context.SaveChanges();


            return CreatedAtAction(nameof(GetChallengeType), new { id = challengeType.ChallengeTypeID }, challengeType);
        }


        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChallengeType([FromRoute]int id, [FromBody]ChallengeTypeDto challengeTypeDto)
        {
           
            var challengeType = _context.ChallengeTypes.Find(id);

           challengeType.Name = challengeTypeDto.name;



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChallengeTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
            var user = await _userManager.FindByIdAsync(userId);

            var auditTrail = new AuditTrail
            {
                UserId = user.Name + " " + user.Surname, // Replace with the actual user ID
                Action = "Challenge types" + challengeTypeDto.name + " Updated",
                Timestamp = DateTime.Now,
                Amount = 0,
                Quantity = 0

            };

            _context.AuditTrails.Add(auditTrail);
            _context.SaveChanges();

            return NoContent();
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChallengeType(int id)
        {
            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
            var user = await _userManager.FindByIdAsync(userId);
            //get challenge type
            var challengeType = await _context.ChallengeTypes.FindAsync(id);

            if (challengeType == null)
            {
                return NotFound();
            }
            //deletion rules
            var challenges = await _context.Challenges.Where(x => x.ChallengeTypeID == challengeType.ChallengeTypeID).Where(x=> x.IsArchived == false).ToListAsync();
            if(challenges.Count() > 0)
            {
                return BadRequest(new { message = "Cannot delete Challenge Type with active challenges!" });
            }

            var challengeTypeMedals = await _context.Medals.Where(x => x.ChallengeTypeId == challengeType.ChallengeTypeID).ToListAsync();
            if(challengeTypeMedals.Count >0)
            {
                return BadRequest(new { message = "Please delete all medals before deleting this challenge type!" });
            }

            _context.ChallengeTypes.Remove(challengeType);
            await _context.SaveChangesAsync();




            var auditTrail = new AuditTrail
            {
                UserId = user.Name + " " + user.Surname, // Replace with the actual user ID
                Action = "Challenge types" + challengeType.Name + " Created",
                Timestamp = DateTime.Now,
                Amount = 0,
                Quantity = 0

            };

            _context.AuditTrails.Add(auditTrail);
            _context.SaveChanges();

            return NoContent();
        }

        private bool ChallengeTypeExists(int id)
        {
            return _context.ChallengeTypes.Any(e => e.ChallengeTypeID == id);
        }
    }
}
