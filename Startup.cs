using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HordeFlow.HR.Infrastructure;
using HordeFlow.HR.Infrastructure.Security;
using HordeFlow.HR.Repositories;
using HordeFlow.HR.Repositories.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

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

            // Cross-Origin Resource Sharing
            // Make sure to always call this before authentication.
            services.AddCors();
            // Security
            //services.ConfigureAuthentication(Configuration);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;

                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = Configuration["TokenAuthentication:Issuer"],
                        ValidAudience = Configuration["TokenAuthentication:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenAuthentication:SecretKey"])),
                        ClockSkew = TimeSpan.Zero,
                        ValidateLifetime = true
                    };
                });
            services.AddAuthorization(options => {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();
            });

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

            // API Documentation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "HordeFlow HR API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            //app.UseAuthentication(Configuration);
            app.UseAuthentication();
            // app.UseSimpleTokenProvider()

            // API Documentation
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.ShowRequestHeaders();
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HordeFlow HR API");
            });

            app.UseMvc();

            // Runs migrations and seeds data that will ensure that the database exists or created.
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<HrContext>();
                await context.Database.MigrateAsync();
                await app.SeedAsync(context);
            }
        }
    }
}
