using HordeFlow.HR.Infrastructure.Extensions;
using HordeFlow.HR.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HordeFlow.HR.Infrastructure.Configurations
{
    public class GroupConfig : EntityMappingConfiguration<Group>
    {
        public override void Map(EntityTypeBuilder<Group> b)
        {
            b.Property(e => e.Name).HasMaxLength(50);
            b.Property(e => e.Description).HasMaxLength(50);
        }
    }
}