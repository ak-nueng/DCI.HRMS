using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Base;
using DCI.HRMS.Model;
using DCI.HRMS.Model.Personal;
using DCI.HRMS.Service;
using DCI.HRMS.Util;
using DCI.HRMS.Common;

namespace DCI.HRMS.Personal.Controls
{
    public partial class EmpFamily_Control : UserControl
    {
        private ArrayList FamilyList = new ArrayList();
        private ArrayList gvData = new ArrayList();
        public EmployeeService empSvr;
        private FamilyInfo information;
        private bool readyOnly = false;
        public delegate void Famili_SelectHandler();
        private FormActionType act = new FormActionType();
        private static string empCode;
        private ApplicationManager appMgr = ApplicationManager.Instance();


        private readonly string[] colName = new string[] { "EmpCode", "Relation", "RelationType", "NameInThai", "Birth", "IdNo", "TaxDed", "CreateBy", "CreateDateTime", "LastUpdateBy","LastUpdateTime" };
        private readonly string[] propName = new string[] { "EmpCode", "Relation", "RelationType", "NameInThai", "Birth", "IdNo", "TaxDed", "CreateBy", "CreateDateTime", "LastUpdateBy", "LastUpdateTime" };
        private readonly int[] width = new int[] { 80, 80, 100, 80, 100, 100, 100, 100, 100, 100,100,100 };


        [Category("Action")]
        [Description("Fires when the MonthComboBox change.")]
        public event Famili_SelectHandler Family_Slect;
        protected virtual void On_Family_Slect()
        {
            if (Family_Slect != null)
            {
                Family_Slect();

            }

        }

        public EmpFamily_Control()
        {
            InitializeComponent();
        }
        private void FillDataGrid()

        {
            dgItems.DataBindings.Clear();
            dgItems.DataSource = null;
            dgItems.DataSource = gvData;
            // dgItems.CurrentCell = null;
            //dgItems.Refresh();

            this.Update();
          //  dgItems.ClearSelection();

        }
        public bool DisableSelect
        {
            get { return readyOnly; }
            set
            {
                readyOnly = value;
                dgItems.Enabled = !readyOnly;
            }
        }
        public bool ReadyOnly
        {
            set { grbAct.Visible = !value; }
            get { return !grbAct.Visible; }
        }
        public string EmpCode
        {
            set { empCode = value; }
        }
        private void SetAction()
        {
            switch (act)
            {
                case FormActionType.None:
                    btnAdd.Enabled = true;
                    btnCancel.Enabled = false;
                    btnSave.Enabled = false;
                    txtIdNo.Enabled = false;
                    btnCancel.Text = "Delete";
                    break;
                case FormActionType.AddNew:
                    btnAdd.Enabled = false; ;
                    btnCancel.Enabled = true;
                    btnSave.Enabled = true;
                    txtIdNo.Enabled = true; ;
                    btnCancel.Text = "Cancel";
                    break;
                case FormActionType.Save:
                    btnAdd.Enabled = true;
                    btnCancel.Enabled = true;
                    btnSave.Enabled = true;
                    txtIdNo.Enabled = false;
                    btnCancel.Text = "Delete";
                    break;
                case FormActionType.SaveAs:
                    btnAdd.Enabled = false;
                    btnCancel.Enabled = true;
                    btnSave.Enabled = true;
                    txtIdNo.Enabled = true;
                    btnCancel.Text = "Cancel";
                
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
        public object Information
        {
            get
            {
                if (dgItems.Rows.Count != 0 || act == FormActionType.SaveAs)
                {
                    try
                    {
                        information = new FamilyInfo();
                        information.EmpCode = empCode;
                        information.IdNo = txtIdNo.Text;
                        information.NameInThai.Name = txtName.Text;
                        information.NameInThai.Title = txtPren.Text;
                        information.NameInThai.Surname = txtSurn.Text;
                        information.Birth = dtpBirth.Value.Date;

                        try
                        {
                            information.Relation = comboBox1.SelectedValue.ToString();
                        }
                        catch
                        {

                        }
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
                
               
                }
                else
                {
                    return null;
                }
            }
            set
            {
                FamilyList = (ArrayList)value;
                gvData = FamilyList;
                act = FormActionType.None; ;
                SetAction();
                FillDataGrid();
            }
        }
        public void ClearAll()
        {
            txtIdNo.Clear();
            txtName.Clear();
            txtPren.Clear();
            txtSurn.Clear();
            dtpBirth.Value = new DateTime(1900, 1, 1);
            comboBox1.SelectedIndex = -1;
            txtTax.Clear();
         //   empCode = "";

        }
        public void Open()
        {
            ArrayList rela = empSvr.GetFamilyRelation();
            comboBox1.DisplayMember = "DetailTh";
            comboBox1.ValueMember = "Description";
            comboBox1.DataSource = rela;
            comboBox1.SelectedIndex = -1;
            information = new FamilyInfo();
            act = FormActionType.None;
            SetAction();
            AddGridViewColumns();


        }


        public void SetFamilyData(string empCode)
        {
            FamilyList = empSvr.GetEmployeeFamily(empCode);
            gvData = FamilyList;

            act = FormActionType.None;
            SetAction();
            FillDataGrid();


        }
        public ArrayList GetFamilyData()
        {
            return FamilyList;


        }
        public void SelectRelation(string relationCode)
        {
            foreach (DataGridViewRow item in dgItems.Rows)
            {
                if (item.Cells["Relation"].Value.ToString() == relationCode)
                {
                    dgItems.CurrentCell = item.Cells[0];
                    return;
                }
            }

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {/*
            TimeSpan ts = DateTime.Today - dateTimePicker1.Value;
            int year = 0;
            int month = 0;
            year = ts.Days / 365;
            month = (ts.Days % 365) / 30;
            txtAge.Text = year.ToString() + " ปี " + month.ToString() + " เดือน";*/
        }

        private void EmpFamily_Control_Load(object sender, EventArgs e)
        {

        }

        private void dgItems_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgItems.SelectedRows[0].Index >= 0)
                {
                    information = (FamilyInfo)FamilyList[dgItems.SelectedRows[0].Index];
                    txtIdNo.Text = information.IdNo;
                    txtName.Text = information.NameInThai.Name;
                    txtPren.Text = information.NameInThai.Title;
                    txtSurn.Text = information.NameInThai.Surname;
                    txtTax.Text = information.TaxDed.ToString();
                    try
                    {
                        // dateTimePicker1.Value = new DateTime(1900,1,1);
                        dtpBirth.Value = information.Birth.Date;
                        // age_Control1.Value = information.Birth.Date;
                    }
                    catch
                    { }
                    comboBox1.SelectedValue = information.Relation;
                    act = FormActionType.Save;
                    SetAction();
                }
                else
                {
                    act = FormActionType.None;
                    SetAction();
                }
            }
            catch
            {
                txtIdNo.Text = "";
                txtPren.Text = "";
                txtName.Text = "";
                txtSurn.Text = "";
                txtTax.Text = "";
                dtpBirth.Value = new DateTime(1900,1,1);
                age_Control1.Value = new DateTime(1900, 1, 1);
                comboBox1.SelectedIndex = -1;
                information = null;
                act = FormActionType.None;
                SetAction();
            }
        }

        private void dgItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            On_Family_Slect();

        }

        private void dateTimePicker1_DateChange()
        {
            age_Control1.Value = dtpBirth.Value;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
        
            dgItems.ClearSelection(); 
            this.ClearAll();
            act = FormActionType.SaveAs;
            SetAction();
            txtIdNo.Focus();
      



        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (act == FormActionType.SaveAs)
            {
                if (txtIdNo.Text != "" )
                {
                    if (comboBox1.SelectedIndex != -1)
                    {
                        FamilyInfo item = (FamilyInfo)this.Information;
                        item.CreateBy = appMgr.UserAccount.AccountId;
                        try
                        {
                            empSvr.SaveEmployeeFamily(item);
                            act = FormActionType.None;
                            SetAction();
                            SetFamilyData(empCode);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้เนื่องจาก\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                    else
                    {
                        MessageBox.Show("กรุณาเลือกประเภทความสัมพันธ์", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        comboBox1.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("กรุณาระบุรหัสบัตรประชาชน", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtIdNo.Focus();
                }


            }
            else if (act == FormActionType.Save)
            {
                FamilyInfo item = (FamilyInfo)this.Information;
                item.LastUpdateBy = appMgr.UserAccount.AccountId;
                try
                {
                    empSvr.UpdateEmployeeFamily(item);

                    SetFamilyData(empCode);  
                    act = FormActionType.None;
                    SetAction();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้เนื่องจาก\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (act == FormActionType.SaveAs)
            {


                try
                {

                  
                    SetFamilyData(empCode); 
                    act = FormActionType.None;
                    SetAction();
                }
                catch (Exception ex)
                {


                }



            }
            else if (act == FormActionType.Save)
            {
                if ( MessageBox.Show("คุณต้องการลบข้อมูลใช่หรือไม่?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)== DialogResult.Yes)
                {   
                    try
                    {
                        empSvr.DeleteEmployeeFamily((FamilyInfo)this.Information);
                        act = FormActionType.None;
                        SetAction();
                        this.ClearAll();
                        SetFamilyData(empCode);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ไม่สามารถลบข้อมูลได้เนื่องจาก\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                    } 
                }
            }
        }

        private void txtTax_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressManager.EnterNumericOnly(e);
        }

        private void txtIdNo_KeyDown(object sender, KeyEventArgs e)
        {
            KeyPressManager.Enter(e);
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

        private void dtpBirth_ValueChanged(object sender, EventArgs e)
        {
            age_Control1.Value = dtpBirth.Value;
        }
    }
}
