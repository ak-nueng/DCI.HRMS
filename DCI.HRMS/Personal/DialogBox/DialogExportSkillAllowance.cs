using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static OfficeOpenXml.ExcelErrorValue;

namespace DCI.HRMS.Personal.DialogBox
{
    public partial class DialogExportSkillAllowance : Form
    {

        public string _empType { get; set; }
        public DialogExportSkillAllowance()
        {
            InitializeComponent();
        }

        private void DialogExportSkillAllowance_Load(object sender, EventArgs e)
        {

        }

        private void btnExp_Click(object sender, EventArgs e)
        {
            if (rdDCI.Checked) { this._empType = "DCI"; }
            else if (rdSUB.Checked) { this._empType = "SUB"; }
            else if (rdTRN.Checked) { this._empType = "TRN"; }
            
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }



    }
}
