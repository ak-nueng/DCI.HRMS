using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace DCI.HRMS
{
    internal class ClsOraConnectDB
    {
        private string oradb = "";

        internal ClsOraConnectDB(string HOST)
        {
            if (HOST == "DCI")
            {
                oradb = @"Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = DCIHRM)(PORT = 1521))(CONNECT_DATA = (SID = DCIHRM)));User Id=dci;Password=dcipsn";

            }
            else if (HOST == "DCISUB")
            {
                oradb = @"Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = DCIHRM)(PORT = 1521))(CONNECT_DATA = (SID = DCIHRM)));User Id=dcitc;Password=dcisub";
            }
            else if (HOST == "DCITRN")
            {
                oradb = @"Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = DCIHRM)(PORT = 1521))(CONNECT_DATA = (SID = DCIHRM)));User Id=dev_office;Password=developo";
            }
            else if (HOST == "TRAIN")
            {
                oradb = @"Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = DCIHRM)(PORT = 1521))(CONNECT_DATA = (SID = DCIHRM)));User Id=dci_office;Password=dcioffice1";
            }
        }



        private bool useDB = true;

        public OracleTransaction BeginTrans()
        {
            OracleTransaction Trans;
            OracleConnection objConn = new OracleConnection(oradb);
            objConn.Open();
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
                OracleConnection oraConn = new OracleConnection();
                try
                {
                    //OracleConnection conn = new OracleConnection(connStr);                        
                    oraConn.ConnectionString = oradb;
                    oraConn.Open();
                }
                catch
                {
                    MessageBox.Show("มีปัญหาด้านการเชื่อมต่อระบบ Network กรุณาติดต่อ IT");
                }

                OracleDataAdapter adapter = new OracleDataAdapter(queryStr, oraConn);
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
                    oraConn.Close();
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
                OracleConnection oraConn = new OracleConnection();
                try
                {
                    oraConn.ConnectionString = oradb;
                    oraConn.Open();

                    //OracleConnection conn = new OracleConnection(connStr);
                    commandDb.Connection = oraConn;
                }
                catch
                {
                    MessageBox.Show("มีปัญหาด้านการเชื่อมต่อระบบ Network กรุณาติดต่อ IT");
                }

                OracleDataAdapter adapter = new OracleDataAdapter(commandDb);
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
                    oraConn.Close();
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
            try
            {
                if (useDB)
                {
                    //OracleConnection conn = new OracleConnection(connStr);
                    OracleConnection oraConn = new OracleConnection();
                    oraConn.ConnectionString = oradb;
                    oraConn.Open();

                    try
                    {
                        OracleCommand commandDb = new OracleCommand(exeStr, oraConn);
                        ExecuteCommand(commandDb);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
            catch
            {
                MessageBox.Show("มีปัญหาด้านการเชื่อมต่อระบบ Network กรุณาติดต่อ IT");
            }

        }

        /// <summary>
        /// ExecuteCommand
        /// </summary>
        /// <param name="commandDb">commanddb for execute</param>
        /// <remarks></remarks>
        public void ExecuteCommand(OracleCommand commandDb)
        {
            try
            {
                if (useDB)
                {
                    //OracleConnection conn = new OracleConnection(connStr);
                    OracleConnection oraConn = new OracleConnection();
                    oraConn.ConnectionString = oradb;
                    oraConn.Open();
                    try
                    {
                        commandDb.Connection = oraConn;
                        //conn.Open();
                        commandDb.ExecuteNonQuery();
                        //conn.Close();
                        oraConn.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error : {ex.Message}");
                        Console.WriteLine(ex.ToString());
                    }
                    finally
                    {
                        oraConn.Close();
                    }

                }
            }
            catch
            {
                MessageBox.Show("มีปัญหาด้านการเชื่อมต่อระบบ Network กรุณาติดต่อ IT");
            }
        }


        public int ExecuteCommandReturn(OracleCommand commandDb)
        {
            int result = -1;
            try
            {
                if (useDB)
                {
                    //OracleConnection conn = new OracleConnection(connStr);
                    OracleConnection oraConn = new OracleConnection();
                    oraConn.ConnectionString = oradb;
                    oraConn.Open();
                    try
                    {
                        commandDb.Connection = oraConn;
                        //conn.Open();
                        commandDb.ExecuteNonQuery();
                        //conn.Close();
                        oraConn.Close();

                        result = 1;
                    }
                    catch (Exception ex)
                    {
                        result = -1;
                        MessageBox.Show($"Error : {ex.Message}");
                        Console.WriteLine(ex.ToString());
                    }
                    finally
                    {
                        oraConn.Close();
                    }

                }
            }
            catch
            {
                result = -1;
                //MessageBox.Show("มีปัญหาด้านการเชื่อมต่อระบบ Network กรุณาติดต่อ IT");
            }

            return result;
        }



    }
}
