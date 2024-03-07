//using DCIBizPro.DTO.SM;
using DCI.Security.Model;
namespace DCI.HRMS
{
	/// <summary>
	/// Summary description for CurrentAccount.
	/// </summary>
	public class ApplicationContext
	{
        private static ApplicationContext context;
		private static UserAccountInfo m_Account;
        
        private UserAccountInfo account;

		private ApplicationContext()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static UserAccountInfo Info
		{
			set { ApplicationContext.m_Account = value; }
			get
			{
				if (ApplicationContext.m_Account == null)
				{
					ApplicationContext.m_Account = new UserAccountInfo();
				}
				return ApplicationContext.m_Account;
			}
		}

        public static ApplicationContext Current
        {
            get
            {
                if (context == null)
                    context = new ApplicationContext();

                return context;
            }
        }

        public UserAccountInfo Account
        {
            set { account = value; }
            get { return account; }
        }
	}
}