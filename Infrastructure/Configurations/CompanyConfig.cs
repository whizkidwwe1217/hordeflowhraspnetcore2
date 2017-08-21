using HordeFlow.HR.Infrastructure.Extensions;
using HordeFlow.HR.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HordeFlow.HR.Infrastructure.Configurations
{
    public class CompanyConfig : EntityMappingConfiguration<Company>
    {
        public override void Map(EntityTypeBuilder<Company> b)
        {
            b.Property(e => e.Active).HasDefaultValue(true);
            b.Property(e => e.Code).HasMaxLength(50).IsRequired();
            b.Property(e => e.Name).HasMaxLength(50).IsRequired();
            b.HasOne(e => e.ParentCompany).WithMany(e => e.Companies).HasForeignKey(e => e.ParentCompanyId);
            b.HasIndex(e => e.Code).IsUnique();
        }
    }
}