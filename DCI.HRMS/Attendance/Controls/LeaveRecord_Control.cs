using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Service;
using System.Collections;
using DCI.HRMS.Model;
using DCI.HRMS.Model.Attendance;
using DCI.HRMS.Util;

namespace DCI.HRMS.Attendance.Controls
{
    public partial class LeaveRecord_Control : UserControl
    {
        private EmployeeLealeRequestInfo empLvrq;
        public EmployeeLeaveService emlvSvr;
        public TimeCardService tmSvr;
        public ShiftService emShft;
        public delegate void DataMove_Handler();
        public delegate void txtCode_EnterHandler( object sender );
        public delegate void txtDate_EnterHandler(object sender);

        public LeaveRecord_Control()
        {
            InitializeComponent();
        }

        [Category("Action")]
        [Description("Fires when the YearComboBox change.")]
        public event DataMove_Handler DataMove;
        protected virtual void OnData_Move( )
        {
            if (DataMove != null)
            {
                DataMove();

            }

        }
        [Category("Action")]
        [Description("Fires when the MonthComboBox change.")]
        public event txtCode_EnterHandler Code_Enter;
        protected virtual void OnTxtCode_Enter(object sender)
        {
            if (Code_Enter != null)
            {
                Code_Enter(sender);

            }

        }
        [Category("Action")]
        [Description("Fires when the MonthComboBox change.")]
        public event txtDate_EnterHandler Date_Enter;
        protected virtual void OnTxtDate_Enter(object sender)
        {
            if (Date_Enter != null)
            {
                Date_Enter(sender);

            }

        }

        public object Infromation
        {

            get{

                try
                {
                    if (txtTotalM.Text=="" || txtTotalH.Text=="" )
                    {
                        txtTotalH.Focus();
                        return null;
                    }

                    EmployeeLealeRequestInfo item = new EmployeeLealeRequestInfo();

                    item.DocId = lblDocId.Text;
                    item.EmpCode = txtCode.Text;
                    item.LvType = cmbLeaveType.SelectedValue.ToString();
                    item.PayStatus = cmbPaySts.SelectedItem.ToString() == "No" ? "N" : "";
                    item.LvDate = txtDateTo.Value.Date;
                    item.LvFrom = txtFrom.Text;
                    item.LvTo = txtTo.Text;


                  // txtTo_Leave(txtFrom, new EventArgs());


                   item.TotalHour = txtTotalH.Text;
          
                   item.TotalMinute = int.Parse(txtTotalM.Text);
                
                 


                    try
                    {
                        item.LvNo = int.Parse(txtLvNo.Text);
                    }
                    catch { }
                    item.Reason = txtReason.Text;
                    empLvrq = item;
                    return empLvrq;
                }
                catch
                {

                    return null ;
                }
            }
            set
            {
                try
                {
                    empLvrq = (EmployeeLealeRequestInfo)value;
                    lblDocId.Text = empLvrq.DocId;
                    txtCode.Text = empLvrq.EmpCode;
                    cmbLeaveType.SelectedValue = empLvrq.LvType;
                    cmbPaySts.SelectedItem = empLvrq.PayStatus == "N" ? "No" : "Yes";
                    txtDateTo.Value = empLvrq.LvDate;
                    txtFrom.Text = empLvrq.LvFrom;
                    txtTo.Text = empLvrq.LvTo;
                    txtLvNo.Text = empLvrq.LvNo.ToString();
                    txtReason.Text = empLvrq.Reason;

                    txtTotalH.Text = empLvrq.TotalHour;
                    txtTotalM.Text = empLvrq.TotalMinute.ToString();


                    
                }
                catch 
                {
                    lblDocId.Text = "";
                    txtCode.Text = "";
                    cmbLeaveType.SelectedIndex = -1;
                    cmbPaySts.SelectedItem = "Yes";
                    txtDateTo.Value = DateTime.Today;
                    txtFrom.Text ="";
                    txtTo.Text = "";
                    txtLvNo.Text = "";
                    txtReason.Text = "";
                    txtTotalH.Text ="";
                    txtTotalM.Text = "";
                    
                }
            }
        }
        private WorkingHourInfo GetWorkHour(string empCode, DateTime lvDt)
        {
            EmployeeShiftInfo sh = emShft.GetEmShift(txtCode.Text, lvDt.ToString("yyyyMM"));

          
            if (sh != null)
            {

                string shft = sh.DateShift(lvDt);

                return  tmSvr.GetWorkingHour(lvDt, shft);
                
            }
            else
            {
                return null;
                // txtCode.Focus();
            }

        }
        public void Open()
        {
            empLvrq = new EmployeeLealeRequestInfo();
        }


        private void LeaveRecord_Control_Load(object sender, EventArgs e)
        {


        }
        public void GetAllType()
        {
            ArrayList lvType = emlvSvr.GetAllLeaveType();
            cmbLeaveType.DisplayMember = "NameForSearching";
            cmbLeaveType.ValueMember = "Code";
            cmbLeaveType.DataSource = lvType;
            cmbLeaveType.SelectedIndex = -1;
        }

        private void dateTimePicker1_Enter(object sender, EventArgs e)
        {
          
        }
        public bool EnableEdit
        {
            set
            {
                txtDateTo.Enabled = value;
                txtCode.Enabled = value;
         
            }
            get { return txtDateTo.Enabled; }
        }

 

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {


            if (sender == txtCode && e.KeyCode == Keys.Enter)
            {
                KeyPressManager.Enter(e);
                OnTxtCode_Enter(sender);

            }
            if (sender == txtReason && e.KeyCode == Keys.Enter)
            {
                OnData_Move();
            }
            else
            {
                KeyPressManager.Enter(e);

            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressManager.EnterNumericOnly(e);

        }

        private void txtFrom_Leave(object sender, EventArgs e)
        {
            KeyPressManager.ConvertTextTime(sender);
        }
        


        private void txtDateFrom_Leave(object sender, EventArgs e)
        {
            KeyPressManager.ConvertTextdate(sender);
       
        }

       

        private void kryptonButton1_Click(object sender, EventArgs e)
        {

        }

        private void cmbPaySts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPaySts.SelectedItem.ToString()=="No")
            {
                cmbPaySts.BackColor = Color.Red;

            }
            else
            {
                cmbPaySts.BackColor = Color.White;
            }
        }

        private void kryptonGroup2_Panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbLeaveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbLeaveType.SelectedValue.ToString() == "ABSE" || cmbLeaveType.SelectedValue.ToString() == "LATE")
                {
                    cmbPaySts.SelectedIndex = 1;
                }
                else
                {
                    cmbPaySts.SelectedIndex = 0;
                }
            }
            catch
            {
                cmbPaySts.SelectedIndex = 0;
            }
        }

        private void txtTo_Leave(object sender, EventArgs e)
        {
            if (KeyPressManager.ConvertTextTime(sender))
            {
                try
                {


                    WorkingHourInfo whr = GetWorkHour(txtCode.Text, txtDateTo.Value.Date);
                    if (whr == null)
                    {
                        whr = new WorkingHourInfo();
                    }

                    int hour = 0;
                    int min = 0;


                    DateTime tFrom = DateTime.Parse(txtDateTo.Value.ToString("dd/MM/yyyy") + " " + txtFrom.Text);
                    DateTime tTo = DateTime.Parse(txtDateTo.Value.ToString("dd/MM/yyyy") + " " + txtTo.Text);
                    if (tTo < tFrom)
                    {
                        tTo = tTo.AddDays(1);
                    }
                    else if (tFrom >= DateTime.Parse(txtDateTo.Value.ToString("dd/MM/yyyy") + " 00:00") && tFrom < DateTime.Parse(txtDateTo.Value.ToString("dd/MM/yyyy") + " 08:00"))
                    {
                        tTo = tTo.AddDays(1);
                        tFrom = tFrom.AddDays(1);
                    }
                    if (tFrom == whr.FirstStart && tTo == whr.SecondEnd)
                    {
                        int total = whr.FirstTotal + whr.SecondTotal;
                        hour = (int)total / 60;
                        min = total % 60;

                        txtTotalH.Text = (hour).ToString("00") + ":" + min.ToString("00");
                        txtTotalM.Text = (hour * 60 + min).ToString();
                    }
                    else if ((tFrom >= whr.FirstStart && tTo <= whr.FirstEnd) || (tFrom >= whr.SecondStart && tTo <= whr.SecondEnd))
                    {
                        TimeSpan t = tTo - tFrom;
                        hour = t.Hours;
                        min = t.Minutes;

                        txtTotalH.Text = (hour).ToString("00") + ":" + min.ToString("00");
                        txtTotalM.Text = (hour * 60 + min).ToString();
                    }

                    else if (tFrom >= whr.FirstStart && tFrom <= whr.FirstEnd && tTo >= whr.SecondStart && tTo <= whr.SecondEnd)
                    {


                        TimeSpan tf = whr.FirstEnd - tFrom;
                        TimeSpan ts = tTo - whr.SecondStart;
                        /*
                                                    if (tFrom >= DateTime.Parse("08:00") && tFrom <= DateTime.Parse("12:00")
                                                        && tTo >= DateTime.Parse("13:00") && tTo <= DateTime.Parse("17:45"))
                                                    {


                                                        t = tTo.AddHours(-1) - tFrom;


                                                    }

                                                    else if (tFrom >= DateTime.Parse("20:00") && tFrom <= DateTime.Parse("00:00").AddDays(1)
                                                        && tTo >= DateTime.Parse("00:40") && tTo <= DateTime.Parse("05:35"))
                                                    {

                                                        t = tTo.AddDays(1).AddMinutes(-50) - tFrom;

                                                    }*/
                        hour = tf.Hours + ts.Hours;
                        min = tf.Minutes + tf.Minutes;

                        txtTotalH.Text = (hour).ToString("00") + ":" + min.ToString("00");
                        txtTotalM.Text = (hour * 60 + min).ToString();
                    }
                    else
                    {

                        if (MessageBox.Show("เวลาไปตรงกับตารางกะ คุณต้องการดำเนินการต่อหรือไม่?", "คำเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        {

                            txtFrom.Clear();
                            txtTo.Clear();

                            txtCode.Focus();

                            txtTotalH.Text = "";
                            txtTotalM.Text = "";
                        }
                    }







                }
                catch
                {


                }

            }

        }
        private void cmbLeaveType_Leave(object sender, EventArgs e) 
        {
            if (txtCode.Text.Trim()!="")
            {
                EmployeeShiftInfo sh = emShft.GetEmShift(txtCode.Text, txtDateTo.Value.ToString("yyyyMM"));


                if (sh != null)
                {

                    string shft = sh.DateShift(txtDateTo.Value);
                    if (shft == "D" || shft == "N")
                    {
                        WorkingHourInfo tmp = tmSvr.GetWorkingHour(txtDateTo.Value, shft);
                        if (tmp != null)
                        {
                            WorkingHourInfo whr = tmp;

                            txtFrom.Text = whr.FirstStart.ToString("HH:mm");
                            txtTo.Text = whr.SecondEnd.ToString("HH:mm");

                        }
                        else
                        {

                            if (shft == "D")
                            {
                                txtFrom.Text = "08:00";
                                txtTo.Text = "17:45";
                            }
                            else if (shft == "N")
                            {
                                txtFrom.Text = "20:00";
                                txtTo.Text = "05:35";
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("พนักงานมีตารางกะวันหยุดกรุณาตรวจสอบวันที่", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        // dtpLvFrom.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูลตารงกะ กรุณาป้อนข้อมูลตารางกะก่อน", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // txtCode.Focus();
                } 
            }

        }


        private void txtTotalH_Leave(object sender, EventArgs e)
        {
            if (KeyPressManager.ConvertTextTime(sender))
            {
                string[] tm = txtTotalH.Text.Split(':');

                int mn = int.Parse(tm[0]) * 60 + int.Parse(tm[1]);
                txtTotalM.Text = mn.ToString();
            }
        }

   

   

      
     
      
           
       
    }
}
