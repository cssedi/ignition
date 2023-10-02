using AutoMapper.Configuration.Annotations;
using BMWIgnition_API.Data;
using BMWIgnition_API.Model;
using BMWIgnition_API.ViewModels;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json.Linq;
using MimeKit.Text;
using static System.Net.WebRequestMethods;
using AuthenticationAndAutherazation.ViewModels;
using Org.BouncyCastle.Asn1.Cmp;
using Microsoft.AspNetCore.Server.HttpSys;

namespace BMWIgnition_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChallengeInstanceController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Challenger> _userManager;

        public ChallengeInstanceController(AppDbContext context,IConfiguration configuration, UserManager<Challenger> userManager)

        {
            _configuration  = configuration;
            _context = context;
            _userManager = userManager;
        }

        [HttpGet()]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "CHALLENGER")]

        public ActionResult GetChallengeInstances()
        {
            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id   
            var challengeInstances =  _context.ChallengeInstances.Include( c => c.Challenge).Include( x => x.Challenger).Include( c => c.Challenge.Prize).Include(c => c.Challenge.Medal).
              Where( c=> c.ChallengerId == userId && c.Submition == null).ToList();
            /*  var res =  challengeInstances.Select(chI => new
            {
                ChallengeId = chI.Challenge.ChallengeID,
                name = chI.Challenge.Name,
                status = chI.ChallengeInstanceStatus.Name,

                challenge = chI.Challenge,
                token = chI.Challenge.Tokens,
                description = chI.Challenge.Description,



            });*/
            return Ok(challengeInstances);
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "CHALLENGER")]
        [HttpGet("getUserSumitedChallenges")]
        public  ActionResult getUserSumitedChallengesAuth()
        {
            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id   
            var challengeInstance =  _context.ChallengeInstances.Where( x=> x.ChallengerId == userId && x.ChallengeInstanceStatusId == 4).Include( x=> x.Challenge).Include( s => s.ChallengeInstanceStatus).ToList();
            var res = new List<object>();
            if (challengeInstance != null)
            {
                foreach (var subminsion in challengeInstance)
                {
                    if (subminsion.Submition != null)
                    {
                        res.Add(subminsion);
                    }
                }
            }


            return Ok(res);
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "REWARDARCHITECT, SUPERARCHITECT")]
        [HttpGet("SubmitedChallenges")]
        public async Task<ActionResult> GetSubmitedChallenges()
        {
            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
            var rewardArchitect = await _userManager.FindByIdAsync(userId);
            // get submitted instances where logged in user's Id matches the Id foreign key in the challenge table
            List<ChallengeInstance> challengeInstance =   _context.ChallengeInstances.
                                            Include(ci => ci.Challenge)
                                            .Include(c=> c.Challenger)
                                            .Where(ci => ci.Challenge.Id == userId && ci.ChallengeInstanceStatusId == 4)
                                            .ToList();
           
           var inbox = challengeInstance.Select(msg => new
            {
                //catch null errors
                challengerId = msg.ChallengerId,
                challengeId = msg.ChallengeID,
                ChallengeName = msg.Challenge?.Name,
                challengeStatus = msg.ChallengeInstanceStatus?.Name,
                ChallengerUsername = msg.Challenger?.UserName,
                challengerName = (msg.Challenger?.Name ?? "") +" "+ (msg.Challenger?.Surname ?? ""),
                Submition = msg.Submition,
                profilePicture = msg.Challenger?.ProfilePicture
            });
            var res = new List<object>();
            if (challengeInstance != null)
            {
                foreach (var subminsion in inbox)
                {
                    if (subminsion.Submition != null)
                    {

                        res.Add(subminsion);
                    }
                }
            }

            var auserId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
            var auser = await _userManager.FindByIdAsync(auserId);

            //new audit trail object
            var auditTrail = new AuditTrail
            {
                UserId = auser.Name + " " + auser.Surname,
                Action = "Viewed challenge submissions",
                Timestamp = DateTime.Now,
                Amount = 0,
                Quantity = 0

            };
            await _context.AuditTrails.AddAsync(auditTrail);
            await _context.SaveChangesAsync();

            return Ok(res);

        }

        [HttpPost("ApproveChallenge")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN, REWARDARCHITECT, SUPERARCHITECT")]

        public async Task<ActionResult> ApproveChallenge(ApproveChallengeVM approveChallengeVM)
        {
            try
            {
                var isntance = _context.ChallengeInstances.Include(c => c.Challenge).Where(x => x.ChallengeID == approveChallengeVM.ChallengeId && x.ChallengerId == approveChallengeVM.ChallengerId).FirstOrDefault();
                isntance.ChallengeInstanceStatusId = 5;
                await _context.SaveChangesAsync();
                var challenge = _context.Challenges.Find(approveChallengeVM.ChallengeId);
                var user = await _userManager.FindByIdAsync(approveChallengeVM.ChallengerId);

                var challengerMedal = new ChallengerMedal
                {
                    MedalId = isntance.Challenge.MedalId,
                    ChallenegerId = approveChallengeVM.ChallengerId
                };

                Challenger challenger = await _userManager.FindByIdAsync(approveChallengeVM.ChallengerId);
                if(challenger.Tokens == 0)
                {
                    challenger.Tokens = Convert.ToInt32(challenge.Tokens);
                }
                else
                {
                    challenger.Tokens = challenger.Tokens + Convert.ToInt32(challenge.Tokens);
                }
                await _userManager.UpdateAsync(challenger);

                _context.ChallengerMedals.Add(challengerMedal);
                await _context.SaveChangesAsync();

                string Imaage = "https://img.freepik.com/free-vector/hand-drawn-happy-people-jumping_23-2149092878.jpg?w=826&t=st=1689936850~exp=1689937450~hmac=d4477ae5660d65757e7c822207f4c4ccc9ce42941c776667eb429dd39137893f";
                var body = $@"
                    <section style=""font-family: Arial, sans-serif; background-color: #f0f0f0; margin: 0; padding: 0;"">
                      <div style=""max-width: 600px; margin: 20px auto; background-color: #fff; border-radius: 10px; box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);"">
                        <div style=""background-color: #3f51b5; color: #fff; padding: 20px; border-top-left-radius: 10px; border-top-right-radius: 10px; text-align: center;"">
                          <h2>Challenge {challenge.Name}</h2>
                        </div>
                        <div style=""padding: 20px; text-align: center;"">
                          <h3 style=""color: #3f51b5; margin-top: 0;"">Congratulations, {user.Name}!</h3>
                          <p>You have successfully completed the challenge.</p>
                          <div style=""width: 100%; height: 300px; background-image: url({Imaage}); background-size: cover; background-position: center; border-radius: 5px;""></div>
                        </div>
                        <div style=""background-color: #3f51b5; color: #fff; padding: 10px; border-bottom-left-radius: 10px; border-bottom-right-radius: 10px; text-align: center;"">
                          <p>Thank you for your participation!</p>
                        </div>
                      </div>
                    </section>";
                var message = new MimeMessage();
                message.From.Add(MailboxAddress.Parse(_configuration["Mail:Email"]));
                message.To.Add(new MailboxAddress("", user.Email));
                message.Subject = "Challenge Completed";

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = body;
                message.Body = new TextPart(TextFormat.Html) { Text = body };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate(_configuration["Mail:Email"], _configuration["Mail:Password"]);
                    client.Send(message);
                    client.Disconnect(true);
                }

                var httppUser = HttpContext.User;
                var auserId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
                var auser = await _userManager.FindByIdAsync(auserId);

                var auditTrail = new AuditTrail
                {
                    UserId = auser.Name + " " + auser.Surname, // Replace with the actual user ID
                    Action = user.Name + " challenge approved",
                    Timestamp = DateTime.Now,
                    Amount = 0,
                    Quantity = 0

                };
                _context.AuditTrails.Add(auditTrail);
                _context.SaveChanges();



                return Ok(new { ChallengeInstance = isntance });
            }
            catch (Exception ex )
            {

                return BadRequest(new { Message = ex.Message });
            } 
         
        }

        [HttpPost("ViewSubmision")]
        public async Task<ActionResult> ViewSubmision(ApproveChallengeVM approveChallengeVM)
        {

            var isntance = _context.ChallengeInstances.Where(x => x.ChallengeID == approveChallengeVM.ChallengeId && x.ChallengerId == approveChallengeVM.ChallengerId).FirstOrDefault();
           
            await _context.SaveChangesAsync();
            return Ok(isntance);
        }
        // GET: api/ChallengeInstance/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChallengeInstance>> GetChallengeInstance(int id)
        {
            var challengeInstance = await _context.ChallengeInstances.FindAsync(id);

            if (challengeInstance == null)
            {
                return NotFound();
            }

            return challengeInstance;
        }

        // POST: api/ChallengeInstance
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "CHALLENGER")]
        [HttpPost]
        public async Task<ActionResult<ChallengeInstance>> CreateChallengeInstance(ChallengeInstanceViewModel challengeInstanceVM)
        {
            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id   

            
            var challengeInstance = new ChallengeInstance
            {
                ChallengeID = challengeInstanceVM.ChallengeID,
                ChallengerId =  userId,
                ChallengeInstanceStatusId = 2,
            };

            _context.ChallengeInstances.Add(challengeInstance);
            await _context.SaveChangesAsync();
            var auserId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
            var auser = await _userManager.FindByIdAsync(auserId);

            var auditTrail = new AuditTrail
            {
                UserId = auser.Name + " " + auser.Surname, // Replace with the actual user ID
                Action = " User enrolled in challenge",
                Timestamp = DateTime.Now,
                Amount = 0,
                Quantity = 0

            };
            _context.AuditTrails.Add(auditTrail);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetChallengeInstance), new { id = challengeInstance.ChallengeInstanceId }, challengeInstance);
        }

        // PUT: api/ChallengeInstance/5
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "CHALLENGER")]
        [HttpPost("completeChallenge")]
        public async Task<IActionResult> CompleteChallenge(SubmitionVM submitionVM)
        {
            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  

            try
            {
                var challengeInstance = _context.ChallengeInstances.Where(c => c.ChallengeID == submitionVM.ChallengeId && c.ChallengerId == userId).FirstOrDefault(); 
                if (challengeInstance == null)
                {
                    return BadRequest(); 

                }
                challengeInstance.Submition = submitionVM.File;
                challengeInstance.ChallengeInstanceStatusId = 4;
                await _context.SaveChangesAsync();

                // Send EMAIL TO REWARD architect

                var auserId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
                var auser = await _userManager.FindByIdAsync(auserId);

                var auditTrail = new AuditTrail
                {
                    UserId = auser.Name + " " + auser.Surname, // Replace with the actual user ID
                    Action = " User completed challenge",
                    Timestamp = DateTime.Now,
                    Amount = 0,
                    Quantity = 0

                };
                _context.AuditTrails.Add(auditTrail);
                _context.SaveChanges();

                return Ok(challengeInstance);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChallengeInstanceExists(submitionVM.ChallengeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        // DELETE: api/ChallengeInstance/5
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "CHALLENGER")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChallengeInstance(int id)
        {
            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id   

            var challengeInstance = await _context.ChallengeInstances.Where( x => x.ChallengeID == id && x.ChallengerId == userId).FirstOrDefaultAsync();
            if (challengeInstance == null)
            {
                return NotFound();
            }

            _context.ChallengeInstances.Remove(challengeInstance);
            await _context.SaveChangesAsync();

            var auserId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
            var auser = await _userManager.FindByIdAsync(auserId);

            var auditTrail = new AuditTrail
            {
                UserId = auser.Name + " " + auser.Surname, // Replace with the actual user ID
                Action = " User cancelled challenge",
                Timestamp = DateTime.Now,
                Amount = 0,
                Quantity = 0

            };
            _context.AuditTrails.Add(auditTrail);
            _context.SaveChanges();


            return NoContent();
        }

        private bool ChallengeInstanceExists(int id)
        {
            return _context.ChallengeInstances.Any(ci => ci.ChallengeInstanceId == id);
        }
    }
}
