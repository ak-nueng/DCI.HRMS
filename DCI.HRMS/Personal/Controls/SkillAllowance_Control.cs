using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace DCI.HRMS.Personal.Controls
{
    public partial class SkillAllowance_Control : UserControl
    {
        private readonly string[] colNameS = new string[] { "RecordId", "Code","Month" ,"CerType", "Level","Cost", "Remark","AddBy", "AddDate", "UpdateBy", "UpdateDate" };
        private readonly string[] propNameS = new string[] { "RecordId", "EmpCode", "Month","CertName", "CertLevel", "CertCost", "Remark", "CreateBy", "CreateDateTime", "LastUpdateBy", "LastUpDateDateTime" };
    //    private readonly int[] widthS = new int[] { 50, 50, 80, 80, 80, 80, 80, 80, 80, 80, 80, 80, 120, 100, 120, 100, 120 };

        private ArrayList gvData = new ArrayList();
       
        public SkillAllowance_Control()
        {
            InitializeComponent();
            AddGridViewColumnsS();
        }

        public ArrayList Information
        {
            set
            {
                gvData = value;
                FillDataGrid();
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
    }
}
