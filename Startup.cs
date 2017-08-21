using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HordeFlow.HR.Infrastructure;
using HordeFlow.HR.Repositories;
using HordeFlow.HR.Repositories.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HordeFlow.HR
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Startup(IHostingEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContextPool<HrContext>(options => options.UseSqlServer(connection));

            // Register Repositories
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IStateRepository, StateRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<ITeamRepository, TeamRepository>();
            services.AddTransient<IDesignationRepository, DesignationRepository>();
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IEmployeeAddressRepository, EmployeeAddressRepository>();
            services.AddTransient<ICompanyAddressRepository, CompanyAddressRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            // Runs migrations and seeds data that will ensure that the database exists or created.
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<HrContext>();
                await context.Database.MigrateAsync();
                await DbInitializer.InitializeAsync(context);
            }
        }
    }
}
