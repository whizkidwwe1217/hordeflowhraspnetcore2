using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HordeFlow.HR.Infrastructure.Models
{
    public class Group : CompanyEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<GroupRole> Roles { get; set; }
        public virtual ICollection<UserGroup> Users { get; set; }
    }
}