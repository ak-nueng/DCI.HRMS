using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Model.Attendance;
using DCI.HRMS.Util;
namespace DCI.HRMS.Attendance.Controls
{
    public partial class OTRequest_Control : UserControl
    {
        private OtRequestInfo reqnfo = new OtRequestInfo();
        private bool canEdit;
        public OTRequest_Control()
        {
            InitializeComponent();

            DataTable dtOTType = new DataTable();
            dtOTType.Columns.Add("dataValue", typeof(string));
            dtOTType.Columns.Add("dataDisplay", typeof(string));
            dtOTType.Rows.Add("00:OT Normal", "00:OT Normal");
            dtOTType.Rows.Add("11:Retroactively", "11:Retroactively");
            dtOTType.Rows.Add("22:Change Work Day", "22:Change Work Day");
            dtOTType.Rows.Add("33:OT On Leave Day", "33:OT On Leave Day");
            dtOTType.Rows.Add("44:Manual OT", "44:Manual OT");
            dtOTType.Rows.Add("55:Business Trip", "55:Business Trip");
            dtOTType.Rows.Add("66:Abnormal Time", "66:Abnormal Time");
            dtOTType.Rows.Add("77:Over Normal Time", "77:Over Normal Time");
            dtOTType.Rows.Add("99:System Error", "99:System Error");

            cmbOTType.DataSource = dtOTType;
            cmbOTType.ValueMember = "dataValue";
            cmbOTType.DisplayMember = "dataDisplay";



            DataTable dtJobType = new DataTable();
            dtJobType.Columns.Add("dataValue", typeof(string));
            dtJobType.Columns.Add("dataDisplay", typeof(string));
            dtJobType.Rows.Add("A", "A : งานเอกสาร");
            dtJobType.Rows.Add("B", "B : กิจกรรมตามแผน");
            dtJobType.Rows.Add("C", "C : กิจกรรมที่ไม่ได้ตามแผน");
            dtJobType.Rows.Add("D", "D : Rework , Sorting");
            dtJobType.Rows.Add("E", "E : Support Production");
            dtJobType.Rows.Add("F", "F : Kaizen");
            dtJobType.Rows.Add("G", "G : ซ่อม,สร้าง");
            dtJobType.Rows.Add("H", "H : วิศวกรรม, Engineering");
            dtJobType.Rows.Add("I", "I : ");
            dtJobType.Rows.Add("J", "J : ");
            dtJobType.Rows.Add("K", "K : ");
            dtJobType.Rows.Add("L", "L : ");
            dtJobType.Rows.Add("M", "M : ");
            cmbJobType.DataSource = dtJobType;
            cmbJobType.ValueMember = "dataValue";
            cmbJobType.DisplayMember = "dataDisplay";

        }
        public delegate void rqMoveHandler( KeyEventArgs e);
        public delegate void rqSaveHandler();


        [Category("Action")]
        [Description("Fires when the arrowkey pressed")]
        public event rqMoveHandler Move_Data ;
        protected virtual void OnMove_Data( KeyEventArgs e  )
        {
            if (Move_Data != null)
            {
                Move_Data( e );

            }

        }
        [Category("Action")]
        [Description("Fires when the YearComboBox change.")]
        public event rqSaveHandler Save_Data;
        protected virtual void OnSave_Data()
        {
            if (Save_Data != null)
            {
                Save_Data();

            }

        }  
     
        public object Information
        {
            get
            {

                //reqnfo = new OtRequestInfo();
                reqnfo.DocId = lblDocId.Text;
                reqnfo.EmpCode = txtEmpCode.Text;
                reqnfo.OtDate = dtpDate.Value.Date;
                reqnfo.OtFrom = txtFrom.Text;
                reqnfo.OtTo = txtTo.Text;
                reqnfo.ReqId = txtReqId.Text;
                reqnfo.EmpType = txtWtype.Text;
                try
                {
                    //reqnfo.JobType = cmbJobType.SelectedItem.ToString();
                    reqnfo.JobType = cmbJobType.SelectedValue.ToString();
                }
                catch
                { }

                if (txt1.Text.Trim() != ":")
                    reqnfo.Rate1 = txt1.Text.Trim();
                else
                    reqnfo.Rate1 = "";

                if (txt15.Text.Trim() != ":")
                    reqnfo.Rate15 = txt15.Text.Trim();
                else
                    reqnfo.Rate15 = "";

                if (txt2.Text.Trim() != ":")
                    reqnfo.Rate2 = txt2.Text.Trim();
                else
                    reqnfo.Rate2 = "";

                if (txt3.Text.Trim() != ":")
                    reqnfo.Rate3 = txt3.Text.Trim();
                else
                    reqnfo.Rate3 = "";

                if (txtOt1From.Text.Trim() != ":")
                    reqnfo.Rate1From = txtOt1From.Text.Trim();
                else
                    reqnfo.Rate15From = "";
                if (txtOt15From.Text.Trim() != ":")
                    reqnfo.Rate15From = txtOt15From.Text.Trim();
                else
                    reqnfo.Rate2From = "";
                if (txtOt2From.Text.Trim() != ":")
                    reqnfo.Rate2From = txtOt2From.Text.Trim();
                else
                    reqnfo.Rate3From = "";
                if (txtOt3From.Text.Trim() != ":")
                    reqnfo.Rate3From = txtOt3From.Text.Trim();
                else
                    reqnfo.Rate1From = "";

                if (txtOt1To.Text.Trim() != ":")
                    reqnfo.Rate1To = txtOt1To.Text.Trim();
                else
                    reqnfo.Rate1To = "";

                if (txtOt15To.Text.Trim() != ":")
                    reqnfo.Rate15To = txtOt15To.Text.Trim();
                else
                    reqnfo.Rate15To = "";

                if (txtOt2To.Text.Trim() != ":")
                    reqnfo.Rate2To = txtOt2To.Text.Trim();
                else
                    reqnfo.Rate2To = "";

                if (txtOt3To.Text.Trim() != ":")
                    reqnfo.Rate3To = txtOt3To.Text.Trim();
                else
                    reqnfo.Rate3To = "";


                if (cmbOTType.SelectedValue.ToString() != "")
                {
                    reqnfo.OtRemark = cmbOTType.SelectedValue.ToString();
                }
                else {
                    reqnfo.OtRemark = "00:OT Normal";
                }

                reqnfo.CalRest = txtCalRest.Text;
                reqnfo.TimeCard = txtTimeCard.Text;

                return reqnfo;

            }
            set
            {
            
                try
                {
                    reqnfo = (OtRequestInfo)value;
                    lblDocId.Text = reqnfo.DocId;
                    if (CanEdit)
                    {
                        if (lblDocId.Text.Trim()!="")
                        {
                            txtEmpCode.ReadOnly = true;
                            txtReqId.ReadOnly = false; 
                            //dtpDate.Enabled = true; 
                        }
                        else
                        {
                            txtEmpCode.ReadOnly = true;
                            txtReqId.ReadOnly = true; 
                            dtpDate.Enabled = false; 
                        }
                        
                    }
                    txtEmpCode.Text = reqnfo.EmpCode;
                    txtReqId.Text = reqnfo.ReqId;
                    dtpDate.Value = reqnfo.OtDate.Date;

                    try {
                        //cmbJobType.SelectedItem = reqnfo.JobType;
                        cmbJobType.SelectedValue = reqnfo.JobType;
                    }
                    catch { }
                    



                    txtWtype.Text = reqnfo.EmpType;
                    try
                    {
                        txtFrom.Text = reqnfo.OtFrom;
                    }
                    catch  {}
                    try
                    {
                        txtTo.Text = reqnfo.OtTo;
                    }
                    catch  {}
                    try
                    {
                        txt1.Text = reqnfo.Rate1;
                    }
                    catch 
                    { }
                    txt1.TabStop = txt1.Text != "";
                    try
                    {
                        txt15.Text = reqnfo.Rate15;
                    }
                    catch  {}
                    txt15.TabStop = txt15.Text != "";
                    try
                    {
                        txt2.Text = reqnfo.Rate2;
                    }
                    catch{ }
                    txt2.TabStop = txt2.Text != "";
                    try
                    {
                        txt3.Text = reqnfo.Rate3;
                    }
                    catch  {}
              
                    try
                    {
                        txtCalRest.Text = reqnfo.CalRest;
                    }
                    catch   {}
                    try
                    {
                        txtN1.Text = reqnfo.N1;
                    }
                    catch  {}
                    try
                    {
                        txtN15.Text = reqnfo.N15;
                    }
                    catch   {}
                    try
                    {
                        txtN2.Text = reqnfo.N2;
                    }
                    catch { }
                    try
                    {
                        txtN3.Text = reqnfo.N3;
                    }
                    catch {}
                    try
                    {
                        txtTimeCard.Text = reqnfo.TimeCard;
                    }
                    catch { }
                    try
                    {
                        txtNFrom.Text = reqnfo.NFrom;
                    }
                    catch   {}
                    try
                    {
                        txtNTo.Text = reqnfo.NTo;
                    }
                    catch {}
                    try
                    {
                        txtOt1From.Text = reqnfo.Rate1From;
                    }
                    catch { }

                    txtOt1From.TabStop = txt1.Text != "";

                    try
                    {
                        txtOt15From.Text = reqnfo.Rate15From;
                    }
                    catch { }
                    txtOt15From.TabStop = txt15.Text != "";
                    try
                    {
                        txtOt2From.Text = reqnfo.Rate2From;
                    }
                    catch { }
                    txtOt2From.TabStop = txt2.Text != "";
                    try
                    {
                        txtOt3From.Text = reqnfo.Rate3From;
                    }
                    catch { }
                    txtOt3From.TabStop = txt3.Text != "";
                    try
                    {
                        txtOt1To.Text = reqnfo.Rate1To;
                    }
                    catch { }
                    txtOt1To.TabStop = txt1.Text != "";
                    try
                    {
                        txtOt15To.Text = reqnfo.Rate15To;
                    }
                    catch { }
                    txtOt15To.TabStop = txt15.Text != "";
                    try
                    {
                        txtOt2To.Text = reqnfo.Rate2To;
                    }
                    catch { }
                    txtOt2To.TabStop = txt2.Text != "";
                    try
                    {
                        txtOt3To.Text = reqnfo.Rate3To;
                    }
                    catch { }
                    txtOt3To.TabStop = txt3.Text != "";


                    try {
                        cmbOTType.SelectedValue = reqnfo.OtRemark;
                    }catch{
                        cmbOTType.SelectedValue = "00:OT Normal";
                    }

                }
                catch { }

                if (txtNFrom.Text != txtFrom.Text)
                {
                    txtFrom.BackColor = Color.Yellow; ;
                }
                else
                {
                    txtFrom.BackColor = Color.White;
                }
                if (txtNTo.Text!= txtTo.Text)
                {
                    txtTo.BackColor = Color.Yellow;
                }
                else
                {
                    txtTo.BackColor = Color.White;
                }
                if (txtN1.Text != txt1.Text)
                {
                    txt1.BackColor = Color.Yellow;
                }
                else
                {
                    txt1.BackColor = Color.White;
                }
                if (txtN15.Text != txt15.Text)
                {
                    txt15.BackColor = Color.Yellow;
                }
                else
                {
                    txt15.BackColor = Color.White;
                }
                if (txtN2.Text != txt2.Text)
                {
                    txt2.BackColor = Color.Yellow;
                }
                else
                {
                    txt2.BackColor = Color.White;
                }
                if (txtN3.Text != txt3.Text)
                {
                    txt3.BackColor = Color.Yellow;
                }
                else
                {
                    txt3.BackColor = Color.White;
                }
            }
        }
        public bool EnableEditDate
        {
            set { chkRqDate.Checked = value; }
            get { return chkRqDate.Checked; }
        }

        public bool CanEdit 
        {
            set
            {
                canEdit = value;
                txtEmpCode.ReadOnly = !value;
                txtReqId.ReadOnly = !value;
                dtpDate.Enabled = value;
                chkRqDate.Enabled = value;
                chkRqDate.Checked = dtpDate.Enabled;
            }
            get
            {
                return canEdit;
            }

        }
        private void txtFrom_KeyDown(object sender, KeyEventArgs e)
        {
      

            if (e.KeyCode == Keys.Enter)
            {
                if (sender == txt3)
                {
                    if (!KeyPressManager.ConvertTextTime(sender))
                    {
                        return; ; 
                    }
                 
                    txtFrom.Focus(); 
                    OnSave_Data();
                }
                else
                {

                    SendKeys.Send("{TAB}");
                }
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode== Keys.Up)
            {         TextBox tt = (TextBox)sender;
                //tt.SelectAll();
                OnMove_Data(e);
      
              
            }


        }



        private void txtFrom_Leave(object sender, EventArgs e)
        {
          KeyPressManager.ConvertTextTime(sender);
        }

        private void txtFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressManager.EnterNumericOnly(e);
        }

        private void chkRqDate_CheckedChanged(object sender, EventArgs e)
        {
            if (canEdit)
            {
                dtpDate.Enabled = chkRqDate.Checked;
            }
            else
            {
                dtpDate.Enabled = false;
            }
        }

        private void txtOt1From_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void kryptonGroup2_Panel_Paint(object sender, PaintEventArgs e)
        {

        }
       



    }
}
