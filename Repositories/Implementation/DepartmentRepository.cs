using HordeFlow.HR.Infrastructure;
using HordeFlow.HR.Infrastructure.Models;

namespace HordeFlow.HR.Repositories.Implementation
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(HrContext context)
            : base(context)
        {
            
        }
    }
}