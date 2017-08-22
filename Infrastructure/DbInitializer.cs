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
            await context.SaveChangesAsync();
        }
    }
}