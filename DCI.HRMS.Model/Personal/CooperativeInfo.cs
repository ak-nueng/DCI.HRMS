using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DCI.HRMS.Model.Personal
{
    [Serializable]
    public class CooperativeInfo
    {
        private DateTime cooDate;

        private DateTime cooTerm;
        private decimal amount;
        private decimal deduct;


        public DateTime CooDate
        {
            get
            {
                if (cooDate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || cooDate <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return cooDate;
                }
            }
            set { cooDate = value; }
        }


        public DateTime CooTerm
        {
            get
            {
                if (cooTerm <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || cooTerm <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return cooTerm;
                }
            }
            set { cooTerm = value; }
        }

        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        public decimal Deduct
        {
            get { return deduct; }
            set { deduct = value; }
        }
    }
}
