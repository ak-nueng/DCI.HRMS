using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using DCI.HRMS.Model.Common;

namespace DCI.HRMS.Model.Personal
{
    public enum ReturnSts
    {
        คืนแล้ว= 1,
        ยังไม่คืน= 0,
        ไม่ต้องคืน = 2, 
    }
    public class PropertyBorrowInfo : ObjectInfo
    {

       private string brId="";
       private string empCode="";
       private string empName="";
       private DateTime  resignDate = DateTime.MinValue;
  
       private string type="";
       private string typeName="";

       private string detail="";
       private string data="";

       private ReturnSts returnSts = ReturnSts.ยังไม่คืน;

      
       private int qty=0;
       private string remark="";
       private DateTime rqDate=DateTime.MinValue;
       private DateTime rcDate=DateTime.MinValue;
       private DateTime rtDate=DateTime.MinValue;

       public PropertyBorrowInfo()
       {
       }


       public string BorrowId
       {
           set { brId = value; }
           get { return brId; }
       }
       public string EmpCode
       {
           set { empCode = value; }
           get { return empCode; }

       }
       public DateTime ResignDate
       {
           set { resignDate = value; }
           get { 
                if (resignDate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || resignDate <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return resignDate;
                }
            }

       }
       public string EmpName
       {
           get { return empName; }
           set { empName = value; }
       }
       public ReturnSts ReturnStatus
       {
           get { return returnSts; }
           set { returnSts = value; }
       }
     
       public string Type
       {
           set { type = value; }
           get { return type; }

       }

       public string TypeName
       {
           get { return typeName; }
           set { typeName = value; }
       }
       public string Detail
       {
           set { detail = value; }
           get { return detail; }

       } 
        public string Data
       {
           get { return data; }
           set { data = value; }
       }
       public int Quantity
       {
           set { qty = value; }
           get { return qty; }

       }
       public string Remark
       {
           set { remark = value; }
           get { return remark; }

       }
       public DateTime RequestDate
       {
           set { rqDate = value; }
           get { return rqDate;
                if (resignDate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || resignDate <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return resignDate;
                }
            }

       }
       public DateTime RecieveDate
       {
           set { rcDate = value; }
           get { 
                if (rcDate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || rcDate <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return rcDate;
                }
            }

       }
       public DateTime ReturnDate
       {
           set { rtDate = value; }
           get { 
                if (rtDate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || rtDate <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return rtDate;
                }
            }

       }

    }
}
