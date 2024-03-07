using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace PCUOnline.Dao
{
    public class DaoManagerBuilder
    {
        public static DaoManager Build(DaoProperty prop)
        {
            try
            {
                DaoManager daoManager = (DaoManager)Assembly.Load(prop.DaoManagerAssembly).CreateInstance(prop.DaoManagerClass);
                daoManager.AddProperty(prop);

                return daoManager;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw ex;
            }
        }
    }
}
