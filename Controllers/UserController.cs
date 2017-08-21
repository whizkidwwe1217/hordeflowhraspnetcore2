using HordeFlow.HR.Infrastructure.Models;
using HordeFlow.HR.Repositories;
using HordeFlow.Hris.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace HordeFlow.HR.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController<User>
    {
        public UserController(IUserRepository repository)
            : base(repository)
        {
            
        }
    }
}