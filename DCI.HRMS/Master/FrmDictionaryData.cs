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
using System.Collections;
using DCI.Security.Model;
using DCI.HRMS.Service;
using DCI.HRMS.Model;
using DCI.HRMS.Util;

namespace DCI.HRMS.Master
{


    public partial class FrmDictionaryData : BaseForm, IFormParent, IFormPermission
    {



        private ArrayList gvData = new ArrayList();
        private ArrayList addData;
        private ArrayList searchData;
        private FormAction formAct = FormAction.New;
        private readonly string[] colName = new string[] { "Type", "Code", "Description1", "Description2", "DetailEn", "DetailTh" };
        private readonly string[] propName = new string[] { "Type", "Code", "Description", "DescriptionTh", "DetailEn", "DetailTh" };
        private readonly int[] width = new int[] { 80, 80, 80, 80, 80, 150, 100, 100, 100, 100 };

        private DictionaryService dicSvr = DictionaryService.Instance();

        public FrmDictionaryData()
        {
            InitializeComponent();
        }

        private void FrmDictionaryData_Load(object sender, EventArgs e)
        {
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
                BasicInfo item = new BasicInfo();
                item.Type = txtType.Text;
                item.Code = txtCode.Text;
                item.Description = txtEnDescr.Text;
                item.DescriptionTh = txtThDescr.Text;
                item.DetailEn = txtEnDetail.Text;
                item.DetailTh = txtThDetail.Text;


                return item;
            }
            set
            {
                BasicInfo item = (BasicInfo)value;
                txtType.Text = item.Type;
                txtCode.Text = item.Code;
                txtEnDescr.Text = item.Description;
                txtThDescr.Text = item.DescriptionTh;
                txtEnDetail.Text = item.DetailEn;
                txtThDetail.Text = item.DetailTh;

            }
        }

        public void AddNew()
        {
            ucl_ActionControl1.CurrentAction = FormActionType.AddNew;
            this.Clear();
            txtCode.ReadOnly = false;
            txtType.ReadOnly = false;
            txtType.Focus();
        }

        public void Save()
        {
            if (ucl_ActionControl1.CurrentAction == FormActionType.Save)
            {
                if (ucl_ActionControl1.Permission.AllowEdit)
                {
                    try
                    {
                        BasicInfo item = (BasicInfo)this.Information;

                        dicSvr.UpdateData(item);
                        this.Search();
                        Findrecord2(item.Type, item.Code);


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
                    if (txtType.Text != "")
                    {
                        if (txtCode.Text != "")
                        {
                            try
                            {
                                BasicInfo item = (BasicInfo)this.Information;
                                dicSvr.InsertData(item);
                                this.Search();
                                Findrecord2(item.Type, item.Code);
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
                        /*Type not enter    */
                        MessageBox.Show("กรุณาป้อน Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        txtType.Focus();
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
                    BasicInfo item = (BasicInfo)this.Information;
                    dicSvr.DeleteData(item.Type, item.Code);
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

            gvData = dicSvr.SelectAll( comboBox1.SelectedValue.ToString());
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
            ArrayList allType = dicSvr.GetAllType();
         
            BasicInfo allSel = new BasicInfo("%", "%", "");
            allSel.Type = "%";
            allType.Insert(0, allSel);
            comboBox1.DataSource = allType;
            this.Search();
   

        }

        public void Clear()
        {
            txtType.Clear();
            txtCode.Clear();
            txtEnDescr.Clear();
            txtThDescr.Clear();
            txtEnDetail.Clear();
            txtThDetail.Clear();

        }

        public void RefreshData()
        {
            string tmptype = txtType.Text;
            string tmpCode = txtCode.Text;
            this.Search();
            Findrecord2(tmptype, tmpCode);
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
                BasicInfo item = (BasicInfo)gvData[dgItems.SelectedRows[0].Index];
                this.Information = item;
                ucl_ActionControl1.CurrentAction = FormActionType.Save;
                txtType.ReadOnly = true;
                txtCode.ReadOnly = true;
            }
            else
            {
                ucl_ActionControl1.CurrentAction = FormActionType.None;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           // Findrecord(comboBox1.SelectedValue.ToString());
            this.Search();
        }
        private void Findrecord(string type)
        {
            foreach (DataGridViewRow item in dgItems.Rows)
            {
                if (type== item.Cells[0].Value.ToString())
                {
                    dgItems.CurrentCell = item.Cells[0];
                    return;
                }
            }
        }
        private void Findrecord2(string type,string code)
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

        private void txtType_KeyDown(object sender, KeyEventArgs e)
        {
            KeyPressManager.Enter(e);
        }

        private void txtThDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode== Keys.Enter)
            {
                this.Save();    
            }
        }

    }
}
