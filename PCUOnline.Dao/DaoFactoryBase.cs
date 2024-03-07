using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace PCUOnline.Dao
{
    public abstract class DaoFactoryBase
    {
        private DaoManager daoManager;

        protected DaoFactoryBase() { }

        protected DaoManager DaoManager
        {
            get { return daoManager; }
            set { daoManager = value; }
        }

        public void StartTransaction()
        {
            StartTransaction(false);
        }
        public void StartTransaction(bool readOnly)
        {
            daoManager.StartTransaction(readOnly);
        }
        public void CommitTransaction()
        {
            daoManager.CommitTrasnaction();
        }
        public void EndTransaction()
        {
            daoManager.EndTransaction();
        }
    }
}
