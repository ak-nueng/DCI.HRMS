using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Base;
//using DCIBizPro.DTO.SM;
using DCI.HRD.Model;
using DCI.HRMS.Util;
using System.Collections;
using System.Diagnostics;
using DCI.HRD.Service;
using DCI.Security.Model;

namespace DCI.HRMS.PSN
{
    public partial class FrmDoctorMstr : Form, IFormParent, IFormPermission
    {
        private readonly FirstAidService firstAidService = FirstAidService.Instance();
        private readonly string[] colName = new string[] { "Code", "Title", "Name", "Surname", "Phone" };
        private readonly string[] propName = new string[] { "Code", "NameInThai", 
                                            "NameInThai" , "NameInThai",
                                            "Phone"};

        private readonly int[] width = new int[] { 90, 90, 150, 150, 100 };

        public FrmDoctorMstr()
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
                PersonInfo item = new PersonInfo();
                
                item.NameInThai = new NameInfo();
                item.Code = txtCode.Text;
                item.NameInThai.Title = txtTitle.Text;
                item.NameInThai.Name = txtFName.Text;
                item.NameInThai.Surname = txtSName.Text;
                item.Phone = txtPhone.Text;

                return item;
            }
            set
            {
                PersonInfo person = (PersonInfo)value;

                txtCode.Text = person.Code;
                txtTitle.Text = person.NameInThai.Title;
                txtFName.Text = person.NameInThai.Name;
                txtSName.Text = person.NameInThai.Surname;
                txtPhone.Text = person.Phone;
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
                    PersonInfo item = (PersonInfo)Information;

                    if (uclAction.CurrentAction == FormActionType.SaveAs)
                    {
                        firstAidService.AddDoctor(item, "SYSTEM");
                        msg = "เพิ่มข้อมูลหมอ/พยาบาล เรียบร้อย";
                    }
                    else if (uclAction.CurrentAction == FormActionType.Save)
                    {
                        firstAidService.UpdateDoctor(item, "SYSTEM");
                        msg = "บันทึกข้อมูลหมอ/พยาบาล เรียบร้อย";
                    }

                    Text = item.Code + " - " + item.NameInThai.ToString();
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
            string msg = string.Format("คุณต้องการลบข้อมูล  : {0} ใช่หรือไม่?", Text);
            DialogResult result = MessageBox.Show(this, msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    firstAidService.DeleteDoctor(txtCode.Text);
                    Clear();

                    this.Text = string.Empty;
                    uclAction.CurrentAction = FormActionType.AddNew;

                    MessageBox.Show(this, "ลบข้อมูลหมอ/พยาบาล เรียบร้อยแล้ว", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PopulateList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            this.Cursor = Cursors.Default;
        }

        public void Search()
        {
            Clear();
            txtCode.ReadOnly = false;
            txtCode.Focus();
        }

        public void Export()
        {
            
        }

        public void Print()
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
            txtTitle.Text = string.Empty;
            txtSName.Text = string.Empty;
            txtFName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            
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

        public void Exit()
        {
            this.Close();
        }

        #endregion

        #region IFormPermission Members

        public PermissionInfo Permission
        {
            set { this.uclAction.Permission = value; }
        }

        #endregion

        private void Search(string item)
        {
            try
            {
                //ArrayList medicines = firstAidService.FindAllMedicine();
                ArrayList doctors = new ArrayList();

                dgItems.AutoGenerateColumns = false;
                dgItems.DataSource = doctors;

                this.AddGridViewColumns();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void AddGridViewColumns()
        {
            this.dgItems.Columns.Clear();

            DataGridViewTextBoxColumn[] columns = new DataGridViewTextBoxColumn[5];

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
        }

        public void PopulateList()
        {
            try
            {
                ArrayList doctors = firstAidService.FindAllDoctor();

                this.dgItems.AutoGenerateColumns = false;
                this.dgItems.DataSource = doctors;

                this.AddGridViewColumns();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void PopulateDataForEdit(PersonInfo item)
        {
            this.txtCode.ReadOnly = true;
            this.Information = item;
            this.uclAction.CurrentAction = FormActionType.Save;
        }
        private bool ValidateInput()
        {
            StringBuilder sb = new StringBuilder();
            if (txtCode.Text == string.Empty)
                sb.Append("- รหัส หมอ/พยาบาล\n"); txtCode.Focus();
            if (txtTitle.Text == string.Empty || txtFName.Text == string.Empty
                || txtSName.Text == string.Empty)
                sb.Append("- ชื่อ หมอ/พยาบาล\n"); txtTitle.Focus();

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

        private void FrmDoctorMstr_Load(object sender, EventArgs e)
        {
            this.Open();
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
                        MessageBox.Show(this, "กรุณาระบุข้อมูลที่ต้องการค้นหา", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                OnKeyEnter(sender, e);
            }
        }

        private void FrmDoctorMstr_KeyDown(object sender, KeyEventArgs e)
        {
            uclAction.OnActionKeyDown(sender, e);
        }

        private void OnKeyEnter(object sender, KeyEventArgs e)
        {
            FormUtil.Enter(e);
        }

        private void dgDoctors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                PersonInfo person = new PersonInfo();
                person.NameInThai = (NameInfo)dgItems[1, e.RowIndex].Value;

                person.Code = (string)dgItems[0, e.RowIndex].Value;
                person.Phone = (string)dgItems[4, e.RowIndex].Value;

                PopulateDataForEdit(person);
            }
        }

        private void dgDoctors_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (IsNameColumn(e.ColumnIndex))
            {
                PersonInfo person = (PersonInfo)dgItems.Rows[e.RowIndex].DataBoundItem;

                if(e.ColumnIndex == dgItems.Columns["Title"].Index)
                    e.Value = person.NameInThai.Title;
                if (e.ColumnIndex == dgItems.Columns["Name"].Index)
                    e.Value = person.NameInThai.Name;
                if (e.ColumnIndex == dgItems.Columns["Surname"].Index)
                    e.Value = person.NameInThai.Surname;

                e.FormattingApplied = true;
            }
        }

        private bool IsNameColumn(int columnIndex)
        {
            if (columnIndex == dgItems.Columns["Title"].Index 
                || columnIndex == dgItems.Columns["Name"].Index
                || columnIndex == dgItems.Columns["Surname"].Index)
                return true;

            return false;
        }
    }
}