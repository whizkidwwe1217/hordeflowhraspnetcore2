using HordeFlow.HR.Infrastructure;
using HordeFlow.HR.Infrastructure.Models;

namespace HordeFlow.HR.Repositories.Implementation
{
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        public CountryRepository(HrContext context)
            : base(context)
        {
            
        }
    }
}