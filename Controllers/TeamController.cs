using HordeFlow.HR.Infrastructure.Models;
using HordeFlow.HR.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HordeFlow.HR.Controllers
{
    [Route("api/[controller]")]
    public class TeamController : BaseController<Team>
    {
        public TeamController(ITeamRepository repository)
            : base(repository)
        {
            
        }
    }
}