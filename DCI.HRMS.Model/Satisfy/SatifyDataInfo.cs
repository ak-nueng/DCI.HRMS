using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DCI.HRMS.Model.Satisfy
{
    public class SatisfyDataInfo
    {
        private string satisfyId = "";
        private string empCode = "";
        private int choice = 0;
        private DateTime satisfydate = DateTime.MinValue;

        public SatisfyDataInfo()
        {
        }
        public string SatisfyId
        {
            get { return satisfyId; }
            set { satisfyId = value; }
        }

        public string EmpCode
        {
            get { return empCode; }
            set { empCode = value; }
        }

        public int Choice
        {
            get { return choice; }
            set { choice = value; }
        }


        public DateTime Satisfydate
        {
            get { 
                if (satisfydate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || satisfydate <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return satisfydate;
                }
            }
            set { satisfydate = value; }
        }


    }
}
