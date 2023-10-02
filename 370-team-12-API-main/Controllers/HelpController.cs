using BMWIgnition_API.Data;
using BMWIgnition_API.Model;
using BMWIgnition_API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;

namespace BMWIgnition_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelpController : ControllerBase
    {
        private readonly AppDbContext _context;
        public HelpController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("Get")]
        public IActionResult GetAllHelp()
        {
            var help = _context.Helps.Include(x => x.Location).ToList();
            return Ok(help);
        }

        [HttpGet("GetLocations")]
        public IActionResult GetLocations()
        {
            var locations = _context.Locations.ToList();
            return Ok(locations);
        }

        [HttpGet("GetContextualHelp/{location}")]
        public IActionResult GetContextualHelp(string location)
        {
            try
            {
                var current = location;
                var helpList = _context.Helps.Where(x => x.Location.Name == location).ToList();

                if (helpList.Count == 0)
                {
                    return NotFound();
                }
                return Ok(helpList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // This one is never used. Consider removing.
        // Get specific help
        [HttpGet("{id}")]
        public IActionResult GetSpecificHelp(int id)
        {
            var help = _context.Helps.Find(id);
            if (help == null)
                return NotFound();

            return Ok(help);
        }

        [HttpPost("CreateHelp")]
        public IActionResult Post([FromBody] HelpDto halp)
        {
            // The new help is not being created properly here, it is null            
            var help = new Help
            {
                Name = halp.Name,
                Description = halp.Description,
                LocationId = halp.LocationId,
            };

            _context.Helps.Add(help);

            // Why is this not saving
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAllHelp), new { id = help.HelpId }, help);
        }

        [HttpPut("Update/{id}")]
        public IActionResult Put(int id, [FromBody] HelpDto helpTemp) // Api doesn't receive new help info properly?
        {
            try
            {
                Help help = _context.Helps.Find(id);

                if (help == null)
                {
                    return NotFound();
                }
                
                help.Name = helpTemp.Name;
                help.Description = helpTemp.Description;
                help.LocationId = helpTemp.LocationId;

                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteHelp/{id}")]
        public IActionResult Delete(int id)
        {
            var help = _context.Helps.Find(id);
            if (help == null)
                return Ok(new { Messaage = "Help not found" });

            _context.Helps.Remove(help);
            _context.SaveChanges();

            return Ok(new { Message = "Help Deleted" });
        }
    }
}