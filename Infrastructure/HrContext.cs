using HordeFlow.HR.Infrastructure.Extensions;
using HordeFlow.HR.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace HordeFlow.HR.Infrastructure
{
    public class HrContext: DbContext
    {
        private int companyId;

        public HrContext(DbContextOptions<HrContext> options)
            : base(options)
        {
            this.companyId = 1;
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Address> States { get; set; }
        public DbSet<CompanyAddress> CompanyAddresses { get; set; }
        public DbSet<EmployeeAddress> EmployeeAddresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddEntityConfigurationsFromAssembly(GetType().Assembly);
            //modelBuilder.AddEntityCompanyFilter(GetType().Assembly);
            // Soft Delete & Multi-tenacy (Multi-company)
            modelBuilder.Entity<Employee>()
                .HasQueryFilter(e => e.CompanyId == 1);
            
            base.OnModelCreating(modelBuilder);
        }
    }
} 