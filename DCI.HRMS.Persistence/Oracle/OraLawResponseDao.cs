using System;
using System.Collections.Generic;
using System.Text;
using PCUOnline.Dao;
using DCI.HRMS.Model.Allowance;
using System.Collections;
using Oracle.ManagedDataAccess.Client;
using PCUOnline.Dao.Ora;
using System.Data;
namespace DCI.HRMS.Persistence.Oracle
{
    class OraLawResponseDao : DaoBase, ILawResponseDao
    {
        private const string SP_GroupMaster = "PKG_lawresp.sp_get_mstr_group";
        private const string SP_GroupMaster_SELECT_Unq = "PKG_lawresp.sp_get_mstr_group_unq";
        private const string SP_GroupMaster_SELECT_TYPE = "PKG_lawresp.sp_get_mstr_group_type";
        private const string SP_GroupMaster_STORE = "PKG_lawresp.sp_mstr_group_store";
        private const string SP_GroupMaster_DELETE = "PKG_lawresp.sp_mstr_group_delete";
        private const string SP_Master = "PKG_lawresp.sp_get_mstr";
        private const string SP_Master_SELECT_UNQ = "PKG_lawresp.sp_get_mstr_unq";
        private const string SP_Master_SELECT_ByGroupId = "PKG_lawresp.sp_get_mstr_byGroupid";
        private const string SP_Master_STORE = "PKG_lawresp.sp_mstr_store";
        private const string SP_Master_DELETE = "PKG_lawresp.sp_mstr_delete";
        private const string SP_RespEmp_SELECT = "PKG_lawresp.sp_get_resemp";
        private const string SP_RespEmp_STORE = "PKG_lawresp.sp_resemp_store";
        private const string SP_RespEmp_DELETE = "PKG_lawresp.sp_resemp_delete";

        private const string PARAM_ID = "p_resid";
        private const string PARAM_CODE = "p_empcode";
        private const string PARAM_TYPE = "p_type";

        private const string PARAM_Action = "p_action";
        private const string PARAM_Name = "p_resname";
        private const string PARAM_Datail = "p_resdetail";
        private const string PARAM_Remark = "p_remark";

        public OraLawResponseDao(DaoManager daoManager)
            : base(daoManager)
        {
        }
        public override object QueryForObject(System.Data.DataRow row, Type t)
        {
            if (t == typeof(LawResponseInfo))
            {

                LawResponseInfo item = new LawResponseInfo();

                try
                {
                    item.ResId = Convert.ToString(this.Parse(row, "res_id"));
                }
                catch { }
                try
                {
                    item.GroupResId = Convert.ToString(this.Parse(row, "res_main_id"));
                }
                catch { }
                try
                {
                    item.ResName = OraHelper.DecodeLanguage((string)this.Parse(row, "res_name"));
                }
                catch { }
                try
                {
                    item.ReaDetail = OraHelper.DecodeLanguage((string)this.Parse(row, "res_detail"));
                }
                catch { }
                try
                {
                    item.Remark = OraHelper.DecodeLanguage((string)this.Parse(row, "remark"));
                }
                catch { }
                return item;
            }
            if (t == typeof(LawResponseGroupInfo))
            {

                LawResponseGroupInfo item = new LawResponseGroupInfo();

                try
                {
                    item.ResId = Convert.ToString(this.Parse(row, "res_id"));
                }
                catch { }
                try
                {
                    item.Type = Convert.ToString(this.Parse(row, "type"));
                }
                catch { }
                try
                {
                    item.ResName = OraHelper.DecodeLanguage((string)this.Parse(row, "res_name"));
                }
                catch { }
                try
                {
                    item.ReaDetail = OraHelper.DecodeLanguage((string)this.Parse(row, "res_detail"));
                }
                catch { }
                try
                {
                    item.Remark = OraHelper.DecodeLanguage((string)this.Parse(row, "remark"));
                }
                catch { }
                return item;
            }
            if (t == typeof(EmpLawResponseInfo))
            {
                EmpLawResponseInfo item = new EmpLawResponseInfo();

                try
                {
                    item.LawRespId = Convert.ToString(this.Parse(row, "res_id"));
                }
                catch { }
                try
                {
                    item.EmpCode = Convert.ToString(this.Parse(row, "code"));
                }
                catch { }
                try
                {
                    item.LicenseNo = Convert.ToString(this.Parse(row, "license_no"));
                }
                catch { }
                try
                {
                    item.LicenseDate = Convert.ToDateTime(this.Parse(row, "license_date"));
                }
                catch { }
                try
                {
                    item.LicenseExp = Convert.ToDateTime(this.Parse(row, "license_exp"));
                }
                catch { }
                try
                {
                    item.Spare = Convert.ToInt16(this.Parse(row, "spare"));
                }
                catch { }
                try
                {
                    item.Remark = OraHelper.DecodeLanguage((string)this.Parse(row, "remark"));
                }
                catch { }
                return item;
            }
            else
            {
                return null;
            }

        }

        public override void AddParameters(System.Data.IDbCommand cmd, object obj)
        {

        }

        #region ILawResponseDao Members

        public ArrayList SelectAllGroupMaster()
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_GroupMaster, CommandType.StoredProcedure);
            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(LawResponseGroupInfo));

        }

        public ArrayList SelectGroupMaster(string type)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_GroupMaster_SELECT_TYPE, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_TYPE, OracleDbType.Varchar2).Value = type;
            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(LawResponseGroupInfo));
        }
        public LawResponseGroupInfo GetGroupMasterUnq(string id)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_GroupMaster_SELECT_Unq, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_ID, OracleDbType.Varchar2).Value = id;
            return OraHelper.ExecuteQuery(this, this.Transaction, cmd, typeof(LawResponseGroupInfo)) as LawResponseGroupInfo;

        }

        public void SaveGroupMaster(LawResponseGroupInfo grp)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_GroupMaster_STORE, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_Action, OracleDbType.Varchar2).Value = "ADD";
            cmd.Parameters.Add(PARAM_ID, OracleDbType.Varchar2).Value = grp.ResId;
            cmd.Parameters.Add(PARAM_Name, OracleDbType.Varchar2).Value =  OraHelper.EncodeLanguage( grp.ResName);
            cmd.Parameters.Add(PARAM_Datail, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage( grp.ReaDetail);
            cmd.Parameters.Add(PARAM_TYPE, OracleDbType.Varchar2).Value = grp.Type;
            cmd.Parameters.Add(PARAM_Remark, OracleDbType.Varchar2).Value =OraHelper.EncodeLanguage(  grp.Remark);
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void UpdateGroupMaster(LawResponseGroupInfo grp)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_GroupMaster_STORE, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_Action, OracleDbType.Varchar2).Value = "UPDATE";
            cmd.Parameters.Add(PARAM_ID, OracleDbType.Varchar2).Value = grp.ResId;
            cmd.Parameters.Add(PARAM_Name, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage( grp.ResName);
            cmd.Parameters.Add(PARAM_Datail, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage( grp.ReaDetail);
            cmd.Parameters.Add(PARAM_TYPE, OracleDbType.Varchar2).Value = grp.Type;
            cmd.Parameters.Add(PARAM_Remark, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage( grp.Remark);
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void DeleteGroupMaster(string id)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_GroupMaster_DELETE, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_ID, OracleDbType.Varchar2).Value = id;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }


        public ArrayList SelectAllMaster()
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_Master, CommandType.StoredProcedure);

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(LawResponseInfo));

        }

        public void SaveMaster(LawResponseInfo master)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_Master_STORE, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_Action, OracleDbType.Varchar2).Value = "ADD";

            cmd.Parameters.Add("p_res_id", OracleDbType.Varchar2).Value = master.ResId;
            cmd.Parameters.Add("p_res_main_id", OracleDbType.Varchar2).Value = master.GroupResId;
            cmd.Parameters.Add("p_res_name", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage( master.ResName);
            cmd.Parameters.Add("p_res_detail", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage( master.ReaDetail);
            cmd.Parameters.Add("p_remark", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage( master.Remark);
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void UpdateMaster(LawResponseInfo master)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_Master_STORE, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_Action, OracleDbType.Varchar2).Value = "UPDATE";

            cmd.Parameters.Add("p_res_id", OracleDbType.Varchar2).Value = master.ResId;
            cmd.Parameters.Add("p_res_main_id", OracleDbType.Varchar2).Value =  master.GroupResId;
            cmd.Parameters.Add("p_res_name", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage( master.ResName);
            cmd.Parameters.Add("p_res_detail", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage( master.ReaDetail);
            cmd.Parameters.Add("p_remark", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage( master.Remark);
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void DeleteMaster(string id)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_Master_DELETE, CommandType.StoredProcedure);
            cmd.Parameters.Add("p_res_id", OracleDbType.Varchar2).Value = id;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }


        public LawResponseInfo GetMaster(string resId)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_Master_SELECT_UNQ, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_ID, OracleDbType.Varchar2).Value = resId;
            return (LawResponseInfo)OraHelper.ExecuteQuery(this, this.Transaction, cmd, typeof(LawResponseInfo));

        }

        public ArrayList SelectAllMaster(string groupId)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_Master_SELECT_ByGroupId, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_ID, OracleDbType.Varchar2).Value = groupId;

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(LawResponseInfo));

        }




        public ArrayList SelectEmpResponse(string respId, string code)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_RespEmp_SELECT, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_ID, OracleDbType.Varchar2).Value = respId;

            cmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = code;

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(EmpLawResponseInfo));

        }

        public void SaveEmpResponse(EmpLawResponseInfo master)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_RespEmp_STORE, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_Action, OracleDbType.Varchar2).Value = "ADD";
            cmd.Parameters.Add("p_res_id", OracleDbType.Varchar2).Value = master.LawRespId;
            cmd.Parameters.Add("p_code", OracleDbType.Varchar2).Value = master.EmpCode;
            cmd.Parameters.Add("p_license_no", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage( master.LicenseNo);
            cmd.Parameters.Add("p_license_exp", OracleDbType.Date).Value = master.LicenseExp;
            cmd.Parameters.Add("p_license_date", OracleDbType.Date).Value = master.LicenseDate;
            cmd.Parameters.Add("p_spare", OracleDbType.Varchar2).Value = master.Spare;
            cmd.Parameters.Add("p_remark", OracleDbType.Varchar2).Value =OraHelper.EncodeLanguage(  master.Remark);
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);

        }

        public void UpdateEmpResponse(EmpLawResponseInfo master)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_RespEmp_STORE, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_Action, OracleDbType.Varchar2).Value = "UPDATE";
            cmd.Parameters.Add("p_res_id", OracleDbType.Varchar2).Value = master.LawRespId;
            cmd.Parameters.Add("p_code", OracleDbType.Varchar2).Value = master.EmpCode;
            cmd.Parameters.Add("p_license_no", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage( master.LicenseNo);
            cmd.Parameters.Add("p_license_exp", OracleDbType.Date).Value = master.LicenseExp;
            cmd.Parameters.Add("p_license_date", OracleDbType.Date).Value = master.LicenseDate;
            cmd.Parameters.Add("p_spare", OracleDbType.Varchar2).Value = master.Spare;
            cmd.Parameters.Add("p_remark", OracleDbType.Varchar2).Value =OraHelper.EncodeLanguage(  master.Remark);
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void DeleteEmpResponse(string respId, string code)
        {

            OracleCommand cmd = OraHelper.CreateCommand(SP_RespEmp_DELETE, CommandType.StoredProcedure);

            cmd.Parameters.Add("p_res_id", OracleDbType.Varchar2).Value = respId;
            cmd.Parameters.Add("p_code", OracleDbType.Varchar2).Value = code;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }


        #endregion
    }
}
