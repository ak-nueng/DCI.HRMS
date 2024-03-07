using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Model.Attendance;
using System.Collections;
using DCI.HRMS.Service;

namespace DCI.HRMS.Attendance.Controls
{
    public partial class Penalty_Control : UserControl
    {
        private PenaltyInfo info = new PenaltyInfo();
        private PenaltyService penSvr;
      
        public Penalty_Control()
        {
            InitializeComponent();
        }

        public void Open()
        {
            penSvr = PenaltyService.Instanse();
            ArrayList tmrqType = penSvr.SelectPenaltyType();


            cboPenType.DisplayMember = "NameForSearching";
            cboPenType.ValueMember = "Code";
            cboPenType.DataSource = tmrqType;
            cboPenType.SelectedIndex = 0;
        }

        public PenaltyInfo Information
        {
            get
            {

                try
                {
                    PenaltyInfo item = new PenaltyInfo();
                    item.PenaltyId = lblId.Text;
                    item.EmpCode = txtCode.Text;
                    item.WDescription = txtReason.Text;
                    item.WFrom = dtpWfrom.Value;
                    item.WTo = dtpWto.Value;
                    item.WTotal = int.Parse(txtWtotal.Text);
                    item.PenaltyType = cboPenType.SelectedValue.ToString();
                    item.PenaltyDate = dtpPenalty.Value;
                    item.PenaltyFrom = dtpPenFrom.Value;
                    item.PenaltyTo = dtpPenTo.Value;
                    item.PenaltyTotal = int.Parse(txtPenTotal.Text);
                    return item;
                }
                catch 
                {

                    return null;
                }

            }
            set
            {

                try
                {
                    PenaltyInfo item = value;
                    lblId.Text = item.PenaltyId;
                    txtCode.Text = item.EmpCode;
                    txtReason.Text = item.WDescription;
                    dtpWfrom.Value = item.WFrom;
                    dtpWto.Value = item.WTo;
                    txtWtotal.Text = item.WTotal.ToString();
                    cboPenType.SelectedValue = item.PenaltyType;
                    dtpPenalty.Value = item.PenaltyDate;
                    dtpPenFrom.Value = item.PenaltyFrom;
                    dtpPenTo.Value = item.PenaltyTo;
                    txtPenTotal.Text = item.PenaltyTotal.ToString();
                }
                catch 
                {
                }
            }
        }

        private void Penalty_Control_Load(object sender, EventArgs e)
        {
   
        }
    }
}
