using System;
using System.Text;
using Oracle.ManagedDataAccess.Client;
//using System.Data.OracleClient;
using System.Data;

namespace PCUOnline.Dao.Ora
{
    public class OraDaoManager : DaoManager
    {
        public override void StartTransaction(bool readOnly)
        {
            
            OracleConnection c = new OracleConnection(this.Property.ConnectionString);

            //string connStr = @"Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = DCIDMC)(PORT = 1521)))(CONNECT_DATA =(SID = ORCL)));User Id=dci;Password=dcipsn";
            //System.Data.OracleClient.OracleConnection c = new System.Data.OracleClient.OracleConnection(connStr);
            
            this.AddTransaction(new DaoTransaction(c, (readOnly == false)));
        }
    }
}
