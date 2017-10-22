using System.Threading.Tasks;
using HordeFlow.HR.Infrastructure.Models;
using HordeFlow.HR.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HordeFlow.HR.Controllers
{
    [Route("api/[controller]")]
    public class RoleController : BaseController<Role>
    {
        private readonly RoleManager<Role> roleManager;
        private readonly UserManager<User> userManager;

        public RoleController(IRoleRepository repository, RoleManager<Role> roleManager, UserManager<User> userManager)
            : base(repository)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public override async Task<IActionResult> Delete(int id)
        {   
            Role role = await roleManager.FindByIdAsync(id.ToString());
            if(role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if(result.Succeeded)
                {
                    return NoContent();
                }
            }
            ModelState.AddModelError("", "Role doesn't exists.");
            return NotFound();
        }
    }
}