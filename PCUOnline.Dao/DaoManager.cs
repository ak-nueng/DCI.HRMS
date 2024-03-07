using System;
using System.Collections.Generic;
using System.Text;

namespace PCUOnline.Dao
{
    public abstract class DaoManager
    {
        private DaoTransaction transaction = null;
        private DaoProperty prop = null;

        public void StartTransaction()
        {
            StartTransaction(false);
        }
        public void CommitTrasnaction()
        {
            if (transaction != null){
                if (transaction.IsRequiredCommit && transaction.IsEnded == false){
                    transaction.Commit();
                }
            }
        }
        public void EndTransaction(){
            if (transaction != null){
                if (transaction.IsRequiredCommit && transaction.IsEnded == false){
                    transaction.Rollback();
                }
                transaction.Close();
            }
        }
        public void AddProperty(DaoProperty prop)
        {
            this.prop = prop;
        }
        protected void AddTransaction(DaoTransaction tx)
        {
            this.transaction = tx;
        }
        public DaoTransaction Transaction {
            get { return transaction; }
        }
        public DaoProperty Property {
            get { return prop; }
        }
        public abstract void StartTransaction(bool readOnly);
        
    }
}
