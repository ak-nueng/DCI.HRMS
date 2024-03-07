using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DCI.Security.Model;
using DCI.Security.Service;
using DCI.HRMS.Common;
using DCI.HRMS.Util;

namespace DCI.HRMS.Security
{
    public partial class Dlg_ChangePassword : Form
    {
        private UserAccountManager usrSvr = new UserAccountManager();
        private UserAccountInfo userInfo = new UserAccountInfo();
        private ApplicationManager appMgr = ApplicationManager.Instance();
        public Dlg_ChangePassword(UserAccountInfo usrInfo)
        {
            InitializeComponent();
            userInfo = usrInfo;
            this.Text = "Change Password: " + userInfo.AccountId;
        }
        public bool EnableCancel
        {
            set
            {
                button2.Enabled = value;
            }
            get
            {
                return button2.Enabled;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckPsw())
                {
                    usrSvr.changePassword(userInfo.AccountId, txtWd1.Text, txtPwd2.Text, appMgr.UserAccount.AccountId);
                    UserAccountService.Instance().KeepLog(userInfo.AccountId, this.Text, SystemInformation.ComputerName, "ChangePassword", "Success");
                    this.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Dlg_ChangePassword_Load(object sender, EventArgs e)
        {

        }

        private void txtWd1_KeyDown(object sender, KeyEventArgs e)
        {
            KeyPressManager.Enter(e);
        }

        private void txtPwd3_KeyDown(object sender, KeyEventArgs e) 
        {

            if (e.KeyCode==  Keys.Enter)
            {
                button1_Click(sender, new EventArgs()); 
            }
        }
        private bool CheckPsw()
        {
            if (txtWd1.Text=="")
            {
                MessageBox.Show("กรุณาป้อน Password เก่า", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtWd1.Focus();
                return false;
            }
            if (txtPwd2.Text=="")
            {
                MessageBox.Show("กรุณาป้อน Password ใหม่", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPwd2.Focus();
                return false;
            }
            if (txtPwd3.Text =="")
            {
                  MessageBox.Show("กรุณายืนยัน Password ใหม่", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPwd3.Focus();
                return false;
            }
            if (txtPwd2.Text!= txtPwd3.Text)
            {
                MessageBox.Show("กรุณาป้อนและยืนยัน Password ใหม่อีกครั้ง", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPwd2.Focus();
                txtPwd2.Clear();
                txtPwd3.Clear();
                return false;
            }
            return true;
        }
    }
}
