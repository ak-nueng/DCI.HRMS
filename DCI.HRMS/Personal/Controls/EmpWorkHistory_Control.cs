using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Model.Personal;
using DCI.HRMS.Service;
using System.Collections;
using DCI.HRMS.Base;

namespace DCI.HRMS.Personal.Controls
{
    public partial class EmpWorkHistory_Control : UserControl
    {
        private ArrayList wkHistList = new ArrayList();
        private ArrayList gvData = new ArrayList();
        private WorkHistoryInfo information = new WorkHistoryInfo();
        public EmployeeService empSvr;
        private string empCode;


        private FormActionType act = new FormActionType();

        public EmpWorkHistory_Control()
        {
            InitializeComponent();
        }
        public string EmpCode
        {
            get { return empCode; }
            set { empCode = value; }
        }
        public bool ReadyOnly
        {
            set { grbAct.Visible = !value; }
            get { return !grbAct.Visible; }
        }
        public object Information
        {
            set
            {
                try
                {

                    wkHistList = (ArrayList)value;
                    gvData = wkHistList;
                    act = FormActionType.None; ;
                    SetAction();
                    FillDataGrid();
                }
                catch 
                {
              
                }
           

            }
            get
            {
                try
                {
                    information = new WorkHistoryInfo();
                    information.EmpCode = empCode;
                    information.CompanyName = txtName.Text;
                    information.CompanyNameInThai = txtTname.Text;
                    information.Address = txtAddress.Text;
                    information.AddressInThai = txtTAddress.Text;
                    information.ResignReason = txtReason.Text;
                    information.WorkFrom = dtpJoin.Value;
                    information.WorkTo = dtpResign.Value;
                    return information;
                }
                catch
                {

                    return null;
                }
            }

        }

        private void SetAction()
        {

            switch (act)
            {
                case FormActionType.None:
                    btnAdd.Enabled = true;
                    btnCancel.Enabled = false;
                    btnSave.Enabled = false;
                    dtpJoin.Enabled = false;
                    dtpResign.Enabled = false; ;
                    btnCancel.Text = "Delete";
                    break;
                case FormActionType.AddNew:
                    btnAdd.Enabled = false; ;
                    btnCancel.Enabled = true;
                    btnSave.Enabled = true;
                    dtpJoin.Enabled = true;
                    dtpResign.Enabled = true; ;
                    btnCancel.Text = "Cancel";
                    break;
                case FormActionType.Save:
                    btnAdd.Enabled = true;
                    btnCancel.Enabled = true;
                    btnSave.Enabled = true;
                    dtpJoin.Enabled = false;
                    dtpResign.Enabled = false; ;
                    btnCancel.Text = "Delete";
                    break;
                case FormActionType.SaveAs:
                    btnAdd.Enabled = false;
                    btnCancel.Enabled = true;
                    btnSave.Enabled = true;
                    dtpJoin.Enabled = true;
                    dtpResign.Enabled = true; ;
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

        }
        public void Clear()
        {
            txtName.Text = "";
            txtTname.Text = "";
            txtAddress.Text = "";
            txtTAddress.Text = "";
            txtReason.Text = "";
            dtpJoin.Value = new DateTime(1900, 1, 1);
            dtpResign.Value = new DateTime(1900, 1, 1);
        }
        private void FillDataGrid()
        {
            dgItems.DataBindings.Clear();
            dgItems.DataSource = null;
            dgItems.DataSource = gvData;
            // dgItems.CurrentCell = null;
            //dgItems.Refresh();

            this.Update();
            dgItems.ClearSelection();

        }

        public void SetWorkHostoryData(string _empCode)
        {
            empCode = _empCode;
            wkHistList = empSvr.GetEmployeeWorkHistory(_empCode);
            gvData = wkHistList;
            act = FormActionType.None;
            SetAction();
            FillDataGrid();


        }
        public ArrayList GetWorkHostoryData()
        {
            return wkHistList;
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            dgItems.ClearSelection();
            this.Clear();
            act = FormActionType.SaveAs;
            SetAction();
            txtName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (act == FormActionType.SaveAs)
            {
                if (dtpJoin.Value != new DateTime())
                {
                    if (dtpResign.Value!= new DateTime())
                    {
                        try
                        {
                            empSvr.SaveEmployeeWorkHistory((WorkHistoryInfo)this.Information);
                            act = FormActionType.None;
                            SetAction();
                            SetWorkHostoryData(empCode);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้เนื่องจาก\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                    else
                    {
                        MessageBox.Show("กรุณาเลือกวันลาออก", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dtpResign.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("กรุณาระบุวันเริ่มงาน", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpJoin.Focus();
                }


            }
            else if (act == FormActionType.Save)
            {
                try
                {
                    empSvr.UpdateEmployeeWorkHistory((WorkHistoryInfo)this.Information);

                    SetWorkHostoryData(empCode);
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


                    SetWorkHostoryData(empCode);
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
                        empSvr.DeleteEmployeeWorkHistory((WorkHistoryInfo)this.Information);
                        act = FormActionType.None;
                        SetAction();
                        this.Clear();
                        SetWorkHostoryData(empCode);
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
                if (dgItems.SelectedRows[0].Index >= 0)
                {
                    information = (WorkHistoryInfo)wkHistList[dgItems.SelectedRows[0].Index];
                    empCode = information.EmpCode;
                    txtName.Text = information.CompanyName;
                    txtTname.Text = information.CompanyNameInThai;
                    txtAddress.Text = information.Address;
                    txtTAddress.Text = information.AddressInThai;
                    txtReason.Text = information.ResignReason;
                    dtpJoin.Value = information.WorkFrom;
                    dtpResign.Value = information.WorkTo;


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
           
                act = FormActionType.None;
                SetAction();
                this.Clear();
                information = null;

            }
        }
    }
}
