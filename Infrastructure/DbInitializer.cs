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

            var user = new User()
            {
                Company = company,
                Username = "wendell",
                Password = "1234"
            };

            await context.Users.AddAsync(user);
            
            await context.SaveChangesAsync();
        }
    }
}