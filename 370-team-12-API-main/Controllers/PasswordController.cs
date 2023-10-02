using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit.Text;
using AuthenticationAndAutherazation.ViewModels;
using BMWIgnition_API.Model;
using BMWIgnition_API.ViewModels;
using BMWIgnition_API.Data;
using System.Security.Claims;

namespace AuthenticationAndAutherazation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // here 
    public class PasswordController : Controller
    {
        private readonly UserManager<Challenger> _userManager;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _appDbContext;


        public PasswordController(UserManager<Challenger> userManager, IConfiguration configuration, AppDbContext appDbContext)
    {
      _userManager = userManager;
      _configuration = configuration;
            this._appDbContext = appDbContext;

        }
    /* [HttpPost("SendPasswordResetEmail")]
     public void SendPasswordResetEmail(string recipientEmail, string resetLink)
     {
         // create message object
         var message = new MimeMessage();
         message.From.Add(new MailboxAddress("Your Name", "your-email@example.com"));
         message.To.Add(new MailboxAddress("", recipientEmail));
         message.Subject = "Password Reset Request";

         // create message body
         var builder = new BodyBuilder();
         builder.HtmlBody = string.Format("Click <a href='{0}'>here</a> to reset your password.", resetLink);
         message.Body = builder.ToMessageBody();

         // configure email server settings
         var smtpClient = new SmtpClient();
         smtpClient.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
         smtpClient.Authenticate("your-email@example.com", "your-email-password");

         // send message
         smtpClient.Send(message);

         // disconnect from email server
         smtpClient.Disconnect(true);
     }*/
       [HttpPost("Email")]
        public void SendEmail(string toAddress, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration["Mail:Email"]));
            email.To.Add(MailboxAddress.Parse(_configuration["Mail:Email"]));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration["Mail:Email"], _configuration["Mail:Password"]);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
        [HttpGet("SMPTTest")]
        public IActionResult  SMPTTest()
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("u20583542@tuks.co.za"));
            email.To.Add(MailboxAddress.Parse("u20583542@tuks.co.za"));
            email.Subject = " Test subject";
            string body = $"<p>Hi name </p>";
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("mail.smtp2go.com", 465, SecureSocketOptions.StartTls);
            smtp.Authenticate("u20583542@tuks.co.za", "uMwNpVptK4LZoP1g");
            smtp.Send(email);
            smtp.Disconnect(true);

            return Ok("Done");
        }

        [HttpPost("RequestPasswordReset")]
        public async Task<IActionResult> RequestPasswordResetAsync(RequestPasswordResetVM requestPasswordResetVM)
        {
            
            var user = await _userManager.FindByEmailAsync(requestPasswordResetVM.email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
             
                var userId = user.Id; // get user ID
                var callbackUrl = "http://localhost:4200/reset-password/?token=" + Uri.EscapeDataString(token) + "&userId=" + Uri.EscapeDataString(userId);
                var emailBody = $"<p>Hi {user.Name}</p> <p style=''>Forgot your password? No worries, we got you coverd. Click the link bellow to reset your password</p>" +
                   $"<p><a href='{callbackUrl}' target='" + "_blank" + "'>click here to reset</a></p><p style='margin-top: 20px'>IF you did not make this request or made it by mistake, please ignore this email. Your password will remain the same</p>";

                SendEmail("brianne17@ethereal.email", "Password Reset", emailBody);

                var httppUser = HttpContext.User;
                var auditUser = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
                var Auser = await _userManager.FindByIdAsync(auditUser);

                var auditTrail = new AuditTrail
                {
                    UserId = Auser.Name + " " + Auser.Surname, // Replace with the actual user ID
                    Action = "Request Password reset",
                    Timestamp = DateTime.Now,
                    Amount = 0,
                    Quantity = 0
                };

                _appDbContext.AuditTrails.Add(auditTrail);
                _appDbContext.SaveChanges();

                return Ok(new { userId = user.Id, ResetPasswordToken = token });
            }
            else
            {
                return BadRequest("This email is not regsitered, double check it or register");
            }

           // return Ok("Done");
            ///In this example, we're finding the user by their email address using the FindByEmailAsync method of the UserManager class.
            ///If the user exists, we're generating a password reset token using the GeneratePasswordResetTokenAsync method and constructing a callback URL that includes the token. 
            ///We're then sending an email to the user's email address containing the callback URL.
        }
        /// 3. When the user clicks the password reset link in the email, <summary>
        /// 3. When the user clicks the password reset link in the email,
        /// </summary>
        /// your application should verify the token and allow the user to reset their password.
        

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

                if (result.Succeeded)
                {
                    var httppUser = HttpContext.User;
                    var auditUser = httppUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; // retrieve the user id  
                    var Auser = await _userManager.FindByIdAsync(auditUser);
                    var auditTrail = new AuditTrail
                    {
                        UserId = Auser.Name + " " + Auser.Surname, // Replace with the actual user ID
                        Action = "Password succesfully reset",
                        Timestamp = DateTime.Now,
                        Amount = 0,
                        Quantity = 0
                    };

                    _appDbContext.AuditTrails.Add(auditTrail);
                    _appDbContext.SaveChanges();


                    // the user's password has been reset successfully
                    return Ok( new { message = "Password was changed" });
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

    }
}
