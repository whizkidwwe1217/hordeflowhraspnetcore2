using System.Linq;
using System.Threading.Tasks;
using HordeFlow.HR.Infrastructure.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;

namespace HordeFlow.HR.Infrastructure
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(this IApplicationBuilder applicationBuilder, HrContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var company = new Company()
            {
                Active = true,
                Code = "Acme",
                Name = "Acme"
            };
            await context.Companies.AddAsync(company);

            var role = new Role()
            {
                Company = company,
                Name = "Admin",
                Description = "Administrator",
                IsSystemAdministrator = true
            };

            await context.Roles.AddAsync(role);

            var permission = new Permission()
            {
                Name = "admin-create",
                Description = "Allows creating new users."
            };

            await context.Permissions.AddAsync(permission);

            var rolePermission = new RolePermission()
            {
                Role = role,
                Permission = permission
            };

            await context.RolePermissions.AddAsync(rolePermission);

            var user = new User("wendell")
            {
                Company = company,
                Password = "1234"
            };

            await context.Users.AddAsync(user);

            var userRole = new UserRole()
            {
                Role = role,
                User = user
            };

            await context.UserRoles.AddAsync(userRole);
            
            await context.SaveChangesAsync();
        }
    }
}