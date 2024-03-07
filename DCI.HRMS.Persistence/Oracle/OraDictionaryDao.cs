using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using DCI.HRMS.Model;
using PCUOnline.Dao;
using PCUOnline.Dao.Ora;

namespace DCI.HRMS.Persistence.Oracle
{
    public class OraDictionaryDao : DaoBase, IDictionaryDao
    {
        private const string SP_SELECT = "pkg_mstr_data.sp_getbytype";
        private const string SP_STORE = "pkg_mstr_data.sp_store";
        private const string SP_DELETE = "pkg_mstr_data.sp_delete";
        private const string SP_SELECTALLTYPE = "pkg_mstr_data.sp_getalltype";

        private const string PARAM_CODE = "p_code";
        private const string PARAM_TYPE = "p_type";

        private const string PARAM_Action = "p_action";

        private const string PARAM_Descr = "p_descr";
        private const string PARAM_Note = "p_note";
        private const string PARAM_Titem = "p_titem";
        private const string PARAM_Desc2 = "p_desc2";

        public OraDictionaryDao(DaoManager daoManager)
            : base(daoManager)
        {
        }

        public override void AddParameters(IDbCommand cmd, object obj)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override object QueryForObject(DataRow row, Type t)
        {
            if (t == typeof(BasicInfo))
            {
                BasicInfo item = new BasicInfo();
                try
                {
                    item.Type = OraHelper.DecodeLanguage((string)this.Parse(row, "type"));
                }
                catch { }
                try
                {
                    item.Code = OraHelper.DecodeLanguage((string)this.Parse(row, "dt_code"));
                }
                catch { }
                try
                {
                    item.Name = OraHelper.DecodeLanguage((string)this.Parse(row, "dt_name"));
                }
                catch { }
                try
                {
                    item.Description = OraHelper.DecodeLanguage((string)this.Parse(row, "dt_descr"));
                }
                catch { }
                try
                {
                    item.DetailEn = OraHelper.DecodeLanguage((string)this.Parse(row, "dt_en"));
                }
                catch { }
                try
                {
                    item.DetailTh = OraHelper.DecodeLanguage((string)this.Parse(row, "dt_th"));
                }
                catch { }
                try
                {
                    item.DescriptionTh = OraHelper.DecodeLanguage((string)this.Parse(row, "dt_tdescr"));
                }
                catch { }


                return item;
            }
            else
            {
                return null;
            }
        }

        #region IDictionaryDao Members

        public BasicInfo Select(string type, string code)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_TYPE, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(type);
            cmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(code);

            return (BasicInfo)OraHelper.ExecuteQuery(this, this.Transaction, cmd, typeof(BasicInfo));
        }

        public ArrayList SelectAll(string type)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_TYPE, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(type);
            cmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = "%";

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(BasicInfo));
        }
        public ArrayList Find(string type, string code)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_TYPE, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(type);
            cmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = code;

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(BasicInfo));
        }

        #endregion

        #region IDictionaryDao Members


        public void SaveDictData(BasicInfo _data)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_Action, OracleDbType.Varchar2).Value = "ADD";
            cmd.Parameters.Add(PARAM_TYPE, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(_data.Type);
            cmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(_data.Code);
            cmd.Parameters.Add(PARAM_Descr, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(_data.DetailEn);
            cmd.Parameters.Add(PARAM_Note, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(_data.Description);
            cmd.Parameters.Add(PARAM_Titem, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(_data.DescriptionTh);
            cmd.Parameters.Add(PARAM_Desc2, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(_data.DetailTh);

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);

        }

        public void UpdateDictData(BasicInfo _data)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_Action, OracleDbType.Varchar2).Value = "UPDATE";
            cmd.Parameters.Add(PARAM_TYPE, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(_data.Type);
            cmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(_data.Code);
            cmd.Parameters.Add(PARAM_Descr, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(_data.DetailEn);
            cmd.Parameters.Add(PARAM_Note, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(_data.Description);
            cmd.Parameters.Add(PARAM_Titem, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(_data.DescriptionTh);
            cmd.Parameters.Add(PARAM_Desc2, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(_data.DetailTh);

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);

        }

        public void DeleteDictData(string type, string code)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_DELETE, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_TYPE, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(type);
            cmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(code);

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);

        }



        public ArrayList SelectAllType()
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECTALLTYPE, CommandType.StoredProcedure);

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(BasicInfo));

        }

        #endregion
    }
}
