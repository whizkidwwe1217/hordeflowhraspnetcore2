using Microsoft.AspNetCore.Identity;

namespace HordeFlow.HR.Infrastructure.Security
{
    public class ApiRole: IdentityRole
    {
        public string Description { get; set; }    
    }
}