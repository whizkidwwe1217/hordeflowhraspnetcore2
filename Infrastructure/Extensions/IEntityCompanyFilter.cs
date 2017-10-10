    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    namespace HordeFlow.HR.Infrastructure.Extensions
    {
        public interface IEntityCompanyFilter
        {
            void Filter(ModelBuilder b);
        }

        public interface IEntityCompanyFilter<T> : IEntityCompanyFilter where T : class
        {
            void Filter(EntityTypeBuilder<T> b);
        }
    }