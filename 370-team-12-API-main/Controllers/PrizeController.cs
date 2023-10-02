using BMWIgnition_API.Data;
using BMWIgnition_API.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BMWIgnition_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrizeController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Challenger> _userManager;

        public PrizeController(AppDbContext context, IConfiguration configuration, UserManager<Challenger> userManager)

        {
            _configuration = configuration;
            _context = context;
            _userManager = userManager;
        }

       
    }
}
