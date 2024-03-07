using System;
using System.Collections.Generic;
using System.Text; 
using System.Collections;
using System.Data;

namespace DCI.HRMS.Service
{
   public class ServiceUtility
    {

        public static DataTable ToDataTable(ArrayList alist)
        {
            DataTable dt = new DataTable();
            if (alist[0] == null)
                throw new FormatException("Parameter ArrayList empty");
            dt.TableName = alist[0].GetType().Name;
            DataRow dr;
            System.Reflection.PropertyInfo[] propInfo = alist[0].GetType().GetProperties();
            for (int i = 0; i < propInfo.Length; i++)
            {
                dt.Columns.Add(propInfo[i].Name, propInfo[i].PropertyType);
            }
            for (int row = 0; row < alist.Count; row++)
            {
                dr = dt.NewRow();
                for (int i = 0; i < propInfo.Length; i++)
                {
                    try
                    {
                        object tempObject = alist[row];
                        object t = propInfo[i].GetValue(tempObject, null);
                        /*object t =tempObject.GetType().InvokeMember(propInfo[i].Name,     
                         * R.BindingFlags.GetProperty , null,tempObject , new object [] {});*/
                        if (t != null)
                            dr[i] = t.ToString();
                    }
                    catch{}
                } 
                dt.Rows.Add(dr);
            }
            return dt;
        }
        public static DataTable ToDataTable(ArrayList alist, ArrayList alColNames)
        {
            DataTable dt = new DataTable();
            if (alist[0] == null)
                throw new FormatException("Parameter ArrayList empty");
            dt.TableName = alist[0].GetType().Name;
            DataRow dr;
            System.Reflection.PropertyInfo[] propInfo = alist[0].GetType().GetProperties();
            for (int i = 0; i < propInfo.Length; i++)
            {
                for (int j = 0; j < alColNames.Count; j++)
                {
                    if (alColNames[j].ToString() == propInfo[i].Name)
                    {
                        dt.Columns.Add(propInfo[i].Name, propInfo[i].PropertyType);
                        break;
                    }
                }
            }
            for (int row = 0; row < alist.Count; row++)
            {
                dr = dt.NewRow();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    try
                    {
                        object tempObject = alist[row];
                        object t = propInfo[i].GetValue(tempObject, null);
                        /*object t =tempObject.GetType().InvokeMember(propInfo[i].Name,     
                         * R.BindingFlags.GetProperty , null,tempObject , new object [] {});*/
                        if (t != null)
                            dr[i] = t.ToString();
                    }
                    catch {}
                } 
                dt.Rows.Add(dr);
            } return dt;
        }

    }
}
    


