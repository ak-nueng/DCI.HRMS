using System;
using System.Collections.Generic;
using System.Text;
using DCI.HRMS.Persistence;
using System.Collections;
using System.Data;
using DCI.HRMS.Model.Attendance;
using DCI.HRMS.Model.Personal;
using DCI.HRMS.Model;
using System.Globalization;

namespace DCI.HRMS.Service
{
    public  class EmployeeLeaveService
    {
        private static readonly EmployeeLeaveService instance = new EmployeeLeaveService();

        private const string LVRQ_TYPE = "LVRQ";

        private DaoFactory factory = DaoFactory.Instance();
        private  ILeaveRequestDao leaveRqService;
        private IDictionaryDao dictionaryDao;
        private IKeyGeneratorDao keyDao;
        private EmployeeService empSvr = EmployeeService.Instance();
        private CultureInfo c = new CultureInfo("EN-GB");
        internal EmployeeLeaveService()
        {
            keyDao = factory.CreateKeyDao();
            leaveRqService = factory.CreateLeaveReqDao();
            dictionaryDao = factory.CreateDictionaryDao();

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
        public static EmployeeLeaveService Instance()
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
     

            DateTime startcal = DateTime.Parse("01/01/" + caldate.Year.ToString(),c);
            DateTime endtcal = DateTime.Parse("31/12/" + caldate.Year.ToString(),c );
            
            return GetLeaveTotal(code,startcal,endtcal,false);

        }
        public ArrayList GetLeaveTotal(string code, DateTime sdate,DateTime tdate, bool includeAnnual)
        {
            DateTime startcal = sdate;
            DateTime endtcal = tdate;
            ArrayList _leave = GetAllLeave(code, startcal, endtcal, "%");
            ArrayList leaveTotal = new ArrayList();
            if (_leave != null)
            {
                foreach (EmployeeLealeRequestInfo var in _leave)
                {

                    if (!includeAnnual)
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
                    else
                    {
                        if (var.LvDate >= startcal && var.LvDate <= endtcal )
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
                        if ( var.LvType != "ANNU")
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
                                lv.Year=var.LvDate.Year;
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
              
                //EmployeeInfo emp = empSvr.Find(item.EmpCode); 
                ArrayList anuTotal = GetAnnualTotal(item.EmpCode,item.LvDate,true);
                if (anuTotal!=null)
                {
                     AnnualTotal AnTotal = (AnnualTotal)anuTotal[anuTotal.Count-1];    
                    canLeave -= Convert.ToInt32( AnTotal.Remain);
                }
              
          
            }
            return canLeave;

        }



        public ArrayList GetAnnualTotal(string emcode, DateTime caldate, bool fullused)
        {
            //======== Edited By Nueng 08/06/2015 =========

            EmployeeInfo emp = empSvr.Find(emcode);
            DateTime joinDate = emp.JoinDate;
            DateTime annualCalDt = emp.AnnualcalDate;
            DateTime resignDt = emp.ResignDate;
            ArrayList anulv = GetAllLeave(emcode, "ANNU");
            ArrayList annutotal = new ArrayList();
            DataTable lvTb = new DataTable();
            if (anulv != null)
            {
                lvTb = ServiceUtility.ToDataTable(anulv);
            }
            int calyear = annualCalDt.Year;
            //if (annualCalDt >= DateTime.Parse("01/07/" + calyear.ToString(), c))
            if (annualCalDt >= DateTime.Parse("01/JUL/" + calyear.ToString(), c))
                {
                calyear++;
            }


            #region comment
            /*
            //============== Is SUB CONTACT ==============
            if (emcode.StartsWith("6"))
            {
                if (calyear < 2008)
                {
                    calyear = 2008;
                }


                for (; calyear <= caldate.Year; calyear++)
                {
                    DateTime startAnnu = DateTime.Parse("01/07/" + calyear.ToString(),c);
                    DateTime endAnnu = DateTime.Parse("30/06/" + (calyear + 1).ToString(),c);
                    TimeSpan calintv = startAnnu - annualCalDt;
                    if (startAnnu <= caldate)
                    {
                        
                        if (resignDt != DateTime.Parse("01/01/1900") && resignDt < startAnnu)
                        {
                            break;
                        }
                        AnnualTotal AnTotal = new AnnualTotal();
                        AnTotal.Year = calyear;

                        // if check more 1 year
                        if (calintv.Days >= 365)
                        {

                            AnTotal.Get = 6;
                            AnnualTotal temp = new AnnualTotal();
                            if (annutotal.Count > 0)
                            {
                                temp = (AnnualTotal)annutotal[annutotal.Count - 1];
                                AnTotal.Total = AnTotal.Get * 525 + temp.Remain;
                            }
                            else
                            {
                                AnTotal.Total = AnTotal.Get * 525;

                            }
                            AnTotal.FullTotal = AnTotal.Total;

                            //=========== Change Calculate ANNUAL + ACCUMULATE ===========
                            if (calyear < 2009)
                            {
                                if (AnTotal.Total > 12 * 525)
                                {
                                    AnTotal.Total = 12 * 525;
                                }
                            }
                            else if (calyear < 2015)
                            {
                                if (AnTotal.Total > 15 * 525)
                                {
                                    AnTotal.Total = 15 * 525;
                                }
                            }
                            else
                            {
                                if (AnTotal.Total > 16 * 525)
                                {
                                    AnTotal.Total = 16 * 525;
                                }
                            }
                            //=========== Change Calculate ANNUAL + ACCUMULATE ===========


                            if (lvTb.Rows.Count != 0)
                            {
                                string filter = "LvType='ANNU' and   LvDate >= '" + startAnnu.Date.ToShortDateString() + "' and LvDate <='" + endAnnu.Date.ToShortDateString() + "'";

                                DataRow[] yearannu = lvTb.Select(filter);
                                foreach (DataRow var in yearannu)
                                {

                                    if (fullused)
                                    {
                                        AnTotal.Use += (double.Parse(var["TotalMinute"].ToString()));
                                    }
                                    else
                                    {
                                        if (DateTime.Parse(var["lvDate"].ToString()) <= caldate)
                                        {
                                            AnTotal.Use += (double.Parse(var["TotalMinute"].ToString()));
                                        }

                                    }
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

            */

            #endregion


            for (; calyear <= caldate.Year; calyear++)  {

                    DateTime startAnnu = DateTime.Parse("01/JUL/" + calyear.ToString(),c);
                    DateTime endAnnu = DateTime.Parse("30/JUN/" + (calyear + 1).ToString(),c);

                    TimeSpan calintv = startAnnu - annualCalDt;

                    if (startAnnu <= caldate)
                    {
                        //if (resignDt != DateTime.Parse("01/01/1900") && resignDt < startAnnu)
                        if (resignDt != new DateTime(1900,1,1) && resignDt < startAnnu)
                        {
                            break;
                        }
                        AnnualTotal AnTotal = new AnnualTotal();
                        AnTotal.Year = calyear;
                        if (calintv.Days < 365)
                        {
                            double antt = (calintv.Days) / 365d * 6;
                            /***********************************************/
                            /*Check if there is any Annual Leave before get*/
                                    if (lvTb.Rows.Count != 0)
                            {
                                string filter = "LvType='ANNU' and   LvDate < '" + startAnnu.Date.ToShortDateString() + "'";
                                DataRow[] yearannu = lvTb.Select(filter);
                                foreach (DataRow var in yearannu)
                                {
                                    antt-=  (double.Parse(var["TotalMinute"].ToString())/525);
                                }
                            }

                            /**********************************************/

                            int anday = (int)Math.Round(antt, 0);
                            AnTotal.Get = anday;
                            AnTotal.Total = AnTotal.Get * 525;
                        
               

                        }
                        else
                        {
                            //=========== Calculate ANNUAL Year By Year ===========
                            double antt = calintv.Days / 365 + 5;
                            AnTotal.Get = (int)antt;

                            if (calyear < 2015)
                            {
                                if (AnTotal.Get > 12)
                                {
                                    AnTotal.Get = 12;
                                }
                            }
                            if (calyear < 2020)
                            {
                                if (AnTotal.Get > 13)
                                {
                                    AnTotal.Get = 13;
                                }
                            }
                            else
                            {
                                if (AnTotal.Get > 15)
                                {
                                    AnTotal.Get = 15;
                                }
                            }


                            //********************************************************
                            //  Edit by AKONE on 2022-01-28
                            //  In case Sub-Contract change to DCI  
                            //   year in sub-contract = 6 after to DCI is calculate year annual
                            //********************************************************
                            if (joinDate.Year >= calyear) {
                                
                                    AnTotal.Get = 6;

                                Console.WriteLine(joinDate.Year.ToString() + " | " + calyear.ToString() + " = " + AnTotal.Get);
                            }

                            //********************************************************
                            //  Edit by AKONE on 2019-11-19
                            //  Add condition temp get annual per 6 day every year
                            //********************************************************
                            if (emcode.StartsWith("6"))
                            {
                                AnTotal.Get = 6;
                            }
                            //********************************************************
                            //  End Edit by AKONE on 2019-11-19
                            //  Add condition temp get annual per 6 day every year
                            //********************************************************

                            
                            //=========== Calculate ANNUAL Year By Year ===========



                            if (annutotal.Count > 0)
                            {
                                AnnualTotal temp = (AnnualTotal)annutotal[annutotal.Count - 1];
                                AnTotal.Total = AnTotal.Get * 525 + temp.Remain;
                            }
                            else
                            {
                                AnTotal.Total = AnTotal.Get * 525;
                            }
                            AnTotal.FullTotal = AnTotal.Total;
                            

                            //=========== Change Calculate ANNUAL + ACCUMULATE ===========
                            if (calyear < 2009)
                            {
                                if (AnTotal.Total > 12 * 525)
                                {
                                    AnTotal.Total = 12 * 525;
                                }
                            }
                            else if (calyear < 2015)
                            {
                                if (AnTotal.Total > 15 * 525)
                                {
                                    AnTotal.Total = 15 * 525;
                                }
                            }
                            else if (calyear < 2020)
                            {
                                if (AnTotal.Total > 16 * 525)
                                {
                                    AnTotal.Total = 16 * 525;
                                }
                            }
                            else
                            {
                                if (AnTotal.Total > 18 * 525)
                                {
                                    AnTotal.Total = 18 * 525;
                                }
                            }
                            //=========== Calculate ANNUAL + ACCUMULATE Year By Year ===========


                        } // end if


                        if (lvTb.Rows.Count != 0)
                        {

                            string filter = "LvType='ANNU' and LvDate >= '" + startAnnu.Date.ToShortDateString() + "' and LvDate <='" + endAnnu.Date.ToShortDateString() + "'";

                            DataRow[] yearannu = lvTb.Select(filter);
                            foreach (DataRow var in yearannu)
                            {


                                if (fullused)
                                {
                                    AnTotal.Use += (double.Parse(var["TotalMinute"].ToString()));
                                }
                                else
                                {
                                    if (DateTime.Parse(var["lvDate"].ToString()) <= caldate)
                                    {
                                        AnTotal.Use += (double.Parse(var["TotalMinute"].ToString()));
                                    }

                                }
                            } // end foreach
                        } // end if
                        AnTotal.Remain = AnTotal.Total - AnTotal.Use;

                        annutotal.Add(AnTotal);

                    } // end if


                } // end for
            
            
            //} // end check temp
            return annutotal;


        }


        public ArrayList GetLeaveReasonSuggestion()
        {
            try
            {
                factory.StartTransaction(true);
                return leaveRqService.GetLeavReason();
              //  factory.CommitTransaction();


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