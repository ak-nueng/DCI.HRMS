namespace DCI.HRMS.Security
{
    partial class Dlg_ChangePassword
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.grbPwd = new System.Windows.Forms.GroupBox();
            this.txtPwd3 = new System.Windows.Forms.TextBox();
            this.txtPwd2 = new System.Windows.Forms.TextBox();
            this.txtWd1 = new System.Windows.Forms.TextBox();
            this.lblPwd3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblPwd2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblPwd1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.grbPwd.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbPwd
            // 
            this.grbPwd.BackColor = System.Drawing.SystemColors.Control;
            this.grbPwd.Controls.Add(this.txtPwd3);
            this.grbPwd.Controls.Add(this.txtPwd2);
            this.grbPwd.Controls.Add(this.txtWd1);
            this.grbPwd.Controls.Add(this.lblPwd3);
            this.grbPwd.Controls.Add(this.lblPwd2);
            this.grbPwd.Controls.Add(this.lblPwd1);
            this.grbPwd.Location = new System.Drawing.Point(12, 12);
            this.grbPwd.Name = "grbPwd";
            this.grbPwd.Size = new System.Drawing.Size(268, 93);
            this.grbPwd.TabIndex = 0;
            this.grbPwd.TabStop = false;
            // 
            // txtPwd3
            // 
            this.txtPwd3.Location = new System.Drawing.Point(89, 63);
            this.txtPwd3.Name = "txtPwd3";
            this.txtPwd3.PasswordChar = '*';
            this.txtPwd3.Size = new System.Drawing.Size(173, 20);
            this.txtPwd3.TabIndex = 2;
            this.txtPwd3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPwd3_KeyDown);
            // 
            // txtPwd2
            // 
            this.txtPwd2.Location = new System.Drawing.Point(89, 38);
            this.txtPwd2.Name = "txtPwd2";
            this.txtPwd2.PasswordChar = '*';
            this.txtPwd2.Size = new System.Drawing.Size(173, 20);
            this.txtPwd2.TabIndex = 1;
            this.txtPwd2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWd1_KeyDown);
            // 
            // txtWd1
            // 
            this.txtWd1.Location = new System.Drawing.Point(89, 13);
            this.txtWd1.Name = "txtWd1";
            this.txtWd1.PasswordChar = '*';
            this.txtWd1.Size = new System.Drawing.Size(173, 20);
            this.txtWd1.TabIndex = 0;
            this.txtWd1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWd1_KeyDown);
            // 
            // lblPwd3
            // 
            this.lblPwd3.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.lblPwd3.Location = new System.Drawing.Point(30, 64);
            this.lblPwd3.Name = "lblPwd3";
            this.lblPwd3.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.lblPwd3.Size = new System.Drawing.Size(53, 19);
            this.lblPwd3.TabIndex = 0;
            this.lblPwd3.Text = "Confirm:";
            this.lblPwd3.Values.ExtraText = "";
            this.lblPwd3.Values.Image = null;
            this.lblPwd3.Values.Text = "Confirm:";
            // 
            // lblPwd2
            // 
            this.lblPwd2.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.lblPwd2.Location = new System.Drawing.Point(1, 39);
            this.lblPwd2.Name = "lblPwd2";
            this.lblPwd2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.lblPwd2.Size = new System.Drawing.Size(82, 19);
            this.lblPwd2.TabIndex = 0;
            this.lblPwd2.Text = "NewPassword:";
            this.lblPwd2.Values.ExtraText = "";
            this.lblPwd2.Values.Image = null;
            this.lblPwd2.Values.Text = "NewPassword:";
            // 
            // lblPwd1
            // 
            this.lblPwd1.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.lblPwd1.Location = new System.Drawing.Point(5, 14);
            this.lblPwd1.Name = "lblPwd1";
            this.lblPwd1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.lblPwd1.Size = new System.Drawing.Size(78, 19);
            this.lblPwd1.TabIndex = 0;
            this.lblPwd1.Text = "OldPassword:";
            this.lblPwd1.Values.ExtraText = "";
            this.lblPwd1.Values.Image = null;
            this.lblPwd1.Values.Text = "OldPassword:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(124, 111);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(205, 111);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Dlg_ChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(302, 152);
            this.ControlBox = false;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.grbPwd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dlg_ChangePassword";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dlg_ChangePassword";
            this.Load += new System.EventHandler(this.Dlg_ChangePassword_Load);
            this.grbPwd.ResumeLayout(false);
            this.grbPwd.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbPwd;
        private System.Windows.Forms.TextBox txtPwd3;
        private System.Windows.Forms.TextBox txtPwd2;
        private System.Windows.Forms.TextBox txtWd1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblPwd3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblPwd2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblPwd1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}