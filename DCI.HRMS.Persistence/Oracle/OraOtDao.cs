using System;
using System.Collections.Generic;
using System.Text;
using PCUOnline.Dao;
using System.Collections;
using DCI.HRMS.Model.Attendance;
using Oracle.ManagedDataAccess.Client;
using PCUOnline.Dao.Ora;
using System.Data;

namespace DCI.HRMS.Persistence.Oracle
{
    public class OraOtDao : DaoBase, IOTDao
    {
        private const string SP_SELECT_RATE = "pkg_hr_ot.sp_selectrate";
        private const string SP_STORE_RATE = "pkg_hr_ot.sp_storeotrate1";
        private const string SP_DELETE_RATE = "pkg_hr_ot.sp_deleteotrate";


        private const string SP_SELECT_REQ = "pkg_hr_ot.sp_selectotrq";
        private const string SP_SELECT_REQ_search = "pkg_hr_ot.sp_selectotrqsearch";
        private const string SP_SELECT_REQ_searchView = "pkg_hr_ot.sp_selectotrqsearchvi";
        private const string SP_SELECT_REQ_searchView1 = "pkg_hr_ot.sp_selectotrqsearchvi_1";
        private const string SP_SELECT_REST = "pkg_hr_ot.sp_selectotrqrest";
        private const string SP_STORE_REQ = "pkg_hr_ot.sp_storeotrq";
        private const string SP_STORE_REQByID = "pkg_hr_ot.sp_storeotrqid1";
        private const string SP_DELETE_REQ = "pkg_hr_ot.sp_deleteotrq";
        private const string SP_SELECT_OTBUS = "pkg_hr_ot.sp_selectotbusway";
        private const string SP_SELECT_OTSUMARY = "pkg_hr_ot.sp_calotsumary";
        private const string SP_SELECT_OTSUMARYBYCODE = "pkg_hr_ot.sp_calotsumaryByCode";
        private const string SP_SELECT_OTSUMARYBYCODE1 = "pkg_hr_ot.sp_calotsumaryByCode_1";
        private const string SP_SELECT_OTSUMARYBYDVCD = "pkg_hr_ot.sp_selecsumotbydvcd";
        private const string SP_SELECT_OTSUMARYFORBC = "pkg_hr_ot.sp_selectsumotforbc";
        private const string SP_SELECT_OTSUMARYFORBCDVCD = "pkg_hr_ot.sp_selectsumotforbcdvcd";

        private const string PARAM_DOCID = "p_docId";  
        private const string PARAM_OtRateId = "p_rate";
        private const string PARAM_Position = "p_posit";
        private const string PARAM_WorkType = "p_wtype";
        private const string PARAM_EMPCode = "p_code";
        private const string PARAM_EMPType = "p_emType";
        private const string PARAM_Reqdate = "p_odate";
        private const string PARAM_ReqdateTo = "p_odateto";
        private const string PARAM_ReqId = "p_rqid";
        private const string PARAM_Dvcd = "p_dvcd";
        private const string PARAM_CalResult = "p_calrest";
        private const string PARAM_Action = "p_action";
        private const string PARAM_JobType = "p_job";
        private const string PARAM_OtFrom = "p_otfrom";
        private const string PARAM_OtTo = "p_otto";
        private const string PARAM_Ot1 = "p_ot1";
        private const string PARAM_Ot15 = "p_ot15";
        private const string PARAM_Ot2 = "p_ot2";
        private const string PARAM_Ot3 = "p_ot3";

        private const string PARAM_nFrom = "p_nfrom";
        private const string PARAM_nTo = "p_nto";
        private const string PARAM_n1 = "p_n1";
        private const string PARAM_n15 = "p_n15";
        private const string PARAM_n2 = "p_n2";
        private const string PARAM_n3 = "p_n3";
        private const string PARAM_Res = "p_res";
        private const string PARAM_Sts = "p_sts";

        private const string PARAM_Grpl = "p_grpl"; 
        private const string PARAM_Grpot = "p_grpot";
        private const string PARAM_Shift = "p_sh";
        private const string PARAM_User = "p_by";

        private const string PARAM_Ot1From = "p_ot1from";
        private const string PARAM_Ot15From = "p_ot15from";
        private const string PARAM_Ot2From = "p_ot2from";
        private const string PARAM_Ot3From = "p_ot3from";
        private const string PARAM_Ot1To = "p_ot1to";
        private const string PARAM_Ot15To = "p_ot15to";
        private const string PARAM_Ot2To = "p_ot2to";
        private const string PARAM_Ot3To = "p_ot3to";
        private const string PARAM_Remark = "p_remark";

        public OraOtDao(DaoManager daoManager)
            : base(daoManager)
        {
        }

        public override object QueryForObject(System.Data.DataRow row, Type t)
        {
            if (t == typeof(OtRateInfo))
            {
                OtRateInfo item = new OtRateInfo();
                ObjCommon iform = new ObjCommon();
                try
                {
                    item.CreateBy = Convert.ToString(this.Parse(row, "ad_by"));
                }
                catch { }
                try
                {
                    item.CreateDateTime = Convert.ToDateTime(this.Parse(row, "ad__dt"));
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
                    item.RateId = Convert.ToString(this.Parse(row, "rateid"));
                }
                catch
                {
                }
                try
                {
                    item.OtFrom = Convert.ToString(this.Parse(row, "otfrom"));
                }
                catch
                {
                }
                try
                {
                    item.OtTo = Convert.ToString(this.Parse(row, "otto"));
                }
                catch
                {
                }
                try
                {
                    item.Rate1 = Convert.ToString(this.Parse(row, "rate1"));
                }
                catch
                {
                }
                try
                {
                    item.Rate15 = Convert.ToString(this.Parse(row, "rate15"));
                }
                catch
                {
                }
                try
                {
                    item.Rate2 = Convert.ToString(this.Parse(row, "rate2"));
                }
                catch
                {
                }
                try
                {
                    item.Rate3 = Convert.ToString(this.Parse(row, "rate3"));
                }
                catch
                {
                }



                try
                {
                    item.Rate1From = Convert.ToString(this.Parse(row, "Rate1From"));
                }
                catch
                {
                }
                try
                {
                    item.Rate15From = Convert.ToString(this.Parse(row, "Rate15From"));
                }
                catch
                {
                }
                try
                {
                    item.Rate2From = Convert.ToString(this.Parse(row, "Rate2From"));
                }
                catch
                {
                }
                try
                {
                    item.Rate3From = Convert.ToString(this.Parse(row, "Rate3From"));
                }
                catch
                {
                }



                try
                {
                    item.Rate1To = Convert.ToString(this.Parse(row, "Rate1To"));
                }
                catch
                {
                }
                try
                {
                    item.Rate15To = Convert.ToString(this.Parse(row, "Rate15To"));
                }
                catch
                {
                }
                try
                {
                    item.Rate2To = Convert.ToString(this.Parse(row, "Rate2To"));
                }
                catch
                {
                }
                try
                {
                    item.Rate3To = Convert.ToString(this.Parse(row, "Rate3To"));
                }
                catch
                {
                }

                try
                {
                    item.Shift = Convert.ToString(this.Parse(row, "sh"));
                }
                catch
                {
                }
                try
                {
                    item.WorkType = Convert.ToString(this.Parse(row, "wtype"));
                }
                catch
                {
                }

                return item;

            }
            if (t==typeof(OtBusWayInfo))
            {

                OtBusWayInfo item = new OtBusWayInfo();
                try
                {
                    item.BusWay = Convert.ToString(this.Parse(row, "bus"));
                }
                catch 
                {}
                try
                {
                    item.Stop = Convert.ToString(this.Parse(row, "stop"));
                }
                catch
                { }
                try
                {
                    item.Shift = Convert.ToString(this.Parse(row, "shift"));
                }
                catch
                { }
                try
                {
                    item.Code = Convert.ToString(this.Parse(row, "code"));
                }
                catch
                { }
                try
                {
                    item.Name =  OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "name")));
                }
                catch
                { }
                try
                {
                    item.Surname = OraHelper.DecodeLanguage( Convert.ToString(this.Parse(row, "surn")));
                }
                catch
                { }
                try
                {
                    item.Otfrom =Convert.ToString(this.Parse(row, "ot"));
                }
                catch
                { }
                
                try
                {
                    item.Leavetype = Convert.ToString(this.Parse(row, "leave"));
                }
                catch
                { }
                try
                {
                    item.Dvcd = Convert.ToString(this.Parse(row, "DVCD"));
                }
                catch
                { }
                try
                {
                    item.Grpl = Convert.ToString(this.Parse(row, "grpl"));
                }
                catch
                { }
                try
                {
                    item.Grpot = Convert.ToString(this.Parse(row, "grpot"));
                }
                catch
                { }
                try
                {
                    item.Posit = Convert.ToString(this.Parse(row, "posit"));
                }
                catch
                { }
                try
                {
                    item.Wsts = Convert.ToString(this.Parse(row, "WSTS"));
                }
                catch
                { }
                return item;
                
            }
            if (t == typeof(OtRequestInfo))
            {
                OtRequestInfo item = new OtRequestInfo();
                ObjCommon iform = new ObjCommon();
                item.Inform = iform.QueryForObject(row);
                try
                {
                    item.DocId = Convert.ToString(this.Parse(row, "DOC_ID"));
                }
                catch { }
                try
                {
                    item.EmpCode = Convert.ToString(this.Parse(row, "code"));
                }
                catch { }
                try
                {
                    item.EmpName = Convert.ToString(this.Parse(row, "Name"));
                }
                catch { }
                try
                {
                    item.Dvcd = Convert.ToString(this.Parse(row, "dvcd"));
                }
                catch { }
                try
                {
                    item.Grpot = Convert.ToString(this.Parse(row, "grpot"));
                }
                catch { }
                try
                {
                    item.EmpType = Convert.ToString(this.Parse(row, "wtype"));
                }
                catch { }
                try
                {
                    item.OtDate = Convert.ToDateTime(this.Parse(row, "odate"));
                }
                catch { }
                try
                {
                    item.ReqId = Convert.ToString(this.Parse(row, "rq"));
                }
                catch { }
                try
                {
                    item.JobType = Convert.ToString(this.Parse(row, "job"));
                }
                catch { }
                try
                {
                    item.OtFrom = Convert.ToString(this.Parse(row, "otfrom"));
                }
                catch { }
                try
                {
                    item.OtTo = Convert.ToString(this.Parse(row, "otto"));
                }
                catch { }
                try
                {
                    item.Rate1 = Convert.ToString(this.Parse(row, "ot1"));
                }
                catch { }
                try
                {
                    item.Rate15 = Convert.ToString(this.Parse(row, "ot15"));
                }
                catch { }
                try
                {
                    item.Rate2 = Convert.ToString(this.Parse(row, "ot2"));
                }
                catch { }
                try
                {
                    item.Rate3 = Convert.ToString(this.Parse(row, "ot3"));
                }
                catch { }

                try
                {
                    item.Rate1From = Convert.ToString(this.Parse(row, "Ot1From"));
                }
                catch{}
                try
                {
                    item.Rate15From = Convert.ToString(this.Parse(row, "Ot15From"));
                }
                catch{}
                try
                {
                    item.Rate2From = Convert.ToString(this.Parse(row, "Ot2From"));
                }
                catch {}
                try
                {
                    item.Rate3From = Convert.ToString(this.Parse(row, "Ot3From"));
                }
                catch{}



                try
                {
                    item.Rate1To = Convert.ToString(this.Parse(row, "Ot1To"));
                }
                catch{}
                try
                {
                    item.Rate15To = Convert.ToString(this.Parse(row, "Ot15To"));
                }
                catch{}
                try
                {
                    item.Rate2To = Convert.ToString(this.Parse(row, "Ot2To"));
                }
                catch{}
                try
                {
                    item.Rate3To = Convert.ToString(this.Parse(row, "Ot3To"));
                }
                catch{}

                try
                {
                    item.CalRest = Convert.ToString(this.Parse(row, "sts"));
                }
                catch { }
                try
                {
                    item.N1 = Convert.ToString(this.Parse(row, "n1"));
                }
                catch { }
                try
                {
                    item.N15 = Convert.ToString(this.Parse(row, "n15"));
                }
                catch { }
                try
                {
                    item.N2 = Convert.ToString(this.Parse(row, "n2"));
                }
                catch { }
                try
                {
                    item.N3 = Convert.ToString(this.Parse(row, "n3"));
                }
                catch { }
                try
                {
                    item.NFrom = Convert.ToString(this.Parse(row, "nfrom"));
                }
                catch { }
                try
                {
                    item.NTo = Convert.ToString(this.Parse(row, "nto"));
                }
                catch { }
                try
                {
                    item.TimeCard = Convert.ToString(this.Parse(row, "res"));
                }
                catch { }
                try
                {
                    item.Bus = Convert.ToString(this.Parse(row, "bus"));
                }
                catch   {  }

                try
                {
                    item.OtRemark = Convert.ToString(this.Parse(row, "remark"));
                }
                catch { }
     
                return item;

            }
            return null;

        }

        public override void AddParameters(System.Data.IDbCommand cmd, object obj)
        {
            OracleCommand oraCmd = (OracleCommand)cmd;
            if (obj is OtRequestInfo)
            {
                OtRequestInfo rq = (OtRequestInfo)obj;
                // oraCmd.Parameters.Add(PARAM_DOCID, OracleDbType.Varchar2).Value = rq.DocId;
                oraCmd.Parameters.Add(PARAM_EMPCode, OracleDbType.Varchar2).Value = rq.EmpCode;
                oraCmd.Parameters.Add(PARAM_Reqdate, OracleDbType.Date).Value = rq.OtDate;
                oraCmd.Parameters.Add(PARAM_ReqId, OracleDbType.Int32).Value = int.Parse(rq.ReqId);
                oraCmd.Parameters.Add(PARAM_JobType, OracleDbType.Varchar2).Value = rq.JobType;
                oraCmd.Parameters.Add(PARAM_OtFrom, OracleDbType.Varchar2).Value = rq.OtFrom;
                oraCmd.Parameters.Add(PARAM_OtTo, OracleDbType.Varchar2).Value = rq.OtTo;
                oraCmd.Parameters.Add(PARAM_Ot1, OracleDbType.Varchar2).Value = rq.Rate1;
                oraCmd.Parameters.Add(PARAM_Ot15, OracleDbType.Varchar2).Value = rq.Rate15;
                oraCmd.Parameters.Add(PARAM_Ot2, OracleDbType.Varchar2).Value = rq.Rate2;
                oraCmd.Parameters.Add(PARAM_Ot3, OracleDbType.Varchar2).Value = rq.Rate3;
                oraCmd.Parameters.Add(PARAM_nFrom, OracleDbType.Varchar2).Value = rq.NFrom;
                oraCmd.Parameters.Add(PARAM_nTo, OracleDbType.Varchar2).Value = rq.NTo;
                oraCmd.Parameters.Add(PARAM_n1, OracleDbType.Varchar2).Value = rq.N1;
                oraCmd.Parameters.Add(PARAM_n15, OracleDbType.Varchar2).Value = rq.N15;
                oraCmd.Parameters.Add(PARAM_n2, OracleDbType.Varchar2).Value = rq.N2;
                oraCmd.Parameters.Add(PARAM_n3, OracleDbType.Varchar2).Value = rq.N3;
                oraCmd.Parameters.Add(PARAM_Sts, OracleDbType.Varchar2).Value = rq.CalRest;
                oraCmd.Parameters.Add(PARAM_Res, OracleDbType.Varchar2).Value = rq.TimeCard;
                oraCmd.Parameters.Add(PARAM_Ot1From, OracleDbType.Varchar2).Value = rq.Rate1From;
                oraCmd.Parameters.Add(PARAM_Ot15From, OracleDbType.Varchar2).Value = rq.Rate15From;
                oraCmd.Parameters.Add(PARAM_Ot2From, OracleDbType.Varchar2).Value = rq.Rate2From;
                oraCmd.Parameters.Add(PARAM_Ot3From, OracleDbType.Varchar2).Value = rq.Rate3From;
                oraCmd.Parameters.Add(PARAM_Ot1To, OracleDbType.Varchar2).Value = rq.Rate1To;
                oraCmd.Parameters.Add(PARAM_Ot15To, OracleDbType.Varchar2).Value = rq.Rate15To;
                oraCmd.Parameters.Add(PARAM_Ot2To, OracleDbType.Varchar2).Value = rq.Rate2To;
                oraCmd.Parameters.Add(PARAM_Ot3To, OracleDbType.Varchar2).Value = rq.Rate3To;
                oraCmd.Parameters.Add(PARAM_Remark, OracleDbType.Varchar2).Value = rq.OtRemark;
                
            }
            else if (obj is OtRateInfo)
            {
                OtRateInfo rq = (OtRateInfo)obj;

                oraCmd.Parameters.Add(PARAM_OtRateId, OracleDbType.Varchar2).Value = rq.RateId;
                oraCmd.Parameters.Add(PARAM_OtFrom, OracleDbType.Varchar2).Value = rq.OtFrom;
                oraCmd.Parameters.Add(PARAM_OtTo, OracleDbType.Varchar2).Value = rq.OtTo;
                oraCmd.Parameters.Add(PARAM_Ot1, OracleDbType.Varchar2).Value = rq.Rate1;
                oraCmd.Parameters.Add(PARAM_Ot15, OracleDbType.Varchar2).Value = rq.Rate15;
                oraCmd.Parameters.Add(PARAM_Ot2, OracleDbType.Varchar2).Value = rq.Rate2;
                oraCmd.Parameters.Add(PARAM_Ot3, OracleDbType.Varchar2).Value = rq.Rate3;
                oraCmd.Parameters.Add(PARAM_Ot1From, OracleDbType.Varchar2).Value = rq.Rate1From;
                oraCmd.Parameters.Add(PARAM_Ot15From, OracleDbType.Varchar2).Value = rq.Rate15From;
                oraCmd.Parameters.Add(PARAM_Ot2From, OracleDbType.Varchar2).Value = rq.Rate2From;
                oraCmd.Parameters.Add(PARAM_Ot3From, OracleDbType.Varchar2).Value = rq.Rate3From;
                oraCmd.Parameters.Add(PARAM_Ot1To, OracleDbType.Varchar2).Value = rq.Rate1To;
                oraCmd.Parameters.Add(PARAM_Ot15To, OracleDbType.Varchar2).Value = rq.Rate15To;
                oraCmd.Parameters.Add(PARAM_Ot2To, OracleDbType.Varchar2).Value = rq.Rate2To;
                oraCmd.Parameters.Add(PARAM_Ot3To, OracleDbType.Varchar2).Value = rq.Rate3To;
                oraCmd.Parameters.Add(PARAM_WorkType, OracleDbType.Varchar2).Value = rq.WorkType;
                oraCmd.Parameters.Add(PARAM_Shift, OracleDbType.Varchar2).Value = rq.Shift;

            }

        }
        private void DecodeDataSet(ref DataSet _dts, string _dttable)
        {
            DataTable dt = _dts.Tables[_dttable];
            for (int j = 0; j < dt.Rows.Count; j++)
            {


                for (int i = 0; i < dt.Rows[j].ItemArray.Length; i++)
                {
                    try
                    {

                        string temp;
                        temp = OraHelper.DecodeLanguage(dt.Rows[j][i].ToString());
                        _dts.Tables[_dttable].Rows[j][i] = temp;
                    }
                    catch
                    {


                    }
                }
            }

        }
        #region IOTDao Members

        public ArrayList GetOtRates()
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_RATE, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_OtRateId, OracleDbType.Varchar2).Value = "%";
            cmd.Parameters.Add(PARAM_WorkType, OracleDbType.Varchar2).Value = "%";
            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(OtRateInfo));
        }

        public OtRateInfo GetOtRate(string rate, string wtype)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_RATE, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_OtRateId, OracleDbType.Varchar2).Value = rate;
            cmd.Parameters.Add(PARAM_WorkType, OracleDbType.Varchar2).Value = "%" + wtype + "%";
            return (OtRateInfo)OraHelper.ExecuteQuery(this, this.Transaction, cmd, typeof(OtRateInfo));
        }

        public ArrayList GetOTRequest(string empcode, DateTime odate, string reqid, string dvcd)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_REQ, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_EMPCode, OracleDbType.Varchar2).Value = empcode ;
            cmd.Parameters.Add(PARAM_Reqdate, OracleDbType.Date).Value = odate;
            cmd.Parameters.Add(PARAM_ReqId, OracleDbType.Varchar2).Value = reqid ;
            cmd.Parameters.Add(PARAM_Dvcd, OracleDbType.Varchar2).Value = dvcd;
           // cmd.Parameters.Add(PARAM_CalResult, OracleDbType.Varchar2).Value ="%";
            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(OtRequestInfo));
        }
        public ArrayList GetOTRequest(string empcode, DateTime odate, string reqid, string dvcd,string calResult)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_REST, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_EMPCode, OracleDbType.Varchar2).Value = empcode ;
            cmd.Parameters.Add(PARAM_Reqdate, OracleDbType.Date).Value = odate;
            cmd.Parameters.Add(PARAM_ReqId, OracleDbType.Varchar2).Value = reqid ;
            cmd.Parameters.Add(PARAM_Dvcd, OracleDbType.Varchar2).Value = dvcd;
            cmd.Parameters.Add(PARAM_CalResult, OracleDbType.Varchar2).Value = calResult;
            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(OtRequestInfo));
        }

        public ArrayList GetOTRequest(string empcode, DateTime odate ,DateTime odateto, string reqid, string dvcd,string otfrom ,string otto, string otremark)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_REQ_search, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_EMPCode, OracleDbType.Varchar2).Value = empcode ;
            cmd.Parameters.Add(PARAM_Reqdate, OracleDbType.Date).Value = odate;
            cmd.Parameters.Add(PARAM_ReqdateTo, OracleDbType.Date).Value = odateto;
            cmd.Parameters.Add(PARAM_ReqId, OracleDbType.Varchar2).Value = reqid ;
            cmd.Parameters.Add(PARAM_Dvcd, OracleDbType.Varchar2).Value = dvcd;
            cmd.Parameters.Add(PARAM_OtFrom, OracleDbType.Varchar2).Value = otfrom;
            cmd.Parameters.Add(PARAM_OtTo, OracleDbType.Varchar2).Value = otto;
            cmd.Parameters.Add(PARAM_Remark, OracleDbType.Varchar2).Value = otremark;
            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(OtRequestInfo));
        }

        public DataSet GetOTRequestDataSet(string empcode, DateTime odate, DateTime odateto, string reqid, string dvcd, string otfrom, string otto, string otremark)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_REQ_search, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_EMPCode, OracleDbType.Varchar2).Value = empcode;
            cmd.Parameters.Add(PARAM_Reqdate, OracleDbType.Date).Value = odate;
            cmd.Parameters.Add(PARAM_ReqdateTo, OracleDbType.Date).Value = odateto;
            cmd.Parameters.Add(PARAM_ReqId, OracleDbType.Varchar2).Value = reqid;
            cmd.Parameters.Add(PARAM_Dvcd, OracleDbType.Varchar2).Value = dvcd;
            cmd.Parameters.Add(PARAM_OtFrom, OracleDbType.Varchar2).Value = otfrom;
            cmd.Parameters.Add(PARAM_OtTo, OracleDbType.Varchar2).Value = otto;
            cmd.Parameters.Add(PARAM_Remark, OracleDbType.Varchar2).Value = otremark;
            return OraHelper.GetDataSet( this.Transaction, cmd,"OT");
        }
        public ArrayList GetOTRequest(string empcode, string empType, DateTime odate, DateTime odateto, string reqid, string dvcd, string otfrom, string otto, string grpl, string grpot)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_REQ_searchView, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_EMPCode, OracleDbType.Varchar2).Value = empcode;
            cmd.Parameters.Add(PARAM_EMPType, OracleDbType.Varchar2).Value = empType;
          
            cmd.Parameters.Add(PARAM_Reqdate, OracleDbType.Date).Value = odate;
            cmd.Parameters.Add(PARAM_ReqdateTo, OracleDbType.Date).Value = odateto;
            cmd.Parameters.Add(PARAM_ReqId, OracleDbType.Varchar2).Value = reqid;
            cmd.Parameters.Add(PARAM_Dvcd, OracleDbType.Varchar2).Value = dvcd;
            cmd.Parameters.Add(PARAM_OtFrom, OracleDbType.Varchar2).Value = otfrom;
            cmd.Parameters.Add(PARAM_OtTo, OracleDbType.Varchar2).Value = otto;
            cmd.Parameters.Add(PARAM_Grpl, OracleDbType.Varchar2).Value = grpl;
            cmd.Parameters.Add(PARAM_Grpot, OracleDbType.Varchar2).Value = grpot;
            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(OtRequestInfo));
        }
        public ArrayList GetOTRequest(string empcode,string empType,string posit,  DateTime odate, DateTime odateto, string reqid, string dvcd, string otfrom, string otto,string grpl, string grpot, string otremark)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_REQ_searchView1, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_EMPCode, OracleDbType.Varchar2).Value = empcode ;
            cmd.Parameters.Add(PARAM_EMPType, OracleDbType.Varchar2).Value = empType ;
            cmd.Parameters.Add(PARAM_Position, OracleDbType.Varchar2).Value = posit;
            cmd.Parameters.Add(PARAM_Reqdate, OracleDbType.Date).Value = odate;
            cmd.Parameters.Add(PARAM_ReqdateTo, OracleDbType.Date).Value = odateto;
            cmd.Parameters.Add(PARAM_ReqId, OracleDbType.Varchar2).Value = reqid;
            cmd.Parameters.Add(PARAM_Dvcd, OracleDbType.Varchar2).Value = dvcd;
            cmd.Parameters.Add(PARAM_OtFrom, OracleDbType.Varchar2).Value = otfrom ;
            cmd.Parameters.Add(PARAM_OtTo, OracleDbType.Varchar2).Value = otto ;
            cmd.Parameters.Add(PARAM_Grpl, OracleDbType.Varchar2).Value = grpl;
            cmd.Parameters.Add(PARAM_Grpot, OracleDbType.Varchar2).Value = grpot ;
            cmd.Parameters.Add(PARAM_Remark, OracleDbType.Varchar2).Value = otremark;
            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(OtRequestInfo));
        }


        public void SaveOtRequest(OtRequestInfo rq)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_REQByID, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_Action, OracleDbType.Varchar2).Value = "ADD";
            cmd.Parameters.Add(PARAM_DOCID, OracleDbType.Varchar2).Value = rq.DocId;
            AddParameters(cmd, rq);
            cmd.Parameters.Add(PARAM_User, OracleDbType.Varchar2).Value = rq.CreateBy;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);



        }

        public void UpdateOtReqest(OtRequestInfo rq)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_REQ, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_Action, OracleDbType.Varchar2).Value = "UPDATE";
            AddParameters(cmd, rq);
            cmd.Parameters.Add(PARAM_User, OracleDbType.Varchar2).Value = rq.LastUpdateBy;
            OraHelper.ExecuteNonQuery(this.Transaction  , cmd);
        }

        public void UpdateOtReqestById(OtRequestInfo rq)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_REQByID, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_Action, OracleDbType.Varchar2).Value = "UPDATE";
            cmd.Parameters.Add(PARAM_DOCID, OracleDbType.Varchar2).Value = rq.DocId;
            AddParameters(cmd, rq);
            cmd.Parameters.Add(PARAM_User, OracleDbType.Varchar2).Value = rq.LastUpdateBy;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void DeteteOtRequest(OtRequestInfo rq)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_DELETE_REQ, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_EMPCode, OracleDbType.Varchar2).Value = rq.EmpCode;
            cmd.Parameters.Add(PARAM_Reqdate, OracleDbType.Date).Value = rq.OtDate;
            cmd.Parameters.Add(PARAM_ReqId, OracleDbType.Int32).Value = int.Parse( rq.ReqId);
            cmd.Parameters.Add(PARAM_User, OracleDbType.Varchar2).Value = rq.LastUpdateBy;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }




        public void SaveOtRate(OtRateInfo rq)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_RATE, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_Action, OracleDbType.Varchar2).Value = "ADD";
            AddParameters(cmd, rq);
            cmd.Parameters.Add(PARAM_User, OracleDbType.Varchar2).Value = rq.CreateBy;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);


        }

        public void UpdateOtRate(OtRateInfo rq)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_RATE, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_Action, OracleDbType.Varchar2).Value = "UPDATE";
            AddParameters(cmd, rq);
            cmd.Parameters.Add(PARAM_User, OracleDbType.Varchar2).Value = rq.LastUpdateBy;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void DeteteOtRatet(OtRateInfo rq)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_DELETE_RATE, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_OtRateId, OracleDbType.Varchar2).Value = rq.RateId;
            cmd.Parameters.Add(PARAM_WorkType, OracleDbType.Varchar2).Value = rq.WorkType;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }


        public ArrayList GetOTBus( DateTime odate)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_OTBUS, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_Reqdate, OracleDbType.Date).Value = odate;
           
            return OraHelper.ExecuteQueries(this, this.Transaction, cmd,typeof(OtBusWayInfo));
        }

        public DataSet GetOTBusDataSet(DateTime odate)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_OTBUS, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_Reqdate, OracleDbType.Date).Value = odate;

            return OraHelper.GetDataSet(this.Transaction, cmd,"OTBUS");
        }


        public DataSet GetOTSumaryDataSet(DateTime odate, DateTime odateto)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_OTSUMARY, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_Reqdate, OracleDbType.Date).Value = odate;
            cmd.Parameters.Add(PARAM_ReqdateTo, OracleDbType.Date).Value = odateto;

            return OraHelper.GetDataSet(this.Transaction, cmd, "OTSumary");
        }

    


        public DataSet GetOTSumaryDVCDDataSet(DateTime odate, DateTime odateto)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_OTSUMARYBYDVCD, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_Reqdate, OracleDbType.Date).Value = odate;
            cmd.Parameters.Add(PARAM_ReqdateTo, OracleDbType.Date).Value = odateto;

            return OraHelper.GetDataSet(this.Transaction, cmd, "OTSumary");
        }

    


        public DataSet GetOTSumaryEMPDataSet(DateTime odate, DateTime odateto)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_OTSUMARYBYCODE, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_Reqdate, OracleDbType.Date).Value = odate;
            cmd.Parameters.Add(PARAM_ReqdateTo, OracleDbType.Date).Value = odateto;

            DataSet ds = OraHelper.GetDataSet(this.Transaction, cmd, "OTSumary");
            DecodeDataSet(ref ds, "OTSumary");
            return ds;
        }
        public DataSet GetOTSumaryEMPDataSet(string empCode, DateTime odate, DateTime odateto)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_OTSUMARYBYCODE1, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_EMPCode, OracleDbType.Varchar2).Value = empCode;
            cmd.Parameters.Add(PARAM_Reqdate, OracleDbType.Date).Value = odate;
            cmd.Parameters.Add(PARAM_ReqdateTo, OracleDbType.Date).Value = odateto;
            DataSet ds= OraHelper.GetDataSet(this.Transaction, cmd, "OTSumary");
            DecodeDataSet(ref ds, "OTSumary");
            return ds;
        }

        public DataSet GetOTSumaryForBC( DateTime pdate)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_OTSUMARYFORBC, CommandType.StoredProcedure);

            cmd.Parameters.Add("p_date", OracleDbType.Date).Value = pdate;
            DataSet ds = OraHelper.GetDataSet(this.Transaction, cmd, "OTSumary");
         
            return ds;
        }
        
        public DataSet GetOTSumaryForBCDVCD( DateTime pdate, string pdvcd)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_OTSUMARYFORBCDVCD, CommandType.StoredProcedure);

            cmd.Parameters.Add("p_date", OracleDbType.Date).Value = pdate;
            cmd.Parameters.Add("p_dvcd", OracleDbType.Varchar2).Value = pdvcd;
            DataSet ds = OraHelper.GetDataSet(this.Transaction, cmd, "OTSumary");
         
            return ds;
        }

        #endregion
    }
}
