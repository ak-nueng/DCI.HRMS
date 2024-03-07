using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Service;
using DCI.HRMS.Model;
using DCI.HRMS.Common;
using DCI.HRMS.Model.Common;
using DCI.HRMS.Model.Attendance;

namespace DCI.HRMS.Attendance.Controls
{
    public partial class EmpShift_Control : UserControl
    {
        private bool chngByCode = false;
        private EmployeeShiftInfo cpSh = new EmployeeShiftInfo();
        private DateTime monthSh;
   
        private string shData;
        private string shO;
        private string emCode;

        public delegate void month_ChangeHandler();
        public delegate void year_ChangeHandler();
        public delegate void txtCode_EnterHandler();





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
        [Description("Fires when the MonthComboBox change.")]
        public event txtCode_EnterHandler txtCode_Enter;
        protected virtual void OnTxtCode_Enter()
        {
            if (txtCode_Enter != null)
            {
                txtCode_Enter();

            }

        }

      public object Information
        {
            get
            {
                shData = "";
                shO = "";
                emCode = txBCode.Text;
                try
                {
                    foreach (DayShift_Control dcl in flowLayoutPanel1.Controls)
                    {
                        if (dcl.Visible)
                        {
                            shData += dcl.Shift;
                            shO += cmbStso.SelectedItem.ToString();
                        }
                    }
                }
                catch
                {

                }
                EmployeeShiftInfo shift = new EmployeeShiftInfo();
                shift.YearMonth = monthSh.ToString("yyyyMM");
                shift.ShiftData = shData;
                shift.EmpCode = emCode;

                try
                {
                    shift.ShiftO = shO;
                    cmbStso.SelectedItem = shO.Substring(0, 1);
                }
                catch
                {


                }
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



                return (EmployeeShiftInfo)shift;
            }
            set
            {
                try
                {
                    EmployeeShiftInfo shift = (EmployeeShiftInfo)value;
                    DateTime rd = DateTime.Parse("01/" + shift.YearMonth.Substring(4) + "/" + shift.YearMonth.Substring(0, 4));
                    monthSh = rd;
                    emCode = shift.EmpCode;
                    shData = shift.ShiftData.Trim();
                    shO = shift.ShiftO.Substring(0, 1);
                    setShByCon();
                    this.Focus();
                }
                catch 
                {
                    
                 
                }

            }
        }

        public EmpShift_Control()
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
          
          
            cmBoxMonth.SelectedIndex = DateTime.Today.Month - 1;
            cmBoxYear.SelectedIndex = 1;
            Change_Status();
           

        }

        private void EmpShift_Control_Load(object sender, EventArgs e)
        {
            cmbStso.SelectedIndex = 0;



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
        private void txBCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OnTxtCode_Enter();
            }
            if (e.KeyCode == Keys.Down)
            {
                dayShift_Control1.Focus();
            }

        }
        private void Change_Status()
        {
            try
            {
                monthSh = DateTime.Parse("01/" + cmBoxMonth.SelectedItem.ToString() + "/" + cmBoxYear.SelectedItem.ToString());
              
                setShByCal();
            }
            catch { }

        }
        private void setShByCal()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                ClearDayControl();
                DateTime rd = monthSh;
                cmbStso.SelectedIndex = 0;

                while (monthSh.Month == rd.Month)
                {
                    DayShift_Control dcl = (DayShift_Control)flowLayoutPanel1.Controls[rd.Day - 1];
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
            catch 
            {
                
                throw;
            }
        }
        private void setShByCon()
        {

            try
            {
                Cursor = Cursors.WaitCursor;

                ClearDayControl();

                chngByCode = true;
                cmBoxMonth.SelectedItem = monthSh.ToString("MMMM");
                chngByCode = true;
                cmBoxYear.SelectedItem = monthSh.ToString("yyyy");
                chngByCode = false;
                cmbStso.SelectedItem = shO;
                txBCode.Text = emCode;
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
            catch 
            {
                
                throw;
            }
        }
        private void ClearDayControl()
        {

            for (int i = flowLayoutPanel1.Controls.Count - 1; i >= 27; i--)
            {
                DayShift_Control dcl = (DayShift_Control)flowLayoutPanel1.Controls[i];
                dcl.Visible = false;
            }
        }
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cpSh =(EmployeeShiftInfo) this.Information;

        }
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            string shdat = cpSh.ShiftData;
            string yearmonth = cpSh.YearMonth;
            EmployeeShiftInfo item = (EmployeeShiftInfo)this.Information;
            if (item.YearMonth == yearmonth)
            {
                item.ShiftData = shdat;
                item.ShiftO = cpSh.ShiftO;
                this.Information = item;
            }
            else
            {
                MessageBox.Show("เดือนของตารางกะไม่ตรงกัน กรุณาตรวจสอบ", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        public void EmpShift_Control_Enter(object sender, EventArgs e)
        {
            
        }



   
    }
}
