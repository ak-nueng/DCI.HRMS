using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using DCI.HRMS.Model;
using DCI.HRMS.Persistence;
using System.Diagnostics;
using DCI.HRMS.Model.Attendance;

namespace DCI.HRMS.Service.SubContract
{
    public class SubContractShiftService
    {

        private static readonly SubContractShiftService instance = new SubContractShiftService();

        private const string SHIFT_TYPE = "SHFT";

        private SubContractDaoFactory factory = SubContractDaoFactory.Instance();
        private IShiftDao shiftDao;
        private IDictionaryDao dictionaryDao;

        internal SubContractShiftService()
        {
            shiftDao = factory.CreateShiftDao();
            dictionaryDao = factory.CreateDictionaryDao();
        }

        public static SubContractShiftService Instance()
        {
            return instance;
        }

        # region Common data

        public ArrayList GetShiftType()
        {
            try
            {
                factory.StartTransaction(true);
                ArrayList temp = factory.CreateShiftDao().GetShiftAllType();
                return temp;

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
        public ShiftType GetShiftType(string grpot)
        {
            ArrayList allShft = GetShiftType();
            ShiftType shty = new ShiftType();
            shty = shty.GetShiftTypeByOt(grpot, allShft);
            return shty;

        }
        public ArrayList GetShiftByGrp(string shGrp, int year)
        {
            try
            {
                factory.StartTransaction(true);
                return factory.CreateShiftDao().GetMonthShiftByGroup(shGrp,year);
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
        public MonthShiftInfo GetShift(string group,string yearmonth )
        {
            try
            {
                factory.StartTransaction(true);
                return factory.CreateShiftDao().GetMonthShift(yearmonth, group);
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

        public void Delete(MonthShiftInfo shDel)
        {

            try
            {
                factory.StartTransaction(true);
                factory.CreateShiftDao().Delete(shDel);
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
        public void New(MonthShiftInfo sh)
        {
            try
            {
                factory.StartTransaction(true);
                factory.CreateShiftDao().Insert(sh);
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
        public void Save(MonthShiftInfo sh)
        {
            try
            {
                factory.StartTransaction(true);
                factory.CreateShiftDao().Update(sh);
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
        public bool CheckExited(MonthShiftInfo sh)
        {
            return (GetShift(sh.GroupStatus, sh.YearMonth) != null);
        }

        # endregion

        public ArrayList GetShiftByCode(string code , int year)
        {
            try
            {
                factory.StartTransaction(true);
                return factory.CreateShiftDao().GetShiftDataByCode(code, year);
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
        public string GetEmShift(string code, DateTime shDate)
        {
            EmployeeShiftInfo item = GetEmShift(code, shDate.ToString("yyyyMM"));
            if (item != null)
            {
                return item.DateShift(shDate);
            }
            else
            {
                return null;
            }

        }

        public EmployeeShiftInfo GetEmShift(string code, string yearmonth)
        {
            try
            {
                factory.StartTransaction(true);
                return factory.CreateShiftDao().GetShiftData(yearmonth,code);
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

        public void Delete(EmployeeShiftInfo shDel)
        {

            try
            {
                factory.StartTransaction(true);
                factory.CreateShiftDao().Delete(shDel);
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
        public void New(EmployeeShiftInfo sh)
        {
            try
            {
                factory.StartTransaction(true);
                factory.CreateShiftDao().Insert(sh);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                factory.EndTransaction();
            }
        }
        public void Save(EmployeeShiftInfo sh)
        {
            try
            {
                factory.StartTransaction(true);
                factory.CreateShiftDao().Update(sh);
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
        public ArrayList GenerateEmpShiftData(MonthShiftInfo shift,string shsts)
        {

            try
            {
                factory.StartTransaction(true);

                return factory.CreateShiftDao().GenerateEmpShiftData(shift,shsts );
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
        public bool CheckExited(EmployeeShiftInfo sh)
        {
            return (GetEmShift(sh.EmpCode, sh.YearMonth) != null);
        }
    }
}
