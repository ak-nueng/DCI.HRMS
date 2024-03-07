using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using DCI.HRMS.Model.Common;

namespace DCI.HRMS.Model.Attendance
{
    [Serializable]
    public class MonthShiftInfo
    {
        private string shiftData = "";
        private string groupStss = "";
        private string yearMonth = "";
        private ObjectInfo inform = new ObjectInfo();

        public MonthShiftInfo()
        {


        }
        public MonthShiftInfo(string _ym, string _group, string _shift)
        {
            yearMonth = _ym;
            shiftData = _shift;
            groupStss = _group;
        }
        public string ShiftData
        {
            get { return shiftData; }
            set { shiftData = value; }

        }

        public string GroupStatus
        {
            get { return groupStss; }
            set { groupStss = value; }

        }
        public string YearMonth
        {
            get { return yearMonth; }
            set { yearMonth = value; }

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
            string res = "";

            try
            {
                res = ShiftData.Substring(date.Day - 1, 1);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return res;
        }



    }
}
