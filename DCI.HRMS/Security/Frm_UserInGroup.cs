using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Base;
using DCI.Security.Model;
using DCI.Security.Service;
using System.Collections;
using DCI.HRMS.Common;
using DCI.HRMS.Util;

namespace DCI.HRMS.Security
{
    public partial class Frm_UserInGroup : Form, IFormPermission
    {
        private ApplicationManager apMgr = ApplicationManager.Instance();
        private UserAccountManager usrSvr = new UserAccountManager();
        private UserGroupService grpSvr = UserGroupService.Instance();
        private PermissionInfo perm = new PermissionInfo();
        private ArrayList allgrp = new ArrayList();
        private ArrayList gvData = new ArrayList();
        private ArrayList gvDataGrp = new ArrayList();
        private readonly string[] colName = new string[] { "AccountId", "FullName", "Description", "Email", "Enable", "UserGroup", "PasswordLastChange", "JoinDate" };
        private readonly string[] propName = new string[] { "AccountId", "FullName", "Description", "Email", "Enable", "UserGroup", "PasswordLastChange", "JoinDate" };
        private readonly int[] width = new int[] { 80, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100 };


        private readonly string[] colNameGrp = new string[] { "ID", "Name", "Enable", "Description", "Permanent" };
        private readonly string[] propNameGrp = new string[] { "ID", "Name", "Enable", "Description", "Permanent" };
        private readonly int[] widthGrp = new int[] { 80, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100 };



        private FormActionType actUsr = new FormActionType();
        private FormActionType actGrp = new FormActionType();
        public Frm_UserInGroup()
        {
            InitializeComponent();
        }

        private void Frm_UserInGroup_Load(object sender, EventArgs e)
        {





            AddGridViewColumnsGrp();

            OpenGrp();

            AddGridViewColumns();
            OpenUsr();


        }

        private void OpenUsr()
        {
            gvData = usrSvr.findUserAccountByGroup("%");
            FillDataGrid();
        }
        private void OpenGrp()
        {
            gvDataGrp = grpSvr.SelectAll();
            cmbGroup.DisplayMember = "Name";
            cmbGroup.ValueMember = "ID";
            cmbGroup.DataSource = gvDataGrp;
            //gvDataGrp = grpSvr.SelectAll();

            FillDataGridGrp();

        }

        #region IFormPermission Members


        public PermissionInfo Permission
        {
            set { perm = value; }
        }



        #endregion

        private void kryptonHeaderGroup1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void AddGridViewColumns()
        {
            this.dgItems.Columns.Clear();
            dgItems.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn[] columns = new DataGridViewTextBoxColumn[colName.Length];
            for (int index = 0; index < columns.Length; index++)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();

                column.Name = colName[index];
                column.DataPropertyName = propName[index];
                column.ReadOnly = true;
                column.Width = width[index];

                columns[index] = column;
                dgItems.Columns.Add(columns[index]);
            }
            dgItems.ClearSelection();
        }
        private void AddGridViewColumnsGrp()
        {
            this.dgItemsGrp.Columns.Clear();
            dgItemsGrp.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn[] columns = new DataGridViewTextBoxColumn[colNameGrp.Length];
            for (int index = 0; index < columns.Length; index++)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();

                column.Name = colNameGrp[index];
                column.DataPropertyName = propNameGrp[index];
                column.ReadOnly = true;
                column.Width = widthGrp[index];

                columns[index] = column;
                dgItemsGrp.Columns.Add(columns[index]);
            }
            dgItemsGrp.ClearSelection();
        }

        private void dgItems_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            UserAccountInfo item = (UserAccountInfo)gvData[e.RowIndex];
            if (!item.Enable)
            {
                dgItems.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.Gray;
            }
            DataGridViewStyleDefault.ShowRowNumber(dgItems, e);

        }
        private void FillDataGrid()
        {
            dgItems.DataBindings.Clear();
            dgItems.DataSource = null;

            dgItems.DataSource = gvData;
            this.Update();
        }
        private void FillDataGridGrp()
        {
            dgItemsGrp.DataBindings.Clear();
            dgItemsGrp.DataSource = null;

            dgItemsGrp.DataSource = gvDataGrp;
            this.Update();
        }
        private void SetActionUsr()
        {
            switch (actUsr)
            {
                case FormActionType.None:
                    btnAdd.Enabled = true;
                    btnCancel.Enabled = false;
                    btnSave.Enabled = false;
                    //txtIdNo.Enabled = false;
                    btnCancel.Text = "Delete";
                    break;
                case FormActionType.AddNew:
                    btnAdd.Enabled = false; ;
                    btnCancel.Enabled = true;
                    btnSave.Enabled = true;
                    // txtIdNo.Enabled = true; ;
                    btnCancel.Text = "Cancel";

                    break;
                case FormActionType.Save:
                    btnAdd.Enabled = true;
                    btnCancel.Enabled = true;
                    btnSave.Enabled = true;
                    // txtIdNo.Enabled = false;
                    btnCancel.Text = "Delete";

                    break;
                case FormActionType.SaveAs:
                    btnAdd.Enabled = false;
                    btnCancel.Enabled = true;
                    btnSave.Enabled = true;
                    //  txtIdNo.Enabled = true;
                    btnCancel.Text = "Cancel";
                    grbPwd.Visible = true;
                    break;
                case FormActionType.Delete:
                    break;
                case FormActionType.Print:
                    break;
                case FormActionType.Refresh:
                    break;
                case FormActionType.Close:
                    break;
                case FormActionType.Search:
                    break;
                default:
                    break;
            }

        }

        private void SetUserInfo(UserAccountInfo inf)
        {
            txtUsrId.Text = inf.AccountId;
            grbPwd.Visible = false;
            txtUsrId.ReadOnly = true;
            txtFullName.Text = inf.FullName;
            txtEmail.Text = inf.Email;
            txtDescript.Text = inf.Description;
            cmbGroup.SelectedValue = inf.UserGroup.ID;
            chkEnable.Checked = inf.Enable;
            chkChangePwdNextLogon.Checked = inf.ChangePasswordAtNextLogon;
            chkPwdNerchange.Checked = inf.PasswordNeverExpires;
            chkChangPwd.Checked = inf.CannotChangePassword;
            actUsr = FormActionType.Save;
            SetActionUsr();

        }
        private void ClearUserInfo()
        {
            txtUsrId.Clear();
            txtUsrId.ReadOnly = false;
            txtFullName.Clear();
            txtEmail.Clear();
            txtDescript.Clear();
            chkEnable.Checked = false; ;
            chkChangePwdNextLogon.Checked = false;
            chkPwdNerchange.Checked = false;
            chkChangPwd.Checked = false;
            txtUsrId.Focus();
        }

        private void dgItems_SelectionChanged(object sender, EventArgs e)
        {
            if (dgItems.SelectedRows.Count != 0)
            {
                UserAccountInfo item = (UserAccountInfo)gvData[dgItems.SelectedRows[0].Index];
                SetUserInfo(item);



            }
            else
            {

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            dgItems.ClearSelection();
            this.ClearUserInfo();

            actUsr = FormActionType.SaveAs;
            SetActionUsr();
            txtUsrId.ReadOnly = false;


        }
        private UserAccountInfo GetUserInfo()
        {
            UserAccountInfo inf = new UserAccountInfo();
            inf.AccountId = txtUsrId.Text;

            inf.FullName = txtFullName.Text;
            inf.Email = txtEmail.Text;
            inf.Description = txtDescript.Text;
            inf.UserGroup = (UserGroupInfo)cmbGroup.SelectedItem;
            inf.Enable = chkEnable.Checked;
            inf.ChangePasswordAtNextLogon = chkChangePwdNextLogon.Checked;
            inf.PasswordNeverExpires = chkPwdNerchange.Checked;
            inf.CannotChangePassword = chkChangPwd.Checked;
            return inf;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (actUsr == FormActionType.SaveAs)
            {

                if (txtPwd2.Text == "")
                {
                    MessageBox.Show("กรุณาป้อน Password", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPwd2.Focus();
                    return;
                }
                else if (txtPwd3.Text == "")
                {
                    MessageBox.Show("กรุณายืนยัน Password", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPwd3.Focus();
                    return;
                }
                else if (txtPwd2.Text != txtPwd3.Text)
                {
                    MessageBox.Show(" Password ไม่ถูกต้องกรุณายืนยันใหม่อีกครั้ง", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPwd3.Clear();
                    txtPwd3.Focus();
                    return;
                }
                {
                    try
                    {
                        UserAccountInfo item = GetUserInfo();

                        usrSvr.add(item, txtPwd2.Text, apMgr.UserAccount.AccountId);
                        actUsr = FormActionType.None;
                        SetActionUsr();
                        OpenUsr();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้เนื่องจาก\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }


            }
            else if (actUsr == FormActionType.Save)
            {

                try
                {
                    UserAccountInfo item = GetUserInfo();

                    usrSvr.save(item, apMgr.UserAccount.AccountId);
                    actUsr = FormActionType.None;
                    SetActionUsr();
                    OpenUsr();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้เนื่องจาก\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }


            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (actUsr == FormActionType.SaveAs)
            {
                try
                {
                    actUsr = FormActionType.None;
                    SetActionUsr();
                    OpenUsr();
                }
                catch (Exception ex)
                {

                }

            }
            else if (actUsr == FormActionType.Save)
            {
                if (MessageBox.Show("คุณต้องการลบข้อมูลใช่หรือไม่?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        UserAccountInfo item = GetUserInfo();

                        if (item.UserGroup.ID != 9999)
                        {
                            usrSvr.remove(item.AccountId);
                        }
                        else
                        {
                            MessageBox.Show("ไม่สามารถลบผู้ใช้งานที่อยู่ในกลุ่ม " + item.UserGroup.Name, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }

                        actUsr = FormActionType.None;
                        SetActionUsr();
                        this.ClearUserInfo();
                        OpenUsr();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ไม่สามารถลบข้อมูลได้เนื่องจาก\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                    }
                }
            }
        }

        private void dgItems_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgItems.SelectedRows.Count != 0)
            {
                if (e.RowIndex >= 0)
                {
                    UserAccountInfo item = (UserAccountInfo)gvData[e.RowIndex];

                    Dlg_ChangePassword dlgChk = new Dlg_ChangePassword(item);
                    dlgChk.ShowDialog(this);
                    OpenUsr();
                }

            }
            else
            {

            }

        }

        private void btnAdd2_Click(object sender, EventArgs e)
        {
            dgItemsGrp.ClearSelection();
            this.ClearGroupInfo();

            actGrp = FormActionType.SaveAs;
            SetActionGrp();
            txtGrpId.ReadOnly = false;

        }

        private void btnSave2_Click(object sender, EventArgs e)
        {

            if (actGrp == FormActionType.SaveAs)
            {

                if (txtGrpId.Text == "")
                {
                    MessageBox.Show("กรุณาป้อน Group Id", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtGrpId.Focus();
                    return;
                }
           
                {
                    try
                    {
                        UserGroupInfo item = GetGroupInfo();

                        grpSvr.SaveUserGroup(item);
                        actGrp = FormActionType.None;
                        SetActionGrp();
                        OpenGrp();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้เนื่องจาก\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }


            }
            else if (actGrp == FormActionType.Save)
            {

                try
                {
                    UserGroupInfo item = GetGroupInfo();

                    grpSvr.UpdateUserGroup(item);
                    actGrp = FormActionType.None;
                    SetActionGrp();
                    OpenGrp();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้เนื่องจาก\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void btnCancel2_Click(object sender, EventArgs e)
        {

            if (actGrp == FormActionType.SaveAs)
            {
                try
                {
                    actGrp = FormActionType.None;
                   
                    SetActionGrp();
                    OpenGrp();
                }
                catch (Exception ex)
                {

                }

            }
            else if (actGrp == FormActionType.Save)
            {
                if (MessageBox.Show("คุณต้องการลบข้อมูลใช่หรือไม่?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        UserGroupInfo item = GetGroupInfo();

                        if (item.ID != 9999)
                        {

                            ArrayList usrIdGrp = usrSvr.findUserAccountByGroup(item.ID.ToString());


                            if (usrIdGrp.Count == 0)
                            {
                                grpSvr.DeleteUserGroup(item.ID);
                            }
                            else
                            {
                                MessageBox.Show("ไม่สามารถลบกลุ่มได้เนื่องจากมีผู้ใช้งานอยู่ในกลุ่มนี้", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }

                        }
                        else
                        {
                            MessageBox.Show("ไม่สามารถลบกลุ่ม " + item.Name, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }

                        actGrp = FormActionType.None;
                        SetActionGrp();
                        this.ClearGroupInfo();
                        OpenGrp();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ไม่สามารถลบข้อมูลได้เนื่องจาก\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                    }
                }
            }
        }

    
        private void SetActionGrp()
        {
            switch (actGrp)
            {
                case FormActionType.None:
                    btnAdd2.Enabled = true;
                    btnCancel2.Enabled = false;
                    btnSave2.Enabled = false;
                    //txtIdNo.Enabled = false;
                    btnCancel2.Text = "Delete";
                    break;
                case FormActionType.AddNew:
                    btnAdd2.Enabled = false; ;
                    btnCancel2.Enabled = true;
                    btnSave2.Enabled = true;
                    // txtIdNo.Enabled = true; ;
                    btnCancel2.Text = "Cancel";

                    break;
                case FormActionType.Save:
                    btnAdd2.Enabled = true;
                    btnCancel2.Enabled = true;
                    btnSave2.Enabled = true;
                    // txtIdNo.Enabled = false;
                    btnCancel2.Text = "Delete";

                    break;
                case FormActionType.SaveAs:
                    btnAdd2.Enabled = false;
                    btnCancel2.Enabled = true;
                    btnSave2.Enabled = true;
                    //  txtIdNo.Enabled = true;
                    btnCancel2.Text = "Cancel";
                    txtGrpId.ReadOnly = false;

                    break;
                case FormActionType.Delete:
                    break;
                case FormActionType.Print:
                    break;
                case FormActionType.Refresh:
                    break;
                case FormActionType.Close:
                    break;
                case FormActionType.Search:
                    break;
                default:
                    break;
            }

        }

        private void dgItemsGrp_SelectionChanged(object sender, EventArgs e)
        {

            if (dgItemsGrp.SelectedRows.Count != 0)
            {
                UserGroupInfo item = (UserGroupInfo)gvDataGrp[dgItemsGrp.SelectedRows[0].Index];
                SetGroupInfo(item);



            }
            else
            {

            }
        }

        private void SetGroupInfo(UserGroupInfo item)
        {
            txtGrpId.Text = item.ID.ToString();
            txtGrpId.ReadOnly = true;
            txtGrpName.Text = item.Name;
            txtGrpDesc.Text = item.Description;
            chkGrpEnable.Checked = item.Enable;
            actGrp = FormActionType.Save;
            SetActionGrp();


        }
        private UserGroupInfo GetGroupInfo()
        {
            UserGroupInfo item = new UserGroupInfo();
            item.ID = int.Parse(txtGrpId.Text);

            item.Name = txtGrpName.Text;
            item.Description = txtGrpDesc.Text;
            item.Enable = chkGrpEnable.Checked;
            return item;
        }
        private void ClearGroupInfo()
        {
            txtGrpId.Clear();
            txtGrpName.Clear();
            txtGrpDesc.Clear();
            chkGrpEnable.Checked =false;
        }

        private void txtGrpId_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressManager.EnterNumericOnly(e);
        }

        private void dgItemsGrp_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (dgItems.SelectedRows.Count != 0)
            {
                UserGroupInfo item = (UserGroupInfo)gvDataGrp[e.RowIndex];

                if (item.ID!=9999)
                {
                    Frm_GroupPermision dlgChk = new Frm_GroupPermision(item);
                    dlgChk.ShowDialog(this);
                    OpenGrp(); 
                }
                else
                {
                    MessageBox.Show("ไม่สามารถกำหนด Permission ของกลุ่มนี้ได้", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }


            }
            else
            {

            }

        }




    }
}
