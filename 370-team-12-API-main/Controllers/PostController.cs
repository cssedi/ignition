
using AutoMapper.Configuration.Annotations;
using Beam_Feed_API.Models;
using BMWIgnition_API.Data;
using BMWIgnition_API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Data;
using System.Security.Claims;

namespace Beam_Feed_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        //Inject DbContext
        private readonly AppDbContext dbContext;
        private readonly UserManager<Challenger> _userManager;
        public PostController(AppDbContext dbContext, UserManager<Challenger> userManager)
        {
            this.dbContext = dbContext;
            _userManager = userManager;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "CHALLENGER")]
        [HttpGet("GetAllPosts")]
        public async  Task<IActionResult> GetPost()
        {
            List<Post> posts = await dbContext.Posts.Include(x => x.Challenger).Include(x => x.Comments).Include(  x=> x.Likes) .ToListAsync<Post>();

            var user = HttpContext.User;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id
            var a_user = await _userManager.FindByIdAsync(userId);
            var auditTrail = new AuditTrail
            {
                UserId = a_user.Name + " " + a_user.Surname, // Replace with the actual user ID
                Action = "View social feed",
                Timestamp = DateTime.Now,
                Amount = 0,
                Quantity = 0

            };

            dbContext.AuditTrails.Add(auditTrail);
            dbContext.SaveChanges();
            return Ok(posts);

        }


        [Authorize(AuthenticationSchemes = "Bearer", Roles = "CHALLENGER")]
        [HttpGet]
        [Route("GetUserPostAsync")]
        public async Task<IActionResult> GetUserPostAsync()
        {
           var user = HttpContext.User;
           var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id      
            var a_user = await _userManager.FindByIdAsync(userId);
            List<Post> posts =  await dbContext.Posts.Include(x => x.Challenger).Include( x=> x.Comments).ToListAsync<Post>();

            if (user == null)
            {
                return NotFound();
            }

            var auditTrail = new AuditTrail
            {
                UserId = a_user.Name + " " + a_user.Surname, // Replace with the actual user ID
                Action = a_user.Name + " " + a_user.Surname + " posts Viewed",
                Timestamp = DateTime.Now,
                Amount = 0,
                Quantity = 0

            };

            dbContext.AuditTrails.Add(auditTrail);
            dbContext.SaveChanges();
            return Ok(posts);
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "CHALLENGER")]
        [HttpGet]
        [Route("LikePost/{PostId:int}")]
        public async Task<IActionResult> LikePost([FromRoute] int PostId)
        {
            try
            {
                var user = HttpContext.User;
                var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id      
                var posts = await dbContext.Posts.FindAsync(PostId);
                var a_user = await _userManager.FindByIdAsync(userId);
                 Like like = dbContext.Likes.Where(l => l.ChallengerId == userId && l.PostId == PostId).FirstOrDefault();
                if (like != null)
                {
                    dbContext.Likes.Remove(like);
                }
                else
                {
                    var newlike = new Like
                    {
                        ChallengerId = userId,
                        PostId = PostId
                    };
                    dbContext.Likes.Add(newlike);
                }

               

                // create notification 

                await dbContext.SaveChangesAsync();

                var auditTrail = new AuditTrail
                {
                    UserId = a_user.Name + " " + a_user.Surname, // Replace with the actual user ID
                    Action = a_user.Name + " " + a_user.Surname + " post liked",
                    Timestamp = DateTime.Now,
                    Amount = 0,
                    Quantity = 0

                };

                dbContext.AuditTrails.Add(auditTrail);
                dbContext.SaveChanges();

                return Ok(new { Message = "Post liked" });
            }
            catch (Exception ex)
            {

                return BadRequest(new { Error = ex.Message });
            }
        }
        [HttpGet("SeedPosts")]
        public async Task<ActionResult> SeedPosts()
        {
            List<Challenger> challengers = _userManager.Users.ToList();
            dbContext.Posts.Add(new Post
            {
                Text = "Great job on the project completion!",
                Date = DateTime.Now.AddHours(-1),
                ChallengerId = challengers[0].Id,
                Likes = new List<Like>
                {
                    new Like { ChallengerId = challengers[1].Id },
                    new Like { ChallengerId = challengers[2].Id }
                },
                Comments = new List<Comment>
                {
                    new Comment { ChallengerId = challengers[3].Id, Text = "Congratulations!" }
                }
            });

            dbContext.Posts.Add(new Post
            {
                Text = "Employee of the month award goes to John!",
                Date = DateTime.Now.AddHours(-2),
                ChallengerId = challengers[1].Id,
                Likes = new List<Like>
                {
                    new Like { ChallengerId = challengers[0].Id }
                }
            });
            dbContext.Posts.Add(new Post
            {
                Text = "Celebrating 5 years with the company!",
                Date = DateTime.Now.AddDays(-7),
                ChallengerId = challengers[2].Id,
                Likes = new List<Like>
                {
                    new Like { ChallengerId = challengers[0].Id },
                    new Like { ChallengerId = challengers[1].Id }
                }
            });

            dbContext.SaveChangesAsync(); 

            return Ok("seeeded");
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [HttpGet("PostsReport")]
        public async Task<ActionResult> PostsReport()
        {
            DateTime currentDate = DateTime.Now;
            DateTime startDate = new DateTime(currentDate.Year, currentDate.Month, 1); // Start of the current month.
            DateTime endDate = startDate.AddMonths(1).AddDays(-1); // End of the current month.

            List<int> likesData = new List<int>();
            List<int> commentsData = new List<int>();
            List<int> postData = new List<int>();
            List<string> weekLabels = new List<string>();
            List<int> commentData = new List<int>();
            while (startDate <= endDate)
            {
                // Calculate the end of the current week (Sunday).
                DateTime endOfWeek = startDate.AddDays(6);

                // Query the database to count posts for this week.
                int postCount = dbContext.Posts
                    .Where(p => p.Date >= startDate && p.Date <= endOfWeek)
                    .Count();
                int likesCount = dbContext.Likes
             .Where(l => dbContext.Posts.Any(p => p.Date >= startDate && p.Date <= endOfWeek && p.PostID == l.PostId))
             .Count();

                int commentsCount = dbContext.Comments
                    .Where(c => dbContext.Posts.Any(p => p.Date >= startDate && p.Date <= endOfWeek && p.PostID == c.PostID))
                    .Count();
                likesData.Add(likesCount);
                commentsData.Add(commentsCount);
                postData.Add(postCount);
                weekLabels.Add(startDate.ToString("dd MMMM"));

                // Move to the next week.
                startDate = startDate.AddDays(7);
            }

            // Get the comments for every 

            return Ok( new
            {
                Categories = weekLabels.ToArray(),
                Data = postData.ToArray(),
                CommentData = commentData.ToArray(),
                LikesData = likesData.ToArray()
            });
        }


        [Authorize(AuthenticationSchemes = "Bearer", Roles = "CHALLENGER")]
        [HttpPost]
        [Route("AddPostAuth")]
        public async Task<IActionResult> AddPostAuth(PostViewModel addPost)
        {
            var user = HttpContext.User;
            var userId =  user.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id
            var a_user = await _userManager.FindByIdAsync(userId);
            var newPost = new Post
            {
                Text = addPost.Text,
                ChallengerId = userId,
                Date = DateTime.Now, 
            };

            await dbContext.Posts.AddAsync(newPost);
            await dbContext.SaveChangesAsync();

            var userName = User.Identity.Name;
            var auditTrail = new AuditTrail
            {
                UserId = a_user.Name + " " + a_user.Surname, // Replace with the actual user ID
                Action = "post created",
                Timestamp = DateTime.Now,
                Amount = 0,
                Quantity = 0

            };

            dbContext.AuditTrails.Add(auditTrail);
            dbContext.SaveChanges();


            return Ok(new { text = addPost.Text, id = userId, date = newPost.Date });
        }


        [Authorize(AuthenticationSchemes = "Bearer", Roles = "CHALLENGER")]
        [HttpPut]
        [Route("{PostId:guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid PostId, PostViewModel updatepost)
        {
            var post = await dbContext.Posts.FindAsync(PostId);

            if (post != null)
            {
                post.Text = updatepost.Text;
                await dbContext.SaveChangesAsync();

                var user = HttpContext.User;
                var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id
                var a_user = await _userManager.FindByIdAsync(userId);
                var auditTrail = new AuditTrail
                {
                    UserId = a_user.Name + " " + a_user.Surname, // Replace with the actual user ID
                    Action = "post updated",
                    Timestamp = DateTime.Now,
                    Amount = 0,
                    Quantity = 0

                };

                dbContext.AuditTrails.Add(auditTrail);
                dbContext.SaveChanges();

                return Ok(post);
            }

            return NotFound();
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "CHALLENGER")]
        [HttpDelete]
        [Route("DeletePost/{postId}")]
        public async Task<IActionResult> DeletePost( [FromRoute] int postId)
        {
            try
            {
                var post = await dbContext.Posts.FindAsync(postId);

                if (post != null)
                {

                    // Delete all the comments 
                    List<Comment> comments = dbContext.Comments.Where(c => c.PostID == post.PostID).ToList();
                    List<Like> likes = dbContext.Likes.Where(c => c.PostId == post.PostID).ToList();
                    foreach (Comment comment in comments)
                    {
                        dbContext.Comments.Remove(comment);
                        
                    }

                    foreach (Like like in likes)
                    {
                        dbContext.Likes.Remove(like);
                       
                    }
                    dbContext.Posts.Remove(post);
                    dbContext.SaveChanges();

                    var user = HttpContext.User;
                    var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id
                    var a_user = await _userManager.FindByIdAsync(userId);
                    var auditTrail = new AuditTrail
                    {
                        UserId = a_user.Name + " " + a_user.Surname, // Replace with the actual user ID
                        Action = "post delete",
                        Timestamp = DateTime.Now,
                        Amount = 0,
                        Quantity = 0

                    };

                    dbContext.AuditTrails.Add(auditTrail);
                    dbContext.SaveChanges();

                    return Ok(new { Message = "Post Deleted" });
                }
                return NotFound();
            }
            catch (Exception ex)
            {

                return BadRequest(new { Message = ex.Message });
            }
        }


       
    }
}

