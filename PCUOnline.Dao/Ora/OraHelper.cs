using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using Oracle.ManagedDataAccess.Client;
//using System.Data.OracleClient;


namespace PCUOnline.Dao.Ora
{
    public class OraHelper
    {
        public static OracleCommand CreateCommand(string commandText, CommandType cmdType)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = commandText;
            cmd.CommandType = cmdType;
            return cmd;
        }
        public static int ExecuteNonQuery(DaoTransaction tx, OracleCommand cmd)
        {
            SetConnection(tx, cmd);
            int t=cmd.ExecuteNonQuery();
            return t;
        }
        public static object ExecuteQuery(DaoBase dao, DaoTransaction tx, OracleCommand cmd, Type t)
        {
            DataSet ds = GetDataSet(tx, cmd);
            return dao.QueryForObject(ds.Tables["Data"].Rows[0], t);
        }
        public static object ExecuteQuery(DaoBase dao, OracleCommand cmd, Type t)
        {
            DataSet ds = GetDataSet(dao.Transaction, cmd);
            return dao.QueryForObject(ds.Tables["Data"].Rows[0], t);
        }
        public static ArrayList ExecuteQueries(DaoBase dao, DaoTransaction tx, OracleCommand cmd, Type t)
        {
            //throw new Exception("Connection State: " + dao.Transaction.Connection.State.ToString());
            DataSet ds = GetDataSet(tx, cmd);
            return dao.QueryForList(ds.Tables["Data"].Rows, t);
        }
        public static ArrayList ExecuteQueries(DaoBase dao, OracleCommand cmd, Type t)
        {
            //throw new Exception("Connection State: " + dao.Transaction.Connection.State.ToString());
            DataSet ds = GetDataSet(dao.Transaction, cmd);
            return dao.QueryForList(ds.Tables["Data"].Rows, t);
        }
        public static DataSet GetDataSet(OracleCommand cmd)
        {
            return GetDataSet(cmd, "Data");
        }
        public static DataSet GetDataSet(OracleCommand cmd , string dataTableName)
        {
            DataSet set2;
            try
            {
                if (!cmd.Parameters.Contains("cur_out"))
                {
                    cmd.Parameters.Add("cur_out", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    //cmd.Parameters["@cur_out"].Direction = ParameterDirection.Output;
                }
    

                OracleDataAdapter adapter1 = new OracleDataAdapter(cmd);
                DataSet set1 = new DataSet();

                adapter1.Fill(set1, dataTableName);
                if (set1.Tables[dataTableName].Rows.Count == 0)
                {
                    throw new Exception("Sorry, we couldn't find any data.");
                }
                set2 = set1;
            }
            catch
            {
                throw;
            }
            return set2;
        }
        public static DataSet GetDataSet(DaoTransaction tx,OracleCommand cmd)
        {
            SetConnection(tx, cmd);
            return GetDataSet(cmd);
        }
        public static DataSet GetDataSet(DaoTransaction tx, OracleCommand cmd , string dataTableName)
        {
            SetConnection(tx, cmd);
            return GetDataSet(cmd, dataTableName);
        }
        private static void SetConnection(DaoTransaction tx, OracleCommand cmd)
        {
            cmd.Connection = tx.Connection as OracleConnection;
            //cmd.Transaction = (OracleTransaction)tx.Transaction;
        }
        public static string DecodeLanguage(string data)
        {
            StringBuilder output = new StringBuilder();

            if (data != null && data.Length > 0)
            {
                foreach (char c in data)
                {
                    int ascii = (int)c;

                    if (ascii > 160)
                    {
                        ascii += 3424;
                    }
                    output.Append((char)ascii);
                }
            }

            return output.ToString();
        }
        public static string EncodeLanguage(string data)
        {
            StringBuilder output = new StringBuilder();
            if (data != null && data.Length > 0)
            {
                foreach (char c in data)
                {
                    int ascii = (int)c;

                    if (ascii >= 160)
                    {
                        ascii -= 3424;
                    }

                    output.Append((char)ascii);
                }
            }
            return output.ToString();
        }
    }
}
