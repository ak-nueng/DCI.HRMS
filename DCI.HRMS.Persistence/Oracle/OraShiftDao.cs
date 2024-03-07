using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using DCI.HRMS.Model;
using PCUOnline.Dao;
using PCUOnline.Dao.Ora;
using DCI.HRMS.Model.Attendance;

namespace DCI.HRMS.Persistence.Oracle
{
    public class OraShiftDao : DaoBase, IShiftDao
    {
        private const string SP_SELECT_MonthShift = "pkg_hr_shift.sp_select_monthshift";
        private const string SP_SELECT_EmpShift = "pkg_hr_shift.sp_select_empshift";
        private const string SP_SELECT_EmpCode = "pkg_hr_shift.sp_select_empcode";
        private const string SP_SELECT_DVCD_GrpOT = "pkg_hr_shift.sp_select_dvcd_grpot";
        private const string SP_GENERATE_EmpShift = "pkg_hr_shift.sp_generate_empshift";
        private const string SP_STORE_MonthShift = "pkg_hr_shift.sp_store_monthsh";
        private const string SP_STORE_EmpShift = "pkg_hr_shift.sp_store_empsh";
        private const string SP_DEL_MonthShift = "pkg_hr_shift.sp_del_monthshift";
        private const string SP_DEL_EmpShift = "pkg_hr_shift.sp_del_empshift";
        private const string SP_SELECT_AllShift = "pkg_hr_shift.sp_select_shift";
        private const string PARA_EMPDVCD = "p_dvcd";
        private const string PARA_YearMonth = "p_yearmonth";
        private const string PARA_EMPCODE = "p_empcode";
        private const string PARA_SHIFTGROUP = "p_shiftgroup";
        private const string PARA_SHIFTDATA = "p_shiftdata";
        private const string PARA_SHIFTO = "p_shifto";
        private const string PARA_USER = "p_by";
        private const string PARA_ACTION = "p_action";
        
        public OraShiftDao(DaoManager daoManager)
            : base(daoManager)
        {
        }

        #region IShiftDao Members

        public ArrayList GetMonthShiftByGroup(string shgrp, int year)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_MonthShift, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_SHIFTGROUP, shgrp);
            cmd.Parameters.Add(PARA_YearMonth, string.Format("{0}%", year));
            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(MonthShiftInfo));
        }


        public MonthShiftInfo GetMonthShift(string shgrp, string yearmonth)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_MonthShift, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_YearMonth, OracleDbType.Varchar2).Value = yearmonth;
            cmd.Parameters.Add(PARA_SHIFTGROUP, OracleDbType.Varchar2).Value = shgrp;
            return (MonthShiftInfo)OraHelper.ExecuteQuery(this, this.Transaction, cmd, typeof(MonthShiftInfo));
        }

        public void Insert(MonthShiftInfo mhinfo)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_MonthShift, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_YearMonth, mhinfo.YearMonth);
            cmd.Parameters.Add(PARA_SHIFTGROUP, mhinfo.GroupStatus);
            cmd.Parameters.Add(PARA_SHIFTDATA, mhinfo.ShiftData);
            cmd.Parameters.Add(PARA_ACTION, "ADD");
            cmd.Parameters.Add(PARA_USER, mhinfo.CreateBy);
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);


        }

        public void Update(MonthShiftInfo mhinfo)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_MonthShift, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_YearMonth, mhinfo.YearMonth);
            cmd.Parameters.Add(PARA_SHIFTGROUP, mhinfo.GroupStatus);
            cmd.Parameters.Add(PARA_SHIFTDATA, mhinfo.ShiftData);
            cmd.Parameters.Add(PARA_ACTION, "MODI");
            cmd.Parameters.Add(PARA_USER, mhinfo.LastUpdateBy);
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void Delete(MonthShiftInfo mhinfo)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_DEL_MonthShift, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_YearMonth,OracleDbType.Varchar2).Value= mhinfo.YearMonth;
            cmd.Parameters.Add(PARA_SHIFTGROUP,OracleDbType.Varchar2).Value = mhinfo.GroupStatus;
            cmd.Parameters.Add(PARA_USER,OracleDbType.Varchar2).Value = mhinfo.CreateBy;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public ArrayList GetShiftDataByCode(string empCode, int year)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_EmpShift, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_EMPCODE,string.Format("{0}", empCode));
            cmd.Parameters.Add(PARA_YearMonth, string.Format("{0}%", year));
            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(EmployeeShiftInfo));
        }


        public EmployeeShiftInfo GetShiftData(string yearmonth, string empcode)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_EmpShift, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_EMPCODE, empcode);
            cmd.Parameters.Add(PARA_YearMonth,  yearmonth);
            return (EmployeeShiftInfo) OraHelper.ExecuteQuery(this, this.Transaction, cmd, typeof(EmployeeShiftInfo));
        }

        public void Insert(EmployeeShiftInfo empsh)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_EmpShift, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_YearMonth, OracleDbType.Varchar2).Value = empsh.YearMonth;
            cmd.Parameters.Add(PARA_EMPCODE, OracleDbType.Varchar2).Value = empsh.EmpCode;
            cmd.Parameters.Add(PARA_SHIFTDATA, OracleDbType.Varchar2).Value = empsh.ShiftData;
            cmd.Parameters.Add(PARA_SHIFTO, OracleDbType.Varchar2).Value = empsh.ShiftO;
            cmd.Parameters.Add(PARA_ACTION, OracleDbType.Varchar2).Value = "ADD";
            cmd.Parameters.Add(PARA_USER, OracleDbType.Varchar2).Value = empsh.CreateBy;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }
        public void Update(EmployeeShiftInfo empsh)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_EmpShift, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_YearMonth, OracleDbType.Varchar2).Value = empsh.YearMonth;
            cmd.Parameters.Add(PARA_EMPCODE, OracleDbType.Varchar2).Value = empsh.EmpCode;
            cmd.Parameters.Add(PARA_SHIFTDATA, OracleDbType.Varchar2).Value = empsh.ShiftData;
            cmd.Parameters.Add(PARA_SHIFTO, OracleDbType.Varchar2).Value = empsh.ShiftO;
            cmd.Parameters.Add(PARA_ACTION, OracleDbType.Varchar2).Value = "MODI";
            cmd.Parameters.Add(PARA_USER, OracleDbType.Varchar2).Value = empsh.LastUpdateBy;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }
        public ArrayList GetEmployeeByShGroup(string shgroup)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_EmpCode, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_SHIFTGROUP, OracleDbType.Varchar2).Value = shgroup;
            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(BasicInfo));
        }

        public void Delete(EmployeeShiftInfo empsh)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_DEL_EmpShift, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_YearMonth, OracleDbType.Varchar2).Value = empsh.YearMonth;
            cmd.Parameters.Add(PARA_EMPCODE, OracleDbType.Varchar2).Value = empsh.EmpCode;
            cmd.Parameters.Add(PARA_USER, OracleDbType.Varchar2).Value = empsh.CreateBy;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }
        public ArrayList GenerateEmpShiftData(MonthShiftInfo mhinfo,string shifto)
        {
           
            OracleCommand cmd = OraHelper.CreateCommand(SP_GENERATE_EmpShift, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_YearMonth, mhinfo.YearMonth);
            cmd.Parameters.Add(PARA_SHIFTGROUP, mhinfo.GroupStatus);
            cmd.Parameters.Add(PARA_SHIFTDATA, mhinfo.ShiftData);
            cmd.Parameters.Add(PARA_SHIFTO, shifto);   
            cmd.Parameters.Add(PARA_USER, mhinfo.CreateBy);
            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(EmployeeShiftInfo));

        }
        public ArrayList GetShiftAllType()
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_AllShift, CommandType.StoredProcedure);
           
         
            return    OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(ShiftType));

        }

        public ArrayList GetDVCDGrpOT(string empDVCD)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_DVCD_GrpOT, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_EMPDVCD, OracleDbType.Varchar2).Value = empDVCD;
            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(EmployeeDVCDGroupOTShiftInfo));
        }

        #endregion

        public override object QueryForObject(System.Data.DataRow row, Type t)
        {
            if (t == typeof(MonthShiftInfo))
            {
                
                MonthShiftInfo item = new MonthShiftInfo();
                ObjCommon iform = new ObjCommon();
                item.Inform = iform.QueryForObject(row);
                try
                {
                    item.GroupStatus = Convert.ToString(this.Parse(row, "shgrp"));
                }
                catch { }
                try
                {
                    item.YearMonth = Convert.ToString(this.Parse(row, "ym"));
                }
                catch { }
                try
                {
                    item.ShiftData = Convert.ToString(this.Parse(row, "shdata"));
                }
                catch { }


        
                return item;


            }
            if (t == typeof(EmployeeShiftInfo))
            {
                EmployeeShiftInfo item = new EmployeeShiftInfo();
                ObjCommon iform = new ObjCommon();
                item.Inform = iform.QueryForObject(row);
                try
                {
                    item.EmpCode = Convert.ToString(this.Parse(row, "code"));
                }
                catch { }
                try
                {
                    item.YearMonth = Convert.ToString(this.Parse(row, "ym"));
                }
                catch { }
                try
                {
                    item.ShiftData = Convert.ToString(this.Parse(row, "shdata"));
                }
                catch { }
                try
                {
                    item.ShiftO = Convert.ToString(this.Parse(row, "shsts"));
                }
                catch { }


                return item;


            }
             if (t == typeof(BasicInfo))
            {
                BasicInfo item = new BasicInfo();

                try
                {
                    item.Code = OraHelper.DecodeLanguage((string)this.Parse(row,"dt_code"));
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


                return item;
            }
            if (t == typeof(ShiftType))
            {
                ShiftType item = new ShiftType();
                try
                {
                    item.GrpOt = OraHelper.DecodeLanguage((string)this.Parse(row, "grpot"));
                }
                catch { }
                try
                {
                    item.ShiftGroup = OraHelper.DecodeLanguage((string)this.Parse(row, "shgrp"));
                }
                catch { }
                try
                {
                    item.ShiftStatus = OraHelper.DecodeLanguage((string)this.Parse(row, "shsts"));
                }
                catch { }
                try
                {
                    item.Remark = OraHelper.DecodeLanguage((string)this.Parse(row, "remark"));
                }
                 
                catch { }
                 return item;
            }
        
        
           
            return null;
        }

        public override void AddParameters(System.Data.IDbCommand cmd, object obj)
        {
            throw new Exception("The method or operation is not implemented.");
        }


        

    }
}
