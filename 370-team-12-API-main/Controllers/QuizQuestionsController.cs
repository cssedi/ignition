using BMWIgnition_API.Data;
using BMWIgnition_API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BMWIgnition_API.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class QuizQuestionsController : ControllerBase
  {
    private readonly AppDbContext _context;

    public QuizQuestionsController(AppDbContext context)
    {
      _context = context;
    }

    // GET: api/QuizQuestions
    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuizQuestion>>> GetQuizQuestions()
    {
      return await _context.QuizQuestions.ToListAsync();
    }

    // GET: api/QuizQuestions/5
    [HttpGet("{quizId}/{questionId}")]
    public async Task<ActionResult<QuizQuestion>> GetQuizQuestion(int quizId, int questionId)
    {
      var quizQuestion = await _context.QuizQuestions.FindAsync(quizId, questionId);

      if (quizQuestion == null)
      {
        return NotFound();
      }

      return quizQuestion;
    }

    // POST: api/QuizQuestions
    [HttpPost]
    public async Task<ActionResult<QuizQuestion>> PostQuizQuestion(QuizQuestion quizQuestion)
    {
      _context.QuizQuestions.Add(quizQuestion);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetQuizQuestion), new { quizId = quizQuestion.QuizId, questionId = quizQuestion.QuestionId }, quizQuestion);
    }

    // PUT: api/QuizQuestions/5
    [HttpPut("{quizId}/{questionId}")]
    public async Task<IActionResult> PutQuizQuestion(int quizId, int questionId, QuizQuestion quizQuestion)
    {
      if (quizId != quizQuestion.QuizId || questionId != quizQuestion.QuestionId)
      {
        return BadRequest();
      }

      _context.Entry(quizQuestion).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!QuizQuestionExists(quizId, questionId))
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

    // DELETE: api/QuizQuestions/5
    [HttpDelete("{quizId}/{questionId}")]
    public async Task<IActionResult> DeleteQuizQuestion(int quizId, int questionId)
    {
      var quizQuestion = await _context.QuizQuestions.FindAsync(quizId, questionId);
      if (quizQuestion == null)
      {
        return NotFound();
      }

      _context.QuizQuestions.Remove(quizQuestion);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool QuizQuestionExists(int quizId, int questionId)
    {
      return _context.QuizQuestions.Any(e => e.QuizId == quizId && e.QuestionId == questionId);
    }
  }

}
