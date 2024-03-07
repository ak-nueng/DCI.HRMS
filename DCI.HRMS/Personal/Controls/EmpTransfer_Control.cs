using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Model.Personal;

namespace DCI.HRMS.Personal.Controls
{
    public partial class EmpTransfer_Control : UserControl
    {

        public EmpTransfer_Control()
        {
            InitializeComponent();
        }

        private void kryptonGroup2_Panel_Paint(object sender, PaintEventArgs e)
        {

        }
        public EmployeeCodeTransferInfo Information
        {
            get
            {
                EmployeeCodeTransferInfo item = new EmployeeCodeTransferInfo();
                item.TransferDate = dtpTrans.Value.Date;
                item.NewCode = txtNewCode.Text;
                item.OldCode = txtOldCode.Text;
                item.TransferStatus = lblTransSts.Text;
                return item;
            }
            set
            {
                dtpTrans.Value = value.TransferDate;
                txtOldCode.Text = value.OldCode;
                txtNewCode.Text = value.NewCode;
                lblTransSts.Text = value.TransferStatus;
            }
        }
    }
}
