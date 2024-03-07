using System;
using System.Collections.Generic;
using System.Text;

namespace PCUOnline.Dao
{
    public class DaoConfig
    {
        private DaoConfig() { }
        public static DaoManager GetDaoManager()
        {
            return GetDaoManager("DCIGLOBAL");
        }

        public static DaoManager GetDaoManager(string resource)
        {
            DaoManager daoManager = null;
            try
            {
                if (daoManager == null)
                {
                    DaoProperty prop = new DaoProperty(resource);
                    daoManager = DaoManagerBuilder.Build(prop);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return daoManager;
        }
    }
}
