using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace PCUOnline.Dao.Sql
{
    public class SqlHelper
    {
        private SqlHelper() { }

        public static SqlCommand CreateCommand(string commandText , CommandType cmdType)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = commandText;
            cmd.CommandType = cmdType;
            return cmd;
        }
        public static int ExecuteNonQuery(DaoTransaction tx , SqlCommand cmd)
        {
            SetConnection(tx, cmd);
            return cmd.ExecuteNonQuery();
        }
        public static object ExecuteQuery(DaoBase dao , DaoTransaction tx , SqlCommand cmd , Type t)
        {
            DataSet ds = GetDataSet(tx , cmd);
            return dao.QueryForObject(ds.Tables["Data"].Rows[0], t);
        }
        public static object ExecuteQuery(DaoBase dao, SqlCommand cmd, Type t)
        {
            DataSet ds = GetDataSet(dao.Transaction, cmd);
            return dao.QueryForObject(ds.Tables["Data"].Rows[0], t);
        }
        public static ArrayList ExecuteQueries(DaoBase dao, DaoTransaction tx, SqlCommand cmd, Type t)
        {
            DataSet ds = SqlHelper.GetDataSet(tx , cmd);
            return dao.QueryForList(ds.Tables["Data"].Rows, t);
        }
        public static ArrayList ExecuteQueries(DaoBase dao, SqlCommand cmd, Type t)
        {
            DataSet ds = SqlHelper.GetDataSet(dao.Transaction, cmd);
            return dao.QueryForList(ds.Tables["Data"].Rows, t);
        }
        public static DataSet GetDataSet(SqlCommand cmd)
        {
            DataSet set2;
            try
            {
                SqlDataAdapter adapter1 = new SqlDataAdapter(cmd);
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
        public static DataSet GetDataSet(DaoTransaction tx , SqlCommand cmd)
        {
            SetConnection(tx, cmd);
            return GetDataSet(cmd);
        }
        private static void SetConnection(DaoTransaction tx , SqlCommand cmd)
        {
            cmd.Connection = (SqlConnection)tx.Connection;
            cmd.Transaction = (SqlTransaction)tx.Transaction;
        }
    }
}
