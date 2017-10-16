using System.IO;
using HordeFlow.HR.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace HordeFlow.HR.Infrastructure
{
    public class HrContextDesignTimeFactory : IDesignTimeDbContextFactory<HrContext>
    {
        public HrContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            var builder = new DbContextOptionsBuilder<HrContext>();
            var engineConfig = configuration.GetSection("ServerSettings");
            var connectionString = configuration.GetConnectionString("AppHarbor");
            if(engineConfig["Engine"] == "SqlServer")
                builder.UseSqlServer(connectionString);
            else if(engineConfig["Engine"] == "Sqlite")
                builder.UseSqlite("Data Source=hordeflowhr.db");
            return new HrContext(builder.Options);
        }
    }
}