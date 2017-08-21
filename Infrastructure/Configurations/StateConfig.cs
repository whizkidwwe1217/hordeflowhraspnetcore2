using HordeFlow.HR.Infrastructure.Extensions;
using HordeFlow.HR.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HordeFlow.HR.Infrastructure.Configurations
{
    public class StateConfig : EntityMappingConfiguration<State>
    {
        public override void Map(EntityTypeBuilder<State> b)
        {
            b.Property(e => e.Code).HasMaxLength(50).IsRequired();
            b.Property(e => e.Name).HasMaxLength(50).IsRequired();
            b.Property(e => e.CountryId).IsRequired();
            b.HasOne(e => e.Country).WithMany(e => e.States).HasForeignKey(e => e.CountryId);
        }
    }
}