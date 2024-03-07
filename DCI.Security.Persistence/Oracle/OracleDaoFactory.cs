using System;
using System.Collections.Generic;
using System.Text;

namespace DCI.Security.Persistence.Ora
{
    public class OracleDaoFactory : DaoFactory
    {
        public override IAllowModuleDao CreateAllowModuleDao()
        {
            return new OracleAllowModuleDao(this.DaoManager);
        }
        public override IModuleDao CreateModuleDao()
        {
            return new OracleModuleDao(this.DaoManager);
        }
        public override IUserAccountDao CreateUserAccountDao()
        {
            return new OracleUserAccountDao(this.DaoManager);
        }
        public override IUserGroupDao CreateUserGroupDao()
        {
            return new OracleUserGroupDao(this.DaoManager);
        }
    }
}
