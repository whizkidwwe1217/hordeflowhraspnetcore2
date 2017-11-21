using System.Threading.Tasks;
using HordeFlow.HR.Infrastructure.Models;
using HordeFlow.HR.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HordeFlow.HR.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : BaseController<Employee>
    {
        public EmployeeController(IEmployeeRepository repository)
            : base(repository)
        {
            
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> Get(int id)
        {
            var entity = await repository.GetAsync(p => p.Id == id, p => p.Designation);
            if (entity != null)
                return Ok(entity);
            return NotFound(entity);
        }
    }
}