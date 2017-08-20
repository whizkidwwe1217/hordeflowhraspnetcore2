using HordeFlow.HR.Infrastructure.Extensions;
using HordeFlow.HR.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HordeFlow.HR.Infrastructure.Configurations
{
    public class CompanyAddressConfig : EntityMappingConfiguration<CompanyAddress>
    {
        public override void Map(EntityTypeBuilder<CompanyAddress> b)
        {
            b.HasKey(e => new { e.CompanyId, e.AddressId });
            b.HasOne(ea => ea.Address)
                .WithMany(ea => ea.CompanyAddresses)
                .HasForeignKey(ea => ea.AddressId);
            b.HasOne(ea => ea.Company)
                .WithMany(ea => ea.CompanyAddresses)
                .HasForeignKey(ea => ea.CompanyId);
        }
    }
}