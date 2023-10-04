
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Beam_Feed_API.Models;
using BMWIgnition_API.Model;
using BMWIgnition_API.Data;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Security.Claims;
using BMWIgnition_API.ViewModels;
using System.ComponentModel.Design;
using Microsoft.AspNetCore.Identity;

namespace Beam_Feed_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


    public class CommentController : Controller
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<Challenger> _userManager;

        public CommentController(AppDbContext dbContext, UserManager<Challenger> userManager)
        {
            this.dbContext = dbContext;
            _userManager = userManager;

        }


        [HttpGet]
        public IActionResult GetComment()
        {
            return Ok(dbContext.Comments);
        }

        [HttpGet("GetComments/{id}")]
        public async Task<IActionResult> GetComments(int id)
        {
            var comments = await dbContext.Comments.Include(c=> c.Challenger).Where(x => x.PostID == id).ToListAsync();


            return Ok(comments);
    

        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "CHALLENGER")]
        [HttpGet]
        [Route("{CommentId:guid}")]

        public async Task<IActionResult> GetSingleComment([FromRoute] Guid CommentId)
        {
            var comments = await dbContext.Comments.FindAsync(CommentId);

            if (comments == null)
            {
                return NotFound();
            }

            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
            var user = await _userManager.FindByIdAsync(userId);
            var userName = user.UserName;

            var auditTrail = new AuditTrail
            {
                UserId = user.Name + " " + user.Surname, // Replace with the actual user ID
                Action = "Viewed Comments",
                Timestamp = DateTime.Now,
                Amount = 0,
                Quantity = 1

            };
            dbContext.AuditTrails.Add(auditTrail);
            dbContext.SaveChanges();


            return Ok(comments);
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "CHALLENGER")]
        [HttpPost]
        [Route("AddComment")]
        public async Task<IActionResult> AddCommentAuth(CommentViewModel addComment)
        {
            var user = HttpContext.User;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id      
            var comment = new Comment()
            {
                ChallengerId = userId,
                PostID = addComment.PostId,
                Text = addComment.CommentText

            };

            await dbContext.Comments.AddAsync(comment);
            await dbContext.SaveChangesAsync();

            var httppUser = HttpContext.User;
            var auserId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
            var auser = await _userManager.FindByIdAsync(auserId);


            var auditTrail = new AuditTrail
            {
                UserId = auser.Name + " " + auser.Surname, // Replace with the actual user ID
                Action = "Added a Comment",
                Timestamp = DateTime.Now,
                Amount = 0,
                Quantity = 1

            };
            dbContext.AuditTrails.Add(auditTrail);
            dbContext.SaveChanges();

            return Ok(new { comment = addComment.CommentText, });
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "CHALLENGER")]
        [HttpDelete]
        [Route("DeleteComment/{CommentId:int}")]
        public async Task<IActionResult> DeleteComment([FromRoute]int CommentId)
        {
            var comments = await dbContext.Comments.FindAsync(CommentId);

            if (comments != null) 
            {
                dbContext.Remove(comments);
                await dbContext.SaveChangesAsync();


                var httppUser = HttpContext.User;
                var auserId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
                var auser = await _userManager.FindByIdAsync(auserId);


                var auditTrail = new AuditTrail
                {
                    UserId = auser.Name + " " + auser.Surname, // Replace with the actual user ID
                    Action = "Removed a Comment",
                    Timestamp = DateTime.Now,
                    Amount = 0,
                    Quantity = 1

                };
                dbContext.AuditTrails.Add(auditTrail);
                dbContext.SaveChanges();


                return Ok(new {message = " Comment deleted "});
                
            }

            return NotFound();

        }

    }
}
