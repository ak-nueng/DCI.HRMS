using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace DCI.HRMS.Model.Attendance
{
    public  class LeaveTotal
    {
        private string lvtype;
        private int lvtotao;
        private int lvTime;
        private int year;

   
        public LeaveTotal()
        {
        }
        public int Year
        {
            get { return year; }
            set { year = value; }
        }
        public string Type
        {
            set { lvtype = value; }
            get { return lvtype; }
        }
        public int LvTotal
        {
            set { lvtotao = value; }
            get { return lvtotao; }
        }
        public int Time
        {
            set { lvTime = value; }
            get { return lvTime; }
        }
        public string Total
        {
            get { return MnToText(lvtotao); }
        }
        private string MnToText(int mn)
        {

            //int dayrm = mn >= 0 ? (int)Math.Floor((decimal)mn / 525) : (int)Math.Ceiling((decimal)mn / 525);
            //int md = (int)mn % 525;
            //int hr = md >= 0 ? (int)Math.Floor((decimal)md / 60) : (int)Math.Ceiling((decimal)md / 60);

            int dayrm = mn >= 0 ? Convert.ToInt32(Math.Floor(Convert.ToDecimal(mn) / 525)) : Convert.ToInt32( Math.Ceiling(Convert.ToDecimal(mn) / 525));
            int md = (int)mn % 525;
            int hr = md >= 0 ? Convert.ToInt32(Math.Floor(Convert.ToDecimal(md) / 60)) : Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(md) / 60));
            md = md % 60;

            return dayrm.ToString("0") + ":" + hr.ToString("00") + ":" + md.ToString("00");

        }


    }
    public class AnnualTotal
    {
        public AnnualTotal()
        {
        }
        private int year;
        private double total;
        private double fullTotal;

      
        private double get;
        private double use;
        private double remaim;
       

        public int Year
        {
            set { year = value; }
            get { return year; }
        }

        public double Total
        {
            set { total = value; }
            get { return total; }
        }
        public string TotalText
        {
            get
            {
                return MnToText(total);
            }
        } 
        
        public double FullTotal
        {
            get { return fullTotal; }
            set { fullTotal = value; }
        }
        public string FullTotalText
        {
            get
            {
                return MnToText(fullTotal);
            }
        } 
        public double Get
        {
            set { get = value; }
            get { return get; }
        }
        public double Use
        {
            set { use = value; }
            get { return use; }

        }
        public string UseText
        {
            get
            {
                return MnToText(use);
            }
        }
        public double Remain
        {
            set { remaim = value; }
            get { return remaim; }
        }

        public string RemainHr
        {

            get { return MnToText(remaim); }
        }
        private string MnToText(double mn)
        {

            //int dayrm = mn >= 0 ? (int)Math.Floor(mn / 525) : (int)Math.Ceiling(mn / 525);
            //int md = (int)mn % 525;
            //int hr = md >= 0 ? (int)Math.Floor((decimal)md / 60) : (int)Math.Ceiling((decimal)md / 60);
            int dayrm = mn >= 0 ? Convert.ToInt32(Math.Floor(mn / 525)) : Convert.ToInt32(Math.Ceiling(mn / 525));
            int md = Convert.ToInt32(mn) % 525;
            int hr = md >= 0 ? Convert.ToInt32(Math.Floor(Convert.ToDecimal(md) / 60)) : Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(md) / 60));
            md = md % 60;

            return dayrm.ToString("0") + ":" + hr.ToString("00") + ":" + md.ToString("00");

        }



    }
    public class AnnualTotalDesc : IComparer
    {

        // Calls CaseInsensitiveComparer.Compare with the parameters . 
        int IComparer.Compare(object x, object y)
        {
            AnnualTotal xx = (AnnualTotal)x;
            AnnualTotal yy = (AnnualTotal)y;
            return ((new CaseInsensitiveComparer()).Compare( yy.Year,xx.Year));
        }

    }
}
