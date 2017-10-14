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
            this.GetDatabaseUserRolesPermissions();
        }

        public void GetDatabaseUserRolesPermissions()
        {

        }

        public bool HasPermission(string requiredPermission)
        {
            var found = false;
            foreach (UserRole role in this.Roles)
            {
                found = (role.Role.Permissions.Where(p => p.Permission.Description == requiredPermission).ToList().Count > 0);
                if (found)
                    break;
            }
            return found;
        }

        public bool HasRole(string role)
        {
            return Roles.Where(p => p.Role.Name == role).ToList().Count > 0;
        }

        public bool HasRoles(string roles)
        {
            var found = false;
            var _roles = roles.ToLower().Split(';');
            foreach (UserRole role in this.Roles)
            {
                try
                {
                    found = _roles.Contains(role.Role.Name.ToLower());
                    if (found)
                        return found;
                }
                catch (Exception)
                {

                }
            }
            return found;
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
        public ICollection<UserRole> Roles { get; set; }
    }
}