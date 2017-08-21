using HordeFlow.HR.Infrastructure;
using HordeFlow.HR.Infrastructure.Models;

namespace HordeFlow.HR.Repositories.Implementation
{
    public class DesignationRepository : BaseRepository<Designation>, IDesignationRepository
    {
        public DesignationRepository(HrContext context)
            : base(context)
        {
            
        }
    }
}