using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DCI.HRMS.Model.Payroll
{
    public class PayrollAdjustInfo
    {

        public PayrollAdjustInfo()
        {
        }

        private DateTime pDate;
        private string wType = "";
        private string code = "";
        private int aDj = 0;
        private decimal abDay = 0;
        private decimal work1 = 0;
        private decimal work2 = 0;
        private decimal lOt1 = 0;
        private decimal lOt15 = 0;
        private decimal lOt2 = 0;
        private decimal lOt3 = 0;
        private decimal cOt1 = 0;
        private decimal cOt15 = 0;
        private decimal cOt2 = 0;
        private decimal cOt3 = 0;
        private decimal ot = 0;
        private decimal salary = 0;
        private decimal allow = 0;
        private decimal othInn = 0;
        private decimal othDed = 0;
        private decimal a400 = 0;
        private decimal shift = 0;
        private decimal foodSht = 0;
        private decimal full = 0;
        private decimal super = 0;
        private string dvcd = "";
        private string posit = "";
        private decimal adbn = 0;
        private decimal ad_Fd = 0;
        private decimal ad_Fo = 0;
        private decimal ad_Un = 0;
        private decimal ad_Tp = 0;
        private decimal ad_Pz = 0;
        private decimal tran = 0;
        private decimal a200 = 0;

        public DateTime PDate
        {
            get { 
                if (pDate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || pDate <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return pDate;
                }

            }
            set { pDate = value; }
        }

        public string WType
        {
            get { return wType; }
            set { wType = value; }
        }

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public int ADj
        {
            get { return aDj; }
            set { aDj = value; }
        }

        public decimal AbDay
        {
            get { return abDay; }
            set { abDay = value; }
        }

        public decimal Work1
        {
            get { return work1; }
            set { work1 = value; }
        }

        public decimal Work2
        {
            get { return work2; }
            set { work2 = value; }
        }

        public decimal LOt1
        {
            get { return lOt1; }
            set { lOt1 = value; }
        }

        public decimal LOt15
        {
            get { return lOt15; }
            set { lOt15 = value; }
        }

        public decimal LOt2
        {
            get { return lOt2; }
            set { lOt2 = value; }
        }

        public decimal LOt3
        {
            get { return lOt3; }
            set { lOt3 = value; }
        }

        public decimal COt1
        {
            get { return cOt1; }
            set { cOt1 = value; }
        }

        public decimal COt15
        {
            get { return cOt15; }
            set { cOt15 = value; }
        }

        public decimal COt2
        {
            get { return cOt2; }
            set { cOt2 = value; }
        }

        public decimal COt3
        {
            get { return cOt3; }
            set { cOt3 = value; }
        }

        public decimal Ot
        {
            get { return ot; }
            set { ot = value; }
        }

        public decimal Salary
        {
            get { return salary; }
            set { salary = value; }
        }

        public decimal Allow
        {
            get { return allow; }
            set { allow = value; }
        }

        public decimal OthInn
        {
            get { return othInn; }
            set { othInn = value; }
        }

        public decimal OthDed
        {
            get { return othDed; }
            set { othDed = value; }
        }

        public decimal A400
        {
            get { return a400; }
            set { a400 = value; }
        }

        public decimal Shift
        {
            get { return shift; }
            set { shift = value; }
        }

        public decimal FoodSht
        {
            get { return foodSht; }
            set { foodSht = value; }
        }

        public decimal Full
        {
            get { return full; }
            set { full = value; }
        }

        public decimal Super
        {
            get { return super; }
            set { super = value; }
        }

        public string Dvcd
        {
            get { return dvcd; }
            set { dvcd = value; }
        }

        public string Posit
        {
            get { return posit; }
            set { posit = value; }
        }

        public decimal Adbn
        {
            get { return adbn; }
            set { adbn = value; }
        }

        public decimal Ad_Fd
        {
            get { return ad_Fd; }
            set { ad_Fd = value; }
        }

        public decimal Ad_Fo
        {
            get { return ad_Fo; }
            set { ad_Fo = value; }
        }

        public decimal Ad_Un
        {
            get { return ad_Un; }
            set { ad_Un = value; }
        }

        public decimal Ad_Tp
        {
            get { return ad_Tp; }
            set { ad_Tp = value; }
        }

        public decimal Ad_Pz
        {
            get { return ad_Pz; }
            set { ad_Pz = value; }
        }

        public decimal Tran
        {
            get { return tran; }
            set { tran = value; }
        }


        public decimal A200
        {
            get { return a200; }
            set { a200 = value; }
        }

    }
}
