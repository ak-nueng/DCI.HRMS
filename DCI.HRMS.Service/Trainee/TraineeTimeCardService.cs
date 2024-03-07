using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using DCI.HRMS.Persistence;
using DCI.HRMS.Model.Attendance;
using System.Data;

namespace DCI.HRMS.Service.Trainee
{
    public class TraineeTimeCardService
    {
        private static readonly TraineeTimeCardService instance = new TraineeTimeCardService();
        private TraineeDaoFactory factory = TraineeDaoFactory.Instance();
        private DaoFactory emFactory = DaoFactory.Instance();
        private ITimeCardDao tnTimeCardDao;
        private ITimeCardDao emTimeCardDao;
        private IDictionaryDao tmrqType;
        private TraineeLeaveService lvrqSvr = TraineeLeaveService.Instance();
        private TraineeShiftService shSvr = TraineeShiftService.Instance();
        internal TraineeTimeCardService()
        {
            tnTimeCardDao = factory.CreateTimeCardDAO();
            emTimeCardDao = emFactory.CreateTimeCardDAO();
            tmrqType = factory.CreateDictionaryDao();
        }
        public static TraineeTimeCardService Instance()
        {
            return instance;
        }
        public void TimeCardTransfer(ArrayList empTcs)
        {


        }
        public ArrayList GetTimeCardCodeDate(string empcode, DateTime date)
        {
            try
            {
                factory.StartTransaction(true);
                return tnTimeCardDao.GetTimeCardByDate(empcode, date,date);

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
        public DataSet GetTimeCardCodesDatesDataSet (string   empcode, DateTime stdate, DateTime endate)
        {
            try
            {
                factory.StartTransaction(true);
                return tnTimeCardDao.GetTimeCardDataSetByDate(empcode, stdate,endate);

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
        public ArrayList GetTimeCardCodesDates(string empcode, DateTime stdate, DateTime endate)
        {
            try
            {
                factory.StartTransaction(true);
                return tnTimeCardDao.GetTimeCardByDate(empcode, stdate,endate);

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

      
        public bool CheckDupTimeCard(string empcode, DateTime date, string time)
        {
            TimeCardInfo tc = GetTimeCard(empcode, date.Date, time);
            return tc != null;
        }
        public TimeCardInfo GetTimeCard(string empcode, DateTime date, string time)
        {
            try
            {
                factory.StartTransaction(true);
                return tnTimeCardDao.GetUniqTimeCard(empcode, date, time);
                
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

        public ArrayList GetTimeCardManualType()
        {
            try
            {
                factory.StartTransaction(true);
                return tmrqType.SelectAll("TMRQ");
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
        public void StoreTimeCard(TimeCardInfo tcInfo)
        {

            try
            {
                factory.StartTransaction(false);
             tnTimeCardDao.Insert(tcInfo);
                factory.CommitTransaction();
            }
            catch
            {
                throw;
            }
            finally
            {
                factory.EndTransaction();
            }

        }
        public void UpdateTimeCard(TimeCardInfo tcInfo)
        {

            try
            {
                factory.StartTransaction(false);
                tnTimeCardDao.Update(tcInfo);
                factory.CommitTransaction();
            }
            catch
            {
                throw;
            }
            finally
            {
                factory.EndTransaction();
            }

        }
        public void DeleteTimeCard(TimeCardInfo tcInfo)
        {

            try
            {
                factory.StartTransaction(false);
                tnTimeCardDao.Delete(tcInfo);
                factory.CommitTransaction();
            }
            catch
            {
                throw;
            }
            finally
            {
                factory.EndTransaction();
            }

        }
        public void SaveTimeCardManual(TimeCardManualInfo tmrq)
        {

            try
            {
                factory.StartTransaction(false);
                tnTimeCardDao.SaveTimeCardManual(tmrq);
                factory.CommitTransaction();
            }
            catch
            {
                throw;
            }
            finally
            {
                factory.EndTransaction();
            }
        }
        public void UpdateTimeCardManual(TimeCardManualInfo tmrq)
        {

            try
            {
                factory.StartTransaction(false);
                tnTimeCardDao.UpdateTimeCardManual(tmrq);
                factory.CommitTransaction();
            }
            catch
            {
                throw;
            }
            finally
            {
                factory.EndTransaction();
            }
        }
        public void DeleteTimeCardManual(TimeCardManualInfo tmrq)
        {

            try
            {
                factory.StartTransaction(false);
                tnTimeCardDao.DeleteTimeCardManual(tmrq);
                factory.CommitTransaction();
            }
            catch
            {
                throw;
            }
            finally
            {
                factory.EndTransaction();
            }
        }
        public ArrayList GetTimeManual(string code, DateTime cdate,DateTime cdateto, string type)
        {
            try
            {
                factory.StartTransaction(false);
                return tnTimeCardDao.GetTimeCardManual(code, cdate,cdateto, type);
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
        public DataSet GetTimeManualDataSet(string code, DateTime cdate, DateTime cdateto, string type)
        {
            try
            {
                factory.StartTransaction(false);
                return tnTimeCardDao.GetTimeCardManualDataSet(code, cdate, cdateto, type);
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
        public bool CheckTimeManual(TimeCardManualInfo tmrq)
        {
            try
            {
                factory.StartTransaction(false);
                TimeCardManualInfo items = tnTimeCardDao.GetTimeCardManual(tmrq.EmpCode, tmrq.RqDate, tmrq.RqType);
                if (items == null)
                    return false;
                else
                    return true;
            }
            catch 
            {

                return false;
            }
            finally
            {
                factory.EndTransaction();
            }


        }
        public WorkingHourInfo GetWorkingHour(DateTime _date, string _shift)
        {
            try
            {
                emFactory.StartTransaction(false);
                ArrayList wt = new ArrayList();
                try
                {
                    wt = emTimeCardDao.GetWorkingHour(_date.Date, _shift);
                    WorkingHourInfo wtt = (WorkingHourInfo)wt[0];


                    wtt.FirstStart = DateTime.Parse(_date.ToString("dd/MMM/yyyy ") + wtt.FirstStart.Hour.ToString("00") + ":" + wtt.FirstStart.Minute.ToString("00"));
                    wtt.FirstEnd = DateTime.Parse(_date.ToString("dd/MMM/yyyy ") + wtt.FirstEnd.Hour.ToString("00") + ":" + wtt.FirstEnd.Minute.ToString("00"));
                    wtt.SecondStart = DateTime.Parse(_date.ToString("dd/MMM/yyyy ") + wtt.SecondStart.Hour.ToString("00") + ":" + wtt.SecondStart.Minute.ToString("00"));
                    wtt.SecondEnd = DateTime.Parse(_date.ToString("dd/MMM/yyyy ") + wtt.SecondEnd.Hour.ToString("00") + ":" + wtt.SecondEnd.Minute.ToString("00"));

                    if (wtt.FirstEnd < wtt.FirstStart)
                    {
                        wtt.FirstEnd = wtt.FirstEnd.AddDays(1);

                    }
                    if (wtt.SecondStart < wtt.FirstStart)
                    {
                        wtt.SecondStart = wtt.SecondStart.AddDays(1);
                    }
                    if (wtt.SecondEnd < wtt.FirstStart)
                    {
                        wtt.SecondEnd = wtt.SecondEnd.AddDays(1);
                    }




                    return wtt;

                }
                catch (Exception ex)
                {

                    wt = emTimeCardDao.GetWorkingHour(_date.DayOfWeek.ToString().ToUpper(), _shift);

                    WorkingHourInfo wtt = (WorkingHourInfo)wt[0];
                    wtt.FirstStart = DateTime.Parse(_date.ToString("dd/MMM/yyyy ") + wtt.FirstStart.Hour.ToString("00") + ":" + wtt.FirstStart.Minute.ToString("00"));
                    wtt.FirstEnd = DateTime.Parse(_date.ToString("dd/MMM/yyyy ") + wtt.FirstEnd.Hour.ToString("00") + ":" + wtt.FirstEnd.Minute.ToString("00"));
                    wtt.SecondStart = DateTime.Parse(_date.ToString("dd/MMM/yyyy ") + wtt.SecondStart.Hour.ToString("00") + ":" + wtt.SecondStart.Minute.ToString("00"));
                    wtt.SecondEnd = DateTime.Parse(_date.ToString("dd/MMM/yyyy ") + wtt.SecondEnd.Hour.ToString("00") + ":" + wtt.SecondEnd.Minute.ToString("00"));

                    if (wtt.FirstEnd < wtt.FirstStart)
                    {
                        wtt.FirstEnd = wtt.FirstEnd.AddDays(1);

                    }
                    if (wtt.SecondStart < wtt.FirstStart)
                    {
                        wtt.SecondStart = wtt.SecondStart.AddDays(1);
                    }
                    if (wtt.SecondEnd < wtt.FirstStart)
                    {
                        wtt.SecondEnd = wtt.SecondEnd.AddDays(1);
                    }



                    return wtt;
                }
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="empCode"></param>
        /// <param name="workDate"></param>
        /// <returns></returns>
        public EmployeeWorkTimeInfo GetEmployeeWorkingHour(string empCode, DateTime workDate)
        {
            EmployeeWorkTimeInfo empwk = new EmployeeWorkTimeInfo();


            WorkingHourInfo dtWork = GetWorkingHour(workDate, "D");
            WorkingHourInfo ntWork = GetWorkingHour(workDate, "N");
            WorkingHourInfo dtWorkNt = GetWorkingHour(workDate.AddDays(1), "D");
            WorkingHourInfo ntWorkNt = GetWorkingHour(workDate.AddDays(1), "N");
            ArrayList timeCard = GetTimeCardCodesDates(empCode, workDate, workDate.AddDays(1));

            ArrayList timeCardIn = new ArrayList();
            ArrayList timeCardOut = new ArrayList();



            ArrayList empWorkTimes = new ArrayList();

            DateTime ntSecondEndTd = DateTime.Parse(workDate.ToString("dd/MMM/yyyy ") + ntWork.SecondEnd.Hour.ToString("00") + ":" + ntWork.SecondEnd.Minute.ToString("00"));
            DateTime ntSeconStartNd = DateTime.Parse(workDate.AddDays(1).ToString("dd/MMM/yyyy ") + ntWorkNt.SecondStart.Hour.ToString("00") + ":" + ntWorkNt.SecondStart.Minute.ToString("00"));


            DateTime dtFirstEndTd = DateTime.Parse(workDate.ToString("dd/MMM/yyyy ") + dtWork.FirstEnd.Hour.ToString("00") + ":" + dtWork.FirstEnd.Minute.ToString("00"));

            DateTime dtFirstEndNd = DateTime.Parse(workDate.AddDays(1).ToString("dd/MMM/yyyy ") + dtWorkNt.FirstEnd.Hour.ToString("00") + ":" + dtWorkNt.FirstEnd.Minute.ToString("00"));
            DateTime dtSecondStartTd = DateTime.Parse(workDate.ToString("dd/MMM/yyyy ") + dtWork.SecondStart.Hour.ToString("00") + ":" + dtWork.SecondStart.Minute.ToString("00"));
            DateTime dtSecondEndTd = DateTime.Parse(workDate.ToString("dd/MMM/yyyy ") + dtWork.SecondEnd.Hour.ToString("00") + ":" + dtWork.SecondEnd.Minute.ToString("00"));

            empwk.EmpCode = empCode;
            empwk.TimeOk = false;
            empwk.WorkDate = workDate;
            empwk.Remark = "(ABSE)";

            if (timeCard != null)
            {
                foreach (TimeCardInfo item in timeCard)
                {
                    DateTime cd = DateTime.Parse(item.CardDate.ToString("dd/MMM/yyyy ") + item.CardTime);

                    if (cd > ntSecondEndTd && cd < ntSeconStartNd && item.Duty == "I")
                    {
                        timeCardIn.Add(item);
                    }
                    if (dtFirstEndTd < cd && cd < dtFirstEndNd && item.Duty == "O")
                    {
                        timeCardOut.Add(item);
                    }
                }

                if (timeCardIn.Count != 0)
                {
                    empwk.Remark = "(NO OUT)";
                    for (int i = 0; i < timeCardIn.Count; i++)
                    {
                        TimeCardInfo item = (TimeCardInfo)timeCardIn[i];
                        DateTime cd = DateTime.Parse(item.CardDate.ToString("dd/MMM/yyyy ") + item.CardTime);

                        //if (empwk.WorkFrom != new DateTime())
                        if (empwk.WorkFrom != new DateTime(1900,1,1))
                        {
                            empwk.Remark = "(Double In)";
                            empwk.TimeOk = false;
                        }
                        empwk.WorkFrom = cd;

                        //empwk.Remark = item.CardTime;
                        for (int j = 0; j < timeCardOut.Count; j++)
                        {
                            TimeCardInfo itemOut = (TimeCardInfo)timeCardOut[j];
                            DateTime cdOut = DateTime.Parse(itemOut.CardDate.ToString("dd/MMM/yyyy ") + itemOut.CardTime);
                            TimeSpan tm = cdOut - empwk.WorkFrom;

                            if (timeCardIn.Count > i + 1)
                            {
                                TimeCardInfo inNext = (TimeCardInfo)timeCardIn[i + 1];
                                DateTime cdNext = DateTime.Parse(inNext.CardDate.ToString("dd/MMM/yyyy ") + inNext.CardTime);
                                if (cdNext > cdOut && cdOut > cd)
                                {
                                    
                                    //if (empwk.WorkTo == DateTime.MinValue)
                                    if (empwk.WorkTo == new DateTime(1900, 1, 1))
                                        {
                                        empwk.WorkTo = cdOut;
                                        empwk.TimeOk = true;
                                        empwk.Remark = "";
                                    }
                                    else
                                    {
                                        if (tm.TotalHours <= 18)
                                        {
                                            empwk.WorkTo = cdOut;
                                            empwk.TimeOk = true;
                                            empwk.Remark = "(Double Out)";
                                        }
                                    }
                                }

                            }
                            else
                            {
                                
                                //if (empwk.WorkTo == DateTime.MinValue)
                                if (empwk.WorkTo == new DateTime(1900, 1, 1))
                                    {

                                    if (tm.TotalHours <= 18)
                                    {
                                        empwk.WorkTo = cdOut;
                                        empwk.TimeOk = true;
                                        empwk.Remark = "";

                                    }
                                    else
                                    {

                                    }

                                }
                                else
                                {
                                    if (tm.TotalHours <= 18)
                                    {
                                        empwk.WorkTo = cdOut;
                                        empwk.TimeOk = true;
                                        empwk.Remark = "(Double Out)";
                                    }

                                }
                            }

                        }


                    }

                }
                else
                {
                    empwk.Remark = "(ABSE)";
                    empwk.TimeOk = false;
                    foreach (TimeCardInfo item in timeCardOut)
                    {

                        DateTime cdOut = DateTime.Parse(item.CardDate.ToString("dd/MMM/yyyy ") + item.CardTime);

                        empwk.WorkTo = cdOut;
                        empwk.TimeOk = false;
                        empwk.Remark = "(NO IN)";
                    }
                }



            }
            if (!empwk.TimeOk && empwk.Remark != "(ABSE)")
            {
                if (timeCardIn.Count == 2 && timeCardOut.Count == 0 || timeCardIn.Count == 0 && timeCardOut.Count == 2)
                {
                    TimeCardInfo tm1 = new TimeCardInfo();
                    TimeCardInfo tm2 = new TimeCardInfo();
                    if (timeCardIn.Count == 2 && timeCardOut.Count == 0)
                    {
                        tm1 = (TimeCardInfo)timeCardIn[0];
                        tm2 = (TimeCardInfo)timeCardIn[1];

                    }
                    else if (timeCardIn.Count == 0 && timeCardOut.Count == 2)
                    {
                        tm1 = (TimeCardInfo)timeCardOut[0];
                        tm2 = (TimeCardInfo)timeCardOut[1];
                    }

                    DateTime cd1 = DateTime.Parse(tm1.CardDate.ToString("dd/MMM/yyyy ") + tm1.CardTime);
                    DateTime cd2 = DateTime.Parse(tm2.CardDate.ToString("dd/MMM/yyyy ") + tm2.CardTime);
                    TimeSpan tmm = cd2 - cd1;

                    {

                    }
                    if (tmm.TotalHours >= 4 && tmm.TotalHours <= 18 &&
                        (cd1 >= ntSecondEndTd && cd1 <= dtSecondStartTd ||
                        cd1 >= dtSecondStartTd && cd1 <= ntSeconStartNd))
                    {
                        empwk.WorkFrom = cd1;
                        empwk.WorkTo = cd2;
                        empwk.TimeOk = true;
                        empwk.Remark = "(TimeCard I/O Error)";
                    }
                }

            }




            //  
            //Check if leave
            ArrayList lvLs = lvrqSvr.GetAllLeave(empwk.EmpCode, empwk.WorkDate, empwk.WorkDate, "%");
            if (lvLs != null)
            {
                empwk.Remark = "";
                int lvTotal = 0;
                foreach (EmployeeLealeRequestInfo lvItem in lvLs)
                {

                    empwk.Remark += "(" + lvItem.LvType + " " + lvItem.LvFrom + "-" + lvItem.LvTo + ")";
                    lvTotal += lvItem.TotalMinute;
                }

                if (lvTotal < 525)
                {
                    empwk.Remark += "(Half day Leave)";
                }
            }

            if (empwk.Remark == "(ABSE)")
            {
                // Check TimeManual
                ArrayList tmrqLs = GetTimeManual(empwk.EmpCode, empwk.WorkDate, empwk.WorkDate, "%");
                if (tmrqLs != null)
                {
                    foreach (TimeCardManualInfo tmItem in tmrqLs)
                    {

                        DateTime tmFm = DateTime.Parse(tmItem.RqDate.ToString("dd/MMM/yyyy") + " " + tmItem.TimeFrom);
                        empwk.WorkFrom = tmFm;


                        DateTime tmTo = DateTime.Parse(tmItem.RqDate.ToString("dd/MMM/yyyy") + " " + tmItem.TimeTo);

                        if (tmTo < empwk.WorkFrom)
                        {
                            tmTo = tmTo.AddDays(1);
                        }
                        empwk.WorkTo = tmTo;
                        empwk.TimeOk = true;
                        empwk.Remark = "(TimeManual)";

                    }
                }
            }

            //  }
            if (empwk.Remark == "(NO IN)" || empwk.Remark == "(NO OUT)")
            {   // Check TimeManual
                ArrayList tmrqLs = GetTimeManual(empwk.EmpCode, empwk.WorkDate, empwk.WorkDate, "%");
                if (tmrqLs != null)
                {
                    foreach (TimeCardManualInfo tmItem in tmrqLs)
                    {
                        if (empwk.Remark == "(NO IN)")
                        {
                            DateTime tmFm = DateTime.Parse(tmItem.RqDate.ToString("dd/MMM/yyyy") + " " + tmItem.TimeFrom);
                            empwk.WorkFrom = tmFm;
                            empwk.TimeOk = true;
                        }
                        else
                        {
                            DateTime tmTo = DateTime.Parse(tmItem.RqDate.ToString("dd/MMM/yyyy") + " " + tmItem.TimeTo);

                            if (tmTo < empwk.WorkFrom)
                            {
                                tmTo = tmTo.AddDays(1);
                            }
                            empwk.WorkTo = tmTo;
                            empwk.TimeOk = true;
                        }
                        empwk.Remark = "(TimeManual)";

                    }
                }
            }

            //Calculate Shift
            // if (empwk.TimeOk)
            {
                if (empwk.WorkFrom >= ntSecondEndTd && empwk.WorkFrom <= dtSecondStartTd || empwk.WorkTo >= dtFirstEndTd && empwk.WorkTo <= ntSeconStartNd)
                {
                    empwk.Shift = "D";
                    TimeSpan wkTt = empwk.WorkTo - empwk.WorkFrom;
                    if (wkTt.TotalHours < 4 && wkTt.TotalHours >= 0)
                    {
                        empwk.Remark = "(NotWork)";
                        empwk.TimeOk = false;
                    }
                }
                if (empwk.WorkFrom >= dtSecondStartTd && empwk.WorkFrom <= ntSeconStartNd || empwk.WorkTo >= ntSeconStartNd && empwk.WorkTo <= dtFirstEndNd)
                {
                    empwk.Shift = "N";
                    TimeSpan wkTt = empwk.WorkTo - empwk.WorkFrom;
                    if (wkTt.TotalHours < 4 && wkTt.TotalHours >= 0)
                    {
                        empwk.Remark = "(NotWork)";
                        empwk.TimeOk = false;
                    }
                }


                if (empwk.TimeOk)
                {
                    string empShift = shSvr.GetEmShift(empwk.EmpCode, empwk.WorkDate);
                    if (empShift != empwk.Shift)
                    {

                        empwk.Remark += "(Wrong Shift)";
                    }

                    if (empwk.Shift == "D")
                    {
                        if (empwk.WorkFrom > dtWork.FirstStart)
                        {
                            empwk.TimeOk = false;
                            if (empwk.WorkFrom < dtWork.FirstEnd)
                            {

                                if (!empwk.Remark.Contains("(LATE"))
                                {
                                    empwk.Remark += "(LATE)";
                                }
                            }
                            else
                            {
                                if (!empwk.Remark.Contains("(ABSE"))
                                {
                                    empwk.Remark += "(ABSE)";
                                }
                            }
                        }
                        if (empwk.WorkTo < dtWork.SecondEnd)
                        {
                            empwk.TimeOk = false;
                            if (empwk.WorkTo > dtWork.SecondStart)
                            {

                                if (!empwk.Remark.Contains("(LATE"))
                                {
                                    empwk.Remark += "(LATE)";
                                }
                            }
                            else
                            {
                                if (!empwk.Remark.Contains("(ABSE"))
                                {
                                    empwk.Remark += "(ABSE)";
                                }
                            }
                        }
                    }
                    if (empwk.Shift == "N")
                    {
                        if (empwk.WorkFrom > ntWork.FirstStart)
                        {
                            empwk.TimeOk = false;
                            if (empwk.WorkFrom < ntWork.FirstEnd)
                            {

                                if (!empwk.Remark.Contains("(LATE"))
                                {
                                    empwk.Remark += "(LATE)";
                                }
                            }
                            else
                            {
                                if (!empwk.Remark.Contains("(ABSE"))
                                {
                                    empwk.Remark += "(ABSE)";
                                }
                            }
                        }
                        if (empwk.WorkTo < ntWork.SecondEnd)
                        {
                            empwk.TimeOk = false;
                            if (empwk.WorkTo > ntWork.SecondStart)
                            {

                                if (!empwk.Remark.Contains("(LATE"))
                                {
                                    empwk.Remark += "(LATE)";
                                }
                            }
                            else
                            {
                                if (!empwk.Remark.Contains("(ABSE"))
                                {
                                    empwk.Remark += "(ABSE)";
                                }
                            }
                        }
                    }

                }

            }
            return empwk;
        }

    }
}
