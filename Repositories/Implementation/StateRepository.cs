using HordeFlow.HR.Infrastructure;
using HordeFlow.HR.Infrastructure.Models;

namespace HordeFlow.HR.Repositories.Implementation
{
    public class StateRepository : BaseRepository<State>, IStateRepository
    {
        public StateRepository(HrContext context)
            : base(context)
        {
            
        }
    }
}