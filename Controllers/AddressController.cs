using HordeFlow.HR.Infrastructure.Models;
using HordeFlow.HR.Repositories;
using HordeFlow.Hris.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace HordeFlow.HR.Controllers
{
    [Route("api/[controller]")]
    public class AddressController : BaseController<Address>
    {
        public AddressController(IAddressRepository repository)
            : base(repository)
        {
            
        }
    }
}