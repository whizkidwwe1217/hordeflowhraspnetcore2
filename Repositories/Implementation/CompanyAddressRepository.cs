using HordeFlow.HR.Infrastructure;
using HordeFlow.HR.Infrastructure.Models;

namespace HordeFlow.HR.Repositories.Implementation
{
    public class CompanyAddressRepository : BaseRepository<CompanyAddress>, ICompanyAddressRepository
    {
        public CompanyAddressRepository(HrContext context)
            : base(context)
        {
            
        }
    }
}