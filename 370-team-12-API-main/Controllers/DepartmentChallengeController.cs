using BMWIgnition_API.Data;
using BMWIgnition_API.Model;
using BMWIgnition_API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Cmp;
using System.ComponentModel;
using System.Data;
using System.Net.WebSockets;
using System.Security.Claims;

namespace BMWIgnition_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentChallengeController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Challenger> _userManager;

        public DepartmentChallengeController(AppDbContext context, UserManager<Challenger> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/DepartmentChallenge
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "CHALLENGER")]
        [HttpGet("GetChallengerChallenges")]
        public async Task<ActionResult> GetDepartmentChallenges()
        {
            var user = HttpContext.User;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id

            var challenger = await _userManager.FindByIdAsync(userId);
            var departmentChallenges =  _context.DepartmentChallenges.
                Include(c => c.Challenge).
                ThenInclude(u => u.User).ThenInclude( c =>c.ChallengeInstances).
                Include( x=> x.Challenge.Prize).Include( m => m.Challenge.Medal)    
                .Where( c => c.DepartmentId == Convert.ToInt32(challenger.DepartmentId) && c.Challenge.IsArchived == false)
                .ToList();

            


            var challengeInstamces = _context.ChallengeInstances.ToList();
            var results = new List<Object>();

            foreach ( var department in departmentChallenges)
            {
                bool isEnrolled = false;
               for(int i = 0; i < challengeInstamces.Count; i++)
                {
                    if(department.DepartmentId == challengeInstamces[i].ChallengeID  && challengeInstamces[i].ChallengerId == userId)
                    {
                        isEnrolled= true;
                    }
                }
                // Calculate the time left as a TimeSpan and subtract one second
                var timeLeft = department.Challenge.endDate - DateTime.Now - TimeSpan.FromSeconds(1);

                // Format the TimeSpan into the desired format
                var formattedCountdown = $"{(int)timeLeft.TotalDays} days {timeLeft.Hours} hours {timeLeft.Minutes} minutes {timeLeft.Seconds} seconds";

                // Assign the formatted countdown to the Challenge's property
                department.Challenge.countdown = formattedCountdown;

                await _context.SaveChangesAsync();
                if (isEnrolled == false)
                {
                    results.Add(department);
                }
            }
            //archive challenge if required
            AutoArchiveChallenge();
            return Ok(results);
        }

        // GET: api/DepartmentChallenge
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "REWARDARCHITECT")]
        [HttpGet("GetDepartmentChallenge")]
        public async Task<ActionResult> GetDepartmentChallenge()
        {
            var user = HttpContext.User;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id
            var rewardArchitect = await _userManager.FindByEmailAsync(userId);                                                                        // 
            if (Convert.ToInt32(rewardArchitect.DepartmentId) == 0)
            {

            }

            var departmentId = Convert.ToInt32(rewardArchitect.DepartmentId);
            var departmentChallenge =  _context.DepartmentChallenges.Include( c=> c.Challenge).Where( x => x.DepartmentId == departmentId).Select( d => new
            {
                DepartmentId = departmentId,
                Challenge = d

            });

            if (departmentChallenge == null)
            {
                return NotFound();
            }

            return Ok( new { challenges = departmentChallenge});
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartmentChallenge(int id)
        {
            var departmentChallenge = await _context.DepartmentChallenges.FindAsync(id);
            if (departmentChallenge == null)
            {
                return NotFound();
            }

            _context.DepartmentChallenges.Remove(departmentChallenge);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // POST: api/DepartmentChallenge
        

        // PUT: api/DepartmentChallenge/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartmentChallenge(int id, DepartmentChallenge departmentChallenge)
        {
            if (id != departmentChallenge.ChallengeID)
            {
                return BadRequest();
            }

            _context.Entry(departmentChallenge).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
            }

            return NoContent();
        }

        [HttpGet("GetDepartmentChallengeReportsById/{id}")]
        public async Task<IActionResult> GetDepartmentChallengeReports(int id)
        {
            var departmentChallenges = await _context.DepartmentChallenges.Where(ct=> ct.Challenge.ChallengeTypeID == id).ToListAsync();
            //list of departmentChallenge Count
            var departmentVMList = new List<departmentChallengeViewModelReports>();

            //Get list of departments
            var departments = await _context.Departments.ToListAsync();
            foreach (var department in departments)
            {
                //get count of challenges for each department
                var departmentChallengeCount = _context.DepartmentChallenges.Where(dc => dc.DepartmentId == department.DepartmentId && dc.Challenge.ChallengeTypeID == id).Count();
                departmentChallengeViewModelReports model = new departmentChallengeViewModelReports()
                {
                    DepartmentCode = department.DepartmentCode,
                    Count = departmentChallengeCount,
                    department = department,
                };

                // only add departments with challenges created
                if(departmentChallengeCount > 0) 
                {
                    departmentVMList.Add(model);
                }
            }

            //if on page load or if 'All' select list option provided do this
            if (id == 0)
            {
                departmentChallenges = await _context.DepartmentChallenges.ToListAsync();
                //list of departmentChallenge Count
                departmentVMList = new List<departmentChallengeViewModelReports>();

                //Get list of departments
                departments = await _context.Departments.ToListAsync();
                foreach (var department in departments)
                {
                    //get count of challenges for each department
                    var departmentChallengeCount = _context.DepartmentChallenges.Where(dc => dc.DepartmentId == department.DepartmentId).Count();
                    departmentChallengeViewModelReports model = new departmentChallengeViewModelReports()
                    {
                        DepartmentCode = department.DepartmentCode,
                        Count = departmentChallengeCount,
                        department = department,
                    };

                    // only add departments with challenges created
                    if (departmentChallengeCount > 0)
                    {
                        departmentVMList.Add(model);
                    }
                }
            }

            return Ok(departmentVMList);
        }
        [HttpGet("GetDepartmentChallengeReports")]
        public async Task<IActionResult> GetDepartmentChallengeReports()
        {
            var departmentChallenges = await _context.DepartmentChallenges.ToListAsync();
            //list of departmentChallenge Count
            var departmentVMList = new List<departmentChallengeViewModelReports>();

            if (departmentChallenges == null || departmentChallenges.Count == 0)
            {
                return NotFound(new { Message = "No Department Challenges found!" });
            }
            //Get list of departments
            var departments = await _context.Departments.ToListAsync();
            foreach (var department in departments)
            {
                //get count of challenges for each department
                var departmentChallengeCount = _context.DepartmentChallenges.Where(dc => dc.DepartmentId == department.DepartmentId).Count();
                departmentChallengeViewModelReports model = new departmentChallengeViewModelReports()
                {
                    DepartmentCode = department.DepartmentCode,
                    Count = departmentChallengeCount,
                    department = department,
                };

                // only add departments with challenges created
                if (departmentChallengeCount > 0)
                {
                    departmentVMList.Add(model);
                }
            }

            return Ok(departmentVMList);
        }

        [HttpPost("CreateDepartmentChallenge")]
        public async Task<IActionResult> CreateDepartmentChallenge(departmentChallengeVM departmentChallengeVM)
        {
            var departmentChallenge = new DepartmentChallenge()
            {
                DepartmentId = departmentChallengeVM.departmentId,
                ChallengeID = departmentChallengeVM.challengeId
            };

            await _context.DepartmentChallenges.AddAsync(departmentChallenge);
            await _context.SaveChangesAsync();
            return Ok(new { departmentId= departmentChallenge.DepartmentId, challengeId= departmentChallenge.DepartmentId});
        }

        [HttpGet("AutoArchiveChallenge")]
        public async void AutoArchiveChallenge()
        {
            var challenges = _context.Challenges.ToListAsync();
            foreach (var challenge in challenges.Result)
            {
                //if start date is ahead of current date challenge must be archived
                if (challenge.endDate < DateTime.Now)
                {
                    challenge.IsArchived = true;
                    await _context.SaveChangesAsync();
                }
                //if start date is ahead of current date challenge must be archived
                else if (challenge.startDate > DateTime.Now)
                {
                    challenge.IsArchived = true;
                    await _context.SaveChangesAsync();
                }
            }

        }

    }
}
