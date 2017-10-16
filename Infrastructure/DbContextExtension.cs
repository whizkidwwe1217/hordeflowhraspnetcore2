using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using HordeFlow.HR.Infrastructure.Models;

namespace HordeFlow.HR.Infrastructure
{
    public static class DbContextExtension
    {
        public static bool AllMigrationsApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static void EnsureSeeded(this HrContext context)
        {
            var seedsDir = "Seeds" + Path.DirectorySeparatorChar;

            if(!context.Companies.Any())
            {
                var companies = JsonConvert.DeserializeObject<List<Company>>(File.ReadAllText(seedsDir + "companies.json"));
                context.AddRange(companies);
                context.SaveChanges();
            }

            if(!context.Users.Any())
            {
                var users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(seedsDir + "users.json"));
                foreach(User u in users)
                {
                    u.Company = context.Companies.FirstOrDefault();
                }
                context.AddRange(users);
                context.SaveChanges();
            }
        }
    }
}