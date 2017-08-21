using HordeFlow.HR.Infrastructure;
using HordeFlow.HR.Infrastructure.Models;

namespace HordeFlow.HR.Repositories.Implementation
{
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        public AddressRepository(HrContext context)
            : base(context)
        {
            
        }
    }
}