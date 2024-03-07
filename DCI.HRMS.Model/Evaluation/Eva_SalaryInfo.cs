using System;
using System.Collections.Generic;
using System.Text;

namespace DCI.HRMS.Model.Evaluation
{
    public class Eva_SalaryInfo
    {
        public Eva_SalaryInfo()
        {
        }
        private string year = "";
        private string  salaryGrade = "";
        private decimal salary = 0m;
        private decimal wage = 0m;
        private decimal percentUp = 0m;
        private string code = "";

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public string Year
        {
            get { return year; }
            set { year = value; }
        }

        public string SalaryGrade
        {
            get { return salaryGrade; }
            set { salaryGrade = value; }
        }

        public decimal Salary
        {
            get { return salary; }
            set { salary = value; }
        }

        public decimal Wage
        {
            get { return wage; }
            set { wage = value; }
        }


        public decimal PercentUp
        {
            get { return percentUp; }
            set { percentUp = value; }
        }


    }
}
