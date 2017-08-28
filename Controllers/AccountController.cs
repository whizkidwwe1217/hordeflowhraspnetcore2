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

namespace HordeFlow.HR.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AccountController: Controller
    {
        private readonly HrContext context;
        
        public AccountController(HrContext context)
        {
            this.context = context;    
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if(model == null)
                return BadRequest();
        
            var user = await context.Users
                .FirstOrDefaultAsync(e => e.UserName == model.UserName && e.Password == model.Password);
            if(user != null)
            {
                return Ok(user);
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Register([FromBody] AccountViewModel model)
        {
            if(model != null)
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
                catch(Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, 
                        new { message = "An error has occurred while registering a new user.", 
                        success = false, details = ex.Message });
                }
            }

            return BadRequest();
        }
    }
}