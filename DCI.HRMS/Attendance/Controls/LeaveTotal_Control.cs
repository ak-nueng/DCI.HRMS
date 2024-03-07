using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Service;
using DCI.HRMS.Model.Personal;
using System.Collections;
using DCI.HRMS.Model.Attendance;
using DCI.HRMS.Service.SubContract;
using DCI.HRMS.Service.Trainee;

namespace DCI.HRMS.Attendance.Controls
{
    public partial class LeaveTotal_Control : UserControl
    {
        public EmployeeLeaveService emLvSvr;
        public SubContractLeaveService subLvSvr;
        public TraineeLeaveService tnLvSvr;

       //private EmployeeInfo empinfo;
        ArrayList leaveTotal = new ArrayList();
        public LeaveTotal_Control()
        {
            InitializeComponent();
            kryptonDataGridView1.AutoGenerateColumns = false;
        }
        public object Information
        {
            set
            {              
                

            }
            get
            {
                return leaveTotal;
            }
        }
        public void Open(EmployeeService emv)
        {
          
        }
        public void CalLeave(string emp, DateTime _caldate)
        {
            Clear();
            kryptonHeader1.Text = "Total Leaves " + emp;

            if (emp.StartsWith("I"))
            {
                leaveTotal = subLvSvr.GetLeaveTotal(emp, _caldate);
            }
            else if (emp.StartsWith("7"))
            {
                leaveTotal = tnLvSvr.GetLeaveTotal(emp, _caldate);
            }
            else
            {
                leaveTotal = emLvSvr.GetLeaveTotal(emp, _caldate);
            }

            kryptonDataGridView1.DataSource = null;
            kryptonDataGridView1.DataSource = leaveTotal;

        }
        public void Clear()
        {

            kryptonHeader1.Text = "Total Leaves ";
            leaveTotal = new ArrayList();
            kryptonDataGridView1.DataSource = null;
            kryptonDataGridView1.DataSource = leaveTotal;

        }
    }
   
}
