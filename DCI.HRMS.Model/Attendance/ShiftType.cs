using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace DCI.HRMS.Model.Attendance
{
    public class ShiftType
    {
       private   string grpOt;
        private string shiftGroup;
        private string shiftStatus;
        private string remark;
        public ShiftType()
        {
        }

        public string GrpOt
        {

            get { return grpOt; }
            set { grpOt = value; }
        }
        public string ShiftGroup
        {

            get { return shiftGroup; }
            set { shiftGroup = value; }
        }
        public string ShiftStatus
        {

            get { return shiftStatus; }
            set { shiftStatus = value; }
        }
        public string Remark
        {

            get { return remark; }
            set { remark = value; }
        }

        public ArrayList GetUniqueShGrp(ArrayList shtype)
        {
            ArrayList shre = new ArrayList();
            foreach (ShiftType test in shtype)
            {
                foreach (ShiftType temp in shre)
                    if (temp.shiftGroup == test.shiftGroup && temp.ShiftStatus== test.ShiftStatus)
                        goto next;
                shre.Add(test);
            next: ;
            }

            return shre;

        }
        public ShiftType GetShiftTypeByOt(string grtot, ArrayList shtype)
        {
            foreach (ShiftType test in shtype)
            {
                
                    if (test.GrpOt == grtot)
                        return test;
         
            }
            return null;
        }
    }
}
