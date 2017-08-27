using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HordeFlow.HR.Infrastructure.Models
{
    public class Role : CompanyEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsSystemAdministrator { get; set; }
        public bool? Active { get; set; }
        public ICollection<RolePermission> Permissions { get; set; }
        public ICollection<UserRole> Users { get; set; }
    }
}