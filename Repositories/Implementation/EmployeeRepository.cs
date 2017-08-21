using HordeFlow.HR.Infrastructure;
using HordeFlow.HR.Infrastructure.Models;

namespace HordeFlow.HR.Repositories.Implementation
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(HrContext context)
            : base(context)
        {
            
        }
    }
}