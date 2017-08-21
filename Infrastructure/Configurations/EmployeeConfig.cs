using HordeFlow.HR.Infrastructure.Extensions;
using HordeFlow.HR.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HordeFlow.HR.Infrastructure.Configurations
{
    public class EmployeeConfig : EntityMappingConfiguration<Employee>
    {
        public override void Map(EntityTypeBuilder<Employee> b)
        {
            b.Property(e => e.Active).HasDefaultValue(true);
            b.Property(e => e.Code).HasMaxLength(50).IsRequired();
            b.Property(e => e.CompanyId).IsRequired();
            b.Property(e => e.FirstName).HasMaxLength(50).IsRequired();
            b.Property(e => e.LastName).HasMaxLength(50).IsRequired();
            b.Property(e => e.MiddleName).HasMaxLength(50);
            b.Property(e => e.SSS).HasMaxLength(50);
            b.Property(e => e.GSIS).HasMaxLength(50);
            b.Property(e => e.PHIC).HasMaxLength(50);
            b.Property(e => e.TIN).HasMaxLength(50);
            b.Property(e => e.Citizenship).HasMaxLength(100);
            b.Property(e => e.Religion).HasMaxLength(100);
            b.Property(e => e.Email).HasMaxLength(50);
            b.Property(e => e.Avatar).HasMaxLength(300);
            b.HasOne(e => e.Company).WithMany(e => e.Employees).HasForeignKey(e => e.CompanyId);
            b.HasOne(e => e.Team).WithMany(e => e.Employees).HasForeignKey(e => e.TeamId);
            b.HasOne(e => e.Department).WithMany(e => e.Employees).HasForeignKey(e => e.DepartmentId);
            b.HasOne(e => e.Designation).WithMany(e => e.Employees).HasForeignKey(e => e.DesignationId);
            b.HasIndex(e => e.Code).IsUnique();
            b.HasIndex(e => new { e.FirstName, e.LastName }).IsUnique();
            b.HasIndex(e => new { e.CompanyId, e.Id });
            b.HasIndex(e => new { e.CompanyId, e.Code });
        }
    }
}