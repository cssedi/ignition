using BMWIgnition_API.Data;
using BMWIgnition_API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BMWIgnition_API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class QuizController : ControllerBase
  {
    private readonly AppDbContext _context;

    public QuizController(AppDbContext context)
    {
      _context = context;
    }

    [HttpPost("check-answer")]
    public IActionResult CheckAnswer(AnswerCheckViewModel answerCheckDto)
    {
      var selectedOption = _context.QuestionOptions.FirstOrDefault(qo => qo.OptionId == answerCheckDto.SelectedOptionId);
      if (selectedOption == null)
      {
        return BadRequest("Invalid option selected.");
      }

      var question = _context.Questions.Include(q => q.Options)
                                       .FirstOrDefault(q => q.QuestionId == answerCheckDto.QuestionId);
      if (question == null)
      {
        return BadRequest("Invalid question id.");
      }

      // var isCorrect = selectedOption.IsCorrect;
      return Ok(selectedOption.IsCorrect);
    }
  }

}
