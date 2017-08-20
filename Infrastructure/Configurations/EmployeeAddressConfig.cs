using HordeFlow.HR.Infrastructure.Extensions;
using HordeFlow.HR.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HordeFlow.HR.Infrastructure.Configurations
{
    public class EmployeeAddressConfig : EntityMappingConfiguration<EmployeeAddress>
    {
        public override void Map(EntityTypeBuilder<EmployeeAddress> b)
        {
            b.HasKey(e => new { e.EmployeeId, e.AddressId });
            b.HasOne(ea => ea.Address)
                .WithMany(ea => ea.EmployeeAddresses)
                .HasForeignKey(ea => ea.AddressId);
            b.HasOne(ea => ea.Employee)
                .WithMany(ea => ea.EmployeeAddresses)
                .HasForeignKey(ea => ea.EmployeeId);
        }
    }
}