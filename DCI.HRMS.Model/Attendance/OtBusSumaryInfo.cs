using System;
using System.Collections.Generic;
using System.Text;

namespace DCI.HRMS.Model.Attendance
{
   public class OtBusSumaryInfo
    {

        private string busWay;
     
        private string shift;
       
        private string otfrom;
        private string otto;
        private bool leavetype =false ;
        private int count =0;
        public OtBusSumaryInfo(string _bus,string _shift,string _otfrom,string _otto,bool _leave )
        {
            busWay = _bus;
            shift = _shift;
            otfrom = _otfrom;
            otto = _otto;
            leavetype = _leave;
            count=1;
        }

        public string BusWay
        {
            set { busWay = value; }
            get { return busWay; }

        }
       
        public string Shift
        {
            set { shift = value; }
            get { return shift; }

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
        public bool Leave
        {
            set { leavetype = value; }
            get { return leavetype; }

        }
        public int Count
        {
            set { count = value; }
            get { return count; }
        }


    }
}
