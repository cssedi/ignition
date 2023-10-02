using BMWIgnition_API.Data;
using BMWIgnition_API.ViewModels;
using Ignition_Mik_55_510.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BMWIgnition_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChallengeInstanceStatusController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ChallengeInstanceStatusController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ChallengeInstanceStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChallengeInstanceStatus>>> GetChallengeInstanceStatuses()
        {
            return await _context.ChallengeInstanceStatus.ToListAsync();
        }

        // GET: api/ChallengeInstanceStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChallengeInstanceStatus>> GetChallengeInstanceStatus(int id)
        {
            var challengeInstanceStatus = await _context.ChallengeInstanceStatus.FindAsync(id);

            if (challengeInstanceStatus == null)
            {
                return NotFound();
            }

            return challengeInstanceStatus;
        }

        // POST: api/ChallengeInstanceStatus
        [HttpPost]
        public async Task<ActionResult> CreateChallengeInstanceStatus(ChallengeTypeDto challengeTypeDto)
        {
            var challengeInstanceStatus = new ChallengeInstanceStatus
            {
                Name = challengeTypeDto.name
            };
            _context.ChallengeInstanceStatus.Add(challengeInstanceStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetChallengeInstanceStatus), new { id = challengeInstanceStatus.ChallengeInstanceStatusId }, challengeInstanceStatus);
        }

        // PUT: api/ChallengeInstanceStatus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChallengeInstanceStatus(int id, ChallengeInstanceStatus challengeInstanceStatus)
        {
            if (id != challengeInstanceStatus.ChallengeInstanceStatusId)
            {
                return BadRequest();
            }

            _context.Entry(challengeInstanceStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChallengeInstanceStatusExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/ChallengeInstanceStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChallengeInstanceStatus(int id)
        {
            var challengeInstanceStatus = await _context.ChallengeInstanceStatus.FindAsync(id);
            if (challengeInstanceStatus == null)
            {
                return NotFound();
            }

            _context.ChallengeInstanceStatus.Remove(challengeInstanceStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChallengeInstanceStatusExists(int id)
        {
            return _context.ChallengeInstanceStatus.Any(cis => cis.ChallengeInstanceStatusId == id);
        }
    }

}
