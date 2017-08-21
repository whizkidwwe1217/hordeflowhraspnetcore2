using HordeFlow.HR.Infrastructure;
using HordeFlow.HR.Infrastructure.Models;

namespace HordeFlow.HR.Repositories.Implementation
{
    public class TeamRepository : BaseRepository<Team>, ITeamRepository
    {
        public TeamRepository(HrContext context)
            : base(context)
        {
            
        }
    }
}