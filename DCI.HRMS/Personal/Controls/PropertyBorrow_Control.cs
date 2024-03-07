using System;
using System.Collections;
using System.Windows.Forms;
using DCI.HRMS.Base;
using DCI.HRMS.Common;
using DCI.HRMS.Model;
using DCI.HRMS.Model.Personal;
using DCI.HRMS.Service;
using DCI.HRMS.Util;
namespace DCI.HRMS.Personal.Controls
{

    public partial class PropertyBorrow_Control : UserControl
    {
        ApplicationManager appMgr = ApplicationManager.Instance();
        public PropertyBorrowService prtSvr;
        private FormActionType act = new FormActionType();
        private ArrayList gvData = new ArrayList();
        private PropertyBorrowInfo information = new PropertyBorrowInfo();
        private string empCode;
        public PropertyBorrow_Control()
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
            dgItems.ClearSelection();

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

            ArrayList rela = prtSvr.GetAllPropertiesType();
            cmbType.DisplayMember = "PropertyName";
            cmbType.ValueMember = "PropertyId";
            cmbType.DataSource = rela;
            cmbType.SelectedIndex = -1;
            information = new PropertyBorrowInfo();
            act = FormActionType.None;
            SetAction();



        }
        public object Information
        {

            set
            {
                try
                {
                    information = (PropertyBorrowInfo)value;
                    empCode = information.EmpCode;
                    cmbType.SelectedValue = information.Type;
                    txtData.Text = information.Data;
                    txtDetail.Text = information.Detail;
                    txtQty.Text = information.Quantity.ToString();
                    txtRemark.Text = information.Remark;
                    dtpRq.Value = information.RequestDate;
                    dtpRc.Value = information.RecieveDate;
                    dtpRt.Value = information.ReturnDate;
                    lblId.Text = information.BorrowId;
                    switch (information.ReturnStatus)
                    {
                        case ReturnSts.คืนแล้ว:
                            cmbRtSts.SelectedIndex = 1;
                            break;
                        case ReturnSts.ยังไม่คืน:
                            cmbRtSts.SelectedIndex = 0;
                            break;
                        case ReturnSts.ไม่ต้องคืน:
                            cmbRtSts.SelectedIndex = 2;
                            break;
                        default:
                            break;
                    }


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
                    information.EmpCode = empCode;
                    information.Type = cmbType.SelectedValue.ToString();
                    information.Data = txtData.Text;
                    information.Detail = txtDetail.Text;
                    information.Quantity = int.Parse(txtQty.Text);
                    information.Remark = txtRemark.Text;
                    information.RequestDate = dtpRq.Value;
                    information.RecieveDate = dtpRc.Value;
                    information.ReturnDate = dtpRt.Value;
                    information.BorrowId = lblId.Text;
                    information.ReturnStatus = (ReturnSts)cmbRtSts.SelectedIndex;
                }
                catch
                {


                }


                return information;

            }
        }

        private void txtDetail_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PropertyInfo item = (PropertyInfo)cmbType.SelectedItem;
                lblUnit.Text = item.Unit;
                lblDetail.Values.ExtraText = item.Detail;
                lbldata.Values.ExtraText = item.Data;
            }
            catch
            {


            }
        }
        public void SetData(string _empCode)
        {
            this.empCode = _empCode;
            ArrayList prbr = prtSvr.GetDataByCode(empCode);
            grbAct.Enabled = true;
            gvData = prbr;
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
                if (txtQty.Text != "")
                {
                    {
                        try
                        {
                            PropertyBorrowInfo item = (PropertyBorrowInfo)this.Information;
                            item.CreateBy = appMgr.UserAccount.AccountId;
                            prtSvr.SaveData(item);
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
                    MessageBox.Show("กรุณาระบุจำนวน", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtQty.Focus();
                }





            }
            else if (act == FormActionType.Save)
            {
                if (txtQty.Text != "")
                {
                    try
                    {
                        PropertyBorrowInfo item = (PropertyBorrowInfo)this.Information;
                        item.LastUpdateBy = appMgr.UserAccount.AccountId;
                        prtSvr.UpdateData(item);
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
                    MessageBox.Show("กรุณาระบุจำนวน", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtQty.Focus();
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
                        PropertyBorrowInfo item = (PropertyBorrowInfo)this.Information;
                        prtSvr.Deletedata(item.BorrowId);

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
                txtData.Clear();
                txtDetail.Clear();
                txtQty.Text = "1";
                txtRemark.Clear();
                dtpRc.Value = DateTime.MinValue;
                dtpRq.Value = DateTime.MinValue;
                dtpRt.Value = DateTime.MinValue;
                cmbRtSts.SelectedIndex = 0;
            }
            catch
            {

            }
        }

        private void dgItems_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyleDefault.ShowRowNumber(dgItems, e);
            try
            {
                PropertyBorrowInfo item = (PropertyBorrowInfo)dgItems.Rows[e.RowIndex].DataBoundItem;
                if (item.ReturnStatus == ReturnSts.ยังไม่คืน)
                {
                    dgItems.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch
            {
            }

        }

        private void cmbType_KeyDown(object sender, KeyEventArgs e)
        {
            KeyPressManager.Enter(e);
        }
    }
}
