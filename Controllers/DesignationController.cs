using HordeFlow.HR.Infrastructure.Models;
using HordeFlow.HR.Repositories;
using HordeFlow.Hris.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace HordeFlow.HR.Controllers
{
    [Route("api/[controller]")]
    public class DesignationController : BaseController<Designation>
    {
        public DesignationController(IDesignationRepository repository)
            : base(repository)
        {
            
        }
    }
}