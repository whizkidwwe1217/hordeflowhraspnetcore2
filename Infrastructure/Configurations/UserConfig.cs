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
            b.Property(e => e.Username).HasMaxLength(50).IsRequired();
            b.Property(e => e.Password).HasMaxLength(50).IsRequired();
            b.HasIndex(e => e.Username).IsUnique();
        }
    }
}