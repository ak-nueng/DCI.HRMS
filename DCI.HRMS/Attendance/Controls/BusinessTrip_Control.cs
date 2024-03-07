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
    
    public partial class BusinessTrip_Control : UserControl
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
        public BusinessTrip_Control()
        {
            InitializeComponent();
        }
        public BusinesstripInfo Information
        {
            set
            {
                try
                {
                    BusinesstripInfo item = value;
                    txtCode.Text = item.EmpCode;
                   
                    txtFrom.Text = item.TFrom;
                    txtTo.Text = item.TTo;
                  
                    dpkRqDate.Value = item.FDate;
                    dpkRqDateTo.Value = item.TDate;
                    txtNote.Text = item.Note;

                  
                }
                catch 
                {
                    
                
                }
            }
            get
            {
                BusinesstripInfo item = new BusinesstripInfo();

                if (txtCode.Text == "")
                {
                    return null;
                }
             
                item.EmpCode = txtCode.Text;
                
                try
                {
                    item.FDate = dpkRqDate.Value.Date;
                }
                catch 
                {
                }
                try
                {
                    item.TDate = dpkRqDateTo.Value.Date;
                }
                catch
                {
                }
                item.TFrom = txtFrom.Text;
                item.TTo = txtTo.Text;
                item.Note = txtNote.Text;
                return item;
            }

        }
    

        private void TimeCardManual_Control_Load(object sender, EventArgs e)
        {
            
        }

        public void Open()
        {
          
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            KeyPressManager.ConvertTextTime(sender);
            /*
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
              
            }*/
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
        private void dpkRqDate_ValueChanged(object sender, EventArgs e)
        {
            if (sender == dpkRqDate)
            {
                if (dpkRqDate.Value > dpkRqDateTo.Value)
                    dpkRqDateTo.Value = dpkRqDate.Value;
            }
            else
            {
                if (dpkRqDate.Value > dpkRqDateTo.Value)
                    dpkRqDate.Value = dpkRqDateTo.Value;
            }



            if (dpkRqDate.Value.Date == dpkRqDateTo.Value.Date)
            {
                txtTo.ReadOnly = false;
                txtFrom.ReadOnly = false;

            }
            else
            {
                txtFrom.Text = "";
                txtTo.Text = "";
                txtTo.ReadOnly = true;
                txtFrom.ReadOnly = true;


            }
        }
     
    }
}
