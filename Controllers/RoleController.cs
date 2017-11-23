using System.Threading.Tasks;
using HordeFlow.HR.Infrastructure.Models;
using HordeFlow.HR.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using HordeFlow.HR.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using HordeFlow.HR.ViewModels;

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
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return NoContent();
                }
            }
            ModelState.AddModelError("", "Role doesn't exists.");
            return NotFound();
        }

        public override async Task<IActionResult> Create([FromBody] Role role)
        {
            if (ModelState.IsValid)
            {
                var found = await repository.AnyAsync(e => e.CompanyId == role.CompanyId && e.Name == role.Name);
                if (found)
                {
                    ModelState.AddModelError("", "Role already exists.");
                    return StatusCode(StatusCodes.Status409Conflict, found);
                }

                IdentityResult result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return CreatedAtAction("Get", new { id = role.Id }, role);
                }
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddMember([FromBody] UserRoleViewModel model)
        {
            if(ModelState.IsValid)
            {
                var found = await repository.Context.UserRoles.AsNoTracking().AnyAsync(e => e.UserId == model.UserId && e.RoleId == model.RoleId);
                if (found)
                {
                    ModelState.AddModelError("", "User is already a member of this role.");
                    return StatusCode(StatusCodes.Status409Conflict, found);
                }
                var role = await roleManager.FindByIdAsync(model.RoleId.ToString());
                if(role != null)
                {
                    var user = await userManager.FindByIdAsync(model.UserId.ToString());
                    IdentityResult result = await userManager.AddToRoleAsync(user, role.Name);
                    if(result.Succeeded)
                    {
                        return Ok();
                    }
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error assigning a role membership to a user.");
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        [Route("[action]")]
        public async Task<IActionResult> GetMembers(int id)
        {
            if (ModelState.IsValid)
            {
                var users = from ur in repository.Context.UserRoles
                            join u in repository.Context.Users on ur.UserId equals u.Id
                            where ur.RoleId == id
                            select u;
                var response = new ResponseData<User>
                {
                    data = await users.ToListAsync(),
                    total = await users.CountAsync()
                };
                return Ok(response);
            }
            return BadRequest();
        }
    }
}