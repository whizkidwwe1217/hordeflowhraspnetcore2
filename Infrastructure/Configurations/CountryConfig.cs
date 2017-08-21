using HordeFlow.HR.Infrastructure.Extensions;
using HordeFlow.HR.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HordeFlow.HR.Infrastructure.Configurations
{
    public class CountryConfig : EntityMappingConfiguration<Country>
    {
        public override void Map(EntityTypeBuilder<Country> b)
        {
            b.Property(e => e.Code).HasMaxLength(50).IsRequired();
            b.Property(e => e.Name).HasMaxLength(50).IsRequired();
            b.HasIndex(e => e.Code).IsUnique();
            b.HasIndex(e => e.Name).IsUnique();
        }
    }
}