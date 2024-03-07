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
using DCI.HRMS.Service;
using System.Collections;
using DCI.HRMS.Model.Personal;
using DCI.HRMS.Util;
using System.Data.OracleClient;
using ComponentFactory.Krypton.Toolkit;
using System.Configuration;

using Excel = Microsoft.Office.Interop.Excel;


namespace DCI.HRMS.Personal
{
    public partial class FrmEmpChangeUploadProfile : KryptonForm
        //BaseForm, IFormParent, IFormPermission
    {
        public FrmEmpChangeUploadProfile()
        {
            InitializeComponent();
        }



        string sFileName;
        int iRow, iCol = 2;

        private void btnChoseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog1.Title = "Excel File to Edit";
            OpenFileDialog1.FileName = "";
            OpenFileDialog1.Filter = "Excel File|*.xlsx;*.xls";

            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                sFileName = OpenFileDialog1.FileName;

                if (sFileName.Trim() != "")
                {
                    readExcel(sFileName);
                    lblBrowser.Text = "-";
                }
            }
        }


        // GET DATA FROM EXCEL AND POPULATE COMB0 BOX.
        private void readExcel(string sFile)
        {

            //Excel.Application xlApp;
            //Excel.Workbook xlWorkBook;
            //Excel.Worksheet xlWorkSheet;
            //Excel.Range range;

            //object misValue = System.Reflection.Missing.Value;
            //string str;
            //int rCnt ;
            //int cCnt ;
            //int rw = 0;
            //int cl = 0;

            //xlApp = new Excel.Application();
            //xlWorkBook = xlApp.Workbooks.Open(sFile, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            //xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            ////MessageBox.Show(xlWorkSheet.get_Range("A1", "A1").Value2.ToString());
            
            
            //range = xlWorkSheet.UsedRange;
            //rw = range.Rows.Count;
            //if (rw > 15)
            //{
            //    rw = 15;
            //}
            //cl = range.Columns.Count;


            //DataTable dtData = new DataTable();
            //for (cCnt = 1; cCnt  <= cl; cCnt++){
            //    string col = "col"+cCnt;
            //    dtData.Columns.Add(col, typeof(string));
            //}


            //for (rCnt = 3; rCnt <= rw; rCnt++)
            //{

            //    if (this.lblBrowser.InvokeRequired)
            //    {
            //        this.lblBrowser.BeginInvoke((MethodInvoker)delegate() { this.lblBrowser.Text = "กำลังดึงข้อมูลจาก Excel : " + rCnt.ToString() + " / " + rw.ToString(); });
            //    }
            //    else
            //    {
            //        this.lblBrowser.Text = "กำลังดึงข้อมูลจาก Excel : " + rCnt.ToString() + " / " + rw.ToString();
            //    }

            //    DataRow newRow = dtData.NewRow();
            //    for (cCnt = 1; cCnt  <= cl; cCnt++)
            //    {
            //        string col = "col" + cCnt;
            //        string strVal = "";
            //        try{ strVal = (range.Cells[rCnt, cCnt] as Excel.Range).Value2.ToString();}catch{}
            //        newRow[col] = strVal;

            //        //str = (string)(range.Cells[rCnt, cCnt] as Excel.Range).Value2;
            //        //MessageBox.Show(str);
            //    }

            //    dtData.Rows.Add(newRow);
            //}



            //xlWorkBook.Close(true, misValue, misValue);
            //xlApp.Quit();

            //releaseObject(xlWorkSheet);
            //releaseObject(xlWorkBook);
            //releaseObject(xlApp);


            //dgvData.DataSource = dtData;
            //dgvData.Refresh();

            //pnData.ValuesSecondary.Heading = "ข้อมูลทั้งหมด " + dgvData.Rows.Count + " แถว";

            
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        } 


        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count > 0) { 
                foreach(DataRow drData in dgvData.Rows){
                
                }
            
            }
        }

       
    }
}