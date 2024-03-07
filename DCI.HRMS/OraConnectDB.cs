using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Oracle.ManagedDataAccess.Client;
using System.Data.OracleClient;
using System.Data;

namespace DCI.HRMS
{
    class OraConnectDB
    {

        //string oradb = "Data Source=(DESCRIPTION="
        //     + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=DCIDMC)(PORT=1521)))"
        //     + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ORCL)));"
        //     + "User Id=DCI;Password=dcipsn;";

        //string oradbSUB = "Data Source=(DESCRIPTION="
        //         + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=DCIDMC)(PORT=1521)))"
        //         + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ORCL)));"
        //         + "User Id=DCITC;Password=dcisub;";

        //string oradbTRN = "Data Source=(DESCRIPTION="
        //         + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=DCIDMC)(PORT=1521)))"
        //         + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ORCL)));"
        //         + "User Id=DEV_OFFICE;Password=developo;";
        
        
        
        private string connStr = "";

        public OraConnectDB(string DBSource)
        {
            //
            // TODO: Add constructor logic here
            //
            if (DBSource == "DCI"){
                connStr = @"Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = DCIHRM)(PORT = 1521)))(CONNECT_DATA =(SID = DCIHRM)));User Id=dci;Password=dcipsn";

            } else if (DBSource == "DCISUB") {
                connStr = @"Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = DCIHRM)(PORT = 1521)))(CONNECT_DATA =(SID = DCIHRM)));User Id=dcitc;Password=dcisub";
            }
            else if (DBSource == "DCITRN"){
                connStr = @"Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = DCIHRM)(PORT = 1521)))(CONNECT_DATA =(SID = DCIHRM)));User Id=dev_office;Password=developo";
            }
            else if (DBSource == "TRAIN")
            {
                connStr = @"Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = DCIHRM)(PORT = 1521)))(CONNECT_DATA =(SID = DCIHRM)));User Id=dci_office;Password=dcioffice1";
            }          
        }


        private bool useDB = true;
        //private string connStr = System.Configuration.ConfigurationSettings.AppSettings["DesignMachineMonitoring.Properties.Settings.ETD_YCConnectionString"];
        //private string connStr = "Data Source=costy;Initial Catalog=dbDCI; Persist Security Info=True; User ID=sa;Password=decjapan;";


        public OracleTransaction BeginTrans()
        {
            OracleTransaction Trans;
            System.Data.OracleClient.OracleConnection objConn = new System.Data.OracleClient.OracleConnection(connStr);
            Trans = objConn.BeginTransaction(IsolationLevel.ReadCommitted);  //*** Start Transaction
            return Trans;
        }

        public void CommitTrans(OracleTransaction Trans)
        {
            Trans.Commit();  //*** Commit Transaction ***//
        }

        public void RollbackTrans(OracleTransaction Trans)
        {
            Trans.Rollback(); //*** RollBack Transaction ***//
        }

        public DataTable Query(string queryStr)
        {
            if (useDB)
            {
                System.Data.OracleClient.OracleConnection conn = new System.Data.OracleClient.OracleConnection(connStr);
                System.Data.OracleClient.OracleDataAdapter adapter = new System.Data.OracleClient.OracleDataAdapter(queryStr, conn);
                DataTable dTable = new DataTable();
                DataSet dSet = new DataSet();

                try
                {
                    adapter.Fill(dSet, "dataTable");
                    return dSet.Tables["dataTable"];
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return dTable;
                }
                finally {
                    conn.Close();
                }

            }
            else
            {
                return new DataTable();
            }

        }

        /// <summary>
        /// Query table by string and return table 
        /// </summary>
        /// <param name="commandDb">CommandDB for query</param>
        /// <returns>DataTable</returns>
        /// <remarks></remarks>
        public DataTable Query(OracleCommand commandDb)
        {
            if (useDB)
            {
                System.Data.OracleClient.OracleConnection conn = new System.Data.OracleClient.OracleConnection(connStr);
                conn.Open();
                commandDb.Connection = conn;
                System.Data.OracleClient.OracleDataAdapter adapter = new System.Data.OracleClient.OracleDataAdapter(commandDb);
                DataTable dTable = new DataTable();
                DataSet dSet = new DataSet();

                try
                {
                    adapter.Fill(dSet, "dataTable");
                    return dSet.Tables["dataTable"];
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return dTable;
                }
                finally
                {
                    conn.Close();
                }

            }
            else
            {
                return new DataTable();
            }

            //=================================================================================
        }

        /// <summary>
        /// Execute คำสั่ง sql
        /// </summary>
        /// <param name="exeStr">String of sql</param>
        /// <remarks></remarks>

        public void ExecuteCommand(string exeStr)
        {
            if (useDB)
            {
                System.Data.OracleClient.OracleConnection conn = new System.Data.OracleClient.OracleConnection(connStr);
                conn.Open();
                try
                {
                    OracleCommand commandDb = new OracleCommand(exeStr, conn);
                    ExecuteCommand(commandDb);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        /// <summary>
        /// ExecuteCommand
        /// </summary>
        /// <param name="commandDb">commanddb for execute</param>
        /// <remarks></remarks>
        public void ExecuteCommand(OracleCommand commandDb)
        {
            if (useDB)
            {
                System.Data.OracleClient.OracleConnection conn = new System.Data.OracleClient.OracleConnection(connStr);
                try
                {
                    commandDb.Connection = conn;
                    conn.Open();
                    commandDb.ExecuteNonQuery();
                    //conn.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    conn.Close();
                }

            }
        }


        public int ExecuteCommandReturn(OracleCommand commandDb)
        {
            int res = 0;
            if (useDB)
            {
                System.Data.OracleClient.OracleConnection conn = new System.Data.OracleClient.OracleConnection(connStr);
                try
                {
                    commandDb.Connection = conn;
                    conn.Open();
                    res = commandDb.ExecuteNonQuery();
                    conn.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    conn.Close();
                }

            }

            return res;
        }

    }
}
