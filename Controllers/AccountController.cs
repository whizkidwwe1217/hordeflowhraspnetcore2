using System.Threading.Tasks;
using HordeFlow.HR.Infrastructure;
using HordeFlow.HR.Infrastructure.Models;
using HordeFlow.HR.Repositories;
using HordeFlow.HR.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using HordeFlow.HR.Infrastructure.Services.Email;

namespace HordeFlow.HR.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly HrContext context;
        private readonly IConfiguration configuration;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly IEmailSender emailSender;

        public AccountController(HrContext context, IConfiguration configuration,
            UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender)
        {
            this.context = context;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.emailSender = emailSender;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (model == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                if (result.Succeeded)
                {
                    var user = await userManager.FindByNameAsync(model.UserName);
                    return Ok(user);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Unauthorized login.");
                return BadRequest();
            }
            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GenerateToken([FromBody] LoginViewModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
            if (!result.Succeeded)
            {
                return Unauthorized();
            }
            
            var user = await userManager.FindByNameAsync(model.UserName);
            var userClaims = await userManager.GetClaimsAsync(user);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "whizkidwwe1217@live.com"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            }.Union(userClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenAuthentication:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expirationBuffer = configuration["TokenAuthentication:Expiration"] != null ? int.Parse(configuration["TokenAuthentication:Expiration"]) : 3;
            var expiryDate = DateTime.UtcNow.AddMinutes(expirationBuffer);
            var token = new JwtSecurityToken(configuration["TokenAuthentication:Issuer"],
              configuration["TokenAuthentication:Audience"],
              claims,
              expires: expiryDate,
              signingCredentials: creds);

            return Ok(new
            {
                access_token = new JwtSecurityTokenHandler().WriteToken(token),
                expirationBuffer = expirationBuffer,
                expiryDate = expiryDate
            });
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Register([FromBody] AccountViewModel model)
        {
            if (model != null)
            {
                try
                {
                    var user = new User()
                    {
                        Active = model.Active,
                        Email = model.Email,
                        Password = model.Password,
                        ConfirmPassword = model.ConfirmPassword,
                        RecoveryEmail = model.RecoveryEmail,
                        MobileNo = model.MobileNo,
                        IsSystemAdministrator = model.IsSystemAdministrator,
                        IsConfirmed = false,
                        UserName = model.UserName,
                        LockoutEnabled = model.LockoutEnabled,
                        TwoFactorEnabled = model.TwoFactorEnabled
                    };
                    var result = await userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        var confirmationCode = await userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.Action(
                            controller: "Account",
                            action: "ConfirmEmail",
                            values: new { userId = user.Id, code = confirmationCode },
                            protocol: Request.Scheme);
                        await emailSender.SendEmailAsync(
                            email: user.Email,
                            subject: "Confirm Email",
                            message: callbackUrl
                        );

                        return Ok(new { message = "User account registered successfully.", success = true });                        
                    }
                    // await context.Users.AddAsync(user);
                    // await context.SaveChangesAsync();
                    return BadRequest(new { message = "Failed to register user account.",
                        errors = result.Errors, success = false });
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new
                        {
                            message = "An error has occurred while registering a new user.",
                            success = false,
                            details = ex.Message
                        });
                }
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
                return BadRequest();
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();
            var result = await userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return Ok();
            return BadRequest();
        }

        [HttpGet("{id}")]
        [Route("[action]")]
        public async Task<IActionResult> GetClaims(int id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if(user != null)
            {
                var claims = await userManager.GetClaimsAsync(user);
                return Ok(claims);
            }
            return BadRequest();
        }

        [HttpPost()]
        [Route("[action]")]
        public async Task<IActionResult> CreateClaim([FromBody] ClaimViewModel claimViewModel)
        {
            var user = await userManager.FindByIdAsync(claimViewModel.UserId.ToString());
            if(user != null)
            {
                var claim = new Claim(claimViewModel.Type, claimViewModel.Value);
                var result = await userManager.AddClaimAsync(user, claim);
                if(result.Succeeded)
                {
                    return Ok(claim);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors.First().Description);
            }
            return BadRequest();
        }
    }
}