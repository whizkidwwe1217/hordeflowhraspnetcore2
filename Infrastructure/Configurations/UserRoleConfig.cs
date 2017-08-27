using HordeFlow.HR.Infrastructure.Extensions;
using HordeFlow.HR.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HordeFlow.HR.Infrastructure.Configurations
{
    public class UserRoleConfig : EntityMappingConfiguration<UserRole>
    {
        public override void Map(EntityTypeBuilder<UserRole> b)
        {
            b.HasKey(e => new { e.UserId, e.RoleId });
            b.HasOne(e => e.User).WithMany(e => e.Roles).HasForeignKey(e => e.UserId);
            b.HasOne(e => e.Role).WithMany(e => e.Users).HasForeignKey(e => e.RoleId);
        }
    }
}