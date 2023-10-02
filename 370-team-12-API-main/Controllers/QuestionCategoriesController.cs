using BMWIgnition_API.Data;
using BMWIgnition_API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BMWIgnition_API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class QuestionCategoriesController : Controller
  {
    private readonly AppDbContext _context;

    public QuestionCategoriesController(AppDbContext context)
    {
      _context = context;
    }

    // GET: api/QuestionCategories
    [HttpGet()]
    public async Task<ActionResult<IEnumerable<QuestionCategory>>> GetQuestionCategories()
    {
      return await _context.QuestionCategories.ToListAsync();
    }

    // GET: api/QuestionCategories/5
    [HttpGet("{id}")]
    public async Task<ActionResult<QuestionCategory>> GetQuestionCategory(int id)
    {
      var questionCategory = await _context.QuestionCategories.FindAsync(id);

      if (questionCategory == null)
      {
        return NotFound();
      }

      return questionCategory;
    }

  }
}
