using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace PCUOnline.Dao
{
    public abstract class DaoBase
    {
        private DaoManager daoManager = null;

        public DaoBase(DaoManager daoManager)
        {
            this.daoManager = daoManager;
        }
        public DaoTransaction Transaction
        {
            get
            {
                if (daoManager != null)
                    return daoManager.Transaction;
                
                return null;
            }
        }
        public ArrayList QueryForList(DataRowCollection rows , Type t)
        {
            ArrayList items = new ArrayList();
            foreach (DataRow r in rows)
            {
                items.Add(this.QueryForObject(r, t));
            }
            return items;
        }
        
        protected object Parse(DataRow row, string columnName)
        {
            if (IsColumnExist(row, columnName))
                return row[columnName];

            return null;
        }
        protected bool IsColumnExist(DataRow row, string columnName)
        {
            return IsColumnExist(row.Table, columnName);
        }
        protected bool IsColumnExist(DataTable table, string columnName)
        {
            return IsColumnExist(table.Columns, columnName);   
        }
        protected bool IsColumnExist(DataColumnCollection columns, string columnName)
        {
            return columns.Contains(columnName);
        }
        
        public abstract object QueryForObject(DataRow row, Type t);
        public abstract void AddParameters(IDbCommand cmd, object obj);
    }
}
