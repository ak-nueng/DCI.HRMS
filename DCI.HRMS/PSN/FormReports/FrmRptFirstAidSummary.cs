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
using DCIBizPro.Util.Xml;
using CrystalDecisions.CrystalReports.Engine;
using DCI.Security.Model;

namespace DCI.HRMS.PSN
{
    public partial class FrmRptFirstAidSummary : Form , IFormParent , IFormPermission
    {
        private DivisionService divisionService = DivisionService.Instance();

        public FrmRptFirstAidSummary()
        {
            InitializeComponent();
        }

        #region IForm Members

        public string GUID
        {
            get { return null; }
        }

        public object Information
        {
            get
            {
                return null;
            }
            set
            {
                
            }
        }

        public void AddNew()
        {
            
        }

        public void Save()
        {
            
        }

        public void Delete()
        {
            
        }

        public void Search()
        {
            try
            {
                string qSection = cboSection.SelectedValue.ToString();
                string rptDocName = @"D:\Client-Server\Xtra2006\DCI.HRMS\Reports\PSN\FAR_TotalService.rpt";

                DataSet ds = FirstAidReportManager.Instance().GetTotalServiceBySection(qSection, dtFrom.Value, dtTo.Value);
                ds.WriteXml(@"C:\Employee.xml");
                ds.WriteXmlSchema(@"C:\Employee.xsd");

                ReportDocument rptDoc = new ReportDocument();
                rptDoc.Load(rptDocName);

                rptDoc.SetDataSource(ds);
                crtViewer.ReportSource = rptDoc;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                DateTime now = DateTime.Now;
                dtFrom.Value = new DateTime(now.Year,now.Month,1,0,0,0);
                dtTo.Value = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);

                cboSection.DisplayMember = "Name";
                cboSection.ValueMember = "Code";
                cboSection.DataSource = divisionService.FindByType("SECT");
            }
            catch { }
        }

        public void Clear()
        {
            
        }

        public void RefreshData()
        {
            
        }

        public void Exit()
        {
            this.Close();
        }

        #endregion

        #region IFormPermission Members

        public PermissionInfo Permission
        {
            set {  }
        }

        #endregion

        private void FrmRptFirstAidSummary_Load(object sender, EventArgs e)
        {
            this.Open();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.Search();
        }

    }
}