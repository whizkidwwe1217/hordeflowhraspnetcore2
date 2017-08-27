using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HordeFlow.HR.Infrastructure.Models
{
    public class Permission : CompanyEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }
        public ICollection<RolePermission> Roles { get; set; }
    }
}