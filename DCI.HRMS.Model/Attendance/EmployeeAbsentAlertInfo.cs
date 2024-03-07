using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DCI.HRMS.Model.Attendance
{
    [Serializable]
    public class EmployeeAbsentAlertInfo : EmployeeLeaveInfo
    {
        private int totalAbsent;
        private DateTime lastAbsentDate = new DateTime(1900, 1, 1);
        private int totalPenaltyByVerbal;
        private DateTime lastPenaltyByVerbalDate = new DateTime(1900, 1, 1);
        private int totalPenaltyByLetter;
        private DateTime lastPenaltyByLetterDate = new DateTime(1900, 1, 1);
        private int totalPenaltyByBan = 0;
        private DateTime lastPenaltyByBanDate = new DateTime(1900, 1, 1);

        public EmployeeAbsentAlertInfo()
        {
        }

        public int TotalAbsent
        {
            get { return totalAbsent; }
            set { totalAbsent = value; }
        }
        public DateTime LastAbsentDate
        {
            get {
                if (lastAbsentDate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || lastAbsentDate <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return lastAbsentDate;
                }
            }
            set { lastAbsentDate = value; }
        }
        public int TotalPenaltyByVerbal
        {
            get { return totalPenaltyByVerbal; }
            set { totalPenaltyByVerbal = value; }
        }
        public DateTime LastPenaltyByVerbalDate
        {
            get {
                if (lastPenaltyByVerbalDate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || lastPenaltyByVerbalDate <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return lastPenaltyByVerbalDate;
                } 
            }
            set { lastPenaltyByVerbalDate = value; }
        }
        public int TotalPenaltyByLetter
        {
            get { return totalPenaltyByLetter; }
            set { totalPenaltyByLetter = value; }
        }
        public int TotalPenaltyByBan
        {
            get { return totalPenaltyByBan; }
            set { totalPenaltyByBan = value; }
        }
        public DateTime LastPenaltyByBanDate
        {
            get {
                if (lastPenaltyByBanDate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || lastPenaltyByBanDate <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return lastPenaltyByBanDate;
                }
            }
            set { lastPenaltyByBanDate = value; }
        }
        public DateTime LastPenaltyByLetterDate
        {
            get {
                if (lastPenaltyByLetterDate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || lastPenaltyByLetterDate <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return lastPenaltyByLetterDate;
                }
            }
            set { lastPenaltyByLetterDate = value; }
        }
    }
}
