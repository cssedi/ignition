using BMWIgnition_API.Data;
using BMWIgnition_API.Model;
using BMWIgnition_API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BMWIgnition_API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ChallengeStatusController : ControllerBase
  {
    private readonly AppDbContext _context;

    public ChallengeStatusController(AppDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ChallengeStatus>>> GetChallengeStatuses()
    {
      return await _context.ChallengeStatuses.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ChallengeStatus>> GetChallengeStatus(int id)
    {
      var challengeStatus = await _context.ChallengeStatuses.FindAsync(id);

      if (challengeStatus == null)
      {
        return NotFound();
      }

      return challengeStatus;
    }

    [HttpPost]
    public async Task<ActionResult<ChallengeStatus>> CreateChallengeStatus(ChallengeStatusViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var challengeStatus = new ChallengeStatus
      {
        Name = model.Name
      };

      _context.ChallengeStatuses.Add(challengeStatus);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetChallengeStatus), new { id = challengeStatus.ChallengeStatusID }, challengeStatus);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateChallengeStatus(int id, ChallengeStatusViewModel model)
    {
      var challengeStatus = await _context.ChallengeStatuses.FindAsync(id);

      if (challengeStatus == null)
      {
        return NotFound();
      }

      challengeStatus.Name = model.Name;

      await _context.SaveChangesAsync();

      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteChallengeStatus(int id)
    {
      var challengeStatus = await _context.ChallengeStatuses.FindAsync(id);

      if (challengeStatus == null)
      {
        return NotFound();
      }

      _context.ChallengeStatuses.Remove(challengeStatus);
      await _context.SaveChangesAsync();

      return NoContent();
    }
  }

}
