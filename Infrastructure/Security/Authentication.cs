using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HordeFlow.HR.Infrastructure.Security
{
    public static class Authentication
    {
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // The secret key every token will be signed with.
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["TokenAuthentication:SecretKey"]));

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!, 
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = configuration["TokenAuthentication:Issuer"],

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = configuration["TokenAuthentication:Audience"],

                // Validate the token expiry
                ValidateLifetime = true,

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };

            // services.AddJwtBearerAuthentication(options => { 
            //     options.TokenValidationParameters = 
            //     AutomaticAuthenticate = true,
            //     AutomaticChallenge = true,
            //     TokenValidationParameters = tokenValidationParameters
            //  }); 
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = tokenValidationParameters;
            });

            services.AddAuthorization(options => 
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build(); 
            });

            // services.AddJwtBearerAuthentication(options => {
            //     options.TokenValidationParameters = tokenValidationParameters;
            //     options.Audience = "ExampleAudience";
            //     options.ClaimsIssuer = "ExampleIssuer";
            // }); 
        }
        public static void UseAuthentication(this IApplicationBuilder app, IConfiguration configuration)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["TokenAuthentication:SecretKey"]));

            app.UseSimpleTokenProvider(new TokenProviderOptions
            {
                Path = configuration["TokenAuthentication:TokenPath"],
                Audience = configuration["TokenAuthentication:Audience"],
                Issuer = configuration["TokenAuthentication:Issuer"],
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                IdentityResolver = GetIdentity
            });            
        }

        public static Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            // Don't do this in production, obviously!
            if (username == "TEST" && password == "TEST123")
            {
                return Task.FromResult(new ClaimsIdentity(new GenericIdentity(username, "Token"), new Claim[] { }));
            }

            // Credentials are invalid, or account doesn't exist
            return Task.FromResult<ClaimsIdentity>(null);
        }
    }
}