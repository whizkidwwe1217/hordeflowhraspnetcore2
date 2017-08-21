using System;
using System.Threading.Tasks;
using HordeFlow.HR.Infrastructure.Models;
using HordeFlow.HR.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HordeFlow.HR.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly AuthenticationManager authenticationManager;

        public AuthenticationController(UserManager<ApiUser> userManager, SignInManager<ApiUser> signInManager, RoleManager<ApiRole> roleManager
            , IPasswordHasher<ApiUser> passwordHasher, IConfigurationRoot configurationRoot, ILogger<AuthenticationManager> logger)
        {
            this.authenticationManager = new AuthenticationManager(userManager, signInManager, roleManager, passwordHasher, configurationRoot, logger);
        }

        [AllowAnonymous]
		[HttpPost]
		[Route("[action]")]
        public async Task<IActionResult> Register([FromBody]User user)
        {
            if (!ModelState.IsValid)
			{
				return BadRequest();
			}

            var result = await this.authenticationManager.Register(user);

            if (result.Succeeded)
			{
				return Ok(result);
			}
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError("Error", error.Description);
			}
			return BadRequest(result.Errors);
        }

		[HttpPost]
		[Route("[action]")]
		public async Task<IActionResult> CreateToken([FromBody] string username, [FromBody] string password)
        {
            try 
            {
                var token = await this.authenticationManager.CreateToken(username, password);
                if(token == null)
                    return Unauthorized();
                return Ok(token);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}