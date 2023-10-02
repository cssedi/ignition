using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using BMWIgnition_API.Model;
using BMWIgnition_API.ViewModels;
using BMWIgnition_API.Data;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Xml.Linq;

using System;


namespace BMWIgnition_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IRepository _Repository;
        private readonly IMapper _mapper;
        private readonly UserManager<Challenger> _userManager;
        private readonly AppDbContext _appDbContext;

        public DepartmentController(IRepository iRepository, IMapper mapper, AppDbContext appDbContext, UserManager<Challenger> userManager)
        {
            _mapper = mapper;
            _Repository = iRepository;
            _appDbContext = appDbContext;
            _userManager = userManager;
        }


        [HttpGet]
        [Route("GetAllDepartment")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _appDbContext.Departments.Include(d => d.Challenges).ToListAsync();
            if(departments.Count == 0)
            {
                return NotFound(new { Message = "No departments found" });
            }

            return Ok(departments);
        }

        //hello
        [HttpGet]
        [Route("GetDepByID/{id:int}")]
        public IActionResult GetDepartmentByID([FromRoute] int id)
        {
            var dept = _Repository.GetDepartmentById(id);

            //get function and user details
            var user = _appDbContext.Challengers.Where(x => x.Id == dept.Id).FirstOrDefault();
            var function = _appDbContext.Functions.Where(x => x.FunctionId == dept.FunctionId).FirstOrDefault();
            //manually assign user details to Awards Architect property
            dept.AwardsArchitect = user;
            //manually assign function details to function property
            dept.Function = function;

            if (dept == null)
            {
                return NotFound();
            }
            return Ok(dept);
        }


        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [HttpPost]
        [Route("CreateDep")]
        public async Task<IActionResult> CreateDepartment(DepartmentViewModel departmentViewModel)
        {

            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
            var user = await _userManager.FindByIdAsync(userId);

                //validate department code
                var departments = _appDbContext.Departments.ToList();
                foreach (var dept in departments)
                {
                    if (departmentViewModel.DepartmentCode == dept.DepartmentCode)
                    {
                        return BadRequest(new { Message = "Cannot have duplicate department codes" });
                    }
                }
                //create department
                var department = new Department
                {
                    DepartmentCode = departmentViewModel.DepartmentCode,
                    Id = departmentViewModel.awardsArchitectId,
                    Name = departmentViewModel.Name,
                    FunctionId = departmentViewModel.functionId
                };
                await _appDbContext.Departments.AddAsync(department);
                await _appDbContext.SaveChangesAsync();



            var auditTrail = new AuditTrail
                {
                    UserId = user.Name + " " + user.Surname, // Replace with the actual user ID
                    Action = "Department " + department.Name + " Created",
                    Timestamp = DateTime.Now,
                    Amount = 0,
                    Quantity = 0

                };

                _appDbContext.AuditTrails.Add(auditTrail);
                _appDbContext.SaveChanges();


                return Ok(department);
            
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [HttpDelete("DeleteDep/{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var dept = _Repository.GetDepartmentById(id);
            var name = dept.Name;

            if (dept == null)
            {
                return NotFound("Resource not found.");
            }

            var departmentUsers = _appDbContext.Challengers.Where(x => x.DepartmentId == dept.DepartmentId).ToList();
            if (departmentUsers.Count() > 0)
            {
                return BadRequest(new { Message = "Cannot delete department with users assigned to it!" });
            }

            _Repository.DeleteDepartment(dept);

            var httppUser = HttpContext.User;
            var userName = User.Identity.Name;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
            var user = await _userManager.FindByIdAsync(userId);

            var auditTrail = new AuditTrail
            {
                UserId = user.Name + " " + user.Surname, // Replace with the actual user ID
                Action = "Department" + name + "Deleted",
                Timestamp = DateTime.Now,
                Amount = 0,
                Quantity = 0

            };
            _appDbContext.AuditTrails.Add(auditTrail);
            _appDbContext.SaveChanges();

            return Ok(dept);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [HttpPut("UpdateDep/{id}")]
        public async Task<IActionResult> UpdateDepartment([FromRoute] int id, [FromBody] DepartmentViewModel departmentViewModel)
        {
            try
            {
                //validate department code
                var departments = _appDbContext.Departments.Where(x=> x.DepartmentId != id).ToList();
                if (departments.Any(dept => dept.DepartmentCode == departmentViewModel.DepartmentCode))
                {
                    return BadRequest(new { Message = "Cannot have duplicate department codes" });
                }
                var dept = _Repository.UpdateDepartment(id, departmentViewModel);

                var name = departmentViewModel.Name;

                var httppUser = HttpContext.User;
                var userName = User.Identity.Name;
                var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
                var user = await _userManager.FindByIdAsync(userId);

                var auditTrail = new AuditTrail
                {
                    UserId = user.Name + " " + user.Surname, // Replace with the actual user ID
                    Action = "Department" + name + "Updated",
                    Timestamp = DateTime.Now,
                    Amount = 0,
                    Quantity = 0

                };
                _appDbContext.AuditTrails.Add(auditTrail);
                _appDbContext.SaveChanges();


                return Ok(dept);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetSuperArchitectDepartments/{id}")]
        public async Task<IActionResult> GetSuperArchitectDepartments(string id)
        {
            //get logged in super architect
            var superArchitectId = await _appDbContext.Challengers.Where(x => x.Id == id).Select(x => x.Id).FirstOrDefaultAsync();
            //get function id of super architect
            var functionId = await _appDbContext.Functions.Where(x => x.Id == superArchitectId).Select(x => x.FunctionId).FirstOrDefaultAsync();
            //get departments of function
            var departments = await _appDbContext.Departments.Where(x => x.FunctionId == functionId).ToListAsync();

            if (departments.Count == 0)
            {
                return NotFound(new { Message = "No departments found" });
            }
            return Ok(departments);
        }


    }
}
