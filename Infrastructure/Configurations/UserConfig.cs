using HordeFlow.HR.Infrastructure.Extensions;
using HordeFlow.HR.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HordeFlow.HR.Infrastructure.Configurations
{
    public class UserConfig : EntityMappingConfiguration<User>
    {
        public override void Map(EntityTypeBuilder<User> b)
        {
            b.Property(e => e.Active).HasDefaultValue(true);
            b.Property(e => e.UserName).HasMaxLength(50).IsRequired();
            b.Property(e => e.Password).IsRequired();
            b.Property(e => e.IsConfirmed).HasDefaultValue(false);
            b.Property(e => e.IsSystemAdministrator).HasDefaultValue(false);
            b.Property(e => e.RecoveryEmail).HasMaxLength(50);
            b.Property(e => e.MobileNo).HasMaxLength(50);
        }
    }
}