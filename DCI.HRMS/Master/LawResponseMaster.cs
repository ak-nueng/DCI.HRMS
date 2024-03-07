using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Base;
using System.Collections;
using DCI.Security.Model;
using DCI.HRMS.Common;
using DCI.HRMS.Service;
using DCI.HRMS.Model.Allowance;
using DCI.HRMS.Model.Personal;
using DCI.HRMS.Service.SubContract;

namespace DCI.HRMS.Master
{
    public partial class LawResponseMaster : Form, IFormParent, IFormPermission
    {
        private ArrayList gvData = new ArrayList();
        private readonly string[] colName = new string[] { "ID", "Name", "Type", "Detail", "Remark" };
        private readonly string[] propName = new string[] { "ResId", "ResName", "Type", "ReaDetail", "Remark" };
        private readonly int[] width = new int[] { 80, 80, 80, 80, 300, 150, 100, 100, 100, 100 };
        private LawResponseService lawSvr = LawResponseService.Instance();

        public LawResponseMaster()
        {
            InitializeComponent();
        }
        private void LawResponseMaster_Load(object sender, EventArgs e)
        {
            cboFlType.SelectedIndex = 0;
            this.Open();
            ucl_ActionControl1.Owner = this;


        }
        #region IForm Members

        public string GUID
        {
            get { throw new NotImplementedException(); }
        }
        public object Information
        {
            get
            {
                LawResponseGroupInfo item = new LawResponseGroupInfo();
                item.ResId = lblId.Text;
                item.ResName = txtName.Text;
                item.Remark = txtRemark.Text;
                item.ReaDetail = txtDescr.Text;
                item.Type = cboType.SelectedItem.ToString();
                return item;
            }
            set
            {

                LawResponseGroupInfo item = value as LawResponseGroupInfo;
                lblId.Text = item.ResId;
                txtDescr.Text = item.ReaDetail;
                txtName.Text = item.ResName;
                cboType.SelectedItem = item.Type;
                txtRemark.Text = item.Remark;

            }
        }

        public void AddNew()
        {
            ucl_ActionControl1.CurrentAction = FormActionType.AddNew;
            this.Clear();
            txtName.Focus();

        }

        public void Save()
        {
            if (ucl_ActionControl1.CurrentAction == FormActionType.Save)
            {
                if (ucl_ActionControl1.Permission.AllowEdit)
                {
                    try
                    {
                        LawResponseGroupInfo item = (LawResponseGroupInfo)this.Information;

                        lawSvr.UpdateLawResponseGroupMaster(item);
                        this.Open();
                        Findrecord(item.ResId);


                    }
                    catch (Exception ex)
                    {


                        MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                else
                {
                    MessageBox.Show("ไม่สามารถบันทึกข้อมูล ได้เนื่องจาก คุณไม่มีสิทธิเข้าถึง", "Access Denie", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else if (ucl_ActionControl1.CurrentAction == FormActionType.SaveAs)
            {
                if (ucl_ActionControl1.Permission.AllowAddNew)
                {
                    if (txtName.Text != "")
                    {
                        if (cboType.SelectedItem != null)
                        {
                            try
                            {
                                LawResponseGroupInfo item = (LawResponseGroupInfo)this.Information;
                                lawSvr.SaveLawResponseGroupMaster(item);
                                this.Search();
                                FindrecordByName(item.ResName);
                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                        }
                        else
                        {
                            /* Code not Enter*/
                            MessageBox.Show("กรุณาเลือก Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            cboFlType.Focus();
                        }
                    }
                    else
                    {
                        /*Type not enter    */
                        MessageBox.Show("กรุณาป้อน Name", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        txtName.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("ไม่สามารถบันทึกข้อมูล ได้เนื่องจาก คุณไม่มีสิทธิเข้าถึง", "Access Denie", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        public void Delete()
        {
            try
            {
                if (MessageBox.Show("ต้องการลบข้อมูลใช่หรือไม่?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    lawSvr.DeleteLawResponseGroupMaster(lblId.Text);
                    this.Search();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ไม่สามารถลบข้อมูลได้ เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        public void Search()
        {
            gvData = lawSvr.SelectLawResponseGroupMaster(cboFlType.SelectedItem.ToString());
            FillDataGrid();
        }

        public void Export()
        {

        }

        public void Print()
        {

        }

        public void Open()
        {
            AddGridViewColumns();

            this.Search();
        }

        public void Clear()
        {
            lblId.Text = "";
            txtDescr.Text = "";
            txtName.Text = "";
            cboType.SelectedIndex = -1;
            txtRemark.Text = "";
        }

        public void RefreshData()
        {
            string id = lblId.Text;
            this.Search();
            Findrecord(id);
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
        private void FillDataGrid()
        {
            dgItems.DataBindings.Clear();
            dgItems.DataSource = null;

            dgItems.DataSource = gvData;
            this.Update();
        }
        private void dgItems_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyleDefault.ShowRowNumber(dgItems, e);
        }

        private void dgItems_SelectionChanged(object sender, EventArgs e)
        {
            if (dgItems.SelectedRows.Count > 0)
            {
                string id = dgItems.SelectedRows[0].Cells[0].Value.ToString();

                LawResponseGroupInfo grp = lawSvr.GetLawResponseGroupMaster(id);
                this.Information = grp;
                ucl_ActionControl1.CurrentAction = FormActionType.Save;
                txtName.Focus();

            }

        }

        private void cboFlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Search();
        }
        private void FindrecordByName(string type)
        {
            foreach (DataGridViewRow item in dgItems.Rows)
            {
                if (type == item.Cells[1].Value.ToString())
                {
                    dgItems.CurrentCell = item.Cells[1];
                    return;
                }
            }
        }
        private void Findrecord(string type)
        {
            foreach (DataGridViewRow item in dgItems.Rows)
            {
                if (type == item.Cells[0].Value.ToString())
                {
                    dgItems.CurrentCell = item.Cells[0];
                    return;
                }
            }
        }

        private void dgItems_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string id = dgItems.Rows[e.RowIndex].Cells[0].Value.ToString();
                FrmLawRespMasterDetail frmDetail = new FrmLawRespMasterDetail(id);
                frmDetail.ShowDialog(this);
            }

        }



        private EmployeeService empSvr = EmployeeService.Instance();
        private SubContractService subSvr = SubContractService.Instance();

        private void txtEmpCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                EmployeeInfo emp = empSvr.FindBasicInfo(txtEmpCode.Text);
                if (txtEmpCode.Text.StartsWith("I"))
                {
                    emp = subSvr.FindBasicInfo(txtEmpCode.Text);
                }

                if (emp != null)
                {
                    MessageBox.Show("W/C: " + emp.Workcenter + ", BudgetType: " + emp.BudgetType, "Done", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมมูลพนักงานรหัส " + txtEmpCode.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmpCode.Focus();
                }
            }

        }
    }
}
