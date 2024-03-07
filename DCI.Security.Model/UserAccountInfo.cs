using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DCI.Security.Model
{
    [Serializable]
    public class UserAccountInfo
    {
        private string id;
        private string fullname;
        private string descr;
        private string m_Email;
        private UserGroupInfo userGroup;
        private bool enable = false;
        private bool changePwdAtNextLogOn = false;
        private bool cannotChangePwd = false;
        private bool pwdNeverExpired = false;
        private DateTime pwdLastChangeDate = new DateTime();
        private DateTime joinDate = new DateTime();
        private string lastModified;
        private DateTime lastModifiedDate = DateTime.Now;
        private bool isExist = false;

        public UserAccountInfo()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string AccountId
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string FullName
        {
            get { return this.fullname; }
            set { this.fullname = value; }
        }

        public string Description
        {
            get { return this.descr; }
            set { this.descr = value; }
        }

        public string Email
        {
            get { return this.m_Email; }
            set { this.m_Email = value; }
        }

        public UserGroupInfo UserGroup
        {
            get { return this.userGroup; }
            set { this.userGroup = value; }
        }

        public bool Enable
        {
            get { return this.enable; }
            set { this.enable = value; }
        }

        public DateTime PasswordLastChange
        {
            get { return this.pwdLastChangeDate; }
            set { this.pwdLastChangeDate = value; }
        }

        public bool ChangePasswordAtNextLogon
        {
            get { return this.changePwdAtNextLogOn; }
            set
            {
                this.changePwdAtNextLogOn = value;

                //this.CannotChangePassword = !value;
                //this.PasswordNeverExpires = !value;
            }
        }

        public bool CannotChangePassword
        {
            get { return this.cannotChangePwd; }
            set
            {
                this.cannotChangePwd = value;
                //this.ChangePasswordAtNextLogon = !this.m_CannotChangePwd;
            }
        }

        public bool PasswordNeverExpires
        {
            get { return this.pwdNeverExpired; }
            set
            {
                this.pwdNeverExpired = value;
                //this.m_ChangePwdAtNextLogon = !m_PwdNeverExpires;
            }
        }

        public DateTime JoinDate
        {
            get {
                if (joinDate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || joinDate <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return joinDate;
                }
            }
            set { this.joinDate = value; }
        }
        public bool IsLogOn
        {
            get { return isExist; }
            set { isExist = value; }
        }
        public string LastModifiedBy
        {
            get { return this.lastModified; }
            set { this.lastModified = value; }
        }
        public DateTime LastModifiedDate
        {
            get { 
                if (this.lastModifiedDate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || this.lastModifiedDate <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return this.lastModifiedDate;
                }
            }
            set { this.lastModifiedDate = value; }
        }

    }
}
