using System.Data.SqlClient;
using System.IO;
using HordeFlow.HR.Infrastructure.Extensions;
using HordeFlow.HR.Infrastructure.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HordeFlow.HR.Infrastructure
{
    public class HrContext : IdentityDbContext<User, Role, int>
    {
        private int companyId;
        private readonly Tenant tenant;

        public HrContext(DbContextOptions<HrContext> options)
            : base(options)
        {
            this.companyId = 1;
        }

        // protected override void OnConfiguring(DbContextOptionsBuilder builder)
        // {
        //     var tenantDbName = tenant.Name.Replace(" ", "-").ToLowerInvariant();
        //     IConfigurationRoot configuration = new ConfigurationBuilder()
        //     .SetBasePath(Directory.GetCurrentDirectory())
        //     .AddJsonFile("appsettings.json")
        //     .Build();

        //     var serverSettings = configuration.GetSection("ServerSettings");
        //     var connectionString = serverSettings[tenant.ConnectionString];
        //     var sb = new SqlConnectionStringBuilder(connectionString);
        //     sb.DataSource = tenant.Name; 
        //     // var connectionString = configuration.GetConnectionString(serverSettings["ConnectionStringKey"] == null ? "DefaultConnection" : serverSettings["ConnectionStringKey"]);
        //     if(serverSettings["Engine"] == "SqlServer")
        //         builder.UseSqlServer(connectionString);
        //     else if(serverSettings["Engine"] == "MySQL")
        //         builder.UseMySql(connectionString);
        //     else if(serverSettings["Engine"] == "Sqlite")
        //         builder.UseSqlite("Data Source=hordeflowhr.db");

        //     base.OnConfiguring(builder);
        // }

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
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupRole> GroupRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddEntityConfigurationsFromAssembly(GetType().Assembly);
            //modelBuilder.AddEntityCompanyFilter(GetType().Assembly);
            // Soft Delete & Multi-tenacy (Multi-company)
            // modelBuilder.Entity<Employee>()
            //     .HasQueryFilter(e => e.CompanyId == companyId);
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<UserRole>().ToTable("UserRoles");
            modelBuilder.Entity<UserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<RoleClaim>().ToTable("RoleClaims");
            modelBuilder.Entity<UserLogin>().ToTable("UserLogins");

            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");

            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");
        }
    }
}