using BMWIgnition_API.Data;
using BMWIgnition_API.Model;
using Microsoft.AspNetCore.Mvc;

namespace BMWIgnition_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmojiController : Controller
    {
        private readonly List<Emoji> _emojis;
        private readonly AppDbContext _appDbContext;
        public EmojiController(AppDbContext appDbContext)
        {
            _emojis = new List<Emoji>();
            _appDbContext = appDbContext;
        }

        // GET: api/Emoji
        [HttpGet]
        public ActionResult<IEnumerable<Emoji>> GetEmojis()
        {
            var results = _appDbContext.Emojis.ToList();
            return Ok(results);
        }

        // GET: api/Emoji/5
        [HttpGet("{id}")]
        public ActionResult<Emoji> GetEmojiById(int id)
        {
            var emoji = _appDbContext.Emojis.Find(id);
            if (emoji == null)
            {
                return NotFound();
            }

            return Ok(emoji);
        }

        // POST: api/Emoji
        [HttpPost]
        public ActionResult<Emoji> CreateEmoji(Emoji emoji)
        {
            
            _appDbContext.Emojis.Add(emoji);
            _appDbContext.SaveChanges();
            return CreatedAtAction(nameof(GetEmojiById), new { id = emoji.Id }, emoji);
        }

        // PUT: api/Emoji/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmoji(int id, Emoji emoji)
        {
            var existingEmoji = await _appDbContext.Emojis.FindAsync(id);
            if (existingEmoji == null)
            {
                return NotFound();
            }

            existingEmoji.ImageBase64 = emoji.ImageBase64;
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE: api/Emoji/5
        [HttpDelete("{id}")]
        public IActionResult DeleteEmoji(int id)
        {
            var existingEmoji = _appDbContext.Emojis.Find(id);
            if (existingEmoji == null)
            {
                return NotFound();
            }

            _appDbContext.Emojis.Remove(existingEmoji);
            _appDbContext.SaveChanges();

            return Ok( new { message = "deleted " + existingEmoji.ImageBase64  });
        }

        // Helper method to generate a unique ID
        private int GetNextId()
        {
            int maxId = _emojis.Count > 0 ? _emojis.Max(e => e.Id) : 0;
            return maxId + 1;
        }
    }

 

}
