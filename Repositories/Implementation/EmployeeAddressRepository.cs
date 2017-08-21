using HordeFlow.HR.Infrastructure;
using HordeFlow.HR.Infrastructure.Models;

namespace HordeFlow.HR.Repositories.Implementation
{
    public class EmployeeAddressRepository : BaseRepository<EmployeeAddress>, IEmployeeAddressRepository
    {
        public EmployeeAddressRepository(HrContext context)
            : base(context)
        {
            
        }
    }
}