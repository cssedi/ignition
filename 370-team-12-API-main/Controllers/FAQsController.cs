using BMWIgnition_API.Data;
using BMWIgnition_API.Model;
using BMWIgnition_API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading;

namespace BMWIgnition_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FAQsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Challenger> _userManager;

        public FAQsController(AppDbContext context, UserManager<Challenger> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: api/faqs
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "CHALLENGER,REWARDARCHITECT,ADMIN,SUPERARCHITECT")]
        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            //execute stored procedure
            var faqs = await _context.FAQs.FromSqlRaw("EXEC GetAllFAQs").ToListAsync();
            //stored procedure SQL script
            {
                //CREATE PROCEDURE GetAllFAQs
                //AS
                //SELECT* FROM FAQs
                //GO;
            }
            //if stored proc fails
            if(faqs ==null || faqs.Count == 0)
            {
                faqs = await _context.FAQs.ToListAsync();
            }
            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
            var user = await _userManager.FindByIdAsync(userId);
            var userName = user.UserName;

            var auditTrail = new AuditTrail
            {
                UserId = user.Name + " " + user.Surname, // Replace with the actual user ID
                Action = "FAQs Viewed",
                Timestamp = DateTime.Now,
                Amount = 0,
                Quantity = 0

            };

            _context.AuditTrails.Add(auditTrail);
            _context.SaveChanges();
            return Ok(faqs);
        }

        // GET: api/faqs/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var faq = _context.FAQs.Find(id);
            if (faq == null)
                return NotFound();

            return Ok(faq);
        }

        // POST: api/faqs
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [HttpPost("CreateFAQ")]
        public async Task<IActionResult> Post([FromBody] FAQDto faqDto)
        {
            var faq = new FAQ
            {
                Question = faqDto.Question,
                Answer = faqDto.Answer
            };

            _context.FAQs.Add(faq);
            _context.SaveChanges();


            var question = faqDto.Question;

            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
            var user = await _userManager.FindByIdAsync(userId);


            var auditTrail = new AuditTrail
            {
                UserId = user.Name + " " + user.Surname, // Replace with the actual user ID
                Action = "FAQ for' " + question + " ' has been Created",
                Timestamp = DateTime.Now,
                Amount = 0,
                Quantity = 2

            };

            _context.AuditTrails.Add(auditTrail);
            _context.SaveChanges();


            return CreatedAtAction(nameof(Get), new { id = faq.FAQId }, faq);
        }

        // PUT: api/faqs/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] FAQDto faqDto)
        {
            var faq = _context.FAQs.Find(id);
            if (faq == null)
                return NotFound();

            faq.Question = faqDto.Question;
            faq.Answer = faqDto.Answer;

            _context.SaveChanges();

            var question = faqDto.Question;

            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
            var user = await _userManager.FindByIdAsync(userId);

            var auditTrail = new AuditTrail
            {
                UserId = user.Name + " " + user.Surname, // Replace with the actual user ID
                Action = "FAQ for' " + question + " ' has been Updated",
                Timestamp = DateTime.Now,
                Amount = 0,
                Quantity = 2

            };

            _context.AuditTrails.Add(auditTrail);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/faqs/{id}
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [HttpDelete("DeleteFAQ/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var faq = _context.FAQs.Find(id);
            if (faq == null)
                return Ok(new {Messaage = "FAQ not found"});

            _context.FAQs.Remove(faq);
            _context.SaveChanges();

            var question = faq.Question;
            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
            var user = await _userManager.FindByIdAsync(userId);

            var auditTrail = new AuditTrail
            {
                UserId = user.Name + " " + user.Surname, // Replace with the actual user ID
                Action = "FAQ for' " + question + " ' has been Deleted",
                Timestamp = DateTime.Now,
                Amount = 0,
                Quantity = 2

            };

            _context.AuditTrails.Add(auditTrail);
            _context.SaveChanges();

            return Ok(new {Message = "FAQ Deleted "});
        }

    }
}
