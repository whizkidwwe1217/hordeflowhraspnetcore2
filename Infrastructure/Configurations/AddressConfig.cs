using HordeFlow.HR.Infrastructure.Extensions;
using HordeFlow.HR.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HordeFlow.HR.Infrastructure.Configurations
{
    public class AddressConfig : EntityMappingConfiguration<Address>
    {
        public override void Map(EntityTypeBuilder<Address> b)
        {
            b.HasKey(e => e.Id);
            b.Property(e => e.AddressLine1).HasMaxLength(300).IsRequired();
            b.Property(e => e.AddressLine2).HasMaxLength(300);
            b.Property(e => e.PostalCode).HasMaxLength(20);
            b.Property(e => e.AddressType).HasMaxLength(15);
            b.Property(e => e.StateId).IsRequired();
            b.HasOne(e => e.State).WithMany(e => e.Addresses).HasForeignKey(e => e.StateId);
        }
    }
}