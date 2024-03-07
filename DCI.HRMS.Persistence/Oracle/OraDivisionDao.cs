using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using DCI.HRMS.Model;
using PCUOnline.Dao;
using PCUOnline.Dao.Ora;
using DCI.HRMS.Model.Organize;

namespace DCI.HRMS.Persistence.Oracle
{
    public class OraDivisionDao : DaoBase , IDivisionDao
    {
        private const string SP_SELECT = "pkg_hr_div.sp_get";
        private const string SP_SELECT_All = "pkg_hr_div.sp_getall";
        private const string SP_SELECT_BY_TYPE = "pkg_hr_div.sp_getbytype";
        private const string SP_SELECT_BY_OWNER = "pkg_hr_div.sp_getbranch";

        private const string PARAM_CODE = "p_dv_cd";
        private const string PARAM_TYPE = "p_dv_type";
        private const string PARAM_OWNER = "p_hdv_cd";

        public OraDivisionDao(DaoManager daoManager)
            : base(daoManager)
        {
        }

        public override void AddParameters(IDbCommand cmd, object obj)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override object QueryForObject(DataRow row, Type t)
        {
            if (t == typeof(DivisionInfo))
            {
                DivisionInfo item = new DivisionInfo();
                try
                {
                    item.Code = OraHelper.DecodeLanguage((string)this.Parse(row, "dv_cd"));
                }
                catch
                { }
                try
                {
                    if (item.Code == "")
                    {
                        item.Code = OraHelper.DecodeLanguage((string)this.Parse(row, "dvcd"));
                    }
                }
                catch { }

                try
                {
                    item.Name = OraHelper.DecodeLanguage((string)this.Parse(row, "dv_ename"));
                }
                catch { }
                try
                {
                    item.ShortName = OraHelper.DecodeLanguage((string)this.Parse(row, "dv_descr"));
                }
                catch { }
                try
                {
                    item.Type = DivisionInfo.ConvertToDivisionType(
                        OraHelper.DecodeLanguage((string)this.Parse(row, "dv_type")));
                }
                catch { }
                try
                {
                    item.DivisionOwner = new DivisionInfo();
                    item.DivisionOwner.Code = OraHelper.DecodeLanguage((string)this.Parse(row, "dv_hdv_cd"));
                }
                catch { }
                try
                {
                    item.DivisionOwner.Name = OraHelper.DecodeLanguage((string)this.Parse(row, "dv_hdv_ename"));
                }
                catch { }
                try
                {
                    item.DivisionOwner.ShortName = OraHelper.DecodeLanguage((string)this.Parse(row, "dv_hdv_descr"));
                }
                catch { }
                return item;
            }
            else
            {
                return null;
            }
        }

        public DivisionInfo QueryForObject(DataRow row, DivisionType type)
        {
            DivisionInfo item = new DivisionInfo();
            item.Type = type;

            if (type == DivisionType.Department)
            {
                try
                {
                    item.Code = OraHelper.DecodeLanguage((string)this.Parse(row, "dept_cd"));
                }
                catch
                { }
                try
                {
                    if (item.Code == "")
                    {
                        item.Code = OraHelper.DecodeLanguage((string)this.Parse(row, "dvcd"));
                    }
                }
                catch { }

                try
                {
                    item.Name = OraHelper.DecodeLanguage((string)this.Parse(row, "dept_ename"));
                }
                catch { }
                try
                {
                    item.ShortName = OraHelper.DecodeLanguage((string)this.Parse(row, "dept_descr"));
                }
                catch { }
            }
            else if (type == DivisionType.Section)
            {
                try
                {
                    item.Code = OraHelper.DecodeLanguage((string)this.Parse(row, "sect_cd"));
                }
                catch { }
                try
                {
                    item.Name = OraHelper.DecodeLanguage((string)this.Parse(row, "sect_ename"));
                }
                catch { }
                try
                {
                    item.ShortName = OraHelper.DecodeLanguage((string)this.Parse(row, "sect_descr"));
                }
                catch { }
            }
            else if (type == DivisionType.Group)
            {
                try
                {
                    item.Code = OraHelper.DecodeLanguage((string)this.Parse(row, "grp_cd"));
                }
                catch { }
                try
                {
                    item.Name = OraHelper.DecodeLanguage((string)this.Parse(row, "grp_ename"));
                }
                catch { }
                try
                {
                    item.ShortName = OraHelper.DecodeLanguage((string)this.Parse(row, "grp_descr"));
                }
                catch { }
            }
            return item;
        }
        #region IDivisionDao Members

        public DivisionInfo Select(string divisionCode)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_CODE,OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(divisionCode);

            return (DivisionInfo)OraHelper.ExecuteQuery(this, this.Transaction, cmd, typeof(DivisionInfo));
        }
        public ArrayList SelectAll()
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_All, CommandType.StoredProcedure);

            ArrayList items = OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(DivisionInfo));
            return items;
        }
        public ArrayList SelectByType(string typeCode)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_BY_TYPE, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_TYPE, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(typeCode);

            ArrayList items = OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(DivisionInfo));
            return items;
        }

        public ArrayList SelectByOwner(string divisionOwnerCode)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_BY_OWNER, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_OWNER, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(divisionOwnerCode);

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(DivisionInfo));
        }

        #endregion
    }
}
