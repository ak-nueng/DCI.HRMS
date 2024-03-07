using System;
using System.Windows.Forms;
using DCI.HRMS.Model.Attendance;
using DCI.HRMS.Util;

namespace DCI.HRMS.Attendance.Controls
{
    public partial class TimeCard_Control : UserControl
    {
        public TimeCard_Control()
        {
            InitializeComponent();

        }
        public TimeCardInfo Information
        {
            set
            {
                try
                {
                    TimeCardInfo item = value;
                    txtCode.Text = item.EmpCode;
                    dtpCardDate.Value = item.CardDate.Date;
                    txtTime.Text = item.CardTime;
                    txtTaffId.Text = item.CardMachId.ToString();
                    cmbDuty.SelectedItem = item.Duty;
                }
                catch
                {
                }
            }
            get
            {

                try
                {
                    TimeCardInfo item = new TimeCardInfo();
                    item.EmpCode = txtCode.Text;
                    item.CardDate = dtpCardDate.Value;
                    item.CardTime = txtTime.Text;
                    item.CardMachId = int.Parse(txtTaffId.Text);
                    item.Duty = cmbDuty.SelectedItem.ToString();
                    return item;
                }
                catch 
                {
                    return new TimeCardInfo();
                }
            }
        }

        private void txtTime_KeyDown(object sender, KeyEventArgs e)
        {
            KeyPressManager.Enter(e);
        }

        private void txtTime_Leave(object sender, EventArgs e)
        {
            KeyPressManager.ConvertTextTime(sender);
        }

        private void txtTaffId_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressManager.EnterNumericOnly(e);
        }
    }
}
