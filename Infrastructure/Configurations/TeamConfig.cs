using HordeFlow.HR.Infrastructure.Extensions;
using HordeFlow.HR.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HordeFlow.HR.Infrastructure.Configurations
{
    public class TeamConfig : EntityMappingConfiguration<Team>
    {
        public override void Map(EntityTypeBuilder<Team> b)
        {
            b.Property(e => e.Active).HasDefaultValue(true);
            b.Property(e => e.Name).HasMaxLength(50).IsRequired();
            b.HasIndex(e => new { e.CompanyId, e.Name }).IsUnique();
        }
    }
}