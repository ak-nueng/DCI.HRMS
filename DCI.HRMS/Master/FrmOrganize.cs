using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Common;
using DCI.HRMS.Base;
using DCI.Security.Model;
using System.Collections;
using DCI.HRMS.Util;
using DCI.HRMS.Model;
using DCI.HRMS.Model.Organize;
using DCI.HRMS.Service;

namespace DCI.HRMS.Master
{
    public partial class FrmOrganize : BaseForm, IFormParent, IFormPermission
    {
        private ArrayList gvData = new ArrayList();
        private ArrayList addData;
        private ArrayList searchData;
        private FormAction formAct = FormAction.New;
        private DivisionService divSvr = DivisionService.Instance();
        private readonly string[] colName = new string[] { "Code", "Name", "ShortName", "Type", "DivisionOwner", "Remark" };
        private readonly string[] propName = new string[] { "Code", "Name", "ShortName", "Type", "OwnerName", "Remark" };
        private readonly int[] width = new int[] { 80, 80, 80, 80, 80, 150, 100, 100, 100, 100 };
        public FrmOrganize()
        {
            InitializeComponent();
        }

        private void FrmOrganize_Load(object sender, EventArgs e)
        {
            ucl_ActionControl1.Owner = this;
            this.Open();
       
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
                DivisionInfo item = new DivisionInfo();
                item.DivisionOwner = new DivisionInfo();
                item.Code = txtCode.Text;
                item.Name = txtName.Text;
                item.ShortName = txtShortName.Text;
                item.Type = (DivisionType)Enum.Parse(typeof(DivisionType), cboType.SelectedValue.ToString());
                item.DivisionOwner = cboOwner.SelectedItem as DivisionInfo;
                item.Remark = txtRemark.Text;
               return item;
            }
            set
            {
                DivisionInfo item = (DivisionInfo) value;

                txtCode.Text = item.Code;
                txtName.Text = item.Name;
                txtShortName.Text = item.ShortName;
                cboType.SelectedValue = DivisionInfo.ConvertToDivisionType(item.Type);
                cboOwner.SelectedValue = item.DivisionOwner.Code;
                txtRemark.Text = item.Remark;


            }
        }

        public void AddNew()
        {
            ucl_ActionControl1.CurrentAction = FormActionType.AddNew;
            this.Clear();
            txtName.ReadOnly = false;
            txtCode.ReadOnly = false;
            txtCode.Focus();
        }

        public void Save()
        {
            if (ucl_ActionControl1.CurrentAction == FormActionType.Save)
            {
                if (ucl_ActionControl1.Permission.AllowEdit)
                {
                    try
                    {
                        DivisionInfo item = (DivisionInfo)this.Information;

                   divSvr.Update(item);
                        this.Open();
                        Findrecord(item.Code);


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
                  
                        if (txtCode.Text != "")
                        {
                            try
                            {
                                DivisionInfo item = (DivisionInfo)this.Information;
                                divSvr.Save(item);
                                this.Search();
                                Findrecord( item.Code);
                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                        }
                        else
                        {
                            /* Code not Enter*/
                            MessageBox.Show("กรุณาป้อน Code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtCode.Focus();
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

                DivisionInfo item = (DivisionInfo)this.Information;

                item = divSvr.Find(item.Code, true);

                if (item.DivisionChild == null)
                {



                    if (MessageBox.Show("ต้องการลบข้อมูลใช่หรือไม่?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {




                        divSvr.Delete(item.Code);
                        this.Search();

                    }

                }
                else
                {
                    MessageBox.Show("ไม่สามารถลบข้อมูลได้ กรุณาลบหรือย้ายหน่วยงานในสังกัดก่อน", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ไม่สามารถลบข้อมูลได้ เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        public void Search()
        {
            gvData = divSvr.GetAll();
            FillDataGrid();
        }

        public void Export()
        {
           // throw new NotImplementedException();
        }

        public void Print()
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            AddGridViewColumns();
            ArrayList allOrg = divSvr.GetAllType();
            cboType.DisplayMember = "Name";
            cboType.ValueMember = "Code";
            cboType.DataSource = allOrg;
            cboType.SelectedIndex = 0;

            this.Search();
        }

        public void Clear()
        {
            txtCode.Clear();
            txtName.Clear();
            txtShortName.Clear();
            cboType.SelectedIndex = 0;
            cboOwner.SelectedIndex = 0;
            txtRemark.Clear();
        }

        public void RefreshData()
        {
       
            string tmpCode = txtCode.Text;
            this.Search();
            Findrecord( tmpCode);
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

        private void FrmDictionaryData_KeyDown(object sender, KeyEventArgs e)
        {
            ucl_ActionControl1.OnActionKeyDown(sender, e);
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
            if (dgItems.SelectedRows.Count != 0)
            {
                DivisionInfo item = (DivisionInfo)gvData[dgItems.SelectedRows[0].Index];
                if (item.Code != null)
                {
                    this.Information = item;
                    ucl_ActionControl1.CurrentAction = FormActionType.Save;
                    txtCode.ReadOnly = true; 
                }
               
            }
            else
            {
                ucl_ActionControl1.CurrentAction = FormActionType.None;
            }
        }
        private void txtType_KeyDown(object sender, KeyEventArgs e)
        {
            KeyPressManager.Enter(e);
        }

        private void txtThDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Save();
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
        private void Findrecord2(string type, string code)
        {
            foreach (DataGridViewRow item in dgItems.Rows)
            {
                if (type == item.Cells[0].Value.ToString() && code == item.Cells[1].Value.ToString())
                {
                    dgItems.CurrentCell = item.Cells[0];
                    return;
                }
            }
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboType.SelectedIndex!=-1)
            {
                BasicInfo sel = (BasicInfo)cboType.SelectedItem;
                ArrayList dvHead = new ArrayList();
                if (sel.Description!="-")
                {
                    dvHead = divSvr.FindByType(sel.Description);
                }
                else
                {
                    dvHead = new ArrayList();
                    DivisionInfo dv = new DivisionInfo();
                    dv.Code = "0";
                    dvHead.Add(dv);
                }
                cboOwner.DisplayMember = "DispText";
                cboOwner.ValueMember = "Code";
                cboOwner.DataSource = dvHead;
                cboOwner.SelectedIndex = 0;

            }

        }
    }
}
