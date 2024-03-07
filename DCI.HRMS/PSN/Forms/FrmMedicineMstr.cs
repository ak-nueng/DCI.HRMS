using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Base;
//using DCIBizPro.DTO.SM;
using DCI.HRD.Service;
using System.Collections;
using DCI.HRD.Model;
using System.Diagnostics;
using DCI.HRMS.Util;
using DCI.Security.Model;

namespace DCI.HRMS.PSN
{
    public partial class FrmMedicineMstr : Form , IFormParent , IFormPermission
    {
        private FirstAidService firstAidService = FirstAidService.Instance();

        public FrmMedicineMstr()
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
                MedicineInfo item = new MedicineInfo();
                item.Code = txtCode.Text;
                item.Name = txtName.Text;
                item.Unit = txtUnit.Text;

                return item;
            }
            set
            {
                MedicineInfo item = (MedicineInfo)value;
                txtCode.Text = item.Code;
                txtName.Text = item.Name;
                txtUnit.Text = item.Unit;
            }
        }

        public void AddNew()
        {
            uclAction.CurrentAction = FormActionType.AddNew;
            Clear();

            this.txtCode.ReadOnly = false;
            txtCode.Focus();
        }

        public void Save()
        {
            this.Cursor = Cursors.WaitCursor;
            if (ValidateInput())
            {
                try
                {
                    string msg = string.Empty;
                    MedicineInfo item = (MedicineInfo)Information;

                    if (uclAction.CurrentAction == FormActionType.SaveAs)
                    {
                        firstAidService.AddMedicine(item, "SYSTEM");
                        msg = "เพิ่มข้อมูลยารักษาโรคเรียบร้อย";
                    }
                    else if (uclAction.CurrentAction == FormActionType.Save)
                    {
                        firstAidService.UpdateMedicine(item, "SYSTEM");
                        msg = "บันทึกข้อมูลยารักษาโรคเรียบร้อย";
                    }

                    Text = item.Code + " - " + item.Name;
                    uclAction.CurrentAction = FormActionType.Save;

                    MessageBox.Show(this, msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PopulateList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            this.Cursor = Cursors.Default;
        }

        public void Delete()
        {
            this.Cursor = Cursors.WaitCursor;
            string msg = string.Format("คุณต้องการลบข้อมูลยารักษาโรค : {0} ใช่หรือไม่?", txtName.Text);
            DialogResult result = MessageBox.Show(this, msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    firstAidService.DeleteMedicine(txtCode.Text);
                    Clear();
                    
                    this.Text = string.Empty;
                    uclAction.CurrentAction = FormActionType.AddNew;
                    
                    MessageBox.Show(this, "ลบข้อมูลยารักษาโรคเรียบร้อยแล้ว", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PopulateList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            this.Cursor = Cursors.Default;
        }

        public void RefreshData()
        {
            string activeItem = txtCode.Text;

            this.PopulateList();
            if (activeItem.Length > 0)
            {
                Search(activeItem);
            }
        }

        public void Print()
        {
            
        }

        public void Exit()
        {
            this.Close();
        }

        public void Export()
        {
            
        }

        public void Open()
        {
            try
            {
                uclAction.Owner = this;
                uclAction.CurrentAction = FormActionType.None;
            }
            catch { }

            PopulateList();
            Clear();
        }

        public void Clear()
        {
            txtCode.Text = string.Empty;
            txtName.Text = string.Empty;
            txtUnit.Text = string.Empty;
        }

        public void Search()
        {
            Clear();
            txtCode.ReadOnly = false;
            txtCode.Focus();
        }

        public void Search(string activeItem)
        {
            foreach (DataGridViewRow row in this.dgItems.Rows)
            {
                if (row.Cells[0].Value.ToString() == activeItem)
                {
                    MedicineInfo item = new MedicineInfo();

                    item.Code = (string)row.Cells[0].Value;
                    item.Name = (string)row.Cells[1].Value;
                    item.Unit = (string)row.Cells[2].Value;

                    dgItems.Rows[row.Index].Selected = true;

                    this.PopulateDataForEdit(item);
                    break;
                }
            }
        }
        #endregion

        #region IFormPermission Members

        public PermissionInfo Permission
        {
            set { uclAction.Permission = value; }
        }

        #endregion


        private void AddGridViewColumns()
        {
            dgItems.Columns.Clear();

            DataGridViewTextBoxColumn[] columns = new DataGridViewTextBoxColumn[3];

            string[] colName = new string[] { "รหัสยา", "ชื่อยา", "หน่วยนับ" };
            string[] propName = new string[] { "Code", "Name", "Unit" };

            int[] width = new int[] { 90,200,100 };

            for(int index = 0 ; index < columns.Length ; index++)
            {

                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                
                column.Name = colName[index];
                column.DataPropertyName = propName[index];
                column.ReadOnly = true;
                column.Width = width[index];

                columns[index] = column;
                dgItems.Columns.Add(columns[index]);
            }
        }
        private void PopulateList()
        {
            try
            {
                ArrayList medicines = firstAidService.FindAllMedicine();

                dgItems.AutoGenerateColumns = false;
                dgItems.DataSource = medicines;

                this.AddGridViewColumns();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private void PopulateDataForEdit(MedicineInfo item)
        {
            this.txtCode.ReadOnly = true;
            this.Information = item;
            this.uclAction.CurrentAction = FormActionType.Save;
        }
        private bool ValidateInput()
        {
            StringBuilder sb = new StringBuilder();
            if (txtCode.Text == string.Empty)
                sb.Append("- รหัสยา\n"); txtCode.Focus();
            if (txtName.Text == string.Empty)
                sb.Append("- ชื่อยา\n"); txtName.Focus();

            string err = sb.ToString();
            if (err.Length > 0)
            {
                sb.Insert(0, "กรุณาระบุข้อมูลต่อไปนี้ให้เรียบร้อย\n");
                MessageBox.Show(this, sb.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return false;
            }
            else
            {
                return true;
            }
        }

        private void FrmMedicineMstr_Load(object sender, EventArgs e)
        {
            this.Open();
        }

        private void dgMedicines_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                MedicineInfo item = new MedicineInfo();

                item.Code = (string)dgItems[0, e.RowIndex].Value;
                item.Name = (string)dgItems[1, e.RowIndex].Value;
                item.Unit = (string)dgItems[2, e.RowIndex].Value;

                PopulateDataForEdit(item);
            }
        }

        private void OnKeyEnter(object sender, KeyEventArgs e)
        {
            FormUtil.Enter(e);
        }

        private void FrmMedicineMstr_KeyDown(object sender, KeyEventArgs e)
        {
            uclAction.OnActionKeyDown(sender, e);
        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (uclAction.CurrentAction == FormActionType.Search)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string searchItem = txtCode.Text;
                    if (searchItem.Length > 0)
                    {
                        Search(searchItem);
                    }
                    else
                    {
                        MessageBox.Show(this,"กรุณาระบุข้อมูลที่ต้องการค้นหา", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }else{
                OnKeyEnter(sender, e);
            }
        }

    }
}