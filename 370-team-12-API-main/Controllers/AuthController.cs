using AutoMapper.Configuration.Annotations;
using BMWIgnition_API.Data;
using BMWIgnition_API.Model;
using BMWIgnition_API.ViewModels;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json.Linq;
using MimeKit.Text;
using static System.Net.WebRequestMethods;
using AuthenticationAndAutherazation.ViewModels;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Bcpg;
using System;
using System.Data;
using System.Linq;

namespace BMWIgnition_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        // All the injections go here 
        private readonly UserManager<Challenger> _userManager;
        private readonly SignInManager<Challenger> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _appDbContext;

        // Inject repositories here
        private IChallengerAuthRepository _chellengerAuhtRepository;

        public AuthController(UserManager<Challenger> userManager, SignInManager<Challenger> signInManager,
                              IConfiguration configuration, RoleManager<IdentityRole> roleManager,
                              IChallengerAuthRepository challengerAuthRepository, AppDbContext appDbContext
                               )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _chellengerAuhtRepository = challengerAuthRepository;
            _appDbContext = appDbContext;
          
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

                if (result.Succeeded)
                {

                    //create audit entry
                    var auditTrail = new AuditTrail
                    {
                        UserId = user.Name + " " + user.Surname, // Replace with the actual user ID
                        Action = user.Name + " Reset their Password",
                        Timestamp = DateTime.Now,
                        Amount = 0,
                        Quantity = 0
                    };
                    await _appDbContext.AuditTrails.AddAsync(auditTrail);
                    // the user's password has been reset successfully
                    return Ok(new { message = "Password was changed" });
                }
                else
                {
                    return BadRequest("Password was not changed");
                    // newPASS10000@$52442 there was an error resetting the user's password
                }
            }
            else
            {
                // the user does not exist
                return Ok("the user does not exist");
            }


        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(RequestPasswordResetVM requestPasswordResetVM )
        {

            var user = await _userManager.FindByEmailAsync(requestPasswordResetVM.email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var userId = user.Id; // get user ID
                var callbackUrl = "http://localhost:4200/reset-password/?token=" + Uri.EscapeDataString(token) + "&userId=" + Uri.EscapeDataString(userId);
                var body = $@"<!DOCTYPE html>
                <html>
                <head>
                  <title>Forgot Password</title>
                </head>
                <body>
                  <div style='font-family: Arial, sans-serif; line-height: 1.6; color: #333; max-width: 700px; margin: 0 auto; padding: 20px; border-radius: 15px; box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1); background-color: #f8f8f8;'>
                    <div style='text-align: center; font-size: 24px; margin-bottom: 30px;'>Forgot Your Password?</div>
                    <p>Hi {user.Name} {user.Surname} ,</p>
                    <p>No worries, we've got you covered. Click the link below to reset your password:</p>
                    <p>
                      <a href='{callbackUrl}' target='_blank' style='display: block; text-align: center; background-color: #4CAF50; color: #fff; text-decoration: none; font-weight: bold; padding: 12px 20px; border-radius: 5px;'>Click here to reset</a>
                    </p>
                    <p>If you did not make this request or made it by mistake, please ignore this email. Your password will remain the same.</p>
                    <div style='text-align: center; margin-top: 30px; color: #888;'>Thank you,<br> Codexa Team </div>
                  </div>
                </body>
                </html>";
                
                var message = new MimeMessage();
                message.From.Add(MailboxAddress.Parse(_configuration["Mail:Email"]));
                message.To.Add(new MailboxAddress("", requestPasswordResetVM.email));
                message.Subject = "Forgot Password";

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = body;
                message.Body = new TextPart(TextFormat.Html) { Text = body };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate(_configuration["Mail:Email"], _configuration["Mail:Password"]);
                    client.Send(message);
                    client.Disconnect(true);
                }

                //create audit entry
                var auditTrail = new AuditTrail
                {
                    UserId = user.Name + " " + user.Surname, // Replace with the actual user ID
                    Action = user.Name + " Forgot their Password",
                    Timestamp = DateTime.Now,
                    Amount = 0,
                    Quantity = 0
                };
                await _appDbContext.AuditTrails.AddAsync(auditTrail);

                return Ok("Message sent");
            }

            return BadRequest( new { Message = "Email not found is not registered " });

        }
        [HttpGet("GenerateOTP")]
        public string GenerateOTP()
        {
            // Generate a random OTP using your preferred method
            // For example, you can use a library like Rfc6238NetCore or GoogleAuthenticatorNetCore
            string otp = "123456"; // Replace with your OTP generation logic
            return otp;
        }
        [HttpPost("VerifyEmail")]
        public async Task<IActionResult> VerifyEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var otp = GenerateOTP();
            await _userManager.SetAuthenticationTokenAsync(user, "EmailVerification", "OTP", otp);
           
            return Ok("");
        }
        [HttpPost("VerifyEmailOTP")]
        public async Task<IActionResult> VerifyEmailOTP(string enteredOTP)
        {
            var user = await _userManager.GetUserAsync(User);
            var storedOTP = await _userManager.GetAuthenticationTokenAsync(user, "EmailVerification", "OTP");

            if (enteredOTP == storedOTP)
            {
                // OTP is valid, mark email as verified
               //await _userManager.SetEmailConfirmedAsync(user, true);
                // Perform any additional actions, such as updating the user's email verification status

                return RedirectToAction("EmailVerified");
            }
            else
            {
                // OTP is invalid, display an error message
                ModelState.AddModelError(string.Empty, "Invalid OTP entered.");
                return View();
            }
        }

        [HttpPost("ValidateToken")]
        public IActionResult ValidateToken(TokenVM token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);

            try
            {
                // Set the validation parameters
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };

                // Validate the token and extract the claims
                var claimsPrincipal = tokenHandler.ValidateToken(token.Token, validationParameters, out var validatedToken);
                // var user = await _challengerManager.FindByEmailAsync(model.Email);
                var claims = claimsPrincipal.Claims;
                var email = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier);
                // Optionally, you can access and use the claims from the token

                var roleClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role);

                return Ok(new { isLoggedIn = true, userclaims = roleClaim, userEmail = email });
            }
            catch (Exception ex)
            {
                return BadRequest(new { isLoggedIn = true, error = ex.Message });
            }
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "CHALLENGER")]
        [HttpDelete("UpdateUserAuth")]
        public async Task<IActionResult> DeleteUser()
        {
            var httpuser = HttpContext.User;
            var userId = httpuser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id
            // Find the user by their ID
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                // User not found
                return NotFound();
            }

            // Delete the user
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                // User deleted successfully
                return Ok("User deleted successfully");
            }
            else
            {
                // Failed to delete user
                // Handle the error
                return StatusCode(500);
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "CHALLENGER")]
        [HttpPut("UpdateUserAuth")]
        public async Task<IActionResult> UpdateUser(UpdateUserVM updateUserVM)
        {
            var httpuser = HttpContext.User;
            var userId = httpuser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            // Check if the user name has changed 
            if (user.UserName != updateUserVM.UserName)
            {
                var userNameChangeresult = await _userManager.SetUserNameAsync(user, updateUserVM.UserName);
                if (!userNameChangeresult.Succeeded)
                {
                    return BadRequest(userNameChangeresult.Errors);
                }
            }


            // Update user properties
            user.Name = updateUserVM.Name;
            user.Surname = updateUserVM.Surname;
          //  user.DateOfBirth = DateTime.Parse(updateUserVM.DateOfBirth.ToString());




            // Update other properties...

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {

                var auditTrail = new AuditTrail
                {
                    UserId = updateUserVM.Name + " " + updateUserVM.Surname, // Replace with the actual user ID
                    Action = "User Updated",
                    Timestamp = DateTime.Now,
                    Amount = 0,
                    Quantity = 0

                };

                _appDbContext.AuditTrails.Add(auditTrail);
                _appDbContext.SaveChanges();

                return Ok(updateUserVM);
            }
            else
            {
                // Handle error
                return BadRequest(result.Errors);
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "CHALLENGER")]
        [HttpGet]
        [Route("GetUserDetails")]
        public async Task<IActionResult> GetUserDetails()
       {
            var user = HttpContext.User;
            if (user == null)
            {
                return NotFound();
            }
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id
            var userName = user.Identity.Name; // retrieve the user name
            var userEmail = user.FindFirst("Email")?.Value; // retrieve the user email
            var idenityUser = await _userManager.FindByIdAsync(userId);
            if(idenityUser == null)
            {
                return NotFound();
            }

            var medals = _appDbContext.ChallengerMedals.Include( x => x.Medal).Where(x => x.ChallenegerId == idenityUser.Id).ToList();
            return Ok( new { challenger = idenityUser, medals = medals } );
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "CHALLENGER")]
        [HttpGet("GetUserById/{challengerId}")]
        
        public async Task<IActionResult> GetUserById([FromRoute]string challengerId)
        {
            var user = HttpContext.User;
            if (user == null)
            { 
                return NotFound();
            }
            
            var idenityUser = await _userManager.FindByIdAsync(challengerId);
            if (idenityUser == null)
            {
                return NotFound();
            }
            var posts = _appDbContext.Posts.Include(p => p.Challenger).Include(c => c.Comments).Where( p => p.ChallengerId == challengerId).ToList(); 

            var medals = _appDbContext.ChallengerMedals.Include(x => x.Medal).Where(x => x.ChallenegerId == idenityUser.Id).ToList();
            return Ok(new { challenger = idenityUser, medals = medals, posts = posts });
        }

        

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!await _roleManager.RoleExistsAsync("CHALLENGER"))
            {
                // Create the challenger role
                await _roleManager.CreateAsync(new IdentityRole("CHALLENGER"));
            }
            string dateString = "15/07/2023";
            var challenger = new Challenger
            {
                UserName = model.Username,
                Email = model.Email,
                Name = model.Name,
                Surname = model.Surname,
                Bio = model.Bio,
                ProfilePicture = model.ProfilePicture,
                DepartmentId = model.DepartmentId,
                DateOfBirth = DateTime.ParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture)

            };

            var userList = await _appDbContext.Challengers.ToListAsync();

            for (int i=0; i < userList.Count(); i++)
            {
                if (challenger.UserName == userList[i].UserName)
                {
                    return BadRequest(new { Message = "Username already taken" });
                }
                if (challenger.Email == userList[i].Email)
                {
                    return BadRequest(new { Message = "Email already taken" });
                }
                else if (challenger.Email == userList[i].Email)
                {
                    return BadRequest(new { Message = "Email already taken" });
                }
            }


            var result = await _userManager.CreateAsync(challenger, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new { Message = result.Errors });
            }

            await _userManager.AddToRoleAsync(challenger, "CHALLENGER");


            var auditTrail = new AuditTrail
            {
                UserId = model.Name + " " + model.Surname, // Replace with the actual user ID
                Action = "User Registered",
                Timestamp = DateTime.Now,
                Amount = 0,
                Quantity = 0

            };

            _appDbContext.AuditTrails.Add(auditTrail);
            _appDbContext.SaveChanges();
            AutoArchiveChallenge();

            return Ok(new { token = GenerateToken(challenger) });
        }

        private string GenerateToken(Challenger challenger)
        {
            // Create a claims 
            var claims = new[]
            {
                    new Claim(ClaimTypes.Role, "Challenger"),
                    new Claim(ClaimTypes.Email, challenger.Email),
                    new Claim(ClaimTypes.NameIdentifier, challenger.Id.ToString()),
                    new Claim(ClaimTypes.Name, challenger.Name),
                    new Claim(ClaimTypes.Surname, challenger.Surname),
                    new Claim (ClaimTypes.DateOfBirth, challenger.DateOfBirth.ToString()),
                    new Claim("DepartmentID", challenger.DepartmentId.ToString())

             };
            // var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));


            // Create a token 
            var token = new JwtSecurityToken
            (
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)


            );

            return  new JwtSecurityTokenHandler().WriteToken(token);
        }
        [HttpPost("login")]
        public async Task<IActionResult> ChallengerLoggin(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
               
                
                var results = await _chellengerAuhtRepository.LogginChallengerAsync(model);
                if (results.isSuccess)
                {
                    var auditTrail = new AuditTrail
                    {
                        UserId = user.Name + " " + user.Surname, // Replace with the actual user ID
                        Action = "User Logged in",
                        Timestamp = DateTime.Now,
                        Amount = 0,
                        Quantity = 0

                    };

                    _appDbContext.AuditTrails.Add(auditTrail);
                    _appDbContext.SaveChanges();


                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles.Count > 1)
                    {
                        return Ok(new
                        {
                            token = results.token,
                            roles = roles,
                            user = user
                        });
                    }
                    return Ok( new
                    {
                        token = results.token,
                        roles = roles,
                        user = user
                    } );
                }
                return BadRequest(results);
            };
            //archive challenge if required
            AutoArchiveChallenge();

            return BadRequest("some properties are missing");
        }


        [Authorize(AuthenticationSchemes = "Bearer", Roles = "CHALLENGER")]
        [HttpGet("SuperArchitectLogin")]
        public async Task<IActionResult> SuperArchitectLogin()
        {
            var httppUser = HttpContext.User;
            var superArchitectId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id   
            var user = await _userManager.FindByIdAsync(superArchitectId);

            if (user == null)
            {
                return BadRequest();
            }


            // Create a claims 
            var claims = new[]
            {
                    new Claim(ClaimTypes.Role, "SUPERARCHITECT"),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Surname, user.Surname),
                    new Claim (ClaimTypes.DateOfBirth, user.DateOfBirth.ToString()),
                    new Claim("DepartmentID", user.DepartmentId.ToString())
                };

            //Create the the singin in key 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            // Create a token 
            var token = new JwtSecurityToken
            (
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            //archive challenge if required
            AutoArchiveChallenge();

            return Ok(new { token = tokenString });

        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "CHALLENGER")]
        [HttpGet("RewardArchitectLogin")]
        public async Task<IActionResult> RewardLogin()
        {
            var httppUser = HttpContext.User;
            var rewardsArchitectId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id   
            var user = await _userManager.FindByIdAsync(rewardsArchitectId);
           
            if (user == null)
            {
                return BadRequest();
            }


            // Create a claims 
            var claims = new[]
            {
                    new Claim(ClaimTypes.Role, "REWARDARCHITECT"),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Surname, user.Surname),
                    new Claim (ClaimTypes.DateOfBirth, user.DateOfBirth.ToString()),
                    new Claim("DepartmentID", user.DepartmentId.ToString())
                };

            //Create the the singin in key 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            // Create a token 
            var token = new JwtSecurityToken
            (
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            //archive challenge if required
            AutoArchiveChallenge();
            return Ok(new { token  = tokenString });
          
        }


        [Authorize(AuthenticationSchemes = "Bearer", Roles = "CHALLENGER")]
        [HttpGet("AdminLogin")]
        public async Task<IActionResult> AdminLogin()
        {
            var httppUser = HttpContext.User;
            var rewardsArchitectId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id   
            var user = await _userManager.FindByIdAsync(rewardsArchitectId);

            if (user == null)
            {
                return BadRequest();
            }


            // Create a claims 
            var claims = new[]
            {
                    new Claim(ClaimTypes.Role, "ADMIN"),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Surname, user.Surname),
                    new Claim (ClaimTypes.DateOfBirth, user.DateOfBirth.ToString()),
                   
                };

            //Create the the singin in key 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            // Create a token 
            var token = new JwtSecurityToken
            (
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            //archive challenge if required
            AutoArchiveChallenge();

            return Ok(new { token = tokenString, user = user });

        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [HttpPost("RemoveUserRole")]
        public async Task<IActionResult> RemoveUserRole(UpdateUserRoleVM model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return BadRequest(new {Message = "Error user not found"});
            }

            var departments = await _appDbContext.Departments.ToListAsync();
            if(departments.Any(department => department.Id == user.Id) && model.Role == "REWARDARCHITECT")
            {
                return BadRequest(new { Message = "Cannot remove this users role as they are currently an awards architect" });
            }

            var functions = await _appDbContext.Functions.ToListAsync();
            if (functions.Any(function => function.Id == user.Id) && model.Role == "SUPERARCHITECT")
            {
                return BadRequest(new { Message = "Cannot remove this users role as they are currently a super architect" });
            }
            var result = await _userManager.RemoveFromRoleAsync(user , model.Role);
            if (result.Succeeded)
            {
                var httppUser = HttpContext.User;
                var auserId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
                var auser = await _userManager.FindByIdAsync(auserId);

                var auditTrail = new AuditTrail
                {
                    UserId = auser.Name + " " + auser.Surname, // Replace with the actual user ID
                    Action = user.Name + " User role removed",
                    Timestamp = DateTime.Now,
                    Amount = 0,
                    Quantity = 0

                };
                _appDbContext.AuditTrails.Add(auditTrail);
                _appDbContext.SaveChanges();

                return Ok(new { Message = "Role Removed" });

            }

            return BadRequest(new { Message = "Role not removed " });
           

        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [HttpPost("AssignUserRole")]
        public async Task<IActionResult> AssignUserRole(UpdateUserRoleVM model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return BadRequest(new { Message = "Error user not found" });
            }

            var roles = _userManager.GetRolesAsync(user).Result;

            if(roles.Contains(model.Role))
            {
                return BadRequest(new { Message = "User already has this role" });
            }
            else if(roles.Count == 2)
            {
                  return BadRequest(new { Message = "Cannot add more roles" });
            }

            var result = await _userManager.AddToRoleAsync(user, model.Role);
            if (result.Succeeded)
            {
                var httppUser = HttpContext.User;
                var auserId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
                var auser = await _userManager.FindByIdAsync(auserId);

                var auditTrail = new AuditTrail
                {
                    UserId = auser.Name + " " + auser.Surname, // Replace with the actual user ID
                    Action = user.Name + " User assigned new role",
                    Timestamp = DateTime.Now,
                    Amount = 0,
                    Quantity = 0

                };
                _appDbContext.AuditTrails.Add(auditTrail);
                _appDbContext.SaveChanges();

                return Ok(new { Message = "User added to Removed" });

            }

            return BadRequest(new { Message = "Role not assigned " });


        }

       
        [HttpGet("GetAllChallengers")]
        public async Task<IActionResult> GetAllChallengers()
        {
            var userIds = await _userManager.GetUsersInRoleAsync("CHALLENGER");
            var roles = await _userManager.GetRolesAsync(userIds[0]);
            var departemnts =  _appDbContext.Departments.ToArray();
            var users = userIds.Select(n => new {
                name = n.Name,
                userName = n.UserName,
                surname = n.Surname,
                department= departemnts.Where( d => d.DepartmentId.Equals(n.DepartmentId)).FirstOrDefault().Name,
                role = "Challanger",
                status = "Active",
            });

            // Retrieve the ApplicationUser objects for the selected user IDs
            return Ok(users);
        }
        //This should not be used, Seed data has been created for the rewards architects.


        [HttpPost("CreateUser")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles ="ADMIN")]

        public async Task<IActionResult> CreateUser(CreateUserVM createUserVM)
        {
            //get user details
            var httppUser = HttpContext.User;
            var userId = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
            var user = _appDbContext.Challengers.FirstOrDefault(x => x.Id == userId);
            var password = generateRandomPassword();
            var callbackUrl = "http://localhost:4200/login";



            //create awards architect
            if ( createUserVM.Role  == "REWARDARCHITECT") {


                var rewardArchitect = new RewardsArchitect
                {
                    Name = createUserVM.Name,
                    UserName = createUserVM.UserName,
                    Email =  createUserVM.Email,
                    Surname = createUserVM.Surname,
                    Bio = "I have been made a REWARDARCHITECT",
                };

                var userList = await _appDbContext.Challengers.ToListAsync();

                for (int i = 0; i < userList.Count(); i++)
                {
                    if (rewardArchitect.UserName == userList[i].UserName)
                    {
                        return BadRequest(new { Message = "Username already taken" });
                    }
                    if (rewardArchitect.Email == userList[i].Email)
                    {
                        return BadRequest(new { Message = "Email already taken" });
                    }
                }

                var result = await _userManager.CreateAsync(rewardArchitect, password);
                if (!result.Succeeded)
                {
                    return BadRequest(new { Message = result.Errors });
                }
                await _userManager.AddToRoleAsync(rewardArchitect, "REWARDARCHITECT");
                await _userManager.AddToRoleAsync(rewardArchitect, "CHALLENGER");


                var message = new MimeMessage();
                message.From.Add(MailboxAddress.Parse(_configuration["Mail:Email"]));
                message.To.Add(new MailboxAddress("", createUserVM.Email));
                message.Subject = "Account Created";

                var body = @$"
                       <!DOCTYPE html>
                <html>
                <head>
                  <title>Forgot Password</title>
                </head>
                <body>
                  <div style='font-family: Arial, sans-serif; line-height: 1.6; color: #000000; max-width: 700px; margin: 0 auto; padding: 20px; border-radius: 15px; box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);'>
                    <div style='text-align: center; font-size: 24px; margin-bottom: 30px;'><b>Welcome to Ignition!</b></div>
                    <p>Good day <u>{createUserVM.Name} {createUserVM.Surname}</u>!</p>
                    <p>Thank you for agreeing to be apart of the BMW It Hub's employee rewards system.
                        <br>
                     As a Line Manager, you have been made a Awards Architect!</p>
                    <p style=""color: #333;"">You can now log in and start exploring our platform. 
                        <br>
                        Here are your Log in credentials:
                        <br>
                        <br>
                        <b>Email:</b> {createUserVM.Email}
                        <br>
                        <b>Password:</b> {password}
                    </p>

                    <p>
                      <a href='{callbackUrl}' target='_blank' style='display: block; text-align: center; background-color: rgb(60, 60, 128); color: #fff; text-decoration: none; font-weight: bold; padding: 12px 20px; border-radius: 5px;'>Click here to sign in and get started</a>
                    </p>
                    <p>If you did not make this request or made it by mistake, please ignore this email. Your password will remain the same.</p>
                    <div style='text-align: center; margin-top: 30px; color: #888;'>Thank you,<br> Codexa Team </div>
                  </div>
                  <img style='display: block; margin:auto;max-width: 700px; color: #fff; text-decoration: none; font-weight: bold; padding: 12px 20px; border-radius: 5px;' src =""https://media.licdn.com/dms/image/C4D16AQHFV7iMoz5Uwg/profile-displaybackgroundimage-shrink_200_800/0/1632073639842?e=2147483647&v=beta&t=VLa8lJBfDrirR8tw_CV1RnSkFZsdnu-G3wqxso2WKsM"">

                </body>
                </html>
                            ";
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = body;
                message.Body = new TextPart(TextFormat.Html) { Text = body };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate(_configuration["Mail:Email"], _configuration["Mail:Password"]);
                    client.Send(message);
                    client.Disconnect(true);
                }

                return Ok( new { message = "User create" });

            }
            //create super architect
            if (createUserVM.Role == "SUPERARCHITECT")
            {

                if (!await _roleManager.RoleExistsAsync("SUPERARCHITECT"))
                {
                    // Create the super architect role
                    await _roleManager.CreateAsync(new IdentityRole("SUPERARCHITECT"));
                }
                var superArchitect = new RewardsArchitect
                {
                    Name = createUserVM.Name,
                    UserName = createUserVM.UserName,
                    Email = createUserVM.Email,
                    Surname = createUserVM.Surname,
                    Bio = "I have been made a Super Architect",
                };

                var userList = await _appDbContext.Challengers.ToListAsync();

                for (int i = 0; i < userList.Count(); i++)
                {
                    if (superArchitect.UserName == userList[i].UserName)
                    {
                        return BadRequest(new { Message = "Username already taken" });
                    }
                    if (superArchitect.Email == userList[i].Email)
                    {
                        return BadRequest(new { Message = "Email already taken" });
                    }
                }

                var result = await _userManager.CreateAsync(superArchitect, password);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
                await _userManager.AddToRoleAsync(superArchitect, "SUPERARCHITECT");
                await _userManager.AddToRoleAsync(superArchitect, "CHALLENGER");



                var message = new MimeMessage();
                message.From.Add(MailboxAddress.Parse(_configuration["Mail:Email"]));
                message.To.Add(new MailboxAddress("", createUserVM.Email));
                message.Subject = "Account Created";

                var body = @$"
                       <!DOCTYPE html>
                <html>
                <head>
                  <title>Forgot Password</title>
                </head>
                <body>
                  <div style='font-family: Arial, sans-serif; line-height: 1.6; color: #000000; max-width: 700px; margin: 0 auto; padding: 20px; border-radius: 15px; box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);'>
                    <div style='text-align: center; font-size: 24px; margin-bottom: 30px;'><b>Welcome to Ignition!</b></div>
                    <p>Good day <u>{createUserVM.Name} {createUserVM.Surname}</u>!</p>
                    <p>Thank you for agreeing to be apart of the BMW It Hub's employee rewards system.
                        <br>
                     As a General Manager, you have been made a Super Architect!</p>
                    <p style=""color: #333;"">You can now log in and start exploring our platform. 
                        <br>
                        Here are your Log in credentials:
                        <br>
                        <br>
                        <b>Email:</b> {createUserVM.Email}
                        <br>
                        <b>Password:</b> {password}
                    </p>

                    <p>
                      <a href='{callbackUrl}' target='_blank' style='display: block; text-align: center; background-color: rgb(60, 60, 128); color: #fff; text-decoration: none; font-weight: bold; padding: 12px 20px; border-radius: 5px;'>Click here to sign in and get started</a>
                    </p>
                    <p>If you did not make this request or made it by mistake, please ignore this email. Your password will remain the same.</p>
                    <div style='text-align: center; margin-top: 30px; color: #888;'>Thank you,<br> Codexa Team </div>
                  </div>
                  <img style='display: block; margin:auto;max-width: 700px; color: #fff; text-decoration: none; font-weight: bold; padding: 12px 20px; border-radius: 5px;' src =""https://media.licdn.com/dms/image/C4D16AQHFV7iMoz5Uwg/profile-displaybackgroundimage-shrink_200_800/0/1632073639842?e=2147483647&v=beta&t=VLa8lJBfDrirR8tw_CV1RnSkFZsdnu-G3wqxso2WKsM"">

                </body>
                </html>
                            ";
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = body;
                message.Body = new TextPart(TextFormat.Html) { Text = body };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate(_configuration["Mail:Email"], _configuration["Mail:Password"]);
                    client.Send(message);
                    client.Disconnect(true);
                }

                return Ok(new { message = "User created succesfully" });

            }
            //create super architect
            if (createUserVM.Role == "ADMIN")
            {

                var admin = new RewardsArchitect
                {
                    Name = createUserVM.Name,
                    UserName = createUserVM.UserName,
                    Email = createUserVM.Email,
                    Surname = createUserVM.Surname,
                    Bio = "I have been made an Admin",
                };

                var userList = await _appDbContext.Challengers.ToListAsync();

                for (int i = 0; i < userList.Count(); i++)
                {
                    if (admin.UserName == userList[i].UserName)
                    {
                        return BadRequest(new { Message = "Username already taken" });
                    }
                    if (admin.Email == userList[i].Email)
                    {
                        return BadRequest(new { Message = "Email already taken" });
                    }
                }

                var result = await _userManager.CreateAsync(admin, "Reward.123");
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
                await _userManager.AddToRoleAsync(admin, "ADMIN");
                await _userManager.AddToRoleAsync(admin, "CHALLENGER");


                var message = new MimeMessage();
                message.From.Add(MailboxAddress.Parse(_configuration["Mail:Email"]));
                message.To.Add(new MailboxAddress("", createUserVM.Email));
                message.Subject = "Account Created";


                var body = @$"
                       <!DOCTYPE html>
                <html>
                <head>
                  <title>Forgot Password</title>
                </head>
                <body>
                  <div style='font-family: Arial, sans-serif; line-height: 1.6; color: #000000; max-width: 700px; margin: 0 auto; padding: 20px; border-radius: 15px; box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);'>
                    <div style='text-align: center; font-size: 24px; margin-bottom: 30px;'><b>Welcome to Ignition!</b></div>
                    <p>Good day <u>{createUserVM.Name} {createUserVM.Surname}</u>!</p>
                    <p>Thank you for agreeing to be apart of the BMW It Hub's employee rewards system.
                        <br>
                     You have been made a System administrator!</p>
                    <p style=""color: #333;"">You can now log in and start exploring our platform. 
                        <br>
                        Here are your Log in credentials:
                        <br>
                        <br>
                        <b>Email:</b> {createUserVM.Email}
                        <br>
                        <b>Password:</b> {password}
                    </p>

                    <p>
                      <a href='{callbackUrl}' target='_blank' style='display: block; text-align: center; background-color: rgb(60, 60, 128); color: #fff; text-decoration: none; font-weight: bold; padding: 12px 20px; border-radius: 5px;'>Click here to sign in and get started</a>
                    </p>
                    <p>If you did not make this request or made it by mistake, please ignore this email. Your password will remain the same.</p>
                    <div style='text-align: center; margin-top: 30px; color: #888;'>Thank you,<br> Codexa Team </div>
                  </div>
                  <img style='display: block; margin:auto;max-width: 700px; color: #fff; text-decoration: none; font-weight: bold; padding: 12px 20px; border-radius: 5px;' src =""https://media.licdn.com/dms/image/C4D16AQHFV7iMoz5Uwg/profile-displaybackgroundimage-shrink_200_800/0/1632073639842?e=2147483647&v=beta&t=VLa8lJBfDrirR8tw_CV1RnSkFZsdnu-G3wqxso2WKsM"">

                </body>
                </html>
                            ";
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = body;
                message.Body = new TextPart(TextFormat.Html) { Text = body };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate(_configuration["Mail:Email"], _configuration["Mail:Password"]);
                    client.Send(message);
                    client.Disconnect(true);
                }

                return Ok(new { message = "User created" });

            }

            //create audit entry
            var auditTrail = new AuditTrail
            {
                UserId = user.Name + " " + user.Surname, // Replace with the actual user ID
                Action = createUserVM.Name + " User Created",
                Timestamp = DateTime.Now,
                Amount = 0,
                Quantity = 0

            };
            await _appDbContext.AuditTrails.AddAsync(auditTrail);
            _appDbContext.SaveChanges();
            return Ok(new { message = "User created" });
        }
        //test 1
        //this method is only for seeding the Reward Architect
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = _userManager.Users.Include( x => x.Department).ToList();
            
            List<UserViewModel> userList = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                UserViewModel userViewModel = new UserViewModel
                {
                    Id = user.Id,
                    ProfilePicture= user.ProfilePicture,
                    Roles = roles.ToList(),
                    UserName = user.UserName,
                    Email = user.Email,
                    Name = user.Name,
                    Surname = user.Surname,
                    
                    
                };
                if(user.DepartmentId == null)
                {
                    userViewModel.Department = "";
                }
                else
                {
                    var department = _appDbContext.Departments.Where(x => x.DepartmentId == user.DepartmentId).FirstOrDefault();
                    userViewModel.Department = department.DepartmentCode;
                }
                userList.Add(userViewModel);
            }

            return Ok(userList);
        }

        [HttpGet("LeaderBoard")]
        public async Task<IActionResult> LeaderBoard()
        {
            // This has many more includes that need to be added again.
            var users = _userManager.Users
                 .Include(x => x.Department)
                 .ThenInclude(x => x.Challenges)
                 .Include(x => x.ChallengeInstances)
                 .ToList();

            return Ok(users);
        }

        [HttpGet("SeedAdmin")]
        public async Task<IActionResult> SeedAdmin()
        {
            if (!await _roleManager.RoleExistsAsync("ADMIN"))
            {
                // Create the REWARDARCHITECT role
                await _roleManager.CreateAsync(new IdentityRole("ADMIN"));
            }
            // 
            if (!await _roleManager.RoleExistsAsync("CHALLENGER"))
            {
                // Create the challenger role
                await _roleManager.CreateAsync(new IdentityRole("CHALLENGER"));
            }

            var rewardArchitect = new Administrator
            {
                Name = "Courtney",
                UserName = "admin",
                ProfilePicture = "https://media.licdn.com/dms/image/C4D03AQEneoK_obKJ8g/profile-displayphoto-shrink_800_800/0/1654193923712?e=2147483647&v=beta&t=E8MdMbsRv2kl12e0LmvkAb7ysQ1GDMQTubd_SHBXN8A",
                Email = "admin@system.com",
                Surname = "Hart",
                Bio = "Proxy Product Owner (Contracted to BMW ZA Hub)"
            };

            var result = await _userManager.CreateAsync(rewardArchitect, "Reward.123");
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _userManager.AddToRoleAsync(rewardArchitect, "ADMIN");
            await _userManager.AddToRoleAsync(rewardArchitect, "CHALLENGER");
            return Ok(result);
        }


        [HttpGet("SeedArchitects")]
        public async Task<IActionResult> SeedArchitects()
        {

            //seed super architect
            if (!await _roleManager.RoleExistsAsync("SUPERARCHITECT"))
            {
                // Create the SuperArchitect role
                await _roleManager.CreateAsync(new IdentityRole("SUPERARCHITECT"));
            }
            // 
            if (!await _roleManager.RoleExistsAsync("CHALLENGER"))
            {
                // Create the challenger role
                await _roleManager.CreateAsync(new IdentityRole("CHALLENGER"));
            }
            var superArchitect = new RewardsArchitect
            {
                Name = "Danie ",
                UserName = "challengerx",
                Email = "Danie.Smit@bmwithub.co.za",
                Surname = "Smit",
                Bio = "I am a PhD student in Information Systems, a Python developer and an IT Manager in the BMW ZA Hub, responsible for Analytics, AI and Platforms.",
                ProfilePicture = "https://i1.rgstatic.net/ii/profile.image/11431281111552984-1673020759588_Q512/Danie-Smit-3.jpg"
            };

            var saResult = await _userManager.CreateAsync(superArchitect, "Reward.123");

            if (!saResult.Succeeded)
            {
                return BadRequest(saResult.Errors);
            }

            await _userManager.AddToRoleAsync(superArchitect, "SUPERARCHITECT");
            await _userManager.AddToRoleAsync(superArchitect, "CHALLENGER");

            var newFunc = new Function()
            {
                
                FunctionCode = "FG-9-Z-2",
                Name = "Analytics, Artificial Intelligence,Platforms",
                Id = superArchitect.Id
            };


            await _appDbContext.Functions.AddAsync(newFunc);
            await _appDbContext.SaveChangesAsync();

            //create awards architect
            if (!await _roleManager.RoleExistsAsync("REWARDARCHITECT"))
            {
                // Create the REWARDARCHITECT role
                await _roleManager.CreateAsync(new IdentityRole("REWARDARCHITECT"));
            }
            //Gavin
            {
                var Gavin = new RewardsArchitect
                {
                    Name = "Gavin",
                    UserName = "Wiggill",
                    Email = "Gavin.Wiggill@mail.com",
                    Surname = "Wiggill",
                    Bio = "Line Manager at the BMW ZaHub. I love SAP Technology, Solutions and Integration Platforms",
                    ProfilePicture = "https://media.licdn.com/dms/image/C4E03AQHRjpAIvt6Dlw/profile-displayphoto-shrink_800_800/0/1650267378119?e=2147483647&v=beta&t=m1FWqtP0e8WJe49KndEKxGkMyhqWtgxvrvoqgqrstPs"
                };

                var result = await _userManager.CreateAsync(Gavin, "Reward.123");

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                await _userManager.AddToRoleAsync(Gavin, "REWARDARCHITECT");
                await _userManager.AddToRoleAsync(Gavin, "CHALLENGER");

                var newDep = new Department()
                {
                    DepartmentCode = "FG-9-Z-24",
                    Name = "SAP Technology, Solutions,Integration Platforms",
                    FunctionId = newFunc.FunctionId,
                    Id = Gavin.Id
                };

                await _appDbContext.Departments.AddAsync(newDep);
                await _appDbContext.SaveChangesAsync();
            }
            //example
            {
                var exampleBoss = new RewardsArchitect
                {
                    Name = "Department",
                    UserName = "EX.Boss",
                    Email = "example.boss@mail.com",
                    Surname = "Boss",
                    Bio = "Line Manager at the BMW ZaHub.",
                    
                };

                var result = await _userManager.CreateAsync(exampleBoss, "Reward.123");

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                await _userManager.AddToRoleAsync(exampleBoss, "REWARDARCHITECT");
                await _userManager.AddToRoleAsync(exampleBoss, "CHALLENGER");

                var newDepEx = new Department()
                {
                    DepartmentCode = "FG-9-Z-20",
                    Name = "Data Platform",
                    FunctionId = newFunc.FunctionId,
                    Id = exampleBoss.Id
                };

                await _appDbContext.Departments.AddAsync(newDepEx);
                await _appDbContext.SaveChangesAsync();
            }

            // Create a claims 
            var claims = new[]
            {
                    new Claim(ClaimTypes.Role, "SUPERARCHITECT"),
                    new Claim("Email", superArchitect.Email),
                    new Claim(ClaimTypes.NameIdentifier, superArchitect.Id.ToString())
             };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));





            // Create a token 
            var token = new JwtSecurityToken
            (
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)


            );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { message = "Super Architect have been created", token = tokenString });
        }
        


        [HttpGet("SeedChallengers")]
        public async Task<IActionResult> SeedChallengers()
        {
            if (!await _roleManager.RoleExistsAsync("REWARDARCHITECT"))
            {
                // Create the REWARDARCHITECT role
                await _roleManager.CreateAsync(new IdentityRole("REWARDARCHITECT"));
            }
            // 
            if (!await _roleManager.RoleExistsAsync("CHALLENGER"))
            {
                // Create the challenger role
                await _roleManager.CreateAsync(new IdentityRole("CHALLENGER"));
            }
            //Mikail
            {
                Challenger Mikail = new Challenger
                {
                    Name = "Mikail",
                    UserName = "mikksr",
                    DepartmentId = 1,
                    Tokens = 2050,
                    Email = "mikail@mail.com",
                    Surname = "Kieser",
                    Bio = "Fullstack web developer",
                    ProfilePicture = "https://static.wixstatic.com/media/bba993_cd512af79d8c4ac4888b458bc718c772~mv2.jpg/v1/fill/w_324,h_389,al_c,q_80,usm_0.66_1.00_0.01,enc_auto/mikail_pic-transformed_edited.jpg"
                };
                var mikSeeded = await _userManager.CreateAsync(Mikail, "Password.123");

                if (!mikSeeded.Succeeded)
                {
                    return BadRequest(mikSeeded.Errors);
                }
                await _userManager.AddToRoleAsync(Mikail, "CHALLENGER");
            }
            //Simphiwe
            {
                Challenger Simphiwe = new Challenger
                {
                    Name = "Simphiwe",
                    UserName = "sims",
                    DepartmentId = 1,
                    Tokens = 1400,
                    Email = "sims@mail.com",
                    Surname = "Mthembu",
                    Bio = "UX designer",
                    ProfilePicture = "https://static.wixstatic.com/media/bba993_8788975f395649d6b1df42f21668d23a~mv2.jpg/v1/crop/x_0,y_240,w_1241,h_1302/fill/w_324,h_389,al_c,q_80,usm_0.66_1.00_0.01,enc_auto/simphiwe_pic-transformed_edited.jpg"
                };
                var simsSeeded = await _userManager.CreateAsync(Simphiwe, "Password.123");

                if (!simsSeeded.Succeeded)
                {
                    return BadRequest(simsSeeded.Errors);
                }
                await _userManager.AddToRoleAsync(Simphiwe, "CHALLENGER");
            }
            //Tina
            {
                Challenger Tina = new Challenger
                {
                    Name = "Tina",
                    UserName = "tineezy",
                    DepartmentId = 1,
                    Tokens = 3850,
                    Email = "tina@mail.com",
                    Surname = "Mtonga",
                    Bio = "Manager",
                    ProfilePicture = "https://static.wixstatic.com/media/bba993_00078b629e7545faaccda197d8040d90~mv2.jpg/v1/fill/w_324,h_389,al_c,q_80,usm_0.66_1.00_0.01,enc_auto/tina_pic-transformed_edited.jpg"
                };
                var tinaSeeded = await _userManager.CreateAsync(Tina, "Password.123");

                if (!tinaSeeded.Succeeded)
                {
                    return BadRequest(tinaSeeded.Errors);
                }
                await _userManager.AddToRoleAsync(Tina, "CHALLENGER");
            }
            //Oscar
            {
                Challenger Oscar = new Challenger
                {
                    Name = "Oscar",
                    UserName = "osceezy",
                    DepartmentId = 1,
                    Tokens = 450,
                    Email = "oscar@mail.com",
                    Surname = "Makwakwa",
                    Bio = "Software Engineer",
                    ProfilePicture = "https://static.wixstatic.com/media/bba993_ccf1f6794a59493d9d6ace5b93352e99~mv2.jpg/v1/crop/x_124,y_75,w_1320,h_1443/fill/w_324,h_389,al_c,q_80,usm_0.66_1.00_0.01,enc_auto/oscar_pic-transformed_edited.jpg"
                };
                var oscarSeeded = await _userManager.CreateAsync(Oscar, "Password.123");

                if (!oscarSeeded.Succeeded)
                {
                    return BadRequest(oscarSeeded.Errors);
                }
                await _userManager.AddToRoleAsync(Oscar, "CHALLENGER");
            }
            //Brandon
            {
            Challenger Brandon = new Challenger
            {
                Name = "Brandon",
                UserName = "BrandonBB" ,
                DepartmentId = 2,
                Tokens = 350,
                Email = "Brandon@gmail.com",
                Surname = "Beukes",
                Bio = "Software developer at BMW IT Hub",
                ProfilePicture = "https://media.licdn.com/dms/image/C5603AQH_g4rSm193bg/profile-displayphoto-shrink_800_800/0/1617527632428?e=2147483647&v=beta&t=nMMXoGu-I0HnUdil54KfJfqQV6lqc756dHZeO1RFOoM"
            };


            var brandonSeeded = await _userManager.CreateAsync(Brandon, "Password.123");

            if (!brandonSeeded.Succeeded)
            {
                return BadRequest(brandonSeeded.Errors);
            }
            await _userManager.AddToRoleAsync(Brandon, "CHALLENGER");
            }
            //Amore
            {
            Challenger Amore = new Challenger
            {
                Name = "Amore",
                UserName = "Amore",
                DepartmentId = 2,
                Tokens = 2580,
                Email = "Amore@gmail.com",
                Surname = "Rossouw",
                Bio = "Ux Designer at BMW IT Hub",
                ProfilePicture = "https://media.licdn.com/dms/image/C4D03AQFRkyMdX73i5g/profile-displayphoto-shrink_800_800/0/1653298984829?e=2147483647&v=beta&t=92wKi4YHnq0gNyE3WqzbRlP41cu4Ew3VTqioZEdqGf0"
            };


                var amoreSeeded = await _userManager.CreateAsync(Amore, "Password.123");

                if (!amoreSeeded.Succeeded)
                {
                    return BadRequest(amoreSeeded.Errors);
                }
                await _userManager.AddToRoleAsync(Amore, "CHALLENGER");
            }


            return Ok(new {Message = "Users Seeded"});
        }

        [HttpGet("SeedChallengeInstances")]
        public async Task<IActionResult> SeedChallengeInstances()
        {
            try
            {
                var challenges = _appDbContext.Challenges.ToList();

                var challengers = await _userManager.GetUsersInRoleAsync("CHALLENGER");
                Random random = new Random();
                for (int i = 0; i < challenges.Count; i++)
                {
                    for (int x = 0; x < random.Next(1, challengers.Count); x++)
                    {
                        var seedInstanse = new ChallengeInstance
                        {
                            ChallengeID = challenges[i].ChallengeID,
                            ChallengerId = challengers[x].Id,
                            ChallengeInstanceStatusId = 1,
                            Submition = "Seed Data submtion"

                        };
                        _appDbContext.ChallengeInstances.Add(seedInstanse);
                        await _appDbContext.SaveChangesAsync();
                    }
                }

                return Ok(new { Message = "Seeded" });
            }
            catch (Exception ex)
            {

                return BadRequest( new {Message = ex.Message});
            }


        }

        //Get All awards architects currently not assigned to a department
        [HttpGet("GetUnAssignedAwardsArchitects")]
        public async Task<IActionResult> GetUnAssignedAwardsArchitects()
        {
            //get all functions
            var departments = await _appDbContext.Departments.Select(x => x.Id).ToListAsync();

            //get super architects
            var rewardsArchitects = await _userManager.GetUsersInRoleAsync("REWARDARCHITECT");
            List<UnnasignedArchitectVM> unassignedRewardsArchitects = new List<UnnasignedArchitectVM>();
            //iterate through super architects first to get ID of each super architect
            foreach (var superArchitect in rewardsArchitects)
            {
                if (!departments.Contains(superArchitect.Id))
                {
                    //create new awards architect object
                    var architectObject = new UnnasignedArchitectVM
                    {
                        Id = superArchitect.Id,
                        AwardsArchitectFullName = superArchitect.Name + " " + superArchitect.Surname
                    };
                    unassignedRewardsArchitects.Add(architectObject);
                }
            }

            //if (unassignedRewardsArchitects.Count == 0)
            //{
            //    return BadRequest(new { Message = "All Awards Architects are assigned to a function" });
            //}
            return Ok(unassignedRewardsArchitects);




        }

        //Get All awards architects currently not assigned to a department
        [HttpGet("GetUnAssignedSuperArchitects")]
        public async Task<IActionResult> GetUnAssignedSuperArchitects()
        {
            //get all functions
            var functions = await _appDbContext.Functions.Select(x=>x.Id).ToListAsync();

            //get super architects
            var superArchitects = await _userManager.GetUsersInRoleAsync("SUPERARCHITECT");
            List<UnnasignedArchitectVM> unassignedSuperArchitects = new List<UnnasignedArchitectVM>();
            //iterate through super architects first to get ID of each super architect
            foreach(var superArchitect in superArchitects)
            {
                if (!functions.Contains(superArchitect.Id))
                {
                    //create new awards architect object
                    var architectObject = new UnnasignedArchitectVM
                    {
                        Id = superArchitect.Id,
                        AwardsArchitectFullName = superArchitect.Name + " " + superArchitect.Surname
                    };
                    unassignedSuperArchitects.Add(architectObject);
                }
            }
            
            if(unassignedSuperArchitects.Count == 0)
            {
                return BadRequest(new { Message = "All Super Architects are assigned to a function" });
            }
            return Ok(unassignedSuperArchitects);


        }

        [HttpGet("AutoArchiveChallenge")]
        public async void AutoArchiveChallenge()
        {
            var challenges = _appDbContext.Challenges.ToListAsync();
            foreach (var challenge in challenges.Result)
            {
                //if start date is ahead of current date challenge must be archived
                if (challenge.endDate < DateTime.Now)
                {
                    challenge.IsArchived = true;
                    await _appDbContext.SaveChangesAsync();
                }
                //if start date is ahead of current date challenge must be archived
                else if (challenge.startDate > DateTime.Now)
                {
                    challenge.IsArchived = true;
                    await _appDbContext.SaveChangesAsync();
                }
            }

        }

        private string generateRandomPassword()
        {
            var random = new Random();
            var password = new StringBuilder(8); // Initialize the StringBuilder with a capacity of 8 characters.

            // Generate at least one uppercase letter
            password.Append((char)random.Next(65, 91)); // ASCII values for uppercase letters (A-Z)

            // Generate at least one lowercase letter
            password.Append((char)random.Next(97, 123)); // ASCII values for lowercase letters (a-z)

            // Generate at least one digit
            password.Append((char)random.Next(48, 58)); // ASCII values for digits (0-9)

            // Generate at least one special character
            string specialCharacters = "!@#$%^&*()_+-=[]{}|;:,.<>?";
            password.Append(specialCharacters[random.Next(specialCharacters.Length)]);

            // Generate remaining characters
            for (int i = 4; i < 8; i++)
            {
                password.Append((char)random.Next(33, 126)); // ASCII values for printable characters (from '!' to '~')
            }

            // Shuffle the characters to randomize the order
            string shuffledPassword = new string(password.ToString().ToCharArray().OrderBy(x => random.Next()).ToArray());

            return shuffledPassword;
        }



    }
}
