using BMWIgnition_API.Data;
using BMWIgnition_API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BMWIgnition_API.Controllers
{
  [ApiController]
  public class QuestionsController : ControllerBase
  {
    private readonly AppDbContext _context;

    public QuestionsController(AppDbContext context)
    {
      _context = context;
    }

    // GET: api/Questions
    [HttpGet("GetQuestions")]
    public async Task<ActionResult<List<Question>>> GetQuestions()
    {
      var questions = await _context.Questions
          .Include(q => q.Category)
          .Include(q => q.Options)
          .ToListAsync();

      return questions;
    }
    // GET: api/Questions/5
    [HttpGet("GetQuestions/{id}")]
    public async Task<ActionResult<Question>> GetQuestion(int id)
    {
      var question = await _context.Questions.FindAsync(id);

      if (question == null)
      {
        return NotFound();
      }

      return question;
    }

    // POST: api/Questions
    [HttpPost("PostQuestion")]
    public async Task<ActionResult<Question>> PostQuestion(Question question)
    {
      _context.Questions.Add(question);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetQuestion), new { id = question.QuestionId }, question);
    }

    // PUT: api/Questions/5
    [HttpPut("PutQuestion/{id}")]
    public async Task<IActionResult> PutQuestion(int id, Question question)
    {
      if (id != question.QuestionId)
      {
        return BadRequest();
      }

      _context.Entry(question).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!QuestionExists(id))
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

    // DELETE: api/Questions/5
    [HttpDelete("DeleteQuestion/{id}")]
    public async Task<IActionResult> DeleteQuestion(int id)
    {
      var question = await _context.Questions.FindAsync(id);
      if (question == null)
      {
        return NotFound();
      }

      _context.Questions.Remove(question);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool QuestionExists(int id)
    {
      return _context.Questions.Any(e => e.QuestionId == id);
    }
  }

}
