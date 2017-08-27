using HordeFlow.HR.Infrastructure.Extensions;
using HordeFlow.HR.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HordeFlow.HR.Infrastructure.Configurations
{
    public class RolePermissionConfig : EntityMappingConfiguration<RolePermission>
    {
        public override void Map(EntityTypeBuilder<RolePermission> b)
        {
            b.HasKey(e => new { e.RoleId, e.PermissionId });
            b.HasOne(e => e.Role).WithMany(e => e.Permissions).HasForeignKey(e => e.RoleId);
            b.HasOne(e => e.Permission).WithMany(e => e.Roles).HasForeignKey(e => e.PermissionId);
        }
    }
}