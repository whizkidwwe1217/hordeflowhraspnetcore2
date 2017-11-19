using HordeFlow.HR.Infrastructure.Extensions;
using HordeFlow.HR.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HordeFlow.HR.Infrastructure.Configurations
{
    public class UserGroupConfig : EntityMappingConfiguration<UserGroup>
    {
        public override void Map(EntityTypeBuilder<UserGroup> b)
        {
            b.HasKey(e => new { e.UserId, e.GroupId });
            b.HasOne(e => e.Group).WithMany(e => e.Users).HasForeignKey(e => e.GroupId);
            b.HasOne(e => e.User).WithMany(e => e.Groups).HasForeignKey(e => e.UserId);
        }
    }
}