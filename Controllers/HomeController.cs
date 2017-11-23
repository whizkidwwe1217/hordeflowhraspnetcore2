using HordeFlow.HR.Infrastructure;
using HordeFlow.HR.Infrastructure.Models;
using HordeFlow.HR.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HordeFlow.HR.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private HrContext context;

        public HomeController(HrContext context)
        {
            this.context = context;    
        }
    }
}