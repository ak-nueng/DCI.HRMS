using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Model;
using DCI.HRMS.Model.Common;
using DCI.HRMS.Common;
using DCI.HRMS.Model.Attendance;

namespace DCI.HRMS.Attendance.Controls
{
    public partial class MonthShift_Control : UserControl
    {
        private DateTime monthSh;
        private string shData;
       private string grouptype;
       private bool chngByCode = false;
       private ArrayList shtType;
      


        public delegate void month_ChangeHandler();
        public delegate void year_ChangeHandler();
       public delegate void grp_ChangeHandler();


        public MonthShift_Control()
        {
            InitializeComponent();

            DateTime dT = DateTime.Parse("01/01/" + DateTime.Today.Year.ToString());
      
            for (int i = 0; i < 12; i++)
            {
                string dt = dT.AddMonths(i).ToString("MMMM");
                cmBoxMonth.Items.Add(dt);
            }
            for (int i = -1; i < 3; i++)
            {
                string dt = dT.AddYears(i).ToString("yyyy");
                cmBoxYear.Items.Add(dt);

            }
            cmBoxShtype.Items.Add( "D");
            cmBoxShtype.SelectedIndex = 0;
            cmBoxMonth.SelectedIndex = DateTime.Today.Month - 1;
            cmBoxYear.SelectedIndex = 1;

            
          //  setShByCal();

            
        }
        public MonthShift_Control(MonthShiftInfo _shIfo)
        {
            InitializeComponent();
            this.Information= _shIfo;
          
        
        }
       

        [Category("Action")]
        [Description("Fires when the MonthComboBox change.")]
        public event month_ChangeHandler month_Changed;
        protected virtual void Onmonth_Change()
        {
            if (month_Changed != null)
            {
                month_Changed();

            }

        }
        [Category("Action")]
        [Description("Fires when the YearComboBox change.")]
        public event year_ChangeHandler year_Changed;
        protected virtual void Onyear_Change()
        {
            if (year_Changed != null)
            {
                year_Changed();

            }

        }
        [Category("Action")]
        [Description("Fires when the GroupComboBox change.")]
        public event grp_ChangeHandler grp_Changed;
        protected virtual void Ongrp_Change()
        {
            if (grp_Changed != null)
            {
                grp_Changed();
            }

        }


        private void Month_Shift_Control_Load(object sender, EventArgs e)
        {

        }


       
        public object Information
        {
            get
            {
                shData = "";
                foreach (DayShift_Control dcl in flowLayoutPanel1.Controls)
                {
                    if (dcl.Visible)
                        shData += dcl.Shift;
                }
                MonthShiftInfo shift = new MonthShiftInfo();
                shift.YearMonth = monthSh.ToString("yyyyMM");
                shift.ShiftData = shData;
                shift.GroupStatus = grouptype;
                ObjectInfo objinfo = new ObjectInfo();
                try
                {
                    objinfo.CreateBy = ApplicationManager.Instance().UserAccount.AccountId;
                    objinfo.LastUpdateBy = ApplicationManager.Instance().UserAccount.AccountId;
                    objinfo.LastUpdateDateTime = DateTime.Now;
                    objinfo.CreateDateTime = DateTime.Now;
                    shift.Inform = objinfo;
                }
                catch
                {
                    
 
                }
                return (MonthShiftInfo)shift;
            }
            set
            {
                try
                {
                    MonthShiftInfo shift = (MonthShiftInfo)value;
                    DateTime rd = DateTime.Parse("01/" + shift.YearMonth.Substring(4) + "/" + shift.YearMonth.Substring(0, 4));
                    monthSh = rd;
                    shData = shift.ShiftData.Trim();
                    grouptype = shift.GroupStatus;
                    setShByCon();
                }
                catch 
                {
                    
              
                }

            }
            

        }
        private void setShByCal()
        {
            Cursor = Cursors.WaitCursor;
            ClearDayControl();
            DateTime rd = monthSh;
           
            while (monthSh.Month == rd.Month)
            {
            DayShift_Control dcl = (DayShift_Control) flowLayoutPanel1.Controls[rd.Day - 1];
                string sht = "";
                if (rd.DayOfWeek == DayOfWeek.Saturday || rd.DayOfWeek == DayOfWeek.Sunday)
                    sht = "H";
                else
                    sht = "D";

                dcl.Shift = sht;
                dcl.Date = rd.Day.ToString("00");
                dcl.Day = rd.DayOfWeek.ToString().Substring(0, 2);
                dcl.Visible = true;

                //flowLayoutPanel1.Controls.Add(dcl);
                shData += sht;
                rd = rd.AddDays(1);
            }
            Cursor = Cursors.Default;
        }

        private void setShByCon()
        {
       
            Cursor = Cursors.WaitCursor;

            ClearDayControl();

            chngByCode = true;
            cmBoxMonth.SelectedItem = monthSh.ToString("MMMM");
            chngByCode = true;
            cmBoxYear.SelectedItem = monthSh.ToString("yyyy");
            chngByCode = true;
            cmBoxShtype.SelectedItem = grouptype;
            chngByCode = false;
            DateTime rd = monthSh;
            while (monthSh.Month == rd.Month)
            {
                string sh = shData.Substring(rd.Day - 1, 1);

                DayShift_Control dcl = (DayShift_Control)flowLayoutPanel1.Controls[rd.Day - 1];
                dcl.Date = rd.Day.ToString("00");
                dcl.Day = rd.DayOfWeek.ToString().Substring(0, 2);
                dcl.Shift = sh;
                dcl.Visible = true;


                // flowLayoutPanel1.Controls.Add(dcl);

                rd = rd.AddDays(1);

            }
            Cursor = Cursors.Default;
        }
        private void ClearDayControl()
        {

            for(int i= flowLayoutPanel1.Controls.Count-1;i>=0;i--)
            {
                DayShift_Control dcl = (DayShift_Control) flowLayoutPanel1.Controls[i];
                dcl.Visible = false;
            }
        }
        public ArrayList ShtType
        {
            set
            {
                shtType = value;
                cmBoxShtype.Items.Clear();
                ShiftType item = new ShiftType();
                shtType = item.GetUniqueShGrp(shtType);
                foreach (ShiftType temp in shtType)
                {
                    cmBoxShtype.Items.Add(temp.ShiftGroup+temp.ShiftStatus);

                }


               
              
          
                cmBoxShtype.SelectedItem= "D1";

            }
        }



        private void Month_Shift_Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void cmBoxShtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!chngByCode)               
            { 
               // MonthShiftInfo sh =(MonthShiftInfo) this.Information;
                Change_Status();
                Ongrp_Change(); 
            }
            chngByCode = false;
        }

        private void cmBoxYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!chngByCode)
            {   
                Change_Status();
                Onyear_Change();
             
            }
            chngByCode = false;
        }

        private void cmBoxMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!chngByCode)
            {
                Change_Status();
                Onmonth_Change();
               
            }
            chngByCode = false;
        }
        private void Change_Status()
        {
            try
            {
                monthSh = DateTime.Parse("01/" + cmBoxMonth.SelectedItem.ToString() + "/" + cmBoxYear.SelectedItem.ToString());
                grouptype = cmBoxShtype.SelectedItem.ToString();
                setShByCal();
            }
            catch { }

        }
       


    }
}
