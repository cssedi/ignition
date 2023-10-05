using BMWIgnition_API.Data;
using BMWIgnition_API.Model;
using BMWIgnition_API.ViewModels;
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
    public class RewardController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<Challenger> _userManager;
        public RewardController(AppDbContext appDbContext, UserManager<Challenger> userManager)
        {
            this._appDbContext = appDbContext;
            _userManager = userManager;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "CHALLENGER,REWARDARCHITECT,ADMIN,SUPERARCHITECT")]
        [HttpGet]
        [Route("GetAllRewards")]
        public async Task<IActionResult> GetAllRewards()
        {
            var prizes = await _appDbContext.Prizes.Include(po=>po.PrizeOrders).OrderBy(p => p.Price).ToListAsync();

            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
            var user = _appDbContext.Challengers.FirstOrDefault(x => x.Id == userId);


            var auditTrail = new AuditTrail
            {
                UserId = user.Name + " " + user.Surname,
                Action = "Rewards Viewed",
                Timestamp = DateTime.Now,
                Amount = 0,
                Quantity = 0

            };

            _appDbContext.AuditTrails.Add(auditTrail);
            _appDbContext.SaveChanges();

            //var prizes = await _appDbContext.Prizes.Include( po => po.PrizeOrders).OrderBy(p => p.Price).ToListAsync();
            return Ok(prizes);
        }
        [HttpGet]
        [Route("GetReward/{id}")]
        public async Task<IActionResult> GetlReward(int id)
        {
            var prize = await _appDbContext.Prizes.FindAsync(id);
            if (prize == null)
            {
                return NotFound();
            }
            prize.PrizeID = id;
            return Ok(prize);

        }
        [HttpGet]
        [Route("GetAllRewardTypes")]
        public async Task<IActionResult> GetAllRewardTypes()
        {
            var prizes = await _appDbContext.PrizeTypes.ToListAsync();
            return Ok(prizes);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [HttpPost()]
        public async Task<IActionResult> CreateReward( PrizeVM prizeVM)
        {
            try
            {
                var prize = new Prize
                {
                    Name = prizeVM.Name,
                    Price = prizeVM.Tokens,
                    Description = prizeVM.Description,
                    FrontImgURL = prizeVM.Image,
                    BackImgURL = prizeVM.Image,
                    PrizeTypeID = prizeVM.PrizeTypeId,


                };

                _appDbContext.Prizes.Add(prize);
                await _appDbContext.SaveChangesAsync();

                var httppUser = HttpContext.User;
                var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
                var user = await _userManager.FindByIdAsync(userId);


                var auditTrail = new AuditTrail
                {
                    UserId = user.Name + " " + user.Surname,
                    Action = prizeVM.Name + " reward created",
                    Timestamp = DateTime.Now,
                    Amount = prizeVM.Tokens,
                    Quantity = 1

                };

                _appDbContext.AuditTrails.Add(auditTrail);
                _appDbContext.SaveChanges();

                return Ok(new { Message = prize.Name + " was create successfully"});
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> UpdatePrize(int id)
        {
            var prize = await _appDbContext.Prizes.FindAsync(id);
            _appDbContext.Prizes.Remove(prize);
            await _appDbContext.SaveChangesAsync();

            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
            var user = await _userManager.FindByIdAsync(userId);


            var auditTrail = new AuditTrail
            {
                UserId = user.Name + " " + user.Surname,
                Action = prize.Name + " reward deleted",
                Timestamp = DateTime.Now,
                Amount = prize.Price,
                Quantity = 1

            };

            _appDbContext.AuditTrails.Add(auditTrail);
            _appDbContext.SaveChanges();

            //deletion rules
            var challenges = await _appDbContext.Challenges.Where(x => x.PrizeId == prize.PrizeID).Where(x => x.IsArchived == true).ToListAsync();
            if (challenges.Count() > 0)
            {
                return BadRequest(new { message = "Cannot delete Prize with active challenges!" });
            }
            //waiting for prizeOrderStatuses to be seeded
            //var prizeOrders = await _appDbContext.PrizeOrders.Where(x => x.PrizeId == prize.PrizeID).Where(x => x.PrizeOrderStatusId == 1)ToListAsync();
            //if (challenges.Count() > 0)
            //{
            //    return BadRequest(new { message = "Cannot delete Prize with active challenges!" });
            //}


            return Ok(new { Message = prize.Name + "has been deleted" });
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [HttpPut("UpdatePrize/{id}")]
        public async Task<IActionResult> UpdatePrize(int id,  PrizeVM updatedPrize)
        {
            try
            {

                var prize = await _appDbContext.Prizes.FindAsync(id);
                prize.Description = updatedPrize.Description;
                prize.Price = updatedPrize.Tokens;
                prize.Name = updatedPrize.Name;
                prize.FrontImgURL= updatedPrize.Image;
                prize.PrizeTypeID = updatedPrize.PrizeTypeId; 

                await _appDbContext.SaveChangesAsync();

                var httppUser = HttpContext.User;
                var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
                var user = await _userManager.FindByIdAsync(userId);


                var auditTrail = new AuditTrail
                {
                    UserId = user.Name + " " + user.Surname,
                    Action = updatedPrize.Name + " reward updated",
                    Timestamp = DateTime.Now,
                    Amount = updatedPrize.Tokens,
                    Quantity = 1

                };

                _appDbContext.AuditTrails.Add(auditTrail);
                _appDbContext.SaveChanges();

                return Ok(new { Message = prize.Name + "updated Succesfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

    
