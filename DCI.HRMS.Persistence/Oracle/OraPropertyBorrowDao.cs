using System;
using System.Collections.Generic;

using System.Text;
using PCUOnline.Dao;
using DCI.HRMS.Model.Welfare;
using System.Collections;
using PCUOnline.Dao.Ora;
using DCI.HRMS.Model.Common;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Globalization;
using DCI.HRMS.Model.Personal;

namespace DCI.HRMS.Persistence.Oracle
{
    public class OraPropertyBorrowDao : DaoBase, IPropertyBorrowDao
    {

        private const string SP_STORE = "pkg_prpt.sp_store";
        private const string SP_DELETEE = "pkg_prpt.sp_delete";
        private const string SP_SELECT = "pkg_prpt.sp_select";
        private const string SP_SELECTByID = "pkg_prpt.sp_selectbyid";
        private const string SP_SELECTByCode = "pkg_prpt.sp_selectbycode";
        private const string SP_SELECTMaster = "pkg_prpt.sp_selectmaster";
        private const string SP_STOREMaster = "pkg_prpt.sp_storemaster";
        private const string SP_SELECTUniqLockerMaster = "pkg_prpt.sp_selectuniqlockermstr";
        private const string SP_SELECTLockerMaster = "pkg_prpt.sp_selectlockermstr";

        private const string SP_SELECTLockerBorrowData = "pkg_prpt.sp_selectlockerborrow";
        private const string SP_STORELockerMaster = "pkg_prpt.sp_storelockermstr";



        private const string SP_DELETELockerMaster = "pkg_prpt.sp_deletelockermstr";

        private const string PARAM_ACTION = "p_action";
        private const string PARAM_BorrowId = "p_br_id";
        private const string PARAM_EmpCode = "p_code";
        private const string PARAM_Type = "p_type";
        private const string PARAM_Detail = "p_detail";
        private const string PARAM_Data = "p_data";
        private const string PARAM_Remark = "p_remark";
        private const string PARAM_UsedBy = "p_usedby";
        private const string PARAM_ReturnStatus = "p_rt_sts";
        private const string PARAM_Quantity = "p_qty";
        private const string PARAM_RequestDate = "p_rq_date";
        private const string PARAM_RecieveDate = "p_rc_date";
        private const string PARAM_ReturnDate = "p_rt_date";

        private const string PARAM_Id = "p_id";
        private const string PARAM_Name = "p_name";
        private const string PARAM_Unit = "p_unit";
        private const string PARAM_Price1 = "p_price1";
        private const string PARAM_price2 = "p_price2";
        private const string PARAM_LockerId = "p_lockerid";
        private const string PARAM_KeyCode = "p_keycode";



        private const string PARAM_USER = "p_by";

        public OraPropertyBorrowDao(DaoManager daoManager)
            : base(daoManager)
        {
        }

        public override object QueryForObject(DataRow row, Type t)
        {
            if (t == typeof(PropertyBorrowInfo))
            {

                PropertyBorrowInfo item = new PropertyBorrowInfo();


                try
                {
                    item.BorrowId = OraHelper.DecodeLanguage((string)this.Parse(row, "BR_ID"));
                }
                catch
                { }
                try
                {
                    item.EmpCode = OraHelper.DecodeLanguage((string)this.Parse(row, "CODE"));
                }
                catch
                { }
                try
                {
                    item.EmpName = OraHelper.DecodeLanguage((string)this.Parse(row, "NAME"));
                }

                catch
                { } 
                try
                {
                    item.ResignDate = Convert.ToDateTime(this.Parse(row, "RESIGN"));

                    if (item.ResignDate == DateTime.Parse("01/01/1900 00:00:00"))
                    {   
                        item.ResignDate = DateTime.MinValue;
                    }
                }
                catch
                { }
                try
                {
                    item.Type = OraHelper.DecodeLanguage((string)this.Parse(row, "TYPE"));
                }
                catch
                { }
                try
                {
                    item.TypeName = OraHelper.DecodeLanguage((string)this.Parse(row, "DESCR"));
                }
                catch
                { }
                try
                {
                    item.Detail = OraHelper.DecodeLanguage((string)this.Parse(row, "DETAIL"));
                }
                catch
                { }
                try
                {
                    item.Data = OraHelper.DecodeLanguage((string)this.Parse(row, "DATA"));
                }
                catch
                { }
                try
                {
                    item.Remark = OraHelper.DecodeLanguage((string)this.Parse(row, "REMARK"));
                }
                catch
                { }
                try
                {
                    item.ReturnStatus = (ReturnSts)Convert.ToInt16(this.Parse(row, "RT_STS"));
                }
                catch
                { }
                try
                {
                    item.RecieveDate = Convert.ToDateTime(this.Parse(row, "RQ_DT"));
                }
                catch
                { }
                try
                {
                    item.RequestDate = Convert.ToDateTime(this.Parse(row, "RC_DT"));
                }
                catch
                { }
                try
                {
                    item.ReturnDate = Convert.ToDateTime(this.Parse(row, "RT_DT"));
                }
                catch
                { }
                try
                {
                    item.CreateBy = Convert.ToString(this.Parse(row, "cr_by"));
                }
                catch { }
                try
                {
                    item.CreateDateTime = Convert.ToDateTime(this.Parse(row, "cr_dt"));
                }
                catch { }
                try
                {
                    item.LastUpdateBy = Convert.ToString(this.Parse(row, "upd_by"));
                }
                catch { }
                try
                {
                    item.LastUpdateDateTime = Convert.ToDateTime(this.Parse(row, "upd_dt"));
                }
                catch { }
                try
                {
                    item.Quantity = Convert.ToInt32(this.Parse(row, "qty"));
                }
                catch { }
                return item;

            }

            if (t == typeof(PropertyInfo))
            {
                PropertyInfo item = new PropertyInfo();
                try
                {
                    item.PropertyId = OraHelper.DecodeLanguage((string)this.Parse(row, "ID"));
                }
                catch
                { }
                try
                {
                    item.PropertyName = OraHelper.DecodeLanguage((string)this.Parse(row, "NAME"));
                }
                catch
                { }
                try
                {
                    item.Unit = OraHelper.DecodeLanguage((string)this.Parse(row, "UNIT"));
                }
                catch
                { }
                try
                {
                    item.Data = OraHelper.DecodeLanguage((string)this.Parse(row, "DATA"));
                }
                catch
                { }
                try
                {
                    item.Detail = OraHelper.DecodeLanguage((string)this.Parse(row, "DETAIL"));
                }
                catch
                { }
                try
                {
                    item.Price1 = Convert.ToDecimal((string)this.Parse(row, "PRICE1"));
                }
                catch
                { }
                try
                {
                    item.Price2 = Convert.ToDecimal((string)this.Parse(row, "PRICE2"));
                }
                catch
                { }
                try
                {
                    item.Remark = OraHelper.DecodeLanguage((string)this.Parse(row, "REMARK"));
                }
                catch
                { }
                try
                {
                    item.UsedBy = OraHelper.DecodeLanguage((string)this.Parse(row, "usedby"));
                }
                catch
                { }
                try
                {
                    item.CreateBy = Convert.ToString(this.Parse(row, "cr_by"));
                }
                catch { }
                try
                {
                    item.CreateDateTime = Convert.ToDateTime(this.Parse(row, "cr_dt"));
                }
                catch { }
                try
                {
                    item.LastUpdateBy = Convert.ToString(this.Parse(row, "upd_by"));
                }
                catch { }
                try
                {
                    item.LastUpdateDateTime = Convert.ToDateTime(this.Parse(row, "upd_dt"));
                }
                catch { }
                return item;

            } if (t == typeof(LockerInfo))
            {
                LockerInfo item = new LockerInfo();

                try
                {
                    item.LockerId = Convert.ToString(this.Parse(row, "LockerId"));
                }
                catch
                { }
                try
                {
                    item.KeyCode = Convert.ToString(this.Parse(row, "KeyCode"));
                }
                catch
                { }
                try
                {
                    item.EmpCode = Convert.ToString(this.Parse(row, "EmpCode"));
                }
                catch
                { }
                try
                {
                    item.EName = Convert.ToString(this.Parse(row, "ENAME"));
                }
                catch
                { }
                try
                {
                    item.TName = OraHelper.DecodeLanguage( Convert.ToString(this.Parse(row, "TNAME")));
                }
                catch
                { }
                try
                {
                    item.Dv_ename = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "DV_ENAME")));
                }
                catch
                { }
                try
                {
                    item.Resign = Convert.ToDateTime(this.Parse(row, "RESIGN"));
                }
                catch
                { }
                try
                {
                    item.Remark = OraHelper.DecodeLanguage((string)this.Parse(row, "Remark"));
                }
                catch
                { }
                return item;

            }
            return null;

        }

        public override void AddParameters(IDbCommand cmd, object obj)
        {
            OracleCommand oraCmd = (OracleCommand)cmd;

            if (obj is PropertyBorrowInfo)
            {
                PropertyBorrowInfo item = (PropertyBorrowInfo)obj;
                oraCmd.Parameters.Add(PARAM_BorrowId, OracleDbType.Varchar2).Value = item.BorrowId;
                oraCmd.Parameters.Add(PARAM_EmpCode, OracleDbType.Varchar2).Value = item.EmpCode;
                oraCmd.Parameters.Add(PARAM_Type, OracleDbType.Varchar2).Value = item.Type;
                oraCmd.Parameters.Add(PARAM_Detail, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.Detail);

                oraCmd.Parameters.Add(PARAM_Remark, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.Remark);
                oraCmd.Parameters.Add(PARAM_Data, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.Data);
                oraCmd.Parameters.Add(PARAM_Quantity, OracleDbType.Int32).Value = item.Quantity;
                oraCmd.Parameters.Add(PARAM_ReturnStatus, OracleDbType.Varchar2).Value = ((int)item.ReturnStatus).ToString();

                oraCmd.Parameters.Add(PARAM_RequestDate, OracleDbType.Varchar2).Value = item.RequestDate != DateTime.MinValue ? item.RequestDate.ToString("dd/MMM/yyyy", new CultureInfo("EN-US")) : "";
                oraCmd.Parameters.Add(PARAM_RecieveDate, OracleDbType.Varchar2).Value = item.RecieveDate != DateTime.MinValue ? item.RecieveDate.ToString("dd/MMM/yyyy", new CultureInfo("EN-US")) : "";
                oraCmd.Parameters.Add(PARAM_ReturnDate, OracleDbType.Varchar2).Value = item.ReturnDate != DateTime.MinValue ? item.ReturnDate.ToString("dd/MMM/yyyy", new CultureInfo("EN-US")) : "";
            }
            if (obj is PropertyInfo)
            {
                PropertyInfo item = (PropertyInfo)obj;
                oraCmd.Parameters.Add(PARAM_Id, OracleDbType.Varchar2).Value = item.PropertyId;
                oraCmd.Parameters.Add(PARAM_Name, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.PropertyName);
                oraCmd.Parameters.Add(PARAM_Unit, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.Unit);
                oraCmd.Parameters.Add(PARAM_Data, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.Data);
                oraCmd.Parameters.Add(PARAM_Detail, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.Detail);
                oraCmd.Parameters.Add(PARAM_Price1, OracleDbType.Decimal).Value = item.Price1;
                oraCmd.Parameters.Add(PARAM_price2, OracleDbType.Decimal).Value = item.Price2;
                oraCmd.Parameters.Add(PARAM_Remark, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.Remark);
                oraCmd.Parameters.Add(PARAM_UsedBy, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.UsedBy);

            }
            if (obj is LockerInfo)
            {
                LockerInfo item = (LockerInfo)obj;
                oraCmd.Parameters.Add(PARAM_LockerId, OracleDbType.Varchar2).Value = item.LockerId;
                oraCmd.Parameters.Add(PARAM_KeyCode, OracleDbType.Varchar2).Value = item.KeyCode;
                oraCmd.Parameters.Add(PARAM_EmpCode, OracleDbType.Varchar2).Value = item.EmpCode;
                oraCmd.Parameters.Add(PARAM_Remark, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.Remark);



            }
        }

        #region IPropertyBorrowDao Members

        public PropertyBorrowInfo GetByID(string _br_id)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECTByID, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_BorrowId, OracleDbType.Varchar2).Value = _br_id;

            return (PropertyBorrowInfo)OraHelper.ExecuteQuery(this, this.Transaction, cmd, typeof(PropertyBorrowInfo));

        }

        public ArrayList GetByEmpCode(string _empCode)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECTByCode, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_EmpCode, OracleDbType.Varchar2).Value = _empCode;

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(PropertyBorrowInfo));

        }

        public void Savedata(PropertyBorrowInfo _brInfo)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_ACTION, OracleDbType.Varchar2).Value = "ADD";
            this.AddParameters(cmd, _brInfo);
            cmd.Parameters.Add(PARAM_USER, OracleDbType.Varchar2).Value = _brInfo.CreateBy;

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void UpdateData(PropertyBorrowInfo _brInfo)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_ACTION, OracleDbType.Varchar2).Value = "UPDATE";
            this.AddParameters(cmd, _brInfo);
            cmd.Parameters.Add(PARAM_USER, OracleDbType.Varchar2).Value = _brInfo.LastUpdateBy;

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void DeleteData(string _brId)
        {

            OracleCommand cmd = OraHelper.CreateCommand(SP_DELETEE, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_BorrowId, OracleDbType.Varchar2).Value = _brId;

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }




        public ArrayList GetProperty()
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECTMaster, CommandType.StoredProcedure);

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(PropertyInfo));

        }

        public void SaveProperty(PropertyInfo prt)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STOREMaster, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_ACTION, OracleDbType.Varchar2).Value = "ADD";
            this.AddParameters(cmd, prt);
            cmd.Parameters.Add(PARAM_USER, OracleDbType.Varchar2).Value = prt.CreateBy;

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void UpdateProperty(PropertyInfo prt)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STOREMaster, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_ACTION, OracleDbType.Varchar2).Value = "UPDATE";
            this.AddParameters(cmd, prt);
            cmd.Parameters.Add(PARAM_USER, OracleDbType.Varchar2).Value = prt.LastUpdateBy;

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }



        public ArrayList GetData(string _empCode, string _type, string _returnSts)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_EmpCode, OracleDbType.Varchar2).Value = _empCode;
            cmd.Parameters.Add(PARAM_Type, OracleDbType.Varchar2).Value = _type;
            cmd.Parameters.Add(PARAM_ReturnStatus, OracleDbType.Varchar2).Value = _returnSts;
            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(PropertyBorrowInfo));

        }


        public DataSet GetLockerMasterDataSet(string _lockerId, string _keyCode, string _empCode)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECTLockerMaster, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_LockerId, OracleDbType.Varchar2).Value = _lockerId;
            cmd.Parameters.Add(PARAM_KeyCode, OracleDbType.Varchar2).Value = _keyCode;
            cmd.Parameters.Add(PARAM_EmpCode, OracleDbType.Varchar2).Value = _empCode;
            DataSet ds= OraHelper.GetDataSet( this.Transaction, cmd,"Vi_Loclker_Mstr");
            ds.DataSetName= "GaDataSet";
            return ds;
        }

        public ArrayList GetLockerMaster(string _lockerId, string _keyCode, string _empCode)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECTLockerMaster, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_LockerId, OracleDbType.Varchar2).Value = _lockerId;
            cmd.Parameters.Add(PARAM_KeyCode, OracleDbType.Varchar2).Value = _keyCode;
            cmd.Parameters.Add(PARAM_EmpCode, OracleDbType.Varchar2).Value = _empCode;
            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(LockerInfo));
        }

        public void SaveLockerMaster(LockerInfo _lockMstr)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORELockerMaster, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_ACTION, OracleDbType.Varchar2).Value = "ADD";
            this.AddParameters(cmd, _lockMstr);

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void UpdateLockerMaster(LockerInfo _lockMstr)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORELockerMaster, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_ACTION, OracleDbType.Varchar2).Value = "UPDATE";
            this.AddParameters(cmd, _lockMstr);

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void DeleteLockerMaster(string _lockerId)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_DELETELockerMaster, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_LockerId, OracleDbType.Varchar2).Value = _lockerId;

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }




        public LockerInfo GetLockerMaster(string _lockerId)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECTUniqLockerMaster, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_LockerId, OracleDbType.Varchar2).Value = _lockerId;

            return (LockerInfo)OraHelper.ExecuteQuery(this, this.Transaction, cmd, typeof(LockerInfo));
        }




        public ArrayList GetLockerborrowData(string _lockerId)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECTLockerBorrowData, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_LockerId, OracleDbType.Varchar2).Value = _lockerId;

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(PropertyBorrowInfo));
        }





        #endregion
    }
}
