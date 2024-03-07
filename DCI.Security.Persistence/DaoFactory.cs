using System;
using System.Collections.Generic;
using System.Text;
using PCUOnline.Dao;
using System.Reflection;

namespace DCI.Security.Persistence
{
    public abstract class DaoFactory : DaoFactoryBase
    {
        internal DaoFactory() { }

        public static DaoFactory Instance()
        {
            DaoManager tmpDaoManager = DaoConfig.GetDaoManager("SYSDCI");
            DaoProperty prop = tmpDaoManager.Property;

            DaoFactory factory = (DaoFactory)Assembly.Load(prop.DaoFactoryAssembly).CreateInstance(prop.DaoFactoryClass);
            factory.DaoManager = tmpDaoManager;

            return factory;
        }

        public abstract IAllowModuleDao CreateAllowModuleDao();
        public abstract IModuleDao CreateModuleDao();
        public abstract IUserAccountDao CreateUserAccountDao();
        public abstract IUserGroupDao CreateUserGroupDao();
    }
}
