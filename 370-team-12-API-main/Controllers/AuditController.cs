using BMWIgnition_API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BMWIgnition_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public AuditController(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        [HttpGet("GetAllAudits")]
        public async Task<IActionResult> GetAuditTrailEntries()
        {
            var auditTrailEntries = await _appDbContext.AuditTrails
                .OrderByDescending(entry => entry.Timestamp)
                .ToListAsync();

            return Ok(auditTrailEntries);
        }
    }
}
