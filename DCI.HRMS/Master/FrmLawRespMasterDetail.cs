using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Model.Allowance;
using DCI.HRMS.Service;
using System.Collections;
using DCI.HRMS.Base;
using DCI.Security.Model;
using DCI.HRMS.Model.Personal;
using DCI.HRMS.Util;
using DCI.HRMS.Service.SubContract;
using DCI.HRMS.Service.Trainee;

namespace DCI.HRMS.Master
{
    public partial class FrmLawRespMasterDetail : Form
    {
        private FormActionType actGrp = new FormActionType();
        private FormActionType actEmp = new FormActionType();
        private ArrayList gvData = new ArrayList();
        private ArrayList gvDataEmp = new ArrayList();

        private LawResponseService lawSvr = LawResponseService.Instance();
        private EmployeeService empSvr = EmployeeService.Instance();
        private SubContractService subSvr = SubContractService.Instance();
      
        private string id = "";
        private readonly string[] colName = new string[] { "ResId", "ResName", "ReaDetail", "Remark" };
        private readonly string[] propName = new string[] { "ResId", "ResName", "ReaDetail", "Remark" };
        private readonly int[] width = new int[] { 80, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100 };
        private readonly string[] colNameEmp = new string[] { "LawRespId", "Code", "LicenseNo", "Date", "Expire", "Remark" };
        private readonly string[] propNameEmp = new string[] { "LawRespId", "EmpCode", "LicenseNo", "LicenseDate", "LicenseExp", "Remark" };
        private readonly int[] widthEmp = new int[] { 80, 80, 80, 80, 80, 80, 100, 100, 100, 100, 100, 100 };

        private PermissionInfo perm = new PermissionInfo();

        public FrmLawRespMasterDetail(string _id)
        {
            InitializeComponent();
            id = _id;
        }

        private void FrmLawRespMasterDetail_Load(object sender, EventArgs e)
        {
            AddGridViewColumns();
            AddGridViewColumnsEmp();
            Open();

        }

        private void Open()
        {
            LawResponseGroupInfo grp = lawSvr.GetLawResponseGroupMaster(id);
            LawResponseGroupInfo item = grp;
            lblId.Text = item.ResId;
            txtDescr.Text = item.ReaDetail;
            txtName.Text = item.ResName;
            cboType.SelectedItem = item.Type;
            txtRemark.Text = item.Remark;
            actGrp = FormActionType.None;
            SetActionGrp();
            actEmp = FormActionType.None;
            SetActionEmp();
           
            gvData = lawSvr.SelectLawResponseMaster(lblId.Text);

            FillDataGrid();

        }
        private void OpenEmp()
        {
            if (dgItems.SelectedRows.Count != 0)
            {
                LawResponseInfo item = (LawResponseInfo)gvData[dgItems.SelectedRows[0].Index];
               
                SetInfo(item);
            }
            else
            {

            }
        }
        private void FillDataGrid()
        {
            dgItems.DataBindings.Clear();
            dgItems.DataSource = null;

            dgItems.DataSource = gvData;
            this.Update();
        }
        private void FillDataGridEmp()
        {
            dgItemsEmp.DataBindings.Clear();
            dgItemsEmp.DataSource = null;

            dgItemsEmp.DataSource = gvDataEmp;
            this.Update();
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
        private void AddGridViewColumnsEmp()
        {
            this.dgItemsEmp.Columns.Clear();
            dgItemsEmp.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn[] columns = new DataGridViewTextBoxColumn[colNameEmp.Length];
            for (int index = 0; index < columns.Length; index++)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();

                column.Name = colNameEmp[index];
                column.DataPropertyName = propNameEmp[index];
                column.ReadOnly = true;
                column.Width = widthEmp[index];

                columns[index] = column;
                dgItemsEmp.Columns.Add(columns[index]);
            }
            dgItemsEmp.ClearSelection();
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
                    txtDetName.ReadOnly = false;

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
        private void SetActionEmp()
        {
            switch (actEmp)
            {
                case FormActionType.None:
                    btnAdd.Enabled = true;
                    btnDelete.Enabled = false;
                    btnSave.Enabled = false;
                    //txtIdNo.Enabled = false;
                    btnCancel2.Text = "Delete";
                    break;
                case FormActionType.AddNew:
                    btnAdd.Enabled = false; ;
                    btnDelete.Enabled = true;
                    btnSave.Enabled = true;
                    // txtIdNo.Enabled = true; ;
                    btnCancel2.Text = "Cancel";

                    break;
                case FormActionType.Save:
                    btnAdd.Enabled = true;
                    btnDelete.Enabled = true;
                    btnSave.Enabled = true;
                    // txtIdNo.Enabled = false;
                    btnDelete.Text = "Delete";

                    break;
                case FormActionType.SaveAs:
                    btnAdd.Enabled = false;
                    btnDelete.Enabled = true;
                    btnSave.Enabled = true;
                    //  txtIdNo.Enabled = true;
                    btnDelete.Text = "Cancel";
                    txtDetName.ReadOnly = false;

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
        private void dgItems_SelectionChanged(object sender, EventArgs e)
        {
            OpenEmp();
        }

        private void SetInfo(LawResponseInfo item)
        {
            lblDetId.Text = item.ResId;
            txtDetName.Text = item.ResName;
            txtDetDetail.Text = item.ReaDetail;
            txtDetRemark.Text = item.Remark;

            actGrp = FormActionType.Save;
            SetActionGrp();

            gvDataEmp = lawSvr.SelectEmpLawResponse(item.ResId);
            FillDataGridEmp();

        }
        private LawResponseInfo GetGroupInfo()
        {
            LawResponseInfo item = new LawResponseInfo();
       
            item.ResId = lblDetId.Text;
            item.ResName = txtDetName.Text;
            item.ReaDetail = txtDetDetail.Text;
            item.GroupResId = lblId.Text;
            item.Remark = txtRemark.Text;
            return item;
        }
        private void ClearGrpInfo()
        {
            lblDetId.Text = "";
            txtDetName.Text = "";
            txtDetDetail.Text = "";      
            txtDetRemark.Text = "";
        }
        private void dgItemsEmp_SelectionChanged(object sender, EventArgs e)
        {
            if (dgItemsEmp.SelectedRows.Count != 0)
            {
                EmpLawResponseInfo empLaw = gvDataEmp[dgItemsEmp.SelectedRows[0].Index] as EmpLawResponseInfo;
                SetLawEmpInfo(empLaw);
            }
        }

        private void SetLawEmpInfo(EmpLawResponseInfo item)
        {
            txtCode.ReadOnly = true;
            txtCode.Text = item.EmpCode;
            txtLicense.Text = item.LicenseNo;
            dtpDate.Value = item.LicenseDate;
            dtpExpire.Value = item.LicenseExp;
            txtEmpRemark.Text = item.Remark;
            empData_Control1.Information = empSvr.FindBasicInfo(item.EmpCode);
            actEmp = FormActionType.Save;
            SetActionEmp();


        }
        private EmpLawResponseInfo GetLawEmpInfo()
        {
            EmpLawResponseInfo item = new EmpLawResponseInfo();
            item.LawRespId = lblDetId.Text;
            item.EmpCode = txtCode.Text;
            item.LicenseNo = txtLicense.Text;
            item.LicenseDate = dtpDate.Value;
            item.LicenseExp = dtpExpire.Value;
            item.Remark = txtEmpRemark.Text;
            return item;
            

        }
        private void ClearEmpInfo()
        {
          
            txtCode.Text ="";
            txtLicense.Text = "";
            //dtpDate.Value = DateTime.MinValue;
            dtpDate.Value = DateTime.Now;
            //dtpExpire.Value = DateTime.MinValue;
            dtpExpire.Value = DateTime.Now;
            txtEmpRemark.Text = "";
            empData_Control1.Information = new EmployeeInfo();
      
        }
        private void dgItemsEmp_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            EmpLawResponseInfo empLaw = gvDataEmp[e.RowIndex] as EmpLawResponseInfo;

            EmployeeInfo emp = empSvr.FindBasicInfo(empLaw.EmpCode);
            if (emp.Resigned)
            {
                dgItemsEmp.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void btnAdd2_Click(object sender, EventArgs e)
        {
            dgItems.ClearSelection();
            this.ClearGrpInfo();

            actGrp = FormActionType.SaveAs;
            SetActionGrp();
            txtDetName.Focus();
     

        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (lblDetId.Text != "")
            {
                dgItemsEmp.ClearSelection();
                this.ClearEmpInfo();

                actEmp = FormActionType.SaveAs;
                SetActionEmp();
                txtCode.ReadOnly = false;
                txtCode.Focus();
            }
            else
            {
                MessageBox.Show("กรุณาเลือก Group ก่อน", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }

        private void btnSave2_Click(object sender, EventArgs e)
        {

            if (actGrp == FormActionType.SaveAs)
            {

                if (txtName.Text == "")
                {
                    MessageBox.Show("กรุณาป้อน Group Name", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtName.Focus();
                    return;
                }

                {
                    try
                    {
                        LawResponseInfo item = GetGroupInfo();

                        lawSvr.SaveLawResponseMaster(item);
                        actGrp = FormActionType.None;
                        SetActionGrp();
                        Open();
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
                    LawResponseInfo item = GetGroupInfo();

                    lawSvr.UpdateLawResponseMaster(item);
                    actGrp = FormActionType.None;
                    SetActionGrp();
                    Open();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้เนื่องจาก\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }



        private void btnSave_Click(object sender, EventArgs e)
        {

            if (actEmp == FormActionType.SaveAs)
            {

                if (txtCode.Text == "")
                {
                    MessageBox.Show("กรุณาป้อนรหัสพนักงาน", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtName.Focus();
                    return;
                }

                {
                    try
                    {
                        EmpLawResponseInfo item = GetLawEmpInfo();

                        lawSvr.SaveEmpLawResponse(item);
                        actEmp = FormActionType.None;
                        SetActionEmp();
                        OpenEmp();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้เนื่องจาก\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }


            }
            else if (actEmp == FormActionType.Save)
            {

                try
                {
                    EmpLawResponseInfo item = GetLawEmpInfo();

                    lawSvr.UpdateEmpLawResponse(item);
                    actEmp = FormActionType.None;
                    SetActionEmp();
                    OpenEmp();
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
                    Open();
                }
                catch (Exception ex)
                {


                }

            }
            else if (actGrp == FormActionType.Save)
            {
                if (MessageBox.Show("คุณต้องการลบข้อมูลใช่หรือไม่?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dgItemsEmp.Rows.Count != 0)
                    {
                        MessageBox.Show("กรุณาลบข้อมูลพนักงานในกลุ่มออกก่อน", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        try
                        {
                            lawSvr.DeleteLawResponseMaster(lblDetId.Text);
                            ClearGrpInfo();
                            actGrp = FormActionType.None;

                            SetActionGrp();
                            Open();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("ไม่สามารถลบข้อมูลได้เนื่องจาก\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }


                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (actEmp == FormActionType.SaveAs)
            {
                try
                {
                    actEmp = FormActionType.None;

                    SetActionEmp();
                    Open();
                }
                catch (Exception ex)
                {

                }

            }
            else if (actEmp == FormActionType.Save)
            {
                if (MessageBox.Show("คุณต้องการลบข้อมูลใช่หรือไม่?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        lawSvr.DeleteEmpLawResponse(lblDetId.Text, txtCode.Text);
                        ClearEmpInfo();
                        actEmp = FormActionType.None;
                        SetActionEmp();
                        OpenEmp();
                    }
                    catch { }
                }
            }
        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            
            KeyPressManager.Enter(e);
        }

        private void txtCode_Leave(object sender, EventArgs e)
        {
            if (txtCode.Text!="")
            {
                EmployeeInfo emp = empSvr.FindBasicInfo(txtCode.Text);
                if (txtCode.Text.StartsWith("I"))
                {
                    emp = subSvr.FindBasicInfo(txtCode.Text);
                }        

                if (emp!= null)
                {
                    empData_Control1.Information = emp;
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมมูลพนักงานรหัส " + txtCode.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCode.Focus();
                }
            }
        }
    

    }
}
