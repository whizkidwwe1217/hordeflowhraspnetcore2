using HordeFlow.HR.Infrastructure;
using HordeFlow.HR.Infrastructure.Models;

namespace HordeFlow.HR.Repositories.Implementation
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(HrContext context)
            : base(context)
        {
            
        }
    }
}