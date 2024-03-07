using System;
using System.Collections.Generic;
using System.Text;
using DCI.HRMS.Persistence;
using System.Collections;
using System.Data;
using DCI.HRMS.Model.Attendance;
using DCI.HRMS.Model.Personal;
using DCI.HRMS.Model;

namespace DCI.HRMS.Service.SubContract
{
    public  class SubContractLeaveService
    {
        private static readonly SubContractLeaveService instance = new SubContractLeaveService();

        private const string LVRQ_TYPE = "LVRQ";
        private DaoFactory mfactory = DaoFactory.Instance();
        private SubContractDaoFactory factory = SubContractDaoFactory.Instance();
        private  ILeaveRequestDao leaveRqService;
        private IDictionaryDao dictionaryDao;
        private IKeyGeneratorDao keyDao;
        private SubContractService empSvr = SubContractService.Instance();

        internal SubContractLeaveService()
        {
            keyDao = factory.CreateKeyDao();
            leaveRqService = factory.CreateLeaveReqDao();
            dictionaryDao = mfactory.CreateDictionaryDao();

        }
        public string LoadRecordKey()
        {
            try
            {
                factory.StartTransaction(true);
                return keyDao.LoadUnique(LVRQ_TYPE).ToString(true);
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
        private string GenRecordKey()
        {
            try
            {
              //  factory.StartTransaction(true);
                return keyDao.NextId(LVRQ_TYPE);
            }
            catch
            {

                return null;
            }
            finally
            {
               // factory.EndTransaction();
            }



        }
        public static SubContractLeaveService Instance()
        {
            
            return instance;
        }
        public BasicInfo GetLeaveTypeInfo(string type)
        {
            try
            {
                factory.StartTransaction(true);
                return dictionaryDao.Select(LVRQ_TYPE,type);

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
        public ArrayList GetAllLeaveType()
        {
            try
            {
                factory.StartTransaction(true);
                return dictionaryDao.SelectAll(LVRQ_TYPE);

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
        public DataSet GetAllLeave(string empCode)
        {
            try
            {
                factory.StartTransaction(true);
                return leaveRqService.GetLeaveReq(empCode);

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
        public ArrayList GetAllLeave(string empCode , string leaveType)
        {
            try
            {
                factory.StartTransaction(true);
                return leaveRqService.GetLeaveReq(empCode,leaveType);

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
        public ArrayList GetAllLeave(string empCode,DateTime lvfrom,DateTime lvto, string leaveType)
        {
            try
            {
                factory.StartTransaction(true);
                return leaveRqService.GetLeaveReq(empCode,lvfrom,lvto, leaveType);

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
        public DataSet GetAllLeaveDataSet(string empCode, DateTime lvfrom, DateTime lvto, string leaveType)
        {
            try
            {
                factory.StartTransaction(true);
                return leaveRqService.GetLeaveReqDataSet(empCode, lvfrom, lvto, leaveType);

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
        public void SaveLeaveRequest(EmployeeLealeRequestInfo lvrq)
        {
            try
            {




                factory.StartTransaction(false);
                lvrq.DocId = GenRecordKey();
                leaveRqService.SaveLeaveReq(lvrq);
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
        public void UpdateLeaveRequest(EmployeeLealeRequestInfo lvrq)
        {
                     try
            {
                factory.StartTransaction(false);
                leaveRqService.UpdateLeaveReq(lvrq);
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
        public void DeleteLeaveRequest(EmployeeLealeRequestInfo lvrq)
        {
            try
            {
                factory.StartTransaction(false);
                leaveRqService.DeleteLeaveReq(lvrq);
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
        public ArrayList GetLeaveTotal(string code, DateTime caldate)
        {

            DateTime startcal = DateTime.Parse("01/01/" + caldate.Year.ToString());
            DateTime endtcal = DateTime.Parse("31/12/" + caldate.Year.ToString());
            ArrayList _leave = GetAllLeave(code, startcal, endtcal, "%");
            ArrayList leaveTotal = new ArrayList();
            if (_leave!= null)
            {
                foreach (EmployeeLealeRequestInfo var in _leave)
                {

                    if (var.LvDate >= startcal && var.LvDate <= endtcal && var.LvType != "ANNU")
                    {
                        bool found = false;
                        foreach (LeaveTotal lvvar in leaveTotal)
                        {
                            if (var.LvType == lvvar.Type)
                            {
                                found = true;
                                lvvar.Time++;
                                lvvar.LvTotal += var.TotalMinute;

                            }

                        }

                        if (!found)
                        {
                            LeaveTotal lv = new LeaveTotal();
                            lv.Type = var.LvType;
                            lv.LvTotal = var.TotalMinute;
                            lv.Time = 1;
                            leaveTotal.Add(lv);
                        }

                    }

                } 
            }
            return leaveTotal;

        }
        public ArrayList GetLeaveTotalByYear(string code, bool includeAnnual)
        {

            ArrayList _leave = GetAllLeave(code, "%");
            ArrayList leaveTotal = new ArrayList();
            if (_leave != null)
            {
                foreach (EmployeeLealeRequestInfo var in _leave)
                {

                    if (!includeAnnual)
                    {
                        if (var.LvType != "ANNU")
                        {
                            bool found = false;
                            foreach (LeaveTotal lvvar in leaveTotal)
                            {
                                if (var.LvType == lvvar.Type && var.LvDate.Year == lvvar.Year)
                                {
                                    found = true;
                                    lvvar.Time++;
                                    lvvar.LvTotal += var.TotalMinute;

                                }

                            }

                            if (!found)
                            {
                                LeaveTotal lv = new LeaveTotal();
                                lv.Year = var.LvDate.Year;
                                lv.Type = var.LvType;
                                lv.LvTotal = var.TotalMinute;
                                lv.Time = 1;
                                leaveTotal.Add(lv);
                            }

                        }
                    }
                    else
                    {

                        bool found = false;
                        foreach (LeaveTotal lvvar in leaveTotal)
                        {
                            if (var.LvType == lvvar.Type && var.LvDate.Year == lvvar.Year)
                            {
                                found = true;
                                lvvar.Time++;
                                lvvar.LvTotal += var.TotalMinute;

                            }

                        }

                        if (!found)
                        {
                            LeaveTotal lv = new LeaveTotal();
                            lv.Year = var.LvDate.Year;
                            lv.Type = var.LvType;
                            lv.LvTotal = var.TotalMinute;
                            lv.Time = 1;
                            leaveTotal.Add(lv);
                        }


                    }

                }
            }
            return leaveTotal;
        }
        public int CanLeave(EmployeeLealeRequestInfo item)
        {
            BasicInfo Obj = GetLeaveTypeInfo(item.LvType);
           
            int canLeave = 0;
            if (item.LvType != "ANNU")
            {
                try
                {
                    canLeave = int.Parse(Obj.Description) * 525;
                }
                catch
                {
                }
                if (canLeave != 0)
                {
                    ArrayList lvTotal = GetLeaveTotal(item.EmpCode,item.LvDate);
                    foreach (LeaveTotal var in lvTotal)
                    {
                        if (var.Type == item.LvType)
                            canLeave -= var.LvTotal;
                    }
                }
            }
            else
            {
              
                EmployeeInfo emp = empSvr.Find(item.EmpCode); 
                ArrayList anuTotal = GetAnnualTotal(item.EmpCode,emp.JoinDate, item.LvDate);
                if (anuTotal!=null)
                {
                     AnnualTotal AnTotal = (AnnualTotal)anuTotal[anuTotal.Count-1];    
                    canLeave -= Convert.ToInt32( AnTotal.Remain);
                }
              
          
            }
            return canLeave;

        }
        public ArrayList GetAnnualTotal(string emcode, DateTime joinDt, DateTime caldate)
        {


            ArrayList anulv = GetAllLeave(emcode, "ANNU");
           ArrayList annutotal = new ArrayList();
            DataTable lvTb = new DataTable();
            if (anulv != null)
            {
                lvTb = ServiceUtility.ToDataTable(anulv);
            }
            int calyear = joinDt.Year;
            if (joinDt >= DateTime.Parse("01/07/" + calyear.ToString()))
            {
                calyear++;
            }

            if (emcode.StartsWith("6"))
            {
                if (calyear < 2008)
                {
                    calyear = 2008;
                }


                for (; calyear <= caldate.Year; calyear++)
                {
                    DateTime startAnnu = DateTime.Parse("01/07/" + calyear.ToString());
                    DateTime endAnnu = DateTime.Parse("30/06/" + (calyear + 1).ToString());
                    TimeSpan calintv = startAnnu - joinDt;
                    if (startAnnu <= caldate)
                    {
                        AnnualTotal AnTotal = new AnnualTotal();
                        AnTotal.Year = calyear;
                        if (calintv.Days >= 365)
                        {

                            AnTotal.Get = 6;
                            AnnualTotal temp = new AnnualTotal();
                            /*
                            try
                            {
                                temp = (AnnualTotal)annutotal[annutotal.Count - 1];
                            }
                            catch { }
                            AnTotal.Total = AnTotal.Get * 525 + temp.Remain;*/


                            if (annutotal.Count > 0)
                            {
                                temp = (AnnualTotal)annutotal[annutotal.Count - 1];
                                AnTotal.Total = AnTotal.Get * 525 + temp.Remain;
                            }
                            else
                            {
                                AnTotal.Total = AnTotal.Get * 525;

                            }
                            if (AnTotal.Total > 14 * 525)
                            {
                                AnTotal.Total = 14 * 525;
                            }

                            if (lvTb.Rows.Count != 0)
                            {
                                string filter = "LvType='ANNU' and   LvDate >= '" + startAnnu.Date.ToShortDateString() + "' and LvDate <='" + endAnnu.Date.ToShortDateString() + "'";

                                DataRow[] yearannu = lvTb.Select(filter);
                                foreach (DataRow var in yearannu)
                                {

                                    AnTotal.Use += (double.Parse(var["TotalMinute"].ToString()));
                                }
                            }

                            AnTotal.Remain = AnTotal.Total - AnTotal.Use;
                            annutotal.Add(AnTotal);
                        }

                    }

                }

            }

            else
            {
                for (; calyear <= caldate.Year; calyear++)
                {

                    DateTime startAnnu = DateTime.Parse("01/07/" + calyear.ToString());
                    DateTime endAnnu = DateTime.Parse("30/06/" + (calyear + 1).ToString());
                    TimeSpan calintv = startAnnu - joinDt;
                    if (startAnnu <= caldate)
                    {
                        AnnualTotal AnTotal = new AnnualTotal();
                        AnTotal.Year = calyear;
                        if (calintv.Days < 365)
                        {
                            double antt = calintv.Days / 365.25 * 6;
                            int anday = (int)Math.Round(antt, 0);
                            AnTotal.Get = anday;
                            AnTotal.Total = AnTotal.Get * 525;
                        }
                        else
                        {
                            double antt = calintv.Days / 365.25 + 5;
                            int anday = (int)antt;
                            AnTotal.Get = anday;
                            if (annutotal.Count > 0)
                            {
                                AnnualTotal temp = (AnnualTotal)annutotal[annutotal.Count - 1];
                                AnTotal.Total = AnTotal.Get * 525 + temp.Remain;
                            }
                            else
                            {
                                AnTotal.Total = AnTotal.Get * 525;
                            }


                            if (calyear < 2008)
                            {
                                if (AnTotal.Total > 12 * 525)
                                {
                                    AnTotal.Total = 12 * 525;
                                }
                            }
                            else
                            {
                                if (AnTotal.Total > 14 * 525)
                                {
                                    AnTotal.Total = 14 * 525;
                                }
                            }
                        }
                        if (lvTb.Rows.Count != 0)
                        {

                            string filter = "LvType='ANNU' and LvDate >= '" + startAnnu.Date.ToShortDateString() + "' and LvDate <='" + endAnnu.Date.ToShortDateString() + "'";

                            DataRow[] yearannu = lvTb.Select(filter);
                            foreach (DataRow var in yearannu)
                            {

                                AnTotal.Use += (double.Parse(var["TotalMinute"].ToString()));
                            }
                        }
                        AnTotal.Remain = AnTotal.Total - AnTotal.Use;

                        annutotal.Add(AnTotal);

                    }


                }
            }
            return annutotal;
          

        }


    }
}
