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
    [Route("api/[controller]")]
    [ApiController]
    public class MedalController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<Challenger> _userManager;

        public MedalController(AppDbContext dbContext, UserManager<Challenger> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        // GET: api/Medals
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "REWARDARCHITECT,ADMIN,SUPERARCHITECT")]
        [HttpGet]
        public async Task<IEnumerable<Medal>> Get()
        {
            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
            var user = await _userManager.FindByIdAsync(userId);

            var auditTrail = new AuditTrail
            {
                UserId = user.Name + " " + user.Surname, // Replace with the actual user ID
                Action = "Medals Viewed",
                Timestamp = DateTime.Now,
                Amount = 0,
                Quantity = 0

            };

           await _dbContext.AuditTrails.AddAsync(auditTrail);
           await _dbContext.SaveChangesAsync();
            return await _dbContext.Medals.Include(ct=> ct.ChallengeType).ToListAsync();
        }

        // GET: api/Medals/5
        [HttpGet("GetMedalById/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var medal = await _dbContext.Medals.FindAsync(id);

            if (medal == null)
                return NotFound();

            return Ok(medal);
        }

        // POST: api/Medals
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [HttpPost]

        public async Task<ActionResult<Medal>> Post([FromBody] MedalViewModel medalVM)
        {
            var medal = new Medal
            {
                MedalName = medalVM.MedalName,
                ImageString = medalVM.ImageString,
                ChallengeTypeId = medalVM.ChallengeTypeId
            }; 
            await _dbContext.Medals.AddAsync(medal);
            await _dbContext.SaveChangesAsync();

            var name = medalVM.MedalName;

            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
            var user = await _userManager.FindByIdAsync(userId);

            var auditTrail = new AuditTrail
            {
                UserId = user.Name + " " + user.Surname, 
                Action = "Medal " + name + " Created",
                Timestamp = DateTime.Now,
                Amount = 0,
                Quantity = 2

            };
            await _dbContext.AuditTrails.AddAsync(auditTrail);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = medal.MedalId }, medal);
        }

        // PUT: api/Medals/5
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MedalViewModel medalVM)
        {
            var medal = await _dbContext.Medals.FindAsync(id);

            if(id != medal.MedalId)
            {
                return BadRequest();
            }
            
            medal.MedalName = medalVM.MedalName;
            medal.ImageString = medalVM.ImageString;
            medal.ChallengeTypeId = medalVM.ChallengeTypeId;

            await _dbContext.SaveChangesAsync();

            

            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
            var user = await _userManager.FindByIdAsync(userId);

            var auditTrail = new AuditTrail
            {
                UserId = user.Name + " " + user.Surname, 
                Timestamp = DateTime.Now,
                Action = "Medal " + medalVM.MedalName + " Updated",
                Amount = 0,
                Quantity = 0

            };
            await _dbContext.AuditTrails.AddAsync(auditTrail);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Medals/5
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);

            var medal = await _dbContext.Medals.FindAsync(id);

            if (medal == null)
                return NotFound();
            //deletion rules
            var challenges = await _dbContext.Challenges.Where(x => x.MedalId == medal.MedalId).Where(x => x.IsArchived == false).ToListAsync();
            if (challenges.Count() > 0)
            {
                return BadRequest(new { message = "Cannot delete Medal with active challenges!" });
            }
            _dbContext.Medals.Remove(medal);
            await _dbContext.SaveChangesAsync();

            //delete all challenger medals
            var challengerMedals = await _dbContext.ChallengerMedals.Where(cm => cm.MedalId == medal.MedalId).ToListAsync();
            //delete all challenger medals
            _dbContext.ChallengerMedals.RemoveRange(challengerMedals);
            await _dbContext.SaveChangesAsync();
            



            var auditTrail = new AuditTrail
            {
                UserId = user.Name + " " + user.Surname, // Replace with the actual user ID
                Action = "Medal" + medal.MedalName + "Deleted",
                Timestamp = DateTime.Now,
                Amount = 0,
                Quantity = 0

            };
            await _dbContext.AuditTrails.AddAsync(auditTrail);
            await _dbContext.SaveChangesAsync();

            return Ok( new { Message = medal.MedalName + " deleted" });
        }

        [HttpGet("GetMedalsByChallengeType/{id}")]
        public async Task<IActionResult> GetMedalsByChallengeType(int id)
        {
            //get medals
            var medals = await _dbContext.Medals.Where(ct => ct.ChallengeTypeId == id).ToListAsync();
            //error handling
            if(medals == null|| medals.Count ==0)
            {
                return NotFound(new {Message = "No medals with this challenge type"});
            }

            return Ok(medals);
        }
    }
}
