using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace PCUOnline.Dao.Sql
{
    public class SqlDaoManager : DaoManager
    {
        public override void StartTransaction(bool readOnly)
        {
            SqlConnection c = new SqlConnection(this.Property.ConnectionString);
            this.AddTransaction(new DaoTransaction(c, (readOnly == false)));
        }
    }
}
