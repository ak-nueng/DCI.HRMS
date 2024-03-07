using System;
using System.Collections.Generic;
using System.Text;

namespace DCI.HRMS.Model.Attendance
{
   
   public class OtBusWayInfo
    {
       private string busWay;
       private string stop;
       private string shift;
       private string code;
       private string name;
       private string surname;
       private string otfrom;
       private string otto;
       private string leavetype;

       private string dvcd;
       private string posit;
       private string wsts;
       private string grpl;
   private string grpot;
      
       public OtBusWayInfo()
       {
       }

       public string BusWay
       {
           set { busWay = value; }
           get { return busWay; }

       }
       public string Stop
       {
           set { stop = value; }
           get { return stop; }

       }
       public string Shift
       {
           set { shift = value; }
           get { return shift; }

       }
       public string Code
       {
           set { code = value; }
           get { return code; }

       }
       public string Name
       {
           set { name = value; }
           get { return name; }

       }
       public string Surname
       {
           set { surname = value; }
           get { return surname; }

       }
       public string Otfrom
       {
           set { otfrom = value; }
           get { return otfrom; }

       }
       public string Otto
       {
           set { otto = value; }
           get { return otto; }

       }
       public string Leavetype
       {
           set { leavetype = value; }
           get { return leavetype; }

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

       public string Wsts
       {
           get { return wsts; }
           set { wsts = value; }
       }

       public string Grpl
       {
           get { return grpl; }
           set { grpl = value; }
       }


       public string Grpot
       {
           get { return grpot; }
           set { grpot = value; }
       }
    }
}
