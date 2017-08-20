using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HordeFlow.HR.Infrastructure
{
    public static class DbInitializer
    {
        public static async System.Threading.Tasks.Task InitializeAsync(DbContext context) 
        {
            await context.Database.EnsureCreatedAsync();

            // if(context.Set<Employee>().Any())
            //     return;
            
            //context.SaveChanges();
        }
    }
}