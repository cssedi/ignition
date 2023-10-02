using AutoMapper;
using BMWIgnition_API.Data;
using BMWIgnition_API.Model;
using BMWIgnition_API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Security.Claims;

namespace BMWIgnition_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FunctionController : Controller
    {
        private readonly IRepository _Repository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        private readonly UserManager<Challenger> _userManager;

        public FunctionController(IRepository iRepository, IMapper mapper, AppDbContext appDbContext, UserManager<Challenger> userManager)
        {
            _mapper = mapper;
            _Repository = iRepository;
            _context = appDbContext;
            _userManager = userManager;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "CHALLENGER,REWARDARCHITECT,ADMIN,SUPERARCHITECT")]
        [HttpGet]
        [Route("GetAllFunctions")]
        public async Task<IActionResult> GetAllFunctions()
        {
            try
            {
                var query = _context.Functions.Include(x => x.Departments).Include(x => x.SuperArchitect).Select(f => new
                {
                    functionId = f.FunctionId,
                    functionCode = f.FunctionCode,
                    functionName = f.Name,
                    superArchitectUsername = f.SuperArchitect.UserName,
                    superArchitectName = f.SuperArchitect.Name + " " + f.SuperArchitect.Surname,
                    departments = f.Departments
                });

                var httppUser = HttpContext.User;
                var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
                var user = await _context.Challengers.FirstOrDefaultAsync(x => x.Id == userId);

                var auditTrail = new AuditTrail
                {
                    UserId = user.Name + " " + user.Surname,
                    Action = "Functions Viewed",
                    Timestamp = DateTime.Now,
                    Amount = 0,
                    Quantity = 0

                };

                _context.AuditTrails.Add(auditTrail);
                _context.SaveChanges();


                return Ok(query);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }
        [HttpGet]
        [Route("SearchFunctions")]
        public async Task<IActionResult> SearchFunctions(string searchTerm)
        {
            var functions = await _Repository.SearchFunctions(searchTerm);


            return Ok(functions);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetFunctionById([FromRoute] int id)
        {
            try
            {
                var query = _context.Functions.Where(f => f.FunctionId == id).Include(x => x.Departments).Include(x => x.SuperArchitect).Select(f => new
                {
                    functionId = f.FunctionId,
                    functionCode = f.FunctionCode,
                    functionName = f.Name,
                    superArchitectId = f.SuperArchitect.Id,
                    superArchitectName = f.SuperArchitect.Name + " " + f.SuperArchitect.Surname,
                    departments = f.Departments,
                    superArchitect= f.SuperArchitect
                }).FirstOrDefault();
                return Ok(query);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [Route("AddFunction")]
        [HttpPost()]
        public async Task<IActionResult> AddFunction(FunctionViewModel functionViewModel)
        {
            try
            {
                var httppUser = HttpContext.User;
                var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
                var user = await _userManager.FindByIdAsync(userId);



                var func = new Function()
                {
                    Name = functionViewModel.FunctionName,
                    Id = functionViewModel.SuperArchitectId,
                    FunctionCode = functionViewModel.FunctionCode,

                };


                _Repository.AddFunction(func);
                _Repository.SaveAll();

                var auditTrail = new AuditTrail
                {
                    UserId = user.Name + " " + user.Surname, // Replace with the actual user ID
                    Action = "Function " + func.Name + " Created",
                    Timestamp = DateTime.Now,
                    Amount = 0,
                    Quantity = 0

                };

                _context.AuditTrails.Add(auditTrail);
                _context.SaveChanges();

                return Ok(func);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
    

        [HttpDelete("DeleteFunction/{id}")]

        public async Task<IActionResult> DeleteFunction(int id)
        {
            var func = _Repository.GetFunctionById(id);
            var name = func.Name;

            if (func == null)
            {
                return NotFound("Resource not found.");
            }

            var departmentFunctions = await _context.Departments.Where(x => x.FunctionId == func.FunctionId).ToListAsync();
            if (departmentFunctions.Count() > 0)
            {
                return BadRequest(new { Message = "Cannot delete department with sub-departments assigned to it" });
            }

            _Repository.DeleteFunction(func);
            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
            var user = await _userManager.FindByIdAsync(userId);
            var userName = user.UserName;


            var auditTrail = new AuditTrail
            {
                UserId = user.Name + " " + user.Surname, // Replace with the actual user ID
                Action = "Function" + name + "Deleted",
                Timestamp = DateTime.Now,
                Amount = 0,
                Quantity = 0

            };
            _context.AuditTrails.Add(auditTrail);
            _context.SaveChanges();

            return Ok(func);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFunction([FromRoute] int id, [FromBody] FunctionViewModel functionViewModel)
        {
            try
            {

                var func = await _context.Functions.FindAsync(id);

                if (func == null)
                {
                    return NotFound();
                }

                func.Name = functionViewModel.FunctionName;
                func.FunctionCode = functionViewModel.FunctionCode;
                func.Id = functionViewModel.SuperArchitectId;

                await _context.SaveChangesAsync();

                var httppUser = HttpContext.User;
                var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
                var user = await _userManager.FindByIdAsync(userId);
                var userName = user.UserName;

                var auditTrail = new AuditTrail
                {
                    UserId = user.Name + " " + user.Surname, // Replace with the actual user ID
                    Action = "Function " + func.Name + " Updated",
                    Timestamp = DateTime.Now,
                    Amount = 0,
                    Quantity = 0

                };
                _context.AuditTrails.Add(auditTrail);
                _context.SaveChanges();

                return Ok(func);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{functionId}/departments")]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartmentsForFunction(int functionId)
        {
            var departments = await _Repository.Getfunctiondepartments(functionId);

            return departments;
        }


    }
}
