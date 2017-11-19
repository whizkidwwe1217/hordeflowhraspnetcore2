using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HordeFlow.HR.Infrastructure.Models;
using HordeFlow.HR.Infrastructure.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HordeFlow.HR.Infrastructure.Managers
{
    public class GroupManager
    {
        private GroupStore _groupStore;
        private HrContext _context;
        private AspNetUserManager<User> _userManager;
        private AspNetRoleManager<Role> _roleManager;

        public GroupManager(HrContext context, AspNetUserManager<User> userManager, AspNetRoleManager<Role> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _groupStore = new GroupStore(_context);
        }

        public IQueryable<Group> Groups
        {
            get
            {
                return _groupStore.Groups;
            }
        }

        public async Task<IdentityResult> CreateGroupAsync(Group group)
        {
            await _groupStore.CreateAsync(group);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> SetGroupRolesAsync(int groupId, params string[] roleNames)
        {
            // Clear all the roles associated with this group:
            var thisGroup = await this.FindByIdAsync(groupId);
            thisGroup.Roles.Clear();
            await _context.SaveChangesAsync();

            // Add the new roles passed in:
            var newRoles = _roleManager.Roles
                            .Where(r => roleNames.Any(n => n == r.Name));
            foreach (var role in newRoles)
            {
                thisGroup.Roles.Add(new GroupRole
                {
                    GroupId = groupId,
                    RoleId = role.Id
                });
            }
            await _context.SaveChangesAsync();

            // Reset the roles for all affected users:
            foreach (var groupUser in thisGroup.Users)
            {
                await this.RefreshUserGroupRolesAsync(groupUser.UserId);
            }
            return IdentityResult.Success;
        }


        public async Task<IdentityResult> SetUserGroupsAsync(int userId, params int[] groupIds)
        {
            // Clear current group membership:
            var currentGroups = await this.GetUserGroupsAsync(userId);
            foreach (var group in currentGroups)
            {
                group.Users
                    .Remove(group.Users
                    .FirstOrDefault(gr => gr.UserId == userId
                ));
            }
            await _context.SaveChangesAsync();

            // Add the user to the new groups:
            foreach (int groupId in groupIds)
            {
                var newGroup = await this.FindByIdAsync(groupId);
                newGroup.Users.Add(new UserGroup
                {
                    UserId = userId,
                    GroupId = groupId
                });
            }
            await _context.SaveChangesAsync();

            await this.RefreshUserGroupRolesAsync(userId);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> RefreshUserGroupRolesAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new ArgumentNullException("User");
            }
            // Remove user from previous roles:
            var oldUserRoles = await _userManager.GetRolesAsync(user);
            if (oldUserRoles.Count > 0)
            {
                await _userManager.RemoveFromRolesAsync(user, oldUserRoles.ToArray());
            }

            // Find the roles this user is entitled to from group membership:
            var newGroupRoles = await this.GetUserGroupRolesAsync(userId);

            // Get the damn role names:
            var allRoles = await _roleManager.Roles.ToListAsync();
            var addTheseRoles = allRoles
                .Where(r => newGroupRoles.Any(gr => gr.RoleId == r.Id
            ));
            var roleNames = addTheseRoles.Select(n => n.Name).ToArray();

            // Add the user to the proper roles
            await _userManager.AddToRolesAsync(user, roleNames);

            return IdentityResult.Success;
        }


        public async Task<IdentityResult> DeleteGroupAsync(int groupId)
        {
            var group = await this.FindByIdAsync(groupId);
            if (group == null)
            {
                throw new ArgumentNullException("User");
            }

            var currentGroupMembers = (await this.GetGroupUsersAsync(groupId)).ToList();
            // remove the roles from the group:
            group.Roles.Clear();

            // Remove all the users:
            group.Users.Clear();

            // Remove the group itself:
            _context.Groups.Remove(group);

            await _context.SaveChangesAsync();

            // Reset all the user roles:
            foreach (var user in currentGroupMembers)
            {
                await this.RefreshUserGroupRolesAsync(user.Id);
            }
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateGroupAsync(Group group)
        {
            await _groupStore.UpdateAsync(group);
            foreach (var groupUser in group.Users)
            {
                await this.RefreshUserGroupRolesAsync(groupUser.UserId);
            }
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> ClearUserGroupsAsync(int userId)
        {
            return await this.SetUserGroupsAsync(userId, new int[] { });
        }

        public async Task<IEnumerable<Group>> GetUserGroupsAsync(int userId)
        {
            var result = new List<Group>();
            var userGroups = (from g in this.Groups
                              where g.Users
                                .Any(u => u.UserId == userId)
                              select g).ToListAsync();
            return await userGroups;
        }

        public async Task<IEnumerable<Role>> GetGroupRolesAsync(int groupId)
        {
            var grp = await _context.Groups
                .FirstOrDefaultAsync(g => g.Id == groupId);
            var roles = await _roleManager.Roles.ToListAsync();
            var groupRoles = (from r in roles
                              where grp.Roles
                                .Any(ap => ap.RoleId == r.Id)
                              select r).ToList();
            return groupRoles;
        }

        public async Task<IEnumerable<User>> GetGroupUsersAsync(int groupId)
        {
            var group = await this.FindByIdAsync(groupId);
            var users = new List<User>();
            foreach (var groupUser in group.Users)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == groupUser.UserId);
                users.Add(user);
            }
            return users;
        }

        public async Task<IEnumerable<GroupRole>> GetUserGroupRolesAsync(int userId)
        {
            var userGroups = await this.GetUserGroupsAsync(userId);
            var userGroupRoles = new List<GroupRole>();
            foreach (var group in userGroups)
            {
                userGroupRoles.AddRange(group.Roles.ToArray());
            }
            return userGroupRoles;
        }

        public async Task<Group> FindByIdAsync(int id)
        {
            return await _groupStore.FindByIdAsync(id);
        }

        public Group FindById(int id)
        {
            return _groupStore.FindById(id);
        }
    }
}