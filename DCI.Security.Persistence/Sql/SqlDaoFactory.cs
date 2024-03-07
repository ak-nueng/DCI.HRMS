using System;
using System.Collections.Generic;
using System.Text;

namespace DCI.Security.Persistence.Sql
{
    public class SqlDaoFactory : DaoFactory
    {
        public override IAllowModuleDao CreateAllowModuleDao()
        {
            return new SqlAllowModuleDao(this.DaoManager);
        }
        public override IModuleDao CreateModuleDao()
        {
            return new SqlModuleDao(this.DaoManager);
        }
        public override IUserAccountDao CreateUserAccountDao()
        {
            return new SqlUserAccountDao(this.DaoManager);
        }
        public override IUserGroupDao CreateUserGroupDao()
        {
            return new SqlUserGroupDao(this.DaoManager);
        }
    }
}
