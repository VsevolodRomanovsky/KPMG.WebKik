using System;
using System.Linq;
using System.Web.Security;
using KPMG.WebKik.Data;

namespace KPMG.WebKik.Web.Authorization
{
    public class WebKikRoleProvider : RoleProvider
    {
        public override string ApplicationName { get; set; }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            using (var ctx = new WebKikDataContext())
            {
                return ctx.Roles.Where(x => x.Users.Any(y => y.UserLogin == username)).Select(x => x.Name).ToArray();
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            using (var ctx = new WebKikDataContext())
            {
                return ctx.Users.Where(x => x.Role.Name == roleName).Select(x => x.UserLogin).ToArray();
            }
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            using (var ctx = new WebKikDataContext())
            {
                var user = ctx.Users.SingleOrDefault(x => x.UserLogin == username);
                return user.Role.Name == roleName;
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}