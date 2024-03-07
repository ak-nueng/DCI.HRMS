using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace DCI.Security.Model
{
    [Serializable]
    public class UserGroupInfo
    {
        private int id;
        private string name;
        private bool enable = false;
        private bool permanent = false;
        private string description;

   
        private ArrayList users;
        private ArrayList modules;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }
        public bool Permanent
        {
            get { return permanent; }
            set { permanent = value; }
        } 
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public ArrayList Users
        {
            get {
                SetUserGroupForUsers();
                return users; 
            }
            set 
            { 
                users = value;
            }
        }
        public override string ToString()
        {
            return ID + ":" + Name;
        }

        private void SetUserGroupForUsers()
        {
            if (users != null && users.Count > 0)
            {
                foreach (UserAccountInfo user in users)
                {
                    user.UserGroup = this;
                }
            }
        }
        public ArrayList Modules
        {
            get { return modules; }
            set 
            { 
                modules = value;
                SetUserGroupForModules();
            }
        }

        private void SetUserGroupForModules()
        {
            if (modules != null && modules.Count > 0)
            {
                foreach (ModuleInfo module in modules)
                {
                    module.UserGroup = this;
                }
            }
        }
    }
}
