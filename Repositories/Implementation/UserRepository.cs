using HordeFlow.HR.Infrastructure;
using HordeFlow.HR.Infrastructure.Models;

namespace HordeFlow.HR.Repositories.Implementation
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(HrContext context)
            : base(context)
        {
            
        }
    }
}