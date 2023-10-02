using BMWIgnition_API.Model;
using BMWIgnition_API.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BMWIgnition_API.Data
{
  public class ChallengerAuthRepository : IChallengerAuthRepository
  {
    // All injections go here 
    private UserManager<Challenger> _challengerManager;
    private IConfiguration _configuration;

    public ChallengerAuthRepository(UserManager<Challenger> challengerManager, IConfiguration configuration)
    {
      _challengerManager = challengerManager;
      _configuration = configuration;
    }

        public async Task<UserManagertReponse> LogginChallengerAsync(LoginViewModel model)
        {
            // Find the user 
            var user = await _challengerManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new UserManagertReponse
                {
                    Message = "There is no user with this email",
                    isSuccess = false,

                };
            }
            else
            {
                var result = await _challengerManager.CheckPasswordAsync(user, model.Password);
                if (!result)
                {
                    return new UserManagertReponse
                    {
                        Message = "Password is not valid",
                        isSuccess = false
                    };
                }

               
                
                // Create a claims 
                var claims = new[]
                {
                    new Claim(ClaimTypes.Role, "CHALLENGER"),
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

                return new UserManagertReponse
                {
                    token = tokenString,
                    isSuccess = true,
                    Date = DateTime.Now,
                    username = user.UserName,
                    email = user.Email,
                    name = user.Name,
                    surname = user.Surname,
                };

            }
        }

       

        public async Task<UserManagertReponse> LogginRewardArchitectAsync(LoginViewModel model)
        {
            // find the user 
            var user = await _challengerManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new UserManagertReponse
                {
                    Message = "There is no user with this email",
                    isSuccess = false,

                };
            }
            else
            {
                var result = await _challengerManager.CheckPasswordAsync(user, model.Password);
                if (!result)
                {
                    return new UserManagertReponse
                    {
                        Message = "Password is not valid",
                        isSuccess = false
                    };
                }

                // Create a claims 
                var claims = new[]
                {
                    new Claim(ClaimTypes.Role, "REWARDARCHITECT"),
                    new Claim(ClaimTypes.Email, model.Email),
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

                return new UserManagertReponse
                {
                    token = tokenString,
                    isSuccess = true,
                    Date = DateTime.Now,
                    username = user.UserName,
                    email = user.Email,
                    name = user.Name,
                    surname = user.Surname,
                };
            }

        }
    }
}
