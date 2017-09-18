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

namespace HordeFlow.HR.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly HrContext context;
        private readonly IConfiguration configuration;

        public AccountController(HrContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (model == null)
                return BadRequest();

            var user = await context.Users
                .FirstOrDefaultAsync(e => e.UserName == model.UserName && e.Password == model.Password);
            if (user != null)
            {
                return Ok(user);
            }
            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public IActionResult GenerateToken([FromBody] LoginViewModel model)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "whizkidwwe1217@live.com"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

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
                        Active = false,
                        Email = model.Email,
                        RecoveryEmail = model.RecoveryEmail,
                        MobileNo = model.MobileNo,
                        IsSystemAdministrator = false,
                        IsConfirmed = false,
                        UserName = model.UserName,
                        Password = model.Password
                    };
                    await context.Users.AddAsync(user);
                    await context.SaveChangesAsync();
                    return Ok(new { message = "User registered successfully.", success = true });
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
    }
}