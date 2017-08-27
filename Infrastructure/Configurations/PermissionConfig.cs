using HordeFlow.HR.Infrastructure.Extensions;
using HordeFlow.HR.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HordeFlow.HR.Infrastructure.Configurations
{
    public class PermissionConfig : EntityMappingConfiguration<Permission>
    {
        public override void Map(EntityTypeBuilder<Permission> b)
        {
            b.Property(e => e.Active).HasDefaultValue(true);
            b.Property(e => e.Description).HasMaxLength(200);
            b.Property(e => e.Name).HasMaxLength(50).IsRequired();
        }
    }
}