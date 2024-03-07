using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DCI.HRMS.DialogBox
{
    public partial class Dlg_Password : Form
    {
        public string Password;
        public string dlgResult;
        public Dlg_Password()
        {
            InitializeComponent();
        }


        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Password = kryptonTextBox1.Text;
            dlgResult = "OK";
            this.Close();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            Password = "";
            dlgResult = "CANCEL";
            this.Close();
        }

        private void Dlg_Password_Load(object sender, EventArgs e)
        {
            Password = "";
            dlgResult = "";
        }

        private void kryptonTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Password = kryptonTextBox1.Text;
                dlgResult = "OK";
                this.Close();
            }
        }

        private void kryptonTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (kryptonTextBox1.Text=="")
            {
                kryptonButton1.Enabled = false;
            }
            else
            {
                kryptonButton1.Enabled = true;
            }
        }
    }
}
