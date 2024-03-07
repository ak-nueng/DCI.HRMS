using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DCI.HRMS.Base;
using DCI.HRMS.Common;
using DCI.HRMS.Model.Personal;
using DCI.HRMS.Model.Allowance;
using DCI.HRMS.Util;
using DCI.HRMS.Service;

namespace DCI.HRMS.Personal.Controls
{
    public partial class EmpCertificate_Control : UserControl
    {
        private readonly string[] colNameS = new string[] { "Code", "CerType", "Level", "CerDate", "Expire", "Remark", "AddBy", "AddDate", "UpdateBy", "UpdateDate" };
        private readonly string[] propNameS = new string[] { "EmpCode", "CerName", "Level", "CertDate", "ExpireDate", "Remark", "CreateBy", "CreateDateTime", "LastUpdateBy", "LastUpDateDateTime" };
        //    private readonly int[] widthS = new int[] { 50, 50, 80, 80, 80, 80, 80, 80, 80, 80, 80, 80, 120, 100, 120, 100, 120 };

        ApplicationManager appMgr = ApplicationManager.Instance();
        public SkillAllowanceService sklSvr;
        public SubContractSkillAllowanceService subSklSvr;

        private FormActionType act = new FormActionType();
        private ArrayList gvData = new ArrayList();
        private EmpCertInfo information = new  EmpCertInfo();
        private string empCode;
        public EmpCertificate_Control()
        {
            InitializeComponent();
            AddGridViewColumnsS();
        }
        public object Information
        {
            set
            {
                try
                {
                    information = (EmpCertInfo)value;
                    cmbType.SelectedValue = information.CerType;
                    lblId.Text = information.RecordId;
                    txtRemark.Text = information.Remark;
                    dtpRq.Value = information.CertDate;
                    dtpRc.Value = information.ExpireDate;
                    cmbLevel.SelectedValue = information.Level;   

                }
                catch
                {

                    ClearAll();
                }



            }
            get
            {

                try
                {
                    information.CerType = cmbType.SelectedValue.ToString();
                    information.EmpCode = empCode;
      
                    information.Remark = txtRemark.Text;
                    information.CertDate = dtpRq.Value;
                    information.ExpireDate = dtpRc.Value;
             
                    information.RecordId = lblId.Text;
                    information.Level = (int)cmbLevel.SelectedValue;
                }
                catch
                {


                }


                return information;

            }
        }
        public bool EditEnable
        {
            set
            {
                kryptonGroup5.Visible = value;
            }
            get
            {
                return kryptonGroup5.Visible;
            }
        }
     

        private void AddGridViewColumnsS()
        {
            // this.dgItems.Columns.Clear();
            dgItems.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn[] columns = new DataGridViewTextBoxColumn[colNameS.Length];
            for (int index = 0; index < columns.Length; index++)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();

                column.Name = colNameS[index];
                column.DataPropertyName = propNameS[index];
                column.ReadOnly = true;
                //  column.Width = widthS[index];

                columns[index] = column;
                dgItems.Columns.Add(columns[index]);
            }
            //dgItems.ClearSelection();
        }

        private void FillDataGrid()
        {
            dgItems.DataBindings.Clear();
            dgItems.DataSource = null;
            dgItems.DataSource = gvData;
            // dgItems.CurrentCell = null;
            //dgItems.Refresh();

            this.Update();

        }

        private void SetAction()
        {
            switch (act)
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
        public void Open()
        {

            ArrayList rela = sklSvr.GetAllType();
            cmbType.DisplayMember = "DispName";
            cmbType.ValueMember = "CerType";
            cmbType.DataSource = rela;
            cmbType.SelectedIndex = -1;
            information = new  EmpCertInfo();
            act = FormActionType.None;
            SetAction();



        }
   

        private void txtDetail_Paint(object sender, PaintEventArgs e)
        {

        }

        public void SetData(string _empCode)
        {
            this.empCode = _empCode; 
            ArrayList prbr = new ArrayList();
            if (_empCode.StartsWith("I"))
            {
                prbr = subSklSvr.GetCertificateByCode(empCode);
           
            }
            else
            {
                prbr = sklSvr.GetCertificateByCode(empCode);
            }
            gvData = prbr; 
            FillDataGrid();
        }
        public void SetData(string _empCode, bool _showExpired, DateTime _showdate)
        {
            this.empCode = _empCode;

            ArrayList prbr = new ArrayList();
            if (_empCode.StartsWith("I"))
            {
                prbr = subSklSvr.GetCertificateByCode(empCode);

            }
            else
            {
                prbr = sklSvr.GetCertificateByCode(empCode);
            }
           
            ArrayList fillData =new ArrayList();
            if (!_showExpired)
            {
                if (prbr != null)
                {
                    foreach (EmpCertInfo item in prbr)
                    {
                        if (item.ExpireDate > _showdate || item.ExpireDate == DateTime.MinValue)
                        {
                            fillData.Add(item);
                        }
                    }
                    gvData = fillData;

                }
            }
            else
            {
                gvData = prbr;
            }
            FillDataGrid();
        }

        private void PropertyBorrow_Control_Load(object sender, EventArgs e)
        {


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            dgItems.ClearSelection();
            this.ClearAll();
            dtpRq.Value = DateTime.Today;
            act = FormActionType.SaveAs;
            SetAction();
            cmbType.Focus();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (act == FormActionType.SaveAs)
            {
                if (cmbType.SelectedIndex != -1)
                {
                    {
                        try
                        {
                            EmpCertInfo item = (EmpCertInfo)this.Information;
                            item.CreateBy = appMgr.UserAccount.AccountId;
                            if (item.EmpCode.StartsWith("I"))
                            {
                                subSklSvr.SaveEmpCertificate(item); 
                            }
                            else
                            {
                                sklSvr.SaveEmpCertificate(item); 
                            }
                            act = FormActionType.None;
                            SetAction();
                            SetData(empCode);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้เนื่องจาก\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                }
                else
                {
                    MessageBox.Show("กรุณาระบุประเภทใบรับรอง", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbType.Focus();
                }





            }
            else if (act == FormActionType.Save)
            {
                if (cmbType.SelectedIndex!= -1)
                {
                    try
                    {
                        EmpCertInfo item = (EmpCertInfo)this.Information;
                        item.LastUpdateBy = appMgr.UserAccount.AccountId;
                        if (item.EmpCode.StartsWith("I"))
                        {
                            subSklSvr.UpdateEmpCertificate(item);
                        }
                        else
                        {
                            sklSvr.UpdateEmpCertificate(item);
                        }
                     
                        act = FormActionType.None;
                        SetAction();
                        SetData(empCode);
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้เนื่องจาก\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                else
                {
                    MessageBox.Show("กรุณาระบุประเภทใบรับรอง", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbType.Focus();
                }
            }

        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (act == FormActionType.SaveAs)
            {


                try
                {


                    SetData(empCode);
                    act = FormActionType.None;
                    SetAction();
                }
                catch (Exception ex)
                {


                }



            }
            else if (act == FormActionType.Save)
            {
                if (MessageBox.Show("คุณต้องการลบข้อมูลใช่หรือไม่?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        EmpCertInfo item = (EmpCertInfo)this.Information;
                        if (item.EmpCode.StartsWith("I"))
                        {
                            subSklSvr.DeleteEmpCertificate(item.RecordId);

                        }
                        else
                        {
                            sklSvr.DeleteEmpCertificate(item.RecordId);

                        }

               

                        act = FormActionType.None;
                        SetAction();
                        this.ClearAll();
                        SetData(empCode);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ไม่สามารถลบข้อมูลได้เนื่องจาก\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                    }
                }
            }
        }

        private void dgItems_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                this.Information = gvData[dgItems.SelectedRows[0].Index];
                act = FormActionType.Save;
                SetAction();
            }
            catch
            {

                ClearAll();
            }
        }
        public void ClearAll()
        {
            try
            {
                cmbType.SelectedIndex = -1;
         
                txtRemark.Clear();
                dtpRc.Value = DateTime.MinValue;
                dtpRq.Value = DateTime.MinValue;
                lblId.Text = "";
 
          
            }
            catch
            {

            }
        }

        private void dgItems_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyleDefault.ShowRowNumber(dgItems, e);

        }

        private void cmbType_KeyDown(object sender, KeyEventArgs e)
        {
            KeyPressManager.Enter(e);
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbType.SelectedIndex!=-1)
            {
                CertificateInfo item = (CertificateInfo)cmbType.SelectedItem;
                ArrayList level = sklSvr.GetCertLevel(item.CerType);
                cmbLevel.DisplayMember = "Level";
                cmbLevel.ValueMember = "Level";
                cmbLevel.DataSource = level;
                cmbLevel.SelectedValue = 1;
           
            }
            else
            {
                cmbLevel.DataSource = null ;
            }
        }
    }
}

