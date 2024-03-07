using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.OleDb;

namespace PCUOnline.Dao.Ole
{
    public class OleHelper
    {
        private OleHelper() { }

        public static OleDbCommand CreateCommand(string commandText , CommandType cmdType)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = commandText;
            cmd.CommandType = cmdType;
            return cmd;
        }
        public static int ExecuteNonQuery(DaoTransaction tx , OleDbCommand cmd)
        {
            SetConnection(tx, cmd);
            return cmd.ExecuteNonQuery();
        }
        public static object ExecuteQuery(DaoBase dao , DaoTransaction tx , OleDbCommand cmd , Type t)
        {
            DataSet ds = GetDataSet(tx , cmd);
            return dao.QueryForObject(ds.Tables["Data"].Rows[0], t);
        }
        public static object ExecuteQuery(DaoBase dao, OleDbCommand cmd, Type t)
        {
            DataSet ds = GetDataSet(dao.Transaction, cmd);
            return dao.QueryForObject(ds.Tables["Data"].Rows[0], t);
        }
        public static ArrayList ExecuteQueries(DaoBase dao, DaoTransaction tx, OleDbCommand cmd, Type t)
        {
            DataSet ds =OleHelper.GetDataSet(tx , cmd);
            return dao.QueryForList(ds.Tables["Data"].Rows, t);
        }
        public static ArrayList ExecuteQueries(DaoBase dao, OleDbCommand cmd, Type t)
        {
            DataSet ds = OleHelper.GetDataSet(dao.Transaction, cmd);
            return dao.QueryForList(ds.Tables["Data"].Rows, t);
        }
        public static DataSet GetDataSet(OleDbCommand cmd)
        {
            DataSet set2;
            try
            {
                OleDbDataAdapter adapter1 = new OleDbDataAdapter(cmd);
                DataSet set1 = new DataSet();
                adapter1.Fill(set1, "Data");
                if (set1.Tables["Data"].Rows.Count == 0)
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
        public static DataSet GetDataSet(DaoTransaction tx , OleDbCommand cmd)
        {
            SetConnection(tx, cmd);
            return GetDataSet(cmd);
        }
        private static void SetConnection(DaoTransaction tx , OleDbCommand cmd)
        {
            cmd.Connection = (OleDbConnection)tx.Connection;
            cmd.Transaction = (OleDbTransaction)tx.Transaction;
        }
    }
}
