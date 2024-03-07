using System;
using System.ComponentModel;
using System.Windows.Forms;
//using DCIBizPro.Business.Security;
using DCIBizPro.Util.Text;
using DCI.HRMS.Util;
using DCI.Security.Service;
//using Infragistics.Win.Misc;

namespace DCI.HRMS.ControlPanel
{
	/// <summary>
	/// Summary description for FrmSetPassword.
	/// </summary>
	public class FrmSetPassword : Form
	{
		private Label label2;
		private Label label1;
		private Button btnOK;
		private Button btnCancel;
		private TextBox txtPassword1;
		private TextBox txtPassword2;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		private string m_Password = string.Empty;
		private Label lblMsg;
		private bool m_NeedChange = false;

		public FrmSetPassword()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.txtPassword1 = new System.Windows.Forms.TextBox();
			this.txtPassword2 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.lblMsg = new System.Windows.Forms.Label();
			this.btnOK = new    Button();
			this.btnCancel = new    Button();
			this.SuspendLayout();
			// 
			// txtPassword1
			// 
			this.txtPassword1.AutoSize = false;
			this.txtPassword1.BackColor = System.Drawing.Color.Beige;
			this.txtPassword1.Location = new System.Drawing.Point(152, 8);
			this.txtPassword1.MaxLength = 25;
			this.txtPassword1.Name = "txtPassword1";
			this.txtPassword1.PasswordChar = '*';
			this.txtPassword1.Size = new System.Drawing.Size(192, 23);
			this.txtPassword1.TabIndex = 0;
			this.txtPassword1.Text = "";
			this.txtPassword1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyEnter);
			// 
			// txtPassword2
			// 
			this.txtPassword2.AutoSize = false;
			this.txtPassword2.BackColor = System.Drawing.Color.Beige;
			this.txtPassword2.Location = new System.Drawing.Point(152, 36);
			this.txtPassword2.MaxLength = 25;
			this.txtPassword2.Name = "txtPassword2";
			this.txtPassword2.PasswordChar = '*';
			this.txtPassword2.Size = new System.Drawing.Size(192, 23);
			this.txtPassword2.TabIndex = 1;
			this.txtPassword2.Text = "";
			this.txtPassword2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyEnter);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.label2.Location = new System.Drawing.Point(12, 12);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(90, 17);
			this.label2.TabIndex = 46;
			this.label2.Text = "New Password ";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.label1.Location = new System.Drawing.Point(12, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(109, 17);
			this.label1.TabIndex = 47;
			this.label1.Text = "Confirm Password ";
			// 
			// lblMsg
			// 
			this.lblMsg.BackColor = System.Drawing.Color.Transparent;
			this.lblMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.lblMsg.Location = new System.Drawing.Point(12, 68);
			this.lblMsg.Name = "lblMsg";
			this.lblMsg.Size = new System.Drawing.Size(336, 56);
			this.lblMsg.TabIndex = 48;
			this.lblMsg.Text = "If you click Cancel, the password will not be changed and no data  loss will  occ" +
				"ure.";
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(116, 128);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 24);
			this.btnOK.TabIndex = 49;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(196, 128);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 24);
			this.btnCancel.TabIndex = 50;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// FrmSetPassword
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(360, 158);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.lblMsg);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtPassword2);
			this.Controls.Add(this.txtPassword1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmSetPassword";
			this.Text = "Set Password";
			this.Load += new System.EventHandler(this.FrmSetPassword_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.m_Password = string.Empty;
			this.Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			string password = this.txtPassword1.Text.Trim();
			string password2 = this.txtPassword2.Text.Trim();

			if (!StringHelper.CheckStringEmpty(password) && !StringHelper.CheckStringEmpty(password2) && password.Equals(password2))
			{
				if (password.Length < 6)
				{
					MessageBox.Show(this, "ความยาวของรหัสผ่านต้องไม่น้อยกว่า 6 ตัวอักษร", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					this.txtPassword1.Text = string.Empty;
					this.txtPassword2.Text = string.Empty;
					this.txtPassword1.Focus();
				}
				else
				{
					if (this.m_NeedChange)
					{
						try
						{
							UserAccountManager.changePassword(ApplicationContext.Info.AccountId, this.txtPassword1.Text, ApplicationContext.Info.AccountId);
							MessageBox.Show(this, "The password has been set.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
						}
						catch (Exception ex)
						{
							MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
					else
					{
						this.m_Password = password;
					}
					this.Close();
				}
			}
			else
			{
				MessageBox.Show(this, "กรุณายืนยันรหัสผ่านอีกครั้ง", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				this.txtPassword2.Text = string.Empty;
				this.txtPassword2.Focus();
			}
		}

		private void FrmSetPassword_Load(object sender, EventArgs e)
		{
		}

		private void KeyEnter(object sender, KeyEventArgs e)
		{
			KeyPressManager.Enter(e);
		}

		internal bool NeedChangePassword
		{
			set { this.m_NeedChange = value; }
		}

		internal string Password
		{
			get { return this.m_Password; }
		}

		internal string Message
		{
			set { this.lblMsg.Text = value; }
		}
	}
}