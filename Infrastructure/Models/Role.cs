using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HordeFlow.HR.Infrastructure.Models
{
    public class Role : IdentityRoleBase
    {
        public string Description { get; set; }
        public bool? IsSystemAdministrator { get; set; }
        public bool? Active { get; set; }
        public virtual ICollection<GroupRole> Groups { get; set; }
    }
}