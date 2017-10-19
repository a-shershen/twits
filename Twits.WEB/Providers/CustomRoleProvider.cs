﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Security;

namespace Twits.WEB.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        private BLL.Interfaces.IAccount iAccount;

        public CustomRoleProvider()
        {
            iAccount = (BLL.Interfaces.IAccount)
                System.Web.Mvc.DependencyResolver
                .Current.GetService(typeof(BLL.Interfaces.IAccount));
        }

        public CustomRoleProvider(BLL.Interfaces.IAccount iAccount)
        {
            this.iAccount = iAccount;
        }


        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
            return new string[]
            {
                iAccount.GetUserRole(username)
            };
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return iAccount.IsUserInRole(username, roleName);
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