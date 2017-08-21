using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HordeFlow.HR.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace HordeFlow.HR.Infrastructure.Security
{
    public class AuthenticationManager
    {
        private readonly UserManager<ApiUser> userManager;
        private readonly SignInManager<ApiUser> signInManager;
        private readonly RoleManager<ApiRole> roleManager;
        private IPasswordHasher<ApiUser> passwordHasher;
        private IConfigurationRoot configurationRoot;
        private ILogger<AuthenticationManager> logger;

        public AuthenticationManager(UserManager<ApiUser> userManager, SignInManager<ApiUser> signInManager, RoleManager<ApiRole> roleManager
            , IPasswordHasher<ApiUser> passwordHasher, IConfigurationRoot configurationRoot, ILogger<AuthenticationManager> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.logger = logger;
            this.passwordHasher = passwordHasher;
            this.configurationRoot = configurationRoot;
        }

        public async Task<IdentityResult> Register(User user)
        {
            var apiUser = new ApiUser()
            {
                UserName = user.Username,
                Email = user.Email,
                PhoneNumber = user.MobileNo
            };
            var result = await this.userManager.CreateAsync(apiUser, user.Password);

            return result;
        }

        public async Task<dynamic> CreateToken(string username, string password)
        {
            try
            {
                var user = await this.userManager.FindByNameAsync(username);
                if (user == null)
                    return null;

                if (this.passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Success)
                {
                    var userClaims = await this.userManager.GetClaimsAsync(user);

                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email)
                    }.Union(userClaims);

                    var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configurationRoot["JwtSecurityToken:Key"]));
                    var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                    var jwtSecurityToken = new JwtSecurityToken(
                        issuer: this.configurationRoot["JwtSecurityToken:Issuer"],
                        audience: this.configurationRoot["JwtSecurityToken:Audience"],
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(60),
                        signingCredentials: signingCredentials
                    );

                    return new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                        expiration = jwtSecurityToken.ValidTo
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}