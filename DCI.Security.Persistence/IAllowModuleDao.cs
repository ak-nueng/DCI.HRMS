using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using DCI.Security.Model;

namespace DCI.Security.Persistence
{
    public interface IAllowModuleDao
    {
        ModuleInfo SelectAllowModule(string moduleId, int userGroupId);
        ArrayList SelectAllowModules(int userGroupId, ApplicationType applicationType);

       UserGroupPermission SelectUserGroupPermission(string moduleId, int userGroupId);
       void UpdateUserGroupPermission(UserGroupPermission prem);
       void AddUserGroupPermission(UserGroupPermission prem);
       void DeleteUserGroupPermission(string moduleId, int userGroupId);
    }
}
