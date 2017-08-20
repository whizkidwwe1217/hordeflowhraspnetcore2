    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    namespace HordeFlow.HR.Infrastructure.Extensions
    {
        public interface IEntityMappingConfiguration
        {
            void Map(ModelBuilder b);
        }

        public interface IEntityMappingConfiguration<T> : IEntityMappingConfiguration where T : class
        {
            void Map(EntityTypeBuilder<T> b);
        }
    }