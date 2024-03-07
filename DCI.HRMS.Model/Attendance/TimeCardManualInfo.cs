using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using DCI.HRMS.Model.Common;

namespace DCI.HRMS.Model.Attendance
{
   public class TimeCardManualInfo
    {
       private string empCode;
       private DateTime rqDate;
       private string timeForm;
       private string timeTo;
       private string rqType;
       private ObjectInfo inform = new ObjectInfo();

       public TimeCardManualInfo()
       {
       }
       public string EmpCode
       {
           set { empCode = value; }
           get { return empCode; }
       }
       public DateTime RqDate
       {
           set { rqDate = value; }
           get { 
                if (rqDate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || rqDate <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return rqDate;
                }
            }
       }
       public string TimeFrom
       {
           set { timeForm = value; }
           get { return timeForm; }
       }
       public string TimeTo
       {
           set { timeTo = value; }
           get { return timeTo; }
       }
       public string RqType
       {
           set { rqType = value; }
           get { return rqType; }
       }

       public string CreateBy
       {
           get { return inform.CreateBy; }
       }
       public DateTime CreateDateTime
       {
           get { return inform.CreateDateTime; }
       }
       public string LastUpdateBy
       {
           get { return inform.LastUpdateBy; }
       }
       public DateTime LastUpDateDateTime
       {
           get { return inform.LastUpdateDateTime; }
       }
       public ObjectInfo Inform
       {

           set { this.inform = value; }
       }

    }
}
