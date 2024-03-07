using System;
using System.Collections.Generic;
using System.Text;
using DCI.HRMS.Persistence;
using DCI.HRMS.Model.Attendance;
using System.Collections;
using System.Data;

namespace DCI.HRMS.Service.SubContract
{
  public  class SubContractOtService
    {
        private static SubContractOtService instance = new SubContractOtService();

        private SubContractDaoFactory factory = SubContractDaoFactory.Instance();

        private IOTDao otDao;

        private SubContractOtService()
        {
            otDao = factory.CreateOtDao();

        }
        public static SubContractOtService Instance()
        {
            return instance;
        }
        public ArrayList GetAllRate()
        {
            try
            {
                OtRateInfo temp = new OtRateInfo();
                factory.StartTransaction(true);
                return temp.GetUniqueRate(otDao.GetOtRates());
            }
            catch
            {
                return null;
            }
            finally
            {
                factory.EndTransaction();
            }
        }
        public OtRateInfo GetRate(string _rate, string _wtype)
        {
            try
            {
                factory.StartTransaction(true);
                return otDao.GetOtRate(_rate, _wtype);
            }
            catch
            {
                return null;
            }
            finally
            {
                factory.EndTransaction();
            }
        }
        public ArrayList GetOTRequest(string _code, DateTime _odate, DateTime _odateto, string _reqid, string _dvcd, string _otfrom, string _otto, string _otremark)
        {
            try
            {
                factory.StartTransaction(true);
                return otDao.GetOTRequest(_code, _odate, _odateto, _reqid, _dvcd, _otfrom, _otto, _otremark);
            }
            catch
            {
                return new ArrayList();
            }
            finally
            {
                factory.EndTransaction();
            }
        }
        public DataSet GetOTRequestDataSet(string _code, DateTime _odate, DateTime _odateto, string _reqid, string _dvcd, string _otfrom, string _otto, string _otremark)
        {
            try
            {
                string code = _code.Trim() == "" ? "%" : _code;

                string reqid = _reqid.Trim() == "" ? "%" : _reqid;
                string dvcd = _dvcd.Trim() == "" ? "%" : _dvcd;
                string otfrom = _otfrom.Trim() == "" ? "%" : _otfrom;
                string otto = _otto.Trim() == "" ? "%" : _otto;
                string otremark = _otremark.Trim() == "" ? "%" : _otremark;

                factory.StartTransaction(true);
                return otDao.GetOTRequestDataSet(code, _odate, _odateto, reqid, dvcd, otfrom, otto, otremark);
            }
            catch
            {
                return new DataSet();
            }
            finally
            {
                factory.EndTransaction();
            }
        }
        public ArrayList GetOTRequest(string _code, DateTime _odate, string _reqid, string _dvcd)
        {
            try
            {
                string code = _code.Trim() == "" ? "%" : _code;
                string reqid = _reqid.Trim() == "" ? "%" : _reqid;
                string dvcd = _dvcd.Trim() == "" ? "%" : _dvcd;
                factory.StartTransaction(true);
                return otDao.GetOTRequest(code, _odate, reqid, dvcd);
            }
            catch
            {
                return new ArrayList(); ;
            }
            finally
            {
                factory.EndTransaction();
            }
        }
        public ArrayList GetOTRequest(string _code, string _emType, string _posit, DateTime _odate, DateTime _odateto, string _reqid, string _dvcd, string _otfrom, string _otto, string _grpl, string _grpot, string _otremark)
        {
            try
            {

                string code = _code.Trim() == "" ? "%" : _code;
                string emType = _emType.Trim() == "" ? "%" : _emType;
                string reqid = _reqid.Trim() == "" ? "%" : _reqid;
                string dvcd = _dvcd.Trim() == "" ? "%" : _dvcd;
                string otfrom = _otfrom.Trim() == "" ? "%" : _otfrom;
                string otto = _otto.Trim() == "" ? "%" : _otto;
                string grpl = _grpl.Trim() == "" ? "%" : _grpl;
                string grpot = _grpot.Trim() == "" ? "%" : _grpot;

                string posit = _posit.Trim() == "" ? "%" : _posit;
                string otremark = _otremark.Trim() == "" ? "%" : _otremark;

                factory.StartTransaction(true);
                return otDao.GetOTRequest(code, emType, posit, _odate, _odateto, reqid, dvcd, otfrom, otto, grpl, grpot, otremark);
            }
            catch
            {
                return new ArrayList();
            }
            finally
            {
                factory.EndTransaction();
            }
        }
        public ArrayList GetOTRequestResult(string _code, DateTime _odate, string _reqid, string _dvcd, string _calrest)
        {
            try
            {
                factory.StartTransaction(true);
                return otDao.GetOTRequest(_code, _odate, _reqid, _dvcd, _calrest);
            }
            catch
            {
                return new ArrayList(); ;
            }
            finally
            {
                factory.EndTransaction();
            }
        }
        public void SaveOtRequest(OtRequestInfo rq)
        {
            try
            {
                factory.StartTransaction(true);
                otDao.SaveOtRequest(rq);
            }
            catch( Exception ex)
            {
                throw ex;
            }
            finally
            {
                factory.EndTransaction();
            }
        }
        public void UpdateOtRequest(OtRequestInfo rq)
        {
            try
            {
                factory.StartTransaction(true);
                otDao.UpdateOtReqest(rq);
            }
            catch
            {
                // throw;
            }
            finally
            {
                factory.EndTransaction();
            }
        }
        public void UpdateOtRequestById(OtRequestInfo rq)
        {

            try
            {
                factory.StartTransaction(false);
                otDao.UpdateOtReqestById(rq);
                factory.CommitTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                factory.EndTransaction();
            }

        }
        public void DeleteOtRequest(OtRequestInfo rq)
        {
            try
            {
                factory.StartTransaction(true);
                otDao.DeteteOtRequest(rq);
            }
            catch
            {
                // throw;
            }
            finally
            {
                factory.EndTransaction();
            }
        }
        public bool OtReqCheckExit(string code, DateTime odate, string rqfrom, string rqto)
        {

            ArrayList chkRq = new ArrayList();
            try
            {
                chkRq = GetOTRequest(code, odate, "", "%");
            }
            catch
            {
                return false;
            }
            if (chkRq.Count == 0)
            {
                return false;
            }
            else
            {
                DateTime rqf = DateTime.Parse(rqfrom);
                DateTime rqt = DateTime.Parse(rqto);
                if (rqf > rqt)
                    rqt = rqt.AddDays(1);
                bool exit;
                if (chkRq.Count >= 2)
                {
                    return true;
                }
                foreach (OtRequestInfo var in chkRq)
                {
                    DateTime chkfrom = DateTime.Parse(var.OtFrom);
                    DateTime chkto = DateTime.Parse(var.OtTo);
                    if (chkfrom > chkto)
                        chkto = chkto.AddDays(1);

                    if (rqf == chkfrom || rqt == chkto)
                    {
                        return true;
                    }



                }
                return false;
            }
        }
        public bool OtReqCheckExit(string code, DateTime odate, string rqId)
        {
            ArrayList chkRq = new ArrayList();
            try
            {
                chkRq = GetOTRequest(code, odate, rqId, "%");
            }
            catch
            {
                return false;
            }
            if (chkRq.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        public DataSet GetOtBusDataSet(DateTime _odate)
        {
            try
            {
                DataSet dts = new DataSet();
               dts.Tables.Add(ServiceUtility.ToDataTable(GetOtBus(_odate)));
               return dts;
               //factory.StartTransaction(true);
               //return otDao.GetOTBusDataSet(_odate);
            }
            catch
            {
                return null;
            }
            finally
            {
                //  factory.EndTransaction();
            }
        }
        public ArrayList GetOtBus(DateTime _odate)
        {
            try
            {
                factory.StartTransaction(true);
                return otDao.GetOTBus(_odate);
            }
            catch
            {
                return null;
            }
            finally
            {
                factory.EndTransaction();
            }
        }
        public ArrayList GetOtBusSummary(ArrayList otbusls)
        {
            ArrayList otbssum = new ArrayList();

            foreach (OtBusWayInfo var in otbusls)
            {
                foreach (OtBusSumaryInfo test in otbssum)
                {
                    if (test.BusWay == var.BusWay && test.Shift == var.Shift && test.Otfrom == var.Otfrom && test.Otto == var.Otto)
                    {

                        if ((var.Leavetype == "" || var.Leavetype == "LATE") && !test.Leave)
                        {
                            test.Count++;
                            goto next;
                        }
                        else if (var.Leavetype != "" && test.Leave)
                        {
                            test.Count++;
                            goto next;
                        }
                    }

                }
                OtBusSumaryInfo add = new OtBusSumaryInfo(var.BusWay, var.Shift, var.Otfrom, var.Otto,
                     !(var.Leavetype == "" || var.Leavetype == "LATE"));
                otbssum.Add(add);


            next:
                continue;
            }
            return otbssum;

        }
        public DataSet GetOtSumaryEMPDataSet(DateTime _odate, DateTime _odateto)
        {
            try
            {

                factory.StartTransaction(true);
                return otDao.GetOTSumaryEMPDataSet(_odate, _odateto);
            }
            catch
            {
                return null;
            }
            finally
            {
                factory.EndTransaction();
            }
        }
        public DataSet GetOtSumaryForBC(DateTime pdate)
        {
            try
            {

                factory.StartTransaction(true);
                return otDao.GetOTSumaryForBC(pdate);
            }
            catch
            {
                return null;
            }
            finally
            {
                factory.EndTransaction();
            }
        }

        public DataSet GetOtSumaryForBCDVCD(DateTime pdate, string pdvcd)
        {
            try
            {
                factory.StartTransaction(true);
                return otDao.GetOTSumaryForBCDVCD(pdate, pdvcd);
            }
            catch
            {
                return null;
            }
            finally
            {
                factory.EndTransaction();
            }
        }


    }
}
