using HordeFlow.HR.Infrastructure.Extensions;
using HordeFlow.HR.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HordeFlow.HR.Infrastructure.Configurations
{
    public class GroupRoleConfig : EntityMappingConfiguration<GroupRole>
    {
        public override void Map(EntityTypeBuilder<GroupRole> b)
        {
            b.HasKey(e => new { e.GroupId, e.RoleId });
            b.HasOne(e => e.Group).WithMany(e => e.Roles).HasForeignKey(e => e.GroupId);
            b.HasOne(e => e.Role).WithMany(e => e.Groups).HasForeignKey(e => e.RoleId);
        }
    }
}