using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Service;
using DCI.HRMS.Model.Attendance;
using System.Collections;
using DCI.HRMS.Common;
using DCI.HRMS.Base;
using DCI.Security.Model;
using DCI.HRMS.Util;

namespace DCI.HRMS.Attendance
{
    public partial class FrmOtRate : BaseForm, IFormParent, IFormPermission
    {
        public FrmOtRate()
        {
            InitializeComponent();
        }



        private ArrayList gvData = new ArrayList();
        private ArrayList addData;
        private ArrayList searchData;
        OtService otSvr = OtService.Instance();
        private readonly string[] colName = new string[] { "RateId", "Worktype", "Sift", "OT From", "OT To", "OT1From", "OT1To", "OT 1", "OT1.5From", "OT1.5To", "OT 1.5", "OT2From", "OT2To", "OT 2", "OT3From", "OT3To", "OT 3", "AddBy", "AddDate", "UpdateBy", "UpdateDate" };
        private readonly string[] propName = new string[] { "RateId", "WorkType", "Shift", "OTFrom", "OtTo", "Rate1From", "Rate1To", "Rate1", "Rate15From", "Rate15To", "Rate15", "Rate2From", "Rate2To", "Rate2", "Rate3From", "Rate3To", "Rate3", "CreateBy", "CreateDateTime", "LastUpdateBy", "LastUpDateDateTime" };
       // private readonly int[] width = new int[] { 80, 80, 100, 80, 100, 100, 100, 100, 100, 100,100,100,100,100 };
        ApplicationManager appMgr = ApplicationManager.Instance();

        private void FrmOtRate_Load(object sender, EventArgs e)
        {
            this.Open();

            ucl_ActionControl1.Owner = this;
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
                OtRateInfo otRate = new OtRateInfo();




                if (txtRateId.Text != "")
                {
                    otRate.RateId = txtRateId.Text;


                }
                else
                {
                    MessageBox.Show("กรุณาป้อน RateId", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtRateId.Focus();
                    return null;
                }


                if (txtWtype.Text.Trim() != "")
                {
                    otRate.WorkType = txtWtype.Text.Trim();
                }
                else
                {
                    MessageBox.Show("กรุณาป้อน  WorkType", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtWtype.Focus();
                    return null;
                }



                if (txtFrom.Text.Trim() != ":")
                    otRate.OtFrom = txtFrom.Text.Trim();
                else
                    otRate.OtFrom = "";


                if (txtTo.Text.Trim() != ":")
                    otRate.OtTo = txtTo.Text.Trim();
                else

                    otRate.OtTo = "";
                if (txt1.Text.Trim() != ":")
                    otRate.Rate1 = txt1.Text.Trim();
                else
                    otRate.Rate1 = string.Empty;


                if (txt15.Text.Trim() != ":")
                    otRate.Rate15 = txt15.Text.Trim();
                else
                    otRate.Rate15 = string.Empty;

                if (txt2.Text.Trim() != ":")
                    otRate.Rate2 = txt2.Text.Trim();
                else
                    otRate.Rate2 = string.Empty;

                if (txt3.Text.Trim() != ":")
                    otRate.Rate3 = txt3.Text.Trim();
                else
                    otRate.Rate3 = string.Empty;

                if (txtRate1From.Text.Trim() != ":")
                    otRate.Rate1From = txtRate1From.Text.Trim();
                else
                    otRate.Rate1From = string.Empty;
                if (txtRate15From.Text.Trim() != ":")
                    otRate.Rate15From = txtRate15From.Text.Trim();
                else
                    otRate.Rate15From = string.Empty;

                if (txtRate2From.Text.Trim() != ":")
                    otRate.Rate2From = txtRate2From.Text.Trim();
                else
                    otRate.Rate2From = string.Empty;

                if (txtRate3From.Text.Trim() != ":")
                    otRate.Rate3From = txtRate3From.Text.Trim();
                else
                    otRate.Rate3From = string.Empty;

                if (txtRate1To.Text.Trim() != ":")
                    otRate.Rate1To = txtRate1To.Text.Trim();
                else
                    otRate.Rate1To = string.Empty;


                if (txtRate15To.Text.Trim() != ":")
                    otRate.Rate15To = txtRate15To.Text.Trim();
                else
                    otRate.Rate15To = string.Empty;

                if (txtRate2To.Text.Trim() != ":")
                    otRate.Rate2To = txtRate2To.Text.Trim();
                else
                    otRate.Rate2To = string.Empty;

                if (txtRate3To.Text.Trim() != ":")
                    otRate.Rate3To = txtRate3To.Text.Trim();
                else
                    otRate.Rate3To = string.Empty;

                try
                {
                    otRate.Shift = txtShift.Text.Trim();
                }
                catch
                {
                    otRate.Shift = "";
                }

                return otRate;
            }
            set
            {
                OtRateInfo otRate = (OtRateInfo)value;


                try
                {
                    txtRateId.Text = otRate.RateId;
                }
                catch 
                {   }


                try
                {
                    txtFrom.Text = otRate.OtFrom;
                }
                catch { }
                try
                {
                    txtTo.Text = otRate.OtTo;
                }
                catch { }
                try
                {
                    txt1.Text = otRate.Rate1;
                }
                catch
                {
                    txt1.Text = "";
                }
                try
                {
                    txt15.Text = otRate.Rate15;
                }
                catch
                {
                    txt15.Text = "";
                }
                try
                {
                    txt2.Text = otRate.Rate2;
                }
                catch
                {
                    txt2.Text = "";
                }
                try
                {
                    txt3.Text = otRate.Rate3;
                }
                catch
                {
                    txt3.Text = "";
                }

                try
                {
                    txtRate1From.Text = otRate.Rate1From;
                }
                catch
                {
                    txtRate1From.Text = "";
                }
                try
                {
                    txtRate15From.Text = otRate.Rate15From;
                }
                catch
                {
                    txtRate15From.Text = "";
                }
                try
                {
                    txtRate2From.Text = otRate.Rate2From;
                }
                catch
                {
                    txtRate2From.Text = "";
                }
                try
                {
                    txtRate3From.Text = otRate.Rate3From;
                }
                catch
                {
                    txtRate3From.Text = "";
                }
                try
                {
                    txtRate1To.Text = otRate.Rate1To;
                }
                catch
                {
                    txtRate1To.Text = "";
                }
                try
                {
                    txtRate15To.Text = otRate.Rate15To;
                }
                catch
                {
                    txtRate15To.Text = "";
                }
                try
                {
                    txtRate2To.Text = otRate.Rate2To;
                }
                catch
                {
                    txtRate2To.Text = "";
                }
                try
                {
                    txtRate3To.Text = otRate.Rate3To;
                }
                catch
                {
                    txtRate3To.Text = "";
                }


                try
                {
                    txtShift.Text = otRate.Shift;
                }
                catch
                {
                    txtShift.Text = "";
                }
                try
                {
                    txtWtype.Text = otRate.WorkType;
                }
                catch
                {
                    txtWtype.Text = "";
                }
            }
        }

        public void AddNew()
        {
            ucl_ActionControl1.CurrentAction = FormActionType.AddNew;
            txtRateId.ReadOnly = false;
            txtWtype.ReadOnly = false;
            this.Clear();
        }

        public void Save()
        {
            if (ucl_ActionControl1.CurrentAction == FormActionType.Save)
            {
                if (ucl_ActionControl1.Permission.AllowEdit)
                {
                    try
                    {
                        OtRateInfo item = (OtRateInfo)this.Information;
                        item.LastUpdateBy = appMgr.UserAccount.AccountId;
                        otSvr.UpdateOtRate(item);
                        this.Open();
                        Findrecord2(item.RateId, item.WorkType);

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

                    try
                    {
                        OtRateInfo item = (OtRateInfo)this.Information;
                        item.CreateBy = appMgr.UserAccount.AccountId;
                        otSvr.SaveOtRate(item);
                        this.Search();
                        Findrecord2(item.RateId, item.WorkType);
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
        }

        public void Delete()
        {
            try
            {
                if (MessageBox.Show("ต้องการลบข้อมูลใช่หรือไม่?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    OtRateInfo item = (OtRateInfo)this.Information;
                    otSvr.DeleteOtRate(item);
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

            gvData = otSvr.GetOtRates();
            FillDataGrid();
        }

        public void Export()
        {
            throw new NotImplementedException();
        }

        public void Print()
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            AddGridViewColumns();
            gvData = otSvr.GetOtRates();
            FillDataGrid();
        }

        public void Clear()
        {
            txtRateId.Focus();
            txtRateId.Text = "";
            txtFrom.Text = "";
            txtTo.Text = "";
            txt1.Text = "";
            txt15.Text = "";
            txt2.Text = "";
            txt3.Text = "";
            txtShift.Text = "";
            txtWtype.Text = "";
            txtRate1From.Text = "";
            txtRate1To.Text = "";
            txtRate15From.Text = "";
            txtRate15To.Text = "";
            txtRate2From.Text = "";
            txtRate2To.Text = "";
            txtRate3From.Text = "";
            txtRate3To.Text = "";



        }

        public void RefreshData()
        {
            string tmptype = txtRateId.Text;
            string tmpCode = txtWtype.Text;
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
        private void FillDataGrid()
        {
            dgItems.DataBindings.Clear();
            dgItems.DataSource = null;

            dgItems.DataSource = gvData;
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
             //   column.Width = width[index];

                columns[index] = column;
                dgItems.Columns.Add(columns[index]);
            }
            dgItems.ClearSelection();
        }

        private void dgItems_SelectionChanged(object sender, EventArgs e)
        {
            if (dgItems.SelectedRows.Count != 0)
            {
                OtRateInfo item = (OtRateInfo)gvData[dgItems.SelectedRows[0].Index];
                this.Information = item;
                txtRateId.ReadOnly = true;
                txtWtype.ReadOnly = true;
                ucl_ActionControl1.CurrentAction = FormActionType.Save;

            }
            else
            {
                ucl_ActionControl1.CurrentAction = FormActionType.None;
            }
        }

        private void txtFrom_Leave(object sender, EventArgs e)
        {
            KeyPressManager.ConvertTextTime(sender);
        }

        private void txtFrom_KeyDown(object sender, KeyEventArgs e)
        {
            KeyPressManager.Enter(e);
        }

        private void txtFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressManager.EnterNumericOnly(e);
        }

        private void txt3_KeyDown(object sender, KeyEventArgs e)
        {

        }

    }
}
