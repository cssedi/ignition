using BMWIgnition_API.Data;
using BMWIgnition_API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BMWIgnition_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<Challenger> _userManager;

        public DatabaseController(AppDbContext appDbContext, UserManager<Challenger> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;

        }
        [Authorize(AuthenticationSchemes ="Bearer",Roles ="ADMIN")]
        [HttpGet("BackUpDatabase")]
        public async Task<IActionResult> BackUpDatabase()
        {
            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
            var user = await _userManager.FindByIdAsync(userId);

            string backupFilePath = @"C:\Path\To\BackupAndRestore\backup.bak";
            var databaseService = new DatabaseService(_appDbContext);
            // Check if the directory of the backup file exists, if not, create it
            string directoryPath = Path.GetDirectoryName(backupFilePath);
            if (!Directory.Exists(directoryPath))
            {
                try
                {
                    Directory.CreateDirectory(directoryPath);
                    //databaseService.BackupDatabase(directoryPath);

                    var auditTrail = new AuditTrail
                    {
                        UserId = user.Name + " " + user.Surname,
                        Action = "Backed up database",
                        Timestamp = DateTime.Now,
                        Amount = 0,
                        Quantity = 0
                    };

                    await _appDbContext.AuditTrails.AddAsync(auditTrail);
                    await _appDbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return BadRequest( new { Message = $"Error creating directory: {ex.Message}" });
                }
            }
            else
            {
                var auditTrail = new AuditTrail
                {
                    UserId = user.Name + " " + user.Surname,
                    Action = "Backed up database",
                    Timestamp = DateTime.Now,
                    Amount = 0,
                    Quantity = 0
                };

                await _appDbContext.AuditTrails.AddAsync(auditTrail);
                await _appDbContext.SaveChangesAsync();

            }
            return Ok(new {Message ="Back up created succesfully"});
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [HttpGet("RestoreDatabase")]
        public async Task<IActionResult> RestoreDatabase()
        {
            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
            var user = await _userManager.FindByIdAsync(userId);

            string backupFilePath = @"C:\Path\To\BackupAndRestore\backup.bak";
            var databaseService = new DatabaseService(_appDbContext);
            try
            {
                databaseService.RestoreDatabase(backupFilePath);
                var auditTrail = new AuditTrail
                {
                    UserId = user.Name + " " + user.Surname,
                    Action = "Restored database",
                    Timestamp = DateTime.Now,
                    Amount = 0,
                    Quantity = 0
                };

                await _appDbContext.AuditTrails.AddAsync(auditTrail);
                await _appDbContext.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "An Unexpected error has occurred. Please contact developer support" });
            }
            return Ok(new {Message = "Restore Completed successfully"});
        }

        [HttpGet("GetDatabaseAuditTrailEntries")]
        public async Task<IActionResult> GetDatabaseAuditTrailEntries()
        {
            //get specific audit trail entries
            var DbAudits = await _appDbContext.AuditTrails
                .Where(entry => entry.Action.ToLower() == "Backed up database".ToLower() || entry.Action.ToLower() == "Restored database".ToLower())
                .OrderByDescending(entry => entry.Timestamp)
                .ToListAsync();
            //error handling
            if (DbAudits == null)
            {
                return NotFound();
            }

            return Ok(DbAudits);
        }


    }
}
