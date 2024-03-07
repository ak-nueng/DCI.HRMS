using System;
using System.Collections.Generic;
using System.Text;
using DCI.Security.Model;
using System.Collections;

namespace DCI.Security.Persistence
{
    public interface IUserGroupDao
    {
        UserGroupInfo Select(int userGroupId);
        ArrayList Select();
        void Update(UserGroupInfo grp);
        void Save(UserGroupInfo grp);
        void Delete(int grpId);
    }
}
