using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Base;
using DCI.HRMS.Common;
using DCI.Security.Model;
using DCI.HRMS.Service;
using DCI.HRMS.Model.Personal;
using System.Collections;
using DCI.HRMS.Util;

namespace DCI.HRMS.Personal
{
    public partial class FrmEmployee_Family : BaseForm, IFormParent, IFormPermission
    {
        private EmployeeService empSvr = EmployeeService.Instance();
        private FamilyInfo information = new FamilyInfo();
        private ApplicationManager apMgr = ApplicationManager.Instance();
        public FrmEmployee_Family()
        {
            InitializeComponent();
        }
        private bool CheckInput()
        {
            if (txtCode.Text == "")
            {
                MessageBox.Show("กรุณาป้อนรหัส", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCode.Focus();
                return false;
            }
            if (txtIdNo.Text == "")
            {
                MessageBox.Show("กรุณาป้อนเลขที่บัตรประชาชน", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtIdNo.Focus();
                return false;
            }
            if (txtPren.Text == "")
            {
                MessageBox.Show("กรุณาป้อนคำนำหน้าชื่อ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPren.Focus();
                return false;
            }
            if (txtName.Text == "")
            {
                MessageBox.Show("กรุณาป้อนชื่อ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Focus();
                return false;
            }
            if (comboBox1.SelectedIndex<0)
            {
                MessageBox.Show("กรุณาเลือกประเภทความสัมพันธ์", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox1.Focus();
                return false;
            }
            if (txtSurn.Text == "")
            {
                MessageBox.Show("กรุณาป้อนนามลกุล", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSurn.Focus();
                return false;
            }
    
            return true;

        }

        private void FrmEmployee_Family_Load(object sender, EventArgs e)
        {
            ucl_ActionControl1.Owner = this;
            empFamily_Control1.empSvr = empSvr;
            empFamily_Control1.Open();
            ArrayList rela = empSvr.GetFamilyRelation();
            comboBox1.DisplayMember = "DetailTh";
            comboBox1.ValueMember = "Description";
            comboBox1.DataSource = rela;
            comboBox1.SelectedIndex = -1;
        }

        #region IForm Members

        public string GUID
        {
            get { return this.GUID; }
        }

        public object Information
        {
            get
            {
                try
                {
                    information.EmpCode = txtCode.Text;
                    information.IdNo = txtIdNo.Text;
                    information.NameInThai.Name = txtName.Text;
                    information.NameInThai.Title = txtPren.Text;
                    information.NameInThai.Surname = txtSurn.Text;
                    information.Birth = dateTimePicker1.Value.Date;

                    try
                    {
                        information.Relation = comboBox1.SelectedValue.ToString();
                    }
                    catch{ }
                    try
                    {
                        information.TaxDed = int.Parse(txtTax.Text);
                    }
                    catch
                    {
                        information.TaxDed = 0;
                    }
                }
                catch
                {
                    return null;
                }


                return information;
                return information;

            }
            set
            {

            }
        }

        public void AddNew()
        {
            kryptonHeaderGroup1.Collapsed = false;
            ucl_ActionControl1.CurrentAction = FormActionType.None;
           
        }

        public void Save()
        {
            if (ucl_ActionControl1.CurrentAction == FormActionType.SaveAs)
            {
                if (CheckInput())
                {

                    FamilyInfo item = (FamilyInfo)this.Information;
                    item.CreateBy = apMgr.UserAccount.AccountId;
                    try
                    {
                        empSvr.SaveEmployeeFamily(item);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ไมาสามารถเพิ่มข้อมูลได้เนื่องจาก" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    empFamily_Control1.SetFamilyData(item.EmpCode);
                    this.Clear();
                    txtCode.Focus();
                }
               
            }
            else if (ucl_ActionControl1.CurrentAction == FormActionType.Save)
            {
                FamilyInfo item = (FamilyInfo)empFamily_Control1.Information;
                item.LastUpdateBy = apMgr.UserAccount.AccountId;
                try
                {
                    empSvr.UpdateEmployeeFamily(item);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ไมาสามารถบันทึกข้อมูลได้เนื่องจาก" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                empFamily_Control1.SetFamilyData(item.EmpCode);

            }



        }

        public void Delete()
        {
            if (MessageBox.Show("ต้องการลบข้อมูลใช่หรือไม่", "Conferm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                FamilyInfo item = (FamilyInfo)empFamily_Control1.Information;
                try
                {
                    empSvr.DeleteEmployeeFamily(item);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ไมาสามารถลบข้อมูลได้เนื่องจาก" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                empFamily_Control1.SetFamilyData(item.EmpCode);
            }
        }

        public void Search()
        {
            
        }

        public void Export()
        {
           
        }

        public void Print()
        {
           
        }

        public void Open()
        {
            
        }

        public void Clear()
        {
            txtCode.Text = "";
            txtIdNo.Text = "";
            txtPren.Text = "";
            txtName.Text = "";
            txtSurn.Text = "";
            comboBox1.SelectedIndex = -1;
            ucl_ActionControl1.CurrentAction = FormActionType.None;
        }

        public void RefreshData()
        {
            empFamily_Control1.SetFamilyData(information.EmpCode);
        }

        public void Exit()
        {
            this.Close();
        }

        #endregion

        #region IFormPermission Members

        public PermissionInfo Permission
        {
            set { ucl_ActionControl1.Permission = value; }
        }

        #endregion

        private void empFamily_Control1_Family_Slect()
        {
            ucl_ActionControl1.CurrentAction = FormActionType.Save;
            kryptonHeaderGroup1.Collapsed = true;

        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EmployeeInfo empm = empSvr.Find(txtCode.Text);
                if (empm != null)
                {
                    empFamily_Control1.SetFamilyData(txtCode.Text);
                    ucl_ActionControl1.CurrentAction = FormActionType.AddNew;
                    KeyPressManager.Enter(e);
                }
                else
                {
                    ucl_ActionControl1.CurrentAction = FormActionType.None;
                    MessageBox.Show("ไม่พบข้อมูลพนักงาน","Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();

                }

            }
        }

        private void kryptonHeaderGroup1_Click(object sender, EventArgs e)
        {
            this.AddNew();
        }

        private void txtIdNo_KeyDown(object sender, KeyEventArgs e)
        {
            KeyPressManager.Enter(e);
        }

        private void txtTax_KeyDown(object sender, KeyEventArgs e)
        {
            Save();
            //txtCode.Focus();
        }

        private void maskedTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void txtIdNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressManager.EnterNumericOnly(e);
        }
    }
}