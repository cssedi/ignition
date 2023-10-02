using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BMWIgnition_API.Data;
using BMWIgnition_API.Model;
using Microsoft.CodeAnalysis;
using System;

namespace BMWIgnition_API.Controllers
{
    //Note: This controller will likely be the Challenges controller, or the Admin controller - depending on how we decide to go about structuring the API.
    //Having a controller for each entity/entity type does not make sense. A logical grouping would be subsystem-based.
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public AdminController(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        [HttpGet]
        [Route("GetAllChallType")]
        public async Task<IActionResult> GetAllChallType()
        {
            try
            {
                var challengeTypes = await _appDbContext.ChallengeTypes.ToListAsync();
                if(challengeTypes == null)
                {
                    return BadRequest("The challenge types table retrieved from the DB is null.");
                }

                return Ok(challengeTypes);
            }
            catch(Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet]
        [Route("GetChallType/{id}")]
        public async Task<IActionResult> GetChallType(int? id)
        {
            try
            {
                if (id == null || _appDbContext.ChallengeTypes == null)
                {
                    return NotFound();
                }

                var challengeType = await _appDbContext.ChallengeTypes.FindAsync(id);

                if (challengeType == null)
                {
                    return NotFound();
                }

                return Ok(challengeType);
            }
            catch
            {
                return BadRequest("Something went wrong when retrieving the Challenge Type from the Database.");
            }
        }

        //Create Challenge Type by passing a challenge name and description.
        //Still need to add field for the description actually.
        //ChallengeTypeID is created automatically.
        [HttpPost]
        [Route("CreateChallType")]
        public async Task<IActionResult> CreateChallType(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Challenge name cannot be empty.");
            }

            var challengeType = new ChallengeType { Name = name };

            _appDbContext.ChallengeTypes.Add(challengeType);
            await _appDbContext.SaveChangesAsync();

            //Returns 201 status code with a URI for the newly created resource in the Location header of the response.URI is generated using CreateChallType action method and includes ID of the newly created challenge type. challengeType.ChallengeTypeID property also returned as body of response.

            return CreatedAtAction(nameof(CreateChallType), new { id = challengeType.ChallengeTypeID }, challengeType.ChallengeTypeID);
        }

        //Edit Challenge Type
        //This method retrieves the challenge in question by using the ID, and then stores the updated info in the DB overwriting the ChallType's old info.
        [HttpPut]
        [Route("EditChallType")]
        public async Task<IActionResult> EditChallengeType(int id, string name)
        {
            var challengeType = await _appDbContext.ChallengeTypes.FindAsync(id);

            if (challengeType == null)
            {
                return NotFound();
            }

            challengeType.Name = name;
            await _appDbContext.SaveChangesAsync();

            return Ok(challengeType);
        }

        //Delete Challenge Type
        [HttpDelete]
        [Route("DelChallType")]
        public async Task<IActionResult> DelChallType(int id)
        {
            if (_appDbContext.ChallengeTypes == null)
            {
                return Problem("Entity set 'AppDbContext.ChallengeTypes'  is null.");
            }

            var challengeType = await _appDbContext.ChallengeTypes.FindAsync(id);

            if (challengeType != null)
            {
                _appDbContext.ChallengeTypes.Remove(challengeType);
            }

            await _appDbContext.SaveChangesAsync();

            return Ok("The requested Challenge Type has been deleted from the DB.");
        }

        private bool ChallengeTypeExists(int id)
        {
            return (_appDbContext.ChallengeTypes?.Any(e => e.ChallengeTypeID == id)).GetValueOrDefault();
        }
    }
}
