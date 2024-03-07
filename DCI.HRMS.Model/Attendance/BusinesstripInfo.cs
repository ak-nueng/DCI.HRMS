using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using DCI.HRMS.Model.Common;

namespace DCI.HRMS.Model.Attendance
{
    public class BusinesstripInfo : ObjectInfo
    {

        private string empCode;
        private DateTime fDate;
        private DateTime tDate;
        private string note;
        private string tFrom;
        private string tTo;

        public string EmpCode
        {
            get { return empCode; }
            set { empCode = value; }
        }

        public DateTime FDate
        {
            get
            {
                if (fDate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || fDate <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return fDate;
                }
            }
            set { fDate = value; }
        }

        public DateTime TDate
        {
            get
            {
                if (tDate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || tDate <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return tDate;
                }

            }
            set { tDate = value; }
        }



        public string Note
        {
            get { return note; }
            set { note = value; }
        }

        public string TFrom
        {
            get { return tFrom; }
            set { tFrom = value; }
        }


        public string TTo
        {
            get { return tTo; }
            set { tTo = value; }
        }


    }
}
