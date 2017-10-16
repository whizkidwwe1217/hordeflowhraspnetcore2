using HordeFlow.HR.Infrastructure.Extensions;
using HordeFlow.HR.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HordeFlow.HR.Infrastructure.Configurations
{
    public class RoleClaimsConfig : EntityMappingConfiguration<RoleClaims>
    {
        public override void Map(EntityTypeBuilder<RoleClaims> b)
        {
            b.Property(e => e.Active).HasDefaultValue(true);
            b.Property(e => e.ClaimType).HasMaxLength(50);
            b.Property(e => e.ClaimValue).HasMaxLength(300);
        }
    }
}