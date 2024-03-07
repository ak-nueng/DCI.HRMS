using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Service;
using DCI.HRMS.Model.Attendance;
using System.Collections;
using DCI.HRMS.Util;

namespace DCI.HRMS.Attendance.Controls
{
    
    public partial class TimeCardManual_Control : UserControl
    {
        public delegate void Enter_data();
        public delegate void EnterCode();

        [Category("Action")]
        [Description("Fires when the MonthComboBox change.")]
        public event Enter_data enterData;
        protected virtual void OnenterData()
        {
            if (enterData != null)
            {
                enterData();

            }

        }
        [Category("Action")]
        [Description("Fires when the Code TextBox Enter.")]
        public event EnterCode enterCode;
        protected virtual void OnEnterCode()
        {
            if (enterCode != null)
            {
                enterCode();

            }

        }

        public TimeCardService tmcSrv;
        public TimeCardManual_Control()
        {
            InitializeComponent();
        }
        public TimeCardManualInfo Information
        {
            set
            {
                try
                {
                    TimeCardManualInfo item = value;
                    txtCode.Text = item.EmpCode;
                    dpkRqDate.Value = item.RqDate.Date;
                    txtFrom.Text = item.TimeFrom;
                    txtTo.Text = item.TimeTo;
                    comboBox1.SelectedValue = item.RqType;
                }
                catch 
                {
                    
                 
                }
            }
            get
            {
                TimeCardManualInfo item = new TimeCardManualInfo();

                if (txtCode.Text == "")
                {
                    return null;
                }
                else if (txtFrom.Text == "" && txtTo.Text == "")
                {
                    MessageBox.Show("กรุณาป้อนเวลา", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtFrom.Focus();
                    return null;
                }
                else if (comboBox1.SelectedIndex < 0)
                {
                    MessageBox.Show("กรุณาเลือกประเภท", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   comboBox1.Focus();
                    return null;
                }


                item.EmpCode = txtCode.Text;
                
                try
                {
                    item.RqDate = dpkRqDate.Value.Date;
                }
                catch 
                {
                }
                item.TimeFrom = txtFrom.Text;
                item.TimeTo = txtTo.Text;
                try
                {
                    item.RqType = comboBox1.SelectedValue.ToString();
                }
                catch 
                {
                }
                return item;
            }

        }
        public bool CodeEnable
        {
            set
            {
                txtCode.Enabled = value;
                dpkRqDate.Enabled = value;
                kryptonButton1.Enabled = !value;
                comboBox1.Enabled = value;
            }
            get
            {
                return txtCode.Enabled;
            }
        }

        private void TimeCardManual_Control_Load(object sender, EventArgs e)
        {
            
        }
     
        public void Open()
        {
            ArrayList tmrqType = tmcSrv.GetTimeCardManualType();
            comboBox1.DisplayMember = "NameForSearching";
            comboBox1.ValueMember = "Code";
            comboBox1.DataSource = tmrqType;
            comboBox1.SelectedIndex = -1;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            TextBox tt = (TextBox)sender;
            if (tt.Text.Trim() != string.Empty)
            {
                if (!tt.Text.Contains(":"))
                {
                    tt.Text = tt.Text.Insert(tt.Text.Length - 2, ":");
                }
                try
                {
                    DateTime dt = DateTime.Parse(tt.Text); 
                    tt.Text = dt.ToString("HH:mm"); 
                }
                catch 
                {
                    MessageBox.Show("เวลาไม่ถูกต้อง กรุณาป้อนใหม่","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tt.Clear();
                    tt.Focus();
                    
                }
              
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                if (sender == txtCode)
                {
                    OnEnterCode();
                    txtCode.Clear();
                }
                else
                    SendKeys.Send("{TAB}");
            }

        }
        private void kryptonButton1_Click_1(object sender, EventArgs e)
        {
            OnenterData();
        }

    }
}
