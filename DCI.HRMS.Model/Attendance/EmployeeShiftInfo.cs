using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using DCI.HRMS.Model.Common;

namespace DCI.HRMS.Model.Attendance
{
    public class EmployeeShiftInfo
    {
        private string yearMonth;
        private string empCode;
        private string shiftData;
        private string shiftO;
        private ObjectInfo inform = new ObjectInfo();
        public EmployeeShiftInfo()
        {

        }
        public EmployeeShiftInfo(string _ym, string _code, string _shiftData)
        {
            yearMonth = _ym;
            empCode = _code;
            shiftData = _shiftData;

        }
        public string YearMonth
        {
            get { return yearMonth; }
            set { yearMonth = value; }

        }
        public string EmpCode
        {
            set { empCode = value; }
            get { return empCode; }
        }
        public string ShiftData
        {
            set { shiftData = value; }
            get { return shiftData; }
        }
        public string ShiftO
        {
            set { shiftO = value; }
            get { return shiftO; }
        }
        public string CreateBy
        {
            get { return inform.CreateBy; }
        }
        public DateTime CreateDateTime
        {
            get {
                if (inform.CreateDateTime <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || inform.CreateDateTime <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return inform.CreateDateTime;
                }
            }
        }
        public string LastUpdateBy
        {
            get { return inform.LastUpdateBy; }
        }
        public DateTime LastUpDateDateTime
        {
            get {
                if (inform.LastUpdateDateTime <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || inform.LastUpdateDateTime <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return inform.LastUpdateDateTime;
                }
            }
        }
        public ObjectInfo Inform
        {

            set { this.inform = value; }
        }
        public string DateShift(DateTime date)
        {
            return ShiftData.Substring(date.Day-1, 1);
        }


    }
}
