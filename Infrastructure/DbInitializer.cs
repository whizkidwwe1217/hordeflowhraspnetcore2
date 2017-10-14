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
        public static void Seed(this IApplicationBuilder applicationBuilder, HrContext context)
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var found = context.Companies.Where(c => c.Code == "HordeFlow").FirstOrDefault();
            if(found == null)
            {
                var company = new Company() 
                {
                    Active = true,
                    Code = "HordeFlow",
                    Name = "HordeFlow Inc."
                };

                context.Companies.Add(company);
                context.SaveChanges();
            }
        }
    }
}