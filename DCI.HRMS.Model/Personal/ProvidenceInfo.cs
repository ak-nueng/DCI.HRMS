using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DCI.HRMS.Model.Personal
{
    [Serializable]
   public  class ProvidenceInfo
    {
       private string pvno;
       private DateTime pvdate;
       private DateTime pvterm;
       private decimal pvperc;
       private decimal pvpercCompany;
        
       public ProvidenceInfo()
       {
       }
       public string ProvidenceNo
       {
           set { pvno = value; }
           get { return pvno; }
           
       }
       public DateTime ProvidenceDate
       {
           set { pvdate = value; }
           get { 
                if (pvdate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || pvdate <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return pvdate;
                }
            }
       }
       public DateTime ProvidenceTerminate
       {
           set { pvterm = value; }
           get { 
                if (pvterm <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || pvterm <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return pvterm;
                }
            }
       }

       public decimal ProvidencePercent
       {
           set { pvperc = value; }
           get { return pvperc; }
       }


       public decimal ProvidencePercentCompany
       {
           set { pvpercCompany = value; }
           get { return pvpercCompany; }
       }


    }
}
