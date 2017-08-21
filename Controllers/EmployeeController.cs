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
    }
}