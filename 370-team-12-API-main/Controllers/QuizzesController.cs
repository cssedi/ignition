using BMWIgnition_API.Data;
using BMWIgnition_API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BMWIgnition_API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class QuizzesController : ControllerBase
  {
    private readonly AppDbContext _context;

    public QuizzesController(AppDbContext context)
    {
      _context = context;
    }

    // GET: api/Quizzes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Quiz>>> GetQuizzes()
    {
      return await _context.Quizzes.Include(x => x.Category).ToListAsync();
    }

    // GET: api/Quizzes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Quiz>> GetQuiz(int id)
    {
      var quiz = await _context.Quizzes.Include(x => x.Category).Include(x => x.QuizQuestions).ThenInclude(x => x.Question).ThenInclude(x => x.Options).Where(x => x.QuizId == id).FirstOrDefaultAsync();

      if (quiz == null)
      {
        return NotFound();
      }

      return quiz;
    }

    // POST: api/Quizzes
    [HttpPost]
    public async Task<ActionResult<Quiz>> PostQuiz(Quiz quiz)
    {
      _context.Quizzes.Add(quiz);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetQuiz), new { id = quiz.QuizId }, quiz);
    }

    // PUT: api/Quizzes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutQuiz(int id, Quiz quiz)
    {
      if (id != quiz.QuizId)
      {
        return BadRequest();
      }

      _context.Entry(quiz).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!QuizExists(id))
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

    // DELETE: api/Quizzes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQuiz(int id)
    {
      var quiz = await _context.Quizzes.FindAsync(id);
      if (quiz == null)
      {
        return NotFound();
      }

      _context.Quizzes.Remove(quiz);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool QuizExists(int id)
    {
      return _context.Quizzes.Any(e => e.QuizId == id);
    }
  }
}
