using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DCI.HRMS.Model.Common;
using PCUOnline.Dao;


namespace DCI.HRMS.Persistence.Oracle
{
    public class ObjCommon
    {

        public ObjCommon()
           
        {
        }

        public ObjectInfo QueryForObject(DataRow row)
        {
            ObjectInfo item = new ObjectInfo();


            try
            {
                item.CreateBy = Convert.ToString(this.Parse(row, "cr_by"));
            }
            catch { }
            try
            {
                item.CreateDateTime = Convert.ToDateTime(this.Parse(row, "cr_dt"));
            }
            catch { }
            try
            {
                item.LastUpdateBy = Convert.ToString(this.Parse(row, "upd_by"));
            }
            catch { }
            try
            {
                item.LastUpdateDateTime = Convert.ToDateTime(this.Parse(row, "upd_dt"));
            }
            catch { }
            return item;


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


    }
}
