using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using HordeFlow.HR.Infrastructure.Models;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace HordeFlow.HR.Infrastructure
{
    public class Seeder : ISeeder
    {
        private readonly HrContext context;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public Seeder(HrContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public bool AllMigrationsApplied()
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public async Task GenerateDefaultCompanies(string seedsDir)
        {
            var companies = JsonConvert.DeserializeObject<List<Company>>(File.ReadAllText(seedsDir + "companies.json"));
            var hasCompany = false;
            foreach (Company c in companies)
            {
                if (!await context.Companies.Where(e => e.Code == c.Code).AnyAsync())
                {
                    hasCompany = true;
                    await context.AddAsync(c);
                }
            }
            if(hasCompany)
                await context.SaveChangesAsync();
        }

        public async Task GenerateDefaultUsers(string seedsDir)
        {
            var role = "SuperUser";
            var hasCompany = await context.Companies.AnyAsync();

            if (hasCompany)
            {
                var company = await context.Companies.FirstAsync();
                var users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(seedsDir + "users.json"));

                foreach (User u in users)
                {
                    if (await userManager.FindByNameAsync(u.UserName) == null)
                    {
                        if (await roleManager.FindByNameAsync(role) == null)
                        {
                            await roleManager.CreateAsync(new Role() { Company = company, Active = true, Name = role });
                        }
                    }
                    u.Company = company;
                    u.Active = true;
                    IdentityResult result = await userManager.CreateAsync(u, u.Password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(u, role);
                    }
                }
                await context.SaveChangesAsync();
            }
        }

        public async Task EnsureSeededAsync()
        {
            // var context = serviceProvider.GetRequiredService<HrContext>();
            // UserManager<User> userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            // RoleManager<Role> roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
            var seedsDir = "Infrastructure/Seeds" + Path.DirectorySeparatorChar;
            if (!AllMigrationsApplied())
                await context.Database.MigrateAsync();
            else
                await context.Database.EnsureCreatedAsync();
            await GenerateDefaultCompanies(seedsDir);
            await GenerateDefaultUsers(seedsDir);
        }
    }
}