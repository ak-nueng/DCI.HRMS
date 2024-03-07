using System;
using System.Collections.Generic;
using System.Text;
using PCUOnline.Dao;
using DCI.HRMS.Model.Satisfy;
using Oracle.ManagedDataAccess.Client;
using PCUOnline.Dao.Ora;
using System.Data;
using System.Collections;
namespace DCI.HRMS.Persistence.Oracle
{
    public class OraSatisfyDao : DaoBase, ISatisfyDao
    {
        private const string SP_SELECT_ActiveStfMstrt = "pkg_stf.sp_activestfmstr_select";

        private const string SP_SELECT_StfMstrt = "pkg_stf.sp_stfmstr_select";
        private const string SP_DELETE_StfMstrt = "pkg_stf.sp_stfmstr_delete";
        private const string SP_STORE_StfMstrt = "pkg_stf.sp_stfmstr1_store";
        private const string SP_SELECT_Stf = "pkg_stf.sp_stfdata_select";
        private const string SP_STORE_Stf = "pkg_stf.sp_stfdata_store";

        private const string SP_SELECT_ActiveMainMaster = "pkg_stf.sp_activestfmainmstr_select";
        private const string SP_SELECT_MainMaster = "pkg_stf.sp_stfmainmstr_select ";
        private const string SP_DELETE_MainMaster = "pkg_stf.sp_stfmainmstr_delete";
        private const string SP_STORE_MainMaster = "pkg_stf.sp_stfmainmstr1_store";



        private const string PARA_Action = "p_action";
        private const string PARA_SatisfyId = "p_satisfyid";
        private const string PARA_Satisfyname = "p_satisfyname";
        private const string PARA_StartVote = "p_stratvote";
        private const string PARA_EndVote = "p_endvote";

        private const string PARA_Ch1 = "p_ch1";
        private const string PARA_Ch2 = "p_ch2";
        private const string PARA_Ch3 = "p_ch3";
        private const string PARA_Ch4 = "p_ch4";
        private const string PARA_Ch5 = "p_ch5";
        private const string PARA_Ch6 = "p_ch6";
        private const string PARA_Ch7 = "p_ch7";
        private const string PARA_Ch8 = "p_ch8";
        private const string PARA_Ch9 = "p_ch9";
        private const string PARA_Ch10 = "p_ch10";
        private const string PARA_Picture = "p_picturename";
        private const string PARA_Ch1Pict = "p_ch1pict";
        private const string PARA_Ch2Pict = "p_ch2pict";
        private const string PARA_Ch3Pict = "p_ch3pict";
        private const string PARA_Ch4Pict = "p_ch4pict";
        private const string PARA_Ch5Pict = "p_ch5pict";
        private const string PARA_Ch6Pict = "p_ch6pict";
        private const string PARA_Ch7Pict = "p_ch7pict";
        private const string PARA_Ch8Pict = "p_ch8pict";
        private const string PARA_Ch9Pict = "p_ch9pict";
        private const string PARA_Ch10Pict = "p_ch10pict";
        private const string PARA_Active = "p_active";
        private const string PARA_User = "p_by";


        private const string PARA_STFMainId = "p_stfmainid";
        private const string PARA_STFMainName = "p_satisfyname";

        private const string PARA_EmpCode = "p_code";
        private const string PARA_Choice = "p_ch";
        public OraSatisfyDao(DaoManager daoManager)
            : base(daoManager)
        {
        }


        public override object QueryForObject(DataRow row, Type t)
        {
            if (t == typeof(SatisfyMasterInfo))
            {
                SatisfyMasterInfo item = new SatisfyMasterInfo();
                try
                {
                    item.SatisfyId = Convert.ToString(this.Parse(row, "SATISFYID"));
                }
                catch { }
                try
                {
                    item.SatisfyName = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "SATISFYNAME")));
                }
                catch { }
                try
                {
                    item.Choice1 = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "CH1")));
                }
                catch { }
                try
                {
                    item.Choice2 = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "CH2")));
                }
                catch { }
                try
                {
                    item.Choice3 = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "CH3")));
                }
                catch { }
                try
                {
                    item.Choice4 = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "CH4")));
                }
                catch { }
                try
                {
                    item.Choice5 = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "CH5")));
                }
                catch { }
                try
                {
                    item.Choice6 = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "CH6")));
                }
                catch { }
                try
                {
                    item.Choice7 = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "CH7")));
                }
                catch { }
                try
                {
                    item.Choice8 = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "CH8")));
                }
                catch { }
                try
                {
                    item.Choice9 = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "CH9")));
                }
                catch { }
                try
                {
                    item.Choice10 = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "CH10")));
                }
                catch { }


                try
                {
                    item.Ch1Picture = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "CH1_PICT")));
                }
                catch { }
                try
                {
                    item.Ch2Picture = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "CH2_PICT")));
                }
                catch { }
                try
                {
                    item.Ch3Picture = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "CH3_PICT")));
                }
                catch { }
                try
                {
                    item.Ch4Picture = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "CH4_PICT")));
                }
                catch { }
                try
                {
                    item.Ch5Picture = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "CH5_PICT")));
                }
                catch { }
                try
                {
                    item.Ch6Picture = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "CH6_PICT")));
                }
                catch { }
                try
                {
                    item.Ch7Picture = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "CH7_PICT")));
                }
                catch { }
                try
                {
                    item.Ch8Picture = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "CH8_PICT")));
                }
                catch { }
                try
                {
                    item.Ch9Picture = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "CH9_PICT")));
                }
                catch { }
                try
                {
                    item.Ch10Picture = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "CH10_PICT")));
                }
                catch { }
                try
                {
                    item.Picture = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "PICTURE_NAME")));
                }
                catch { }
                 try
                {
                    item.SatifyMainId = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "SATISFYMAINID")));
                }
                catch { }  
                try
                {
                    item.Active = Convert.ToBoolean(this.Parse(row, "ACTIVE"));
                }
                catch { }

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
            }
            else if (t == typeof(SatisfyDataInfo))
            {
                SatisfyDataInfo item = new SatisfyDataInfo();
                try
                {
                    item.SatisfyId = Convert.ToString(this.Parse(row, "SATISFYID"));
                }
                catch { }
                try
                {
                    item.EmpCode = Convert.ToString(this.Parse(row, "CODE"));
                }
                catch { }
                try
                {
                    item.Choice = Convert.ToInt16(this.Parse(row, "CH"));
                }
                catch { }
                try
                {
                    item.Satisfydate = Convert.ToDateTime(this.Parse(row, "STDATE"));
                }
                catch { }
                return item;


            }
            else if (t == typeof(SatisfyMainInfo))
            {
                SatisfyMainInfo item = new SatisfyMainInfo();
                try
                {
                    item.SatisfyMainId = Convert.ToString(this.Parse(row, "STF_MSTRID"));
                }
                catch { }
                try
                {
                    item.SatisfyMainName = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "STF_NAME")));
                }
                catch { }

                try
                {
                    item.Active = Convert.ToBoolean(this.Parse(row, "ACTIVE"));
                }
                catch { }
                try
                {
                    item.StartVote = Convert.ToDateTime(this.Parse(row, "start_vote"));
                }
                catch { }
                try
                {
                    item.EndVote = Convert.ToDateTime(this.Parse(row, "end_vote"));
                }
                catch { }
                try
                {
                    item.CreateDateTime = Convert.ToDateTime(this.Parse(row, "cr_dt"));
                }
                catch { }
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

            }
            return null;
        }

        public override void AddParameters(System.Data.IDbCommand cmd, object obj)
        {
            throw new NotImplementedException();
        }

        #region ISatisfyDao Members

        public ArrayList SelectActiveSatisfy(string stfMainId)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_ActiveStfMstrt, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_STFMainId, OracleDbType.Varchar2).Value = stfMainId;
            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(SatisfyMasterInfo));

        }

        public SatisfyMasterInfo SelestSatisfyMaster(string stfMainId, string stfId)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_StfMstrt, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_SatisfyId, OracleDbType.Varchar2).Value = stfId;
            cmd.Parameters.Add(PARA_STFMainId, OracleDbType.Varchar2).Value = stfMainId;
            return (SatisfyMasterInfo)OraHelper.ExecuteQuery(this, this.Transaction, cmd, typeof(SatisfyMasterInfo));

        }

        public ArrayList SelectSatisfyMaster(string stfMainId)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_StfMstrt, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_SatisfyId, OracleDbType.Varchar2).Value = "%";
            cmd.Parameters.Add(PARA_STFMainId, OracleDbType.Varchar2).Value = stfMainId;

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(SatisfyMasterInfo));

        }

        public void SaveSatisfyMaster(SatisfyMasterInfo stf)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_StfMstrt, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_Action, OracleDbType.Varchar2).Value = "ADD";
            cmd.Parameters.Add(PARA_SatisfyId, OracleDbType.Varchar2).Value = stf.SatisfyId;
            cmd.Parameters.Add(PARA_Satisfyname, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.SatisfyName);
            cmd.Parameters.Add(PARA_Ch1, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Choice1);
            cmd.Parameters.Add(PARA_Ch2, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Choice2);
            cmd.Parameters.Add(PARA_Ch3, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Choice3);
            cmd.Parameters.Add(PARA_Ch4, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Choice4);
            cmd.Parameters.Add(PARA_Ch5, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Choice5);
            cmd.Parameters.Add(PARA_Ch6, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Choice6);
            cmd.Parameters.Add(PARA_Ch7, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Choice7);
            cmd.Parameters.Add(PARA_Ch8, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Choice8);
            cmd.Parameters.Add(PARA_Ch9, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Choice9);
            cmd.Parameters.Add(PARA_Ch10, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Choice10);
            cmd.Parameters.Add(PARA_STFMainId, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.SatifyMainId);
            cmd.Parameters.Add(PARA_Active, OracleDbType.Varchar2).Value = stf.Active.ToString();
            cmd.Parameters.Add(PARA_Picture , OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Picture);
            cmd.Parameters.Add(PARA_Ch1Pict, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Ch1Picture);
            cmd.Parameters.Add(PARA_Ch2Pict, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Ch2Picture);
            cmd.Parameters.Add(PARA_Ch3Pict, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Ch3Picture);
            cmd.Parameters.Add(PARA_Ch4Pict, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Ch4Picture);
            cmd.Parameters.Add(PARA_Ch5Pict, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Ch5Picture);
            cmd.Parameters.Add(PARA_Ch6Pict, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Ch6Picture);
            cmd.Parameters.Add(PARA_Ch7Pict, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Ch7Picture);
            cmd.Parameters.Add(PARA_Ch8Pict, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Ch8Picture);
            cmd.Parameters.Add(PARA_Ch9Pict, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Ch9Picture);
            cmd.Parameters.Add(PARA_Ch10Pict, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Ch10Picture);
            cmd.Parameters.Add(PARA_User, OracleDbType.Varchar2).Value = stf.CreateBy;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void UpdateSatifyMsater(SatisfyMasterInfo stf)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_StfMstrt, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_Action, OracleDbType.Varchar2).Value = "UPDATE";
            cmd.Parameters.Add(PARA_SatisfyId, OracleDbType.Varchar2).Value = stf.SatisfyId;
            cmd.Parameters.Add(PARA_Satisfyname, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.SatisfyName);
            cmd.Parameters.Add(PARA_Ch1, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Choice1);
            cmd.Parameters.Add(PARA_Ch2, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Choice2);
            cmd.Parameters.Add(PARA_Ch3, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Choice3);
            cmd.Parameters.Add(PARA_Ch4, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Choice4);
            cmd.Parameters.Add(PARA_Ch5, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Choice5);
            cmd.Parameters.Add(PARA_Ch6, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Choice6);
            cmd.Parameters.Add(PARA_Ch7, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Choice7);
            cmd.Parameters.Add(PARA_Ch8, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Choice8);
            cmd.Parameters.Add(PARA_Ch9, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Choice9);
            cmd.Parameters.Add(PARA_Ch10, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Choice10);
            cmd.Parameters.Add(PARA_STFMainId, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.SatifyMainId);
            cmd.Parameters.Add(PARA_Active, OracleDbType.Varchar2).Value = stf.Active.ToString();
            cmd.Parameters.Add(PARA_Picture, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Picture);
            cmd.Parameters.Add(PARA_Ch1Pict, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Ch1Picture);
            cmd.Parameters.Add(PARA_Ch2Pict, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Ch2Picture);
            cmd.Parameters.Add(PARA_Ch3Pict, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Ch3Picture);
            cmd.Parameters.Add(PARA_Ch4Pict, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Ch4Picture);
            cmd.Parameters.Add(PARA_Ch5Pict, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Ch5Picture);
            cmd.Parameters.Add(PARA_Ch6Pict, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Ch6Picture);
            cmd.Parameters.Add(PARA_Ch7Pict, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Ch7Picture);
            cmd.Parameters.Add(PARA_Ch8Pict, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Ch8Picture);
            cmd.Parameters.Add(PARA_Ch9Pict, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Ch9Picture);
            cmd.Parameters.Add(PARA_Ch10Pict, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stf.Ch10Picture);
            cmd.Parameters.Add(PARA_User, OracleDbType.Varchar2).Value = stf.LastUpdateDateTime;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void DelectSatifyMaster(string stfid)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_DELETE_StfMstrt, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARA_SatisfyId, OracleDbType.Varchar2).Value = stfid;

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);

        }

        public ArrayList SelestSatisfy(string stfId)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_Stf, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_SatisfyId, OracleDbType.Varchar2).Value = stfId;
            cmd.Parameters.Add(PARA_EmpCode, OracleDbType.Varchar2).Value = "%";
            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(SatisfyDataInfo));

        }

        public ArrayList SelectSatisfy(string stfId, int choice)
        {
            throw new NotImplementedException();
        }

        public void SaveSatisfy(SatisfyDataInfo stf)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_Stf, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_Action, OracleDbType.Varchar2).Value = "ADD";
            cmd.Parameters.Add(PARA_SatisfyId, OracleDbType.Varchar2).Value = stf.SatisfyId;
            cmd.Parameters.Add(PARA_EmpCode, OracleDbType.Varchar2).Value = stf.EmpCode;
            cmd.Parameters.Add(PARA_Choice, OracleDbType.Int16).Value = stf.Choice;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);

        }

        public void UpdateSatify(SatisfyDataInfo stf)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_Stf, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_Action, OracleDbType.Varchar2).Value = "UPDATE";
            cmd.Parameters.Add(PARA_SatisfyId, OracleDbType.Varchar2).Value = stf.SatisfyId;
            cmd.Parameters.Add(PARA_EmpCode, OracleDbType.Varchar2).Value = stf.EmpCode;
            cmd.Parameters.Add(PARA_Choice, OracleDbType.Int16).Value = stf.Choice;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }



        public SatisfyDataInfo SelestSatisfy(string stfId, string empCode)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_Stf, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_SatisfyId, OracleDbType.Varchar2).Value = stfId;
            cmd.Parameters.Add(PARA_EmpCode, OracleDbType.Varchar2).Value = empCode;
            return (SatisfyDataInfo)OraHelper.ExecuteQuery(this, this.Transaction, cmd, typeof(SatisfyDataInfo));

        }



        public SatisfyMainInfo SelectActiveMainMaster()
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_ActiveMainMaster, CommandType.StoredProcedure);

            return (SatisfyMainInfo)OraHelper.ExecuteQuery(this, this.Transaction, cmd, typeof(SatisfyMainInfo));

        }

        public SatisfyMainInfo SelestSatisfyMainMaster(string stMainfId)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_MainMaster, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_STFMainId, OracleDbType.Varchar2).Value = stMainfId;
            return (SatisfyMainInfo)OraHelper.ExecuteQuery(this, this.Transaction, cmd, typeof(SatisfyMainInfo));


        }

        public void SaveSatisfyMainMaster(SatisfyMainInfo stfMain)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_MainMaster, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_Action, OracleDbType.Varchar2).Value = "ADD";
            cmd.Parameters.Add(PARA_STFMainId, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stfMain.SatisfyMainId);
            cmd.Parameters.Add(PARA_STFMainName, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stfMain.SatisfyMainName);
            cmd.Parameters.Add(PARA_Active, OracleDbType.Varchar2).Value = stfMain.Active.ToString();
            cmd.Parameters.Add(PARA_StartVote, OracleDbType.Date).Value = stfMain.StartVote;
            cmd.Parameters.Add(PARA_EndVote, OracleDbType.Date).Value = stfMain.EndVote;
            cmd.Parameters.Add(PARA_User, OracleDbType.Varchar2).Value = stfMain.CreateBy;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);

        }

        public void UpdateSatifyMainMsater(SatisfyMainInfo stfMain)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_MainMaster, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_Action, OracleDbType.Varchar2).Value = "UPDATE";
            cmd.Parameters.Add(PARA_STFMainId, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stfMain.SatisfyMainId);
            cmd.Parameters.Add(PARA_STFMainName, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stfMain.SatisfyMainName);
            cmd.Parameters.Add(PARA_Active, OracleDbType.Varchar2).Value = stfMain.Active.ToString();
            cmd.Parameters.Add(PARA_StartVote, OracleDbType.Date).Value = stfMain.StartVote;
            cmd.Parameters.Add(PARA_EndVote, OracleDbType.Date).Value = stfMain.EndVote;
            cmd.Parameters.Add(PARA_User, OracleDbType.Varchar2).Value = stfMain.CreateBy;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void DelectSatifyMainMaster(string stMainfId)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_DELETE_MainMaster, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARA_STFMainId, OracleDbType.Varchar2).Value = stMainfId;

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }



        public ArrayList SelestSatisfyMainMaster()
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_MainMaster, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_STFMainId, OracleDbType.Varchar2).Value = "%";
            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(SatisfyMainInfo));

        }

        #endregion
    }
}
