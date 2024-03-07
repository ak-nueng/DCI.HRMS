using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Common;
using DCI.HRMS.Base;
using DCI.Security.Model;
using DCI.HRMS.Service;
using System.Collections;
using DCI.HRMS.Model;
using DCI.HRMS.Model.Personal;
using DCI.HRMS.Util;
using DCI.HRMS.Service.SubContract;
using DCI.HRMS.Service.Trainee;

namespace DCI.HRMS.Personal
{
    public partial class DiagResignEmployee : BaseForm, IFormParent, IFormPermission
    {
        private ApplicationManager appMgr= ApplicationManager.Instance();
        private EmployeeService empSvr = EmployeeService.Instance();
        private SubContractService subSvr = SubContractService.Instance();
        private TraineeService tnSvr = TraineeService.Instance();
        public DiagResignEmployee()
        {
            InitializeComponent();
        }

        #region IForm Members

        public string GUID
        {
            get { return string.Empty; }
        }

        public object Information
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void AddNew()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Search()
        {
            throw new NotImplementedException();
        }

        public void Export()
        {
            throw new NotImplementedException();
        }

        public void Print()
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void RefreshData()
        {
            throw new NotImplementedException();
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IFormPermission Members

        public PermissionInfo Permission
        {
            set { throw new NotImplementedException(); }
        }

        #endregion

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                empDetail_Control1.Information = txtCode.Text;
                if (empDetail_Control1.Information != null)
                {
                    btnSave.Enabled = true;
                    KeyPressManager.Enter(e);
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูลพนักงานรหัส " + txtCode.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCode.Clear();
                    txtCode.Focus();
                }
            }
        }

        private void FrmResignEmployee_Load(object sender, EventArgs e)
        {
            empDetail_Control1.empServ = empSvr;
            empDetail_Control1.subSvr = subSvr;
            empDetail_Control1.trSvr = tnSvr;
            ArrayList resign = empSvr.GetAllResignType();
            cmbResignType.DisplayMember = "Code";
            cmbResignType.ValueMember = "Code";
            cmbResignType.DataSource = resign;
            cmbResignType.SelectedIndex = 0;
            dateTimePicker1.Value = DateTime.Today;
        }

        private void cmbResignType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BasicInfo rs = (BasicInfo)cmbResignType.SelectedItem;
                txtRsReason.Text = rs.DetailTh;

            }
            catch
            {
                txtRsReason.Text = string.Empty;

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            EmployeeInfo emp = (EmployeeInfo)empDetail_Control1.Information;
            if (emp != null)
            {
                try
                {
                    string rsType = cmbResignType.Text;
                    if (dateTimePicker1.Value.Date == DateTime.MinValue)
                    {
                        rsType = "0";
                    }

                    if (emp.Code.StartsWith("I"))
                    {
                        subSvr.EmployeeResignation(emp.Code, dateTimePicker1.Value.Date, rsType, txtRsReason.Text,txtRsRemark.Text, appMgr.UserAccount.AccountId);

                    }
                    else if (emp.Code.StartsWith("7"))
                    {
                        tnSvr.EmployeeResignation(emp.Code, dateTimePicker1.Value.Date, rsType, txtRsReason.Text,txtRsRemark.Text, appMgr.UserAccount.AccountId);

                    }
                    else
                    {
                        empSvr.EmployeeResignation(emp.Code, dateTimePicker1.Value.Date, rsType, txtRsReason.Text,txtRsRemark.Text, appMgr.UserAccount.AccountId);

                    }

                   
                    
                    btnSave.Enabled = false;
                    empDetail_Control1.Information = txtCode.Text;
                    txtCode.Focus();
                    txtCode.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ไม่สามารถบันทึกข้อมูลการลาออกได้ได้เนื่องจาก\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCode.Focus();
                 
                }
            }

        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            KeyPressManager.Enter(e);
        }

    }
}
