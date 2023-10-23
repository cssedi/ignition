using BMWIgnition_API.Data;
using BMWIgnition_API.Model;
using BMWIgnition_API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Cmp;
using System.Data;
using System.Globalization;
using System.Net;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Challenge = BMWIgnition_API.Model.Challenge;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

namespace BMWIgnition_API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ChallengeController : ControllerBase
  {
        private readonly AppDbContext _context;
        private readonly UserManager<Challenger> _userManager;
        private readonly IConfiguration _configuration;

        public ChallengeController(AppDbContext context, UserManager<Challenger> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
        }

        // GET: /Challenge
        [Authorize(AuthenticationSchemes = "Bearer", Roles ="ADMIN, SUPERARCHITECT, REWARDARCHITECT")]

        [HttpGet("GetArchitectChallenges/{id}")]
        public async Task<ActionResult<IEnumerable<Challenge>>> GetChallenges(string id)
        {

            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
            var user = await _userManager.FindByIdAsync(userId);
            var userName = user.UserName;
            var challenges = await _context.Challenges.Include(s => s.ChallengeStatus).Include(m => m.Medal).Include(p => p.Prize).Include(ct => ct.ChallengeType).Where(u => u.Id == userId && u.IsArchived == false).ToListAsync();

            var auditTrail = new AuditTrail
            {
                UserId = user.Name + " " + user.Surname,
                Action = "Challenges Viewed",
                Timestamp = DateTime.Now,
                Amount = 0,
                Quantity = 0
            };

            _context.AuditTrails.Add(auditTrail);
            _context.SaveChanges();
            // AutoArchiveChallenge();
            //Change eventInstace to eventInstances
            return Ok(challenges);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN, SUPERARCHITECT, REWARDARCHITECT")]
        [HttpGet("GetArchivedChallenges")]
        public async Task<IActionResult> GetArchivedChallenges()
        {
            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
            var user = await _userManager.FindByIdAsync(userId);
            var userName = user.UserName;

            var archivedChallenges = await _context.Challenges.Include(s => s.ChallengeStatus).
                                            Include(m => m.Medal)
                                            .Include(p => p.Prize)
                                            .Include(ct => ct.ChallengeType)
                                            .Where(u => u.IsArchived == true && u.Id == userId).ToListAsync();

            var auditTrail = new AuditTrail()
            {
                Action = "Viewed Archived Challenges",
                Timestamp = DateTime.Now,
                UserId = user.Name + " " + user.Surname,
                Amount = 0,
                Quantity = 0
            };
            await _context.AuditTrails.AddAsync(auditTrail);
            await _context.SaveChangesAsync();
            //AutoArchiveChallenge();

            return Ok(archivedChallenges);
        }

        [HttpPost("UpdateMaximumTokens/{MaximumTokens}")]
        public async Task<IActionResult> UpdateMaximumTokens( [FromRoute]int MaximumTokens)
        {
            try
            {
                MaximumTokens maximum = _context.MaximumTokens.Find(1);
                maximum.Tokens = MaximumTokens;
                _context.SaveChangesAsync();
                return Ok(maximum);
            }
            catch( Exception ex )
            {
                return BadRequest(new { Error = ex.Message });
            }

     
        }
        [HttpGet("GetMaximunToken")]
        public async Task<IActionResult> GetMaximunToken()
        {
            try
            {
                MaximumTokens maximum = _context.MaximumTokens.Find(1);
                return Ok(maximum.Tokens);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }


        }
        [HttpGet("SeedChallenges")]
        public async Task<IActionResult> SeedChallenges()
        {
            try
            {
                var users = await _userManager.Users.Where(x => x.DepartmentId == 1).FirstOrDefaultAsync();

                //desk challenge
                var deskChallenge = new Challenge
                {
                    Id = users.Id,
                    Name = "Desk Makeover Madness",
                    Tokens = 150,
                    Description = "\"Revamp your workspace and earn points in our Desk Makeover Madness challenge! Snap a 'before' picture of your cluttered desk, then showcase your 'after' transformation. " +
                    "The neatest and most creative setups win",
                    Image = "https://img.freepik.com/free-vector/modern-desktop-compute-concept-illustration_114360-12156.jpg?w=1060&t=st=1692302001~exp=1692302601~hmac=a114e1c37e91db5c7de6a7f6ebb23457732775e270beafd9153ccdd423e81c48",
                    startDate = DateTime.Now.AddDays(1),
                    endDate = DateTime.Now.AddDays(6),
                    ChallengeStatusID = 1,
                    ChallengeTypeID = 1,
                    MedalId = 1,
                    IsArchived = false,


                };
                _context.Challenges.Add(deskChallenge);
                _context.SaveChanges();
                _context.DepartmentChallenges.Add(new DepartmentChallenge
                {
                    DepartmentId = 1,
                    ChallengeID = deskChallenge.ChallengeID,
                });

                //running club challenge
                var runningclub = new Challenge
                {
                    Name = "Running Club",
                    Description = "Run 5 races with the run club",
                    endDate = DateTime.ParseExact("2023-08-25", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    startDate = DateTime.ParseExact("2023-08-25", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Tokens = 0,
                    Id = users.Id,
                    ChallengeStatusID = 1,
                    ChallengeTypeID = 3,
                    MedalId = 1,
                    Image = "https://img.redbull.com/images/c_limit,w_1500,h_1000,f_auto,q_auto/redbullcom/2017/04/19/14810946-3c3d-4284-98d3-6135705a572b/courtney-atkinson",
                    PrizeId = 6,
                    IsArchived = false,

                };

                _context.Challenges.Add(runningclub);
                _context.SaveChanges();
                _context.DepartmentChallenges.Add(new DepartmentChallenge
                {
                    DepartmentId = 1,
                    ChallengeID = runningclub.ChallengeID,
                });

                //ccoking challenge
                var CookingChallenge = new Challenge
                {
                    Id = users.Id,
                    Name = "Cooking Chronicles",
                    Tokens = 450,
                    Description = "Calling all chefs! Share your culinary skills by uploading a picture of your homemade lunch creation.Earn Hubcoins for artistic presentation and mouthwatering dishes!",
                    Image = "https://img.freepik.com/free-vector/people-eating-tacos-concept-illustration_114360-18835.jpg?w=1060&t=st=1692306197~exp=1692306797~hmac=3b4d07cb6faa5e72cf06e48e7a1866eac0d57f81e0775987ac919d6de1019daf",
                    startDate = DateTime.Now.AddDays(2),
                    endDate = DateTime.Now.AddDays(15),
                    ChallengeStatusID = 1,
                    ChallengeTypeID = 1,
                    MedalId = 2,
                    IsArchived = false,

                };
                _context.Challenges.Add(CookingChallenge);
                _context.SaveChanges();
                _context.DepartmentChallenges.Add(new DepartmentChallenge
                {
                    DepartmentId = 1,
                    ChallengeID = CookingChallenge.ChallengeID,
                });
                _context.SaveChanges();
                //virtual traveler
                var VirtualTraveler = new Challenge
                {
                    Id = users.Id,
                    Name = "Virtual Traveler",
                    Tokens = 100,
                    Description = "Wander the world from your screen! Upload images of your virtual travel adventures – from online tours to digital landmarks. Let's explore together and earn Hubcoins along the way!",
                    Image = "https://img.freepik.com/free-vector/group-tourists-with-suitcases-bags-standing-airport_74855-7437.jpg?w=1380&t=st=1692306466~exp=1692307066~hmac=e2c5aa2322e53b5b6ded2c40b40c631b3662de1d5a4442ecca60944794ea12ed",
                    startDate = DateTime.Now.AddDays(1),
                    endDate = DateTime.Now.AddDays(5),
                    ChallengeStatusID = 1,
                    ChallengeTypeID = 1,
                    MedalId = 3,
                    IsArchived = false,



                };
                _context.Challenges.Add(VirtualTraveler);
                _context.SaveChanges();
                _context.DepartmentChallenges.Add(new DepartmentChallenge
                {
                    DepartmentId = 1,
                    ChallengeID = VirtualTraveler.ChallengeID,
                });
                _context.SaveChanges();


                var HomeWorkoutHeroes = new Challenge
                {
                    Id = users.Id,
                    Name = "Home Workout Heroes",
                    Tokens = 300,
                    Description = "et your sweat on! Share your home workout setup or a post-exercise selfie to earn Hubcoins. Show us your fitness routine and inspire others to stay active!",
                    Image = "https://img.freepik.com/free-vector/training-home-concept_52683-37093.jpg?w=1060&t=st=1692306636~exp=1692307236~hmac=9a4f5d178bc073ce28e3241d950de6b4577de37b7a6ce65a35681d9ad6b60772",
                    startDate = DateTime.Now.AddDays(2),
                    endDate = DateTime.Now.AddDays(10),
                    ChallengeStatusID = 1,
                    ChallengeTypeID = 1,
                    MedalId = 4,
                    IsArchived = false,
                };
                _context.Challenges.Add(HomeWorkoutHeroes);
                _context.SaveChanges();


                var BookNookDelights = new Challenge
                {
                    Id = users.Id,
                    Name = "Home Workout Heroes",
                    Tokens = 300,
                    Description = "Find your reading oasis! Share images of your cozy reading nook and the book you're diving into. Earn points for the most enchanting, relaxing, or imaginative reading spots.",
                    Image = "https://img.freepik.com/free-vector/woman-reading-illustration_114360-8536.jpg?w=740&t=st=1692306846~exp=1692307446~hmac=8366c39b38866ed62a39c89ca81e7a4e0695981d213d3a3fd163bc80dc74518a",
                    startDate = DateTime.Now.AddDays(3),
                    endDate = DateTime.Now.AddDays(20),
                    ChallengeStatusID = 1,
                    ChallengeTypeID = 1,
                    MedalId = 7,
                    IsArchived = false,



                };
                _context.Challenges.Add(BookNookDelights);
                _context.SaveChanges();

                //desktop warrior challenge
                var desktopwarrior = new Challenge
                {
                    Name = "Desktop Warrior",
                    Description = "Share a picture at the desk in the Hub",
                    endDate = DateTime.ParseExact("2023-11-07", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    startDate = DateTime.ParseExact("2023-09-26", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Tokens = 0,
                    Id = users.Id,
                    ChallengeStatusID = 1,
                    ChallengeTypeID = 3,
                    MedalId = 1,
                    Image = "https://www.alloffice.co.za/wp-content/uploads/2018/09/Workspace-Scene-01-1920.jpg",
                    PrizeId = 6,
                    IsArchived = false,

                };
                _context.Challenges.Add(desktopwarrior);
                _context.SaveChanges();

                //INF 370 challenge
                var INF370 = new Challenge
                {
                    Name = "INF370",
                    Description = "Not a fun challenge",
                    endDate = DateTime.ParseExact("2023-11-07", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    startDate = DateTime.ParseExact("2023-09-26", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Tokens = 0,
                    Id = users.Id,
                    ChallengeStatusID = 1,
                    ChallengeTypeID = 4,
                    MedalId = 1,
                    Image = "https://www.up.ac.za/media/shared/1/2019/about/ourstories.zp168959.jpg",
                    PrizeId = 6,
                    IsArchived = false,

                };
                _context.Challenges.Add(INF370);
                _context.SaveChanges();
                _context.DepartmentChallenges.Add(new DepartmentChallenge
                {
                    DepartmentId = 1,
                    ChallengeID = INF370.ChallengeID,
                });
                await _context.SaveChangesAsync();


                //INF370two challenge
                var INF370two = new Challenge
                {
                    Name = "INF370",
                    Description = "Not a fun challenge",
                    endDate = DateTime.ParseExact("2023-11-07", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    startDate = DateTime.ParseExact("2023-09-26", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Tokens = 0,
                    Id = users.Id,
                    ChallengeStatusID = 1,
                    ChallengeTypeID = 4,
                    MedalId = 1,
                    Image = "https://www.up.ac.za/media/shared/1/2019/about/ourstories.zp168959.jpg",
                    PrizeId = 6,
                    IsArchived = true,

                };
                _context.DepartmentChallenges.Add(new DepartmentChallenge
                {
                    DepartmentId = 1,
                    ChallengeID = INF370.ChallengeID,
                });
                await _context.SaveChangesAsync();
                _context.Challenges.Add(INF370two);
                _context.SaveChanges();


                //FriendlyHubster challenge
                var FriendlyHubster = new Challenge
                {
                    Name = "Friendly Neighbourhood Hubster",
                    Description = "Tag a friend on the social feed",
                    endDate = DateTime.ParseExact("2023-12-30", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    startDate = DateTime.ParseExact("2023-12-12", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Tokens = 0,
                    Id = users.Id,
                    ChallengeStatusID = 1,
                    ChallengeTypeID = 2,
                    MedalId = 1,
                    Image = "https://img.freepik.com/free-photo/medium-shot-happy-friends-city-lifestyle_23-2149003092.jpg?w=2000",
                    PrizeId = 6,
                    IsArchived = false,

                };
                _context.Challenges.Add(FriendlyHubster);
                _context.SaveChanges();
                _context.DepartmentChallenges.Add(new DepartmentChallenge
                {
                    DepartmentId = 1,
                    ChallengeID = FriendlyHubster.ChallengeID,
                });
                await _context.SaveChangesAsync();



                return Ok(users);
            }
            catch (Exception ex)
            {

                return BadRequest(new {Message = ex.Message});
            }
        }

        [HttpGet("GetChallenge/{id}")]
        public async Task<IActionResult> GetChallenge(int id)
        {
          var challenge = await _context.Challenges.FindAsync(id);

            if(challenge == null) 
            {
                return BadRequest();
            }

           

          if (challenge == null)
          {
            return NotFound();
          }

            return Ok(challenge);
        }

        //POST: /Challenge
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "REWARDARCHITECT")]
        [HttpPost("CreateChallenge")]
        public async Task<IActionResult> CreateChallenge(ChallengeViewModel challengeViewModel)
        {
            var httppUser = HttpContext.User;
            var rewardsArchitectId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id   
            var user = await _userManager.FindByIdAsync(rewardsArchitectId);

              var newChallenge = new Challenge
              {
                Name = challengeViewModel.name,
                Description= challengeViewModel.description,
                endDate = challengeViewModel.endDate,
                startDate = challengeViewModel.startDate, 
                ChallengeTypeID = challengeViewModel.ChallengeTypeId,
                Id = rewardsArchitectId,
                ChallengeStatusID = 1,  
                Image = challengeViewModel.Image,
                MedalId= challengeViewModel.MedalId,
              };

            if(challengeViewModel.Tokens != null || challengeViewModel.Tokens != 0) {
               newChallenge.Tokens = challengeViewModel.Tokens;
            }

            if (challengeViewModel.PrizeId != null || challengeViewModel.PrizeId != 0)
            {
                newChallenge.PrizeId= challengeViewModel.PrizeId;
            }
            //date validator
            if(newChallenge.startDate > newChallenge.endDate)
            {
                return BadRequest(new { message = "Start date cannot be after the end date" });
            }
            //challenge remain archived until start date
            if(newChallenge.startDate > DateTime.Now)
            {
                newChallenge.IsArchived = true;
                if (newChallenge.startDate == DateTime.Now)
                {
                    newChallenge.IsArchived = false;
                }
            }
            //challenge is archived after end date
            else if(newChallenge.endDate > DateTime.Now)
            {
                newChallenge.IsArchived = false;
                if (newChallenge.endDate == DateTime.Now)
                {
                    newChallenge.IsArchived = true;
                }
            }

            
            _context.Challenges.Add(newChallenge);
            await _context.SaveChangesAsync();

            //get awards architect department
            var architectDepartent = _context.Departments.Where(x => x.Id == rewardsArchitectId).FirstOrDefault();

            if (architectDepartent == null)
            {
                return BadRequest(new { message = "You have not been assigned a department. Please contact admin for further support" });
            }
            //create new department challenge object
            var departmentChallenge = new DepartmentChallenge
            {
                ChallengeID = newChallenge.ChallengeID,
                DepartmentId = architectDepartent.DepartmentId
            };
            //department challenge error
            if (departmentChallenge.ChallengeID == 0)
            {
                return BadRequest(new { message = "Unexpected error creating challenge" });
            }

            await _context.DepartmentChallenges.AddAsync(departmentChallenge);


            var auditTrail = new AuditTrail
            {
                UserId = user.Name + " " + user.Surname,
                Action = challengeViewModel.name + " Challenge Created",
                Timestamp = DateTime.Now,
                Amount = (int)challengeViewModel.Tokens,
                Quantity = 0

            };

            _context.AuditTrails.Add(auditTrail);
            _context.SaveChanges();

            //notify all challengers of new challenge
            var departmentUsers = _context.Challengers.Where(d=> d.DepartmentId == departmentChallenge.DepartmentId).ToList();
            var callbackUrl = "http://localhost:4200/user-challenges";


            foreach (var challenger in departmentUsers)
            {

                var message = new MimeMessage();
                message.From.Add(MailboxAddress.Parse(_configuration["Mail:Email"]));
                message.To.Add(new MailboxAddress("", challenger.Email));
                message.Subject = "New Challenge";


                var body = @$"<!DOCTYPE html>
                                <html>

                                <head>
                                  <title>New Challenge</title>
                                </head>

                                <body>
                                  <div >
                                      <div
                                        style='font-family: Arial, sans-serif; line-height: 1.6; color: #000000; max-width: 700px; margin: 0 auto; padding: 20px; border-radius: 15px; box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);'>
                                        <div style='text-align: center; font-size: 24px; margin-bottom: 30px;'><b>New Challenge!</b></div>
                                        <p>Hey <u>{challenger.Name}</u></p>
                                        <p>Your architect has posted a <b>new </b>challenge for you!</p>
                                      <div style=""flex-direction: row;"">
                                        <div>
          
                                          <p>The challenge details are as follows:</p>
                                          <p><b>Challenge Title:</b> {newChallenge.Name}</p>
                                          <p><b>Description:</b> {newChallenge.Description}</p>
                                          <p><b>End Date:</b> {newChallenge.endDate.ToShortDateString()}</p>
                                          <p>Participate in this exciting challenge to win HUbcoins and wonderful challenges</p>
                                        </div>
                                      </div>


                                        <p>
                                          <a href='{callbackUrl}' target='_blank'
                                            style='display: block; text-align: center; background-color: rgb(60, 60, 128); color: #fff; text-decoration: none; font-weight: bold; padding: 12px 20px; border-radius: 5px;'>Click here to view the challenge details</a>
                                        </p>
                                        <p>If you have any questions or need further assistance, feel free to reach out to us.</p>
                                        <p>Good luck, and may the best challenger win!</p>
                                        <div style='text-align: center; margin-top: 30px; color: #888;'>Thank you for being a part of Ignition,<br> The Ignition Team</div>
                                      </div>
                                  </div>


                                  <img style='display: block; margin:auto;max-width: 700px; color: #fff; text-decoration: none; font-weight: bold; padding: 12px 20px; border-radius: 5px;'
                                    src=""https://media.licdn.com/dms/image/C4D16AQHFV7iMoz5Uwg/profile-displaybackgroundimage-shrink_200_800/0/1632073639842?e=2147483647&v=beta&t=VLa8lJBfDrirR8tw_CV1RnSkFZsdnu-G3wqxso2WKsM"">

                                </body>

                                </html>";

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
            }

            await _context.SaveChangesAsync();
            return Ok(newChallenge);
        }

        //POST: /Challenge
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SUPERARCHITECT")]
        [HttpPost("SuperArchitectCreateChallenge")]
        public async Task<IActionResult> SuperArchitectCreateChallenge(ChallengeViewModel challengeViewModel)
        {
            var httppUser = HttpContext.User;
            var superArchitectId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id   
            var user = await _userManager.FindByIdAsync(superArchitectId);

            var newChallenge = new Challenge
            {
                Name = challengeViewModel.name,
                Description = challengeViewModel.description,
                endDate = challengeViewModel.endDate,
                startDate = challengeViewModel.startDate,
                ChallengeTypeID = challengeViewModel.ChallengeTypeId,
                Id = superArchitectId,
                ChallengeStatusID = 1,
                Image = challengeViewModel.Image,
                MedalId = challengeViewModel.MedalId,

            };

            if (challengeViewModel.Tokens != null || challengeViewModel.Tokens != 0)
            {
                newChallenge.Tokens = challengeViewModel.Tokens;
            }

            if (challengeViewModel.PrizeId != null || challengeViewModel.PrizeId != 0)
            {
                newChallenge.PrizeId = challengeViewModel.PrizeId;
            }
            _context.Challenges.Add(newChallenge);
            await _context.SaveChangesAsync();

            return Ok(newChallenge);
        }

        //PUT: /Challenge/5
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN, SUPERARCHITECT, REWARDARCHITECT")]
        [HttpPut("UpdateChallenge/{id}")]
        public async Task<IActionResult> UpdateChallenge(int id, UpdateChallengeVM challengeObj)
        {
                var challenge = await _context.Challenges.FindAsync(id);
                if(challenge == null)
                {
                    return BadRequest(new { message = "no challenge found" });
                }
                challenge.Image = challengeObj.Image;
                challenge.Description = challengeObj.description;
              
                challenge.endDate = challengeObj.endDate;
          
            await _context.SaveChangesAsync();

            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
            var user = await _userManager.FindByIdAsync(userId);

            var auditTrail = new AuditTrail
            {
                UserId = user.Name + " " + user.Surname,
                Action = challenge.Name + " Challenge Updated",
                Timestamp = DateTime.Now,
                Amount = 0,
                Quantity = 0

            };

            _context.AuditTrails.Add(auditTrail);
            _context.SaveChanges();

            return Ok(new {challenge.ChallengeID,challenge.Name, challenge.Description, challenge.endDate, challenge.Tokens, challenge.ChallengeTypeID});
        }

        // DELETE: /Challenge/5
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN, SUPERARCHITECT, REWARDARCHITECT")]
        [HttpGet("ArchiveChallenge/{id}")]
        public async Task<IActionResult> ArchiveChallenge(int id)
        {
            try
            {
            var challenge = await _context.Challenges.FindAsync(id);
            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
            var user = await _userManager.FindByIdAsync(userId);
              var auditTrail = new AuditTrail
            {
                UserId = user.Name + " " + user.Surname,
                Action = challenge.Name + " Challenge Archived",
                Timestamp = DateTime.Now,
                Amount = (int)challenge.Tokens,
                Quantity = 0

            };

            _context.AuditTrails.Add(auditTrail);
            _context.SaveChanges();
            if (challenge == null)
            {
                return NotFound();
            }
            challenge.IsArchived = true;
            await _context.SaveChangesAsync();

            return Ok(challenge);
            }

            catch (Exception ex )
            {

                return BadRequest( new {Error = ex.Message});
            }

        }
        //-------------------------------------------------------------//

        [HttpGet]
        [Route("GetAllChallengeTypes")]
        public async Task<IActionResult> GetAllChallType()
        {
            var challengeTypes = await _context.ChallengeTypes.ToListAsync();

            return Ok(challengeTypes);
        }

        //Enroll in challenge (Create new challenge instance) with status "incomplete" or something.
        [HttpGet]
        [Route("EnrollChall/")]
        public async Task<IActionResult> EnrollChall(int challengeID, string challengerID)
        {
            try
            {
                //Testing the passed ID's
                if (challengeID == null || _context.Challenges == null)
                {
                    return NotFound("Pls check challenge ID.");
                }

                //Fetching and testing the related challenge
                var challenge = await _context.Challenges.FindAsync(challengeID);

                if (challenge == null)
                {
                    return NotFound("Challenge not found.");
                }

                //Fetching and testing the related challenger
                if (challengerID == null || _context.Challengers == null)
                {
                    return NotFound("Pls check challenger ID.");
                }

                var challenger = await _context.Challengers.FindAsync(challengerID);

                if (challenger == null)
                {
                    return NotFound("Challenger not found.");
                }

                //If challenge found and challenger found instantiate an new challenge instance
                //and push it to the DB
                var challengeInstance = new ChallengeInstance
                {
                    //ChallengeId = challenge.ChallengeId,
                    //ChallengerId = challenger.ChallengerId,
                    Challenge = challenge,
                    Challenger = challenger,
                    ChallengeInstanceStatusId = 1,
                };

                //Confirm that the newly created challenge instance is not null
                if (challengeInstance == null)
                {
                    return NotFound("Challenge instance is null for some reason.");
                }

                //First check if the cahlelnge instance doesn't already exist
                //var testInstance = await _context.ChallengeInstances.Select(x => x).Where(x => x.ChallengeId == challengeID && x.ChallengerId == challengerID).ToArrayAsync();

                //if (testInstance != null || testInstance.Length != 0)
                //{
                //    return BadRequest("The challenge instance already exists.");
                //}

                await _context.ChallengeInstances.AddAsync(challengeInstance);

                //challenge.ChallengeInstances.Add(challengeInstance);

                //challenger.ChallengeInstances.Add(challengeInstance);

                //Problem arises when attempting to save to DB.
                await _context.SaveChangesAsync();

                return Ok(challengeInstance);
            }
            catch
            {
                return BadRequest("Something went wrong when attempting to enroll for the challenge.");
            }
        }

        //Delete challenge 
        [HttpGet("GetChallengesForUser/{userEmail}")]
        public async Task<IActionResult> GetAllChallenges(string userEmail)
        {
            var departmentId = await _context.Challengers.
                Where(u => u.Email == userEmail)
                .Select(d => d.DepartmentId)
                .FirstOrDefaultAsync();

            var challengeList = await _context.Challenges
                .Where(dc => dc.DepartmentChallenges.Any(dc => dc.DepartmentId == departmentId))
                .ToListAsync();

            return Ok(challengeList);
        }

        [HttpPost("AddToDepartmentChallenges/{userID}/{challengeID}")]
        public async Task<IActionResult> AddToDepartmentChallenge(string userID, int challengeID)
        {
            var DepartmentId = await _context.Challengers.
                    Where(r => r.Id == userID)
                    .Select(r => r.DepartmentId).FirstOrDefaultAsync();

            var challengeDep = new DepartmentChallenge
            {
                ChallengeID = challengeID,
              /*  DepartmentID? = DepartmentId*/
            };

            if(challengeDep == null) 
            {
                return BadRequest(new { Message = "There has been an error" });
            }

            await _context.DepartmentChallenges.AddAsync(challengeDep);
            await _context.SaveChangesAsync();

            return Ok(challengeDep);

        }
        
        [HttpGet("GetChallengeType/{id}")]
        public async Task<IActionResult> GetChallengeTypeById(int id)
        {
            var challengeType= await _context.ChallengeTypes.FindAsync(id);
            if(challengeType == null) 
            {
            return NotFound();
            }
            return Ok(new {challengeType.Name, challengeType.ChallengeTypeID});
        }

        // Actual get all challenges endpoint
        [HttpGet("GetAllChallengesActual")]
        public async Task<IActionResult> GetAllChallengesActual()
        {
            var challengeList = await _context.Challenges
                .ToListAsync();
            return Ok(challengeList);
        }
        //[HttpGet("AutoArchiveChallenge")]
        //public async void AutoArchiveChallenge()
        //{
        //    var challenges = _context.Challenges.ToListAsync();
        //    foreach (var challenge in challenges.Result)
        //    {
        //        //if start date is ahead of current date challenge must be archived
        //        if (challenge.endDate < DateTime.Now)
        //        {
        //            challenge.IsArchived = true;
        //            await _context.SaveChangesAsync();
        //        }
        //        //if start date is ahead of current date challenge must be archived
        //        else if(challenge.startDate > DateTime.Now)
        //        {
        //            challenge.IsArchived = true;
        //            await _context.SaveChangesAsync();
        //        }
        //    }

        //}


           
        }

    }
