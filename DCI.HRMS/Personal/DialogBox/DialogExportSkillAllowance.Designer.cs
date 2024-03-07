namespace DCI.HRMS.Personal.DialogBox
{
    partial class DialogExportSkillAllowance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogExportSkillAllowance));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExp = new System.Windows.Forms.Button();
            this.rdDCI = new System.Windows.Forms.RadioButton();
            this.rdSUB = new System.Windows.Forms.RadioButton();
            this.rdTRN = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdTRN);
            this.groupBox1.Controls.Add(this.rdSUB);
            this.groupBox1.Controls.Add(this.rdDCI);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox1.Location = new System.Drawing.Point(12, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(164, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ข้อมูลพนักงาน";
            // 
            // btnExp
            // 
            this.btnExp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnExp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnExp.Location = new System.Drawing.Point(12, 116);
            this.btnExp.Name = "btnExp";
            this.btnExp.Size = new System.Drawing.Size(164, 36);
            this.btnExp.TabIndex = 1;
            this.btnExp.Text = "Export ";
            this.btnExp.UseVisualStyleBackColor = false;
            this.btnExp.Click += new System.EventHandler(this.btnExp_Click);
            // 
            // rdDCI
            // 
            this.rdDCI.AutoSize = true;
            this.rdDCI.Checked = true;
            this.rdDCI.Location = new System.Drawing.Point(30, 19);
            this.rdDCI.Name = "rdDCI";
            this.rdDCI.Size = new System.Drawing.Size(47, 20);
            this.rdDCI.TabIndex = 0;
            this.rdDCI.TabStop = true;
            this.rdDCI.Text = "DCI";
            this.rdDCI.UseVisualStyleBackColor = true;
            // 
            // rdSUB
            // 
            this.rdSUB.AutoSize = true;
            this.rdSUB.Location = new System.Drawing.Point(30, 42);
            this.rdSUB.Name = "rdSUB";
            this.rdSUB.Size = new System.Drawing.Size(98, 20);
            this.rdSUB.TabIndex = 0;
            this.rdSUB.Text = "SubContract";
            this.rdSUB.UseVisualStyleBackColor = true;
            // 
            // rdTRN
            // 
            this.rdTRN.AutoSize = true;
            this.rdTRN.Location = new System.Drawing.Point(30, 65);
            this.rdTRN.Name = "rdTRN";
            this.rdTRN.Size = new System.Drawing.Size(72, 20);
            this.rdTRN.TabIndex = 0;
            this.rdTRN.Text = "Trainee";
            this.rdTRN.UseVisualStyleBackColor = true;
            // 
            // DialogExportSkillAllowance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(191, 162);
            this.Controls.Add(this.btnExp);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DialogExportSkillAllowance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Export Skill Allowance";
            this.Load += new System.EventHandler(this.DialogExportSkillAllowance_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnExp;
        private System.Windows.Forms.RadioButton rdTRN;
        private System.Windows.Forms.RadioButton rdSUB;
        private System.Windows.Forms.RadioButton rdDCI;
    }
}