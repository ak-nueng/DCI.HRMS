using System;
using System.Collections.Generic;
using System.Text;
using DCI.Security.Model;
using System.Collections;

namespace DCI.Security.Persistence
{
    public interface IUserAccountDao
    {
        UserAccountInfo Select(string userId);
        UserAccountInfo Select(string userId , string password);
        void KeepLog(string user, string moduleId, string fromMachine, string action, string description);

        void insert(UserAccountInfo account, string hashPassword, string updateBy);

        void update(UserAccountInfo account, string updateBy);

        void delete(string accountId);

        void updateNewPassword(string accountId, string newPassword, string updateBy);

        int getDaysNotChangePassword(string p);

        ArrayList GetUser(string groupId);
    }
}
