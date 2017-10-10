using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HordeFlow.HR.Infrastructure.Extensions
{
    public abstract class EntityCompanyFilter<T> : IEntityCompanyFilter<T> where T : class
    {
        public abstract void Filter(EntityTypeBuilder<T> b);

        public void Filter(ModelBuilder b)
        {
            Filter(b.Entity<T>());
        }
    }
}