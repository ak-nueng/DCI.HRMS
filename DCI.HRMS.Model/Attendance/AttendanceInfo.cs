using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace DCI.HRMS.Model.Attendance
{
    public class AttendanceInfo
    {

        public class mAttInfo
        {

            public string SECT { get; set; }
            public string DVCD { get; set; }
            public string COSTCENTER { get; set; }
            public string GRPOT { get; set; }
            public string GRPL { get; set; }
            public string SHGRP { get; set; }
            public string CODE { get; set; }
            public string FNAME { get; set; }
            public string POSIT { get; set; }
            public string BUDGET { get; set; }
            public string D01WK { get; set; }
            public string D01OT { get; set; }
            public string D02WK { get; set; }
            public string D02OT { get; set; }
            public string D03WK { get; set; }
            public string D03OT { get; set; }
            public string D04WK { get; set; }
            public string D04OT { get; set; }
            public string D05WK { get; set; }
            public string D05OT { get; set; }
            public string D06WK { get; set; }
            public string D06OT { get; set; }
            public string D07WK { get; set; }
            public string D07OT { get; set; }
            public string D08WK { get; set; }
            public string D08OT { get; set; }
            public string D09WK { get; set; }
            public string D09OT { get; set; }
            public string D10WK { get; set; }
            public string D10OT { get; set; }
            public string D11WK { get; set; }
            public string D11OT { get; set; }
            public string D12WK { get; set; }
            public string D12OT { get; set; }
            public string D13WK { get; set; }
            public string D13OT { get; set; }
            public string D14WK { get; set; }
            public string D14OT { get; set; }
            public string D15WK { get; set; }
            public string D15OT { get; set; }
            public string D16WK { get; set; }
            public string D16OT { get; set; }
            public string D17WK { get; set; }
            public string D17OT { get; set; }
            public string D18WK { get; set; }
            public string D18OT { get; set; }
            public string D19WK { get; set; }
            public string D19OT { get; set; }
            public string D20WK { get; set; }
            public string D20OT { get; set; }
            public string D21WK { get; set; }
            public string D21OT { get; set; }
            public string D22WK { get; set; }
            public string D22OT { get; set; }
            public string D23WK { get; set; }
            public string D23OT { get; set; }
            public string D24WK { get; set; }
            public string D24OT { get; set; }
            public string D25WK { get; set; }
            public string D25OT { get; set; }
            public string D26WK { get; set; }
            public string D26OT { get; set; }
            public string D27WK { get; set; }
            public string D27OT { get; set; }
            public string D28WK { get; set; }
            public string D28OT { get; set; }
            public string D29WK { get; set; }
            public string D29OT { get; set; }
            public string D30WK { get; set; }
            public string D30OT { get; set; }
            public string D31WK { get; set; }
            public string D31OT { get; set; }
            public string REMARK { get; set; }
            public string ABSE { get; set; }
            public string ANNU { get; set; }
            public string PERS { get; set; }
            public string SICK { get; set; }
            public string OTHER { get; set; }
            public string WORKDAY { get; set; }
            public string WORKACT { get; set; }
            public string WORKOT { get; set; }
            public string HOLIDAYOT { get; set; }
            public string WORKNO { get; set; }
            public string WORKNOPER { get; set; }
            public mAttInfo()
            {

            }
        }


        public class mAttSummaryInfo
        {
            public string SECT { get; set; }

            public string WORKNOPER { get; set; }
            public string WORKDAYPER { get; set; }
            public string WORKNO { get; set; }
            public string ANNU { get; set; }
            public string PERS { get; set; }
            public string SICK { get; set; }
            public string DCI { get; set; }
            public string SUB { get; set; }
            public string TRN { get; set; }
            public string WORKNO_SU { get; set; }
            public string WORKNO_EN { get; set; }
            public string WORKNO_SF { get; set; }
            public string WORKNO_TR { get; set; }
            public string WORKNO_TE { get; set; }
            public string WORKNO_FO { get; set; }
            public string WORKNO_LE { get; set; }
            public string WORKNO_OP { get; set; }
            public string WORKDAY_SU { get; set; }
            public string WORKDAY_EN { get; set; }
            public string WORKDAY_SF { get; set; }
            public string WORKDAY_TR { get; set; }
            public string WORKDAY_TE { get; set; }
            public string WORKDAY_FO { get; set; }
            public string WORKDAY_LE { get; set; }
            public string WORKDAY_OP { get; set; }


            public mAttSummaryInfo()
            {

            }
        }



        public class mManpowerInfo
        {
            public string Item {  get; set; }
            public string Dept { get; set; }
            public string Sect { get; set; }
            public string CostCenter{  get; set; }
            public string President { get; set; }
            public string Director { get; set; }
            public string SGM { get; set; }
            public string GM { get; set; }
            public string AGM {  get; set; }
            public string SMG {  get; set; }
            public string MGR {  get; set; }
            public string AM {  get; set; }
            public string AV { get; set; }
            public string SU{ get; set; }
            public string Engineer { get; set; }
            public string Staff { get; set; }
            public string Translator { get; set; }
            public string Foreman { get; set; }
            public string Technician { get; set; }
            public string Leader { get; set; }
            public string PermanentOperator { get; set; }
            public string DailyPermanentOperator { get; set; }
            public string TemporaryOperator { get; set; }
            public string Others { get; set; }
            public string Sub { get; set; }
            public string Trainee { get; set; }
            public string Total { get; set; }



        public mManpowerInfo() { }
        }
    
    
    
        public class mOvertimeInfo
        {
            public string Item { get; set; }
            public string Dept { get; set; }
            public string Sect { get; set; }
            public string CostCenter { get; set; }
            public string OT1 { get; set; }
            public string OT15 { get; set; }
            public string OT2 { get; set; }
            public string OT3 { get; set; }
            public string OTTotal { get; set; }
            public string WorkDay { get; set; }
            public string WorkHour { get; set; }


            public mOvertimeInfo()
            {

            }

        }
    
    
    
    
    }
}
