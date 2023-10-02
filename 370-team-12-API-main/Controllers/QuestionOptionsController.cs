using BMWIgnition_API.Data;
using BMWIgnition_API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BMWIgnition_API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class QuestionOptionsController : ControllerBase
  {
    private readonly AppDbContext _context;

    public QuestionOptionsController(AppDbContext context)
    {
      _context = context;
    }

    // GET: api/QuestionOptions
    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuestionOption>>> GetQuestionOptions()
    {
      return await _context.QuestionOptions.ToListAsync();
    }

    // GET: api/QuestionOptions/5
    [HttpGet("{id}")]
    public async Task<ActionResult<QuestionOption>> GetQuestionOption(int id)
    {
      var questionOption = await _context.QuestionOptions.FindAsync(id);

      if (questionOption == null)
      {
        return NotFound();
      }

      return questionOption;
    }

    // POST: api/QuestionOptions
    [HttpPost]
    public async Task<ActionResult<QuestionOption>> PostQuestionOption(QuestionOption questionOption)
    {
      _context.QuestionOptions.Add(questionOption);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetQuestionOption), new { id = questionOption.OptionId }, questionOption);
    }

    // PUT: api/QuestionOptions/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutQuestionOption(int id, QuestionOption questionOption)
    {
      if (id != questionOption.OptionId)
      {
        return BadRequest();
      }

      _context.Entry(questionOption).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!QuestionOptionExists(id))
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

    // DELETE: api/QuestionOptions/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQuestionOption(int id)
    {
      var questionOption = await _context.QuestionOptions.FindAsync(id);
      if (questionOption == null)
      {
        return NotFound();
      }

      _context.QuestionOptions.Remove(questionOption);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool QuestionOptionExists(int id)
    {
      return _context.QuestionOptions.Any(e => e.OptionId == id);
    }
  }

}
