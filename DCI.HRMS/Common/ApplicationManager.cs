using System;
using System.Collections.Generic;
using System.Text;
using DCI.Security.Model;
using System.Collections;

namespace DCI.HRMS.Common
{
    public class ApplicationManager
    {
        private readonly static ApplicationManager instance = new ApplicationManager();
        private UserAccountInfo account;

        private ApplicationManager()
        {
        }

        public static ApplicationManager Instance()
        {
            return instance;
        }

        public UserAccountInfo UserAccount
        {
            get { return account; }
            set { account = value; }
        }

        public void Copy(ArrayList fromArray, ArrayList toArray)
        {
            if (toArray == null)
                toArray = new ArrayList();
            if (toArray.Count > 0)
                toArray = new ArrayList();

            if (fromArray != null && fromArray.Count > 0)
            {
                foreach (object o in fromArray)
                {
                    toArray.Add(o);
                }
            }
        }
    }
}
