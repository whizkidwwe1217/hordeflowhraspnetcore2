using HordeFlow.HR.Infrastructure.Models;
using HordeFlow.HR.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HordeFlow.HR.Controllers
{
    [Route("api/[controller]")]
    public class CompanyController : BaseController<Company>
    {
        public CompanyController(ICompanyRepository repository)
            : base(repository)
        {
            
        }
    }
}