using System;
using System.Collections.Generic;

using System.Text;

namespace DCI.HRMS.Model.Attendance
{
    public class WorkingHourInfo
    {
        public WorkingHourInfo()
        {
        }

        private string dayOfWeek;
        private string shift;
        private DateTime firstStart = DateTime.Parse("08:00");
        private DateTime firstEnd =DateTime.Parse("12:00");
        private int firstTotal =240;
        private DateTime secondStart= DateTime.Parse("13:00");
        private DateTime secondEnd= DateTime.Parse("17:45");
        private int secondTotal=285;
        public string DayOfWeek
        {
            get { return dayOfWeek; }
            set { dayOfWeek = value; }
        }

        public string Shift
        {
            get { return shift; }
            set { shift = value; }
        }

        public DateTime FirstStart
        {
            get { return firstStart; }
            set { firstStart = value; }
        }

        public DateTime FirstEnd
        {
            get { return firstEnd; }
            set { firstEnd = value; }
        }

        public int FirstTotal
        {
            get { return firstTotal; }
            set { firstTotal = value; }
        }

        public DateTime SecondStart
        {
            get { return secondStart; }
            set { secondStart = value; }
        }

        public DateTime SecondEnd
        {
            get { return secondEnd; }
            set { secondEnd = value; }
        }


        public int SecondTotal
        {
            get { return secondTotal; }
            set { secondTotal = value; }
        }


    }
}
