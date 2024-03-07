using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DCI.HRMS.Service;

namespace DCI.HRMS
{
    public partial class FrmTest : Form
    {
        private TimeCardService emTmc = TimeCardService.Instance();
        private OtService otsvr = OtService.Instance();
        public FrmTest()
        {
            InitializeComponent();
        }

        private void FrmTest_Load(object sender, EventArgs e)
        {
            ArrayList tmc1 = emTmc.GetTimeCardCodeDate("25438", DateTime.Parse("02/08/2008"));
            ArrayList otSl = otsvr.GetOTRequest("25438", DateTime.Parse("02/08/2008"), "", "%");
        }
    }
}
