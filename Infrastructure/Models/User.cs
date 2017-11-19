using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HordeFlow.HR.Infrastructure.Models
{
    public class User : IdentityUserBase
    {
        public User() {}
        public User(string userName)
        {
            this.UserName = userName;
            this.IsSystemAdministrator = false;
        }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]        
        public string ConfirmPassword { get; set; }
        public string MobileNo { get; set; }
        public string RecoveryEmail { get; set; }
        public bool? IsConfirmed { get; set; }
        public bool? IsSystemAdministrator { get; set; }
        public bool? Active { get; set; }
        public virtual ICollection<UserGroup> Groups { get; set; }
    }
}