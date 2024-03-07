namespace DCI.HRMS.PSN
{
    partial class FrmDoctorMstr
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
            this.uclAction = new DCI.HRMS.Controls.Ucl_ActionControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgItems = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtFName = new System.Windows.Forms.TextBox();
            this.txtSName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).BeginInit();
            this.SuspendLayout();
            // 
            // uclAction
            // 
            this.uclAction.Dock = System.Windows.Forms.DockStyle.Top;
            this.uclAction.Location = new System.Drawing.Point(0, 0);
            this.uclAction.Name = "uclAction";
            this.uclAction.Size = new System.Drawing.Size(679, 45);
            this.uclAction.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgItems);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(679, 335);
            this.panel1.TabIndex = 1;
            // 
            // dgItems
            // 
            this.dgItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgItems.Location = new System.Drawing.Point(0, 0);
            this.dgItems.Name = "dgItems";
            this.dgItems.Size = new System.Drawing.Size(679, 335);
            this.dgItems.TabIndex = 0;
            this.dgItems.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDoctors_CellClick);
            this.dgItems.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgDoctors_CellFormatting);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(24, 424);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "ชื่อ-สกุล";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(24, 396);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "รหัส";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTitle
            // 
            this.txtTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtTitle.Location = new System.Drawing.Point(144, 420);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(56, 22);
            this.txtTitle.TabIndex = 2;
            this.txtTitle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyEnter);
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtCode.Location = new System.Drawing.Point(144, 392);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(132, 22);
            this.txtCode.TabIndex = 1;
            this.txtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCode_KeyDown);
            // 
            // txtFName
            // 
            this.txtFName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtFName.Location = new System.Drawing.Point(200, 420);
            this.txtFName.Name = "txtFName";
            this.txtFName.Size = new System.Drawing.Size(96, 22);
            this.txtFName.TabIndex = 3;
            this.txtFName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyEnter);
            // 
            // txtSName
            // 
            this.txtSName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtSName.Location = new System.Drawing.Point(296, 420);
            this.txtSName.Name = "txtSName";
            this.txtSName.Size = new System.Drawing.Size(96, 22);
            this.txtSName.TabIndex = 4;
            this.txtSName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyEnter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(24, 452);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 16);
            this.label3.TabIndex = 14;
            this.label3.Text = "Phone.";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPhone
            // 
            this.txtPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtPhone.Location = new System.Drawing.Point(144, 449);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(132, 22);
            this.txtPhone.TabIndex = 5;
            this.txtPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyEnter);
            // 
            // FrmDoctorMstr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 517);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSName);
            this.Controls.Add(this.txtFName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.uclAction);
            this.KeyPreview = true;
            this.Name = "FrmDoctorMstr";
            this.Text = "ลงทะเบียนข้อมูลหมอ-พยาบาล";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmDoctorMstr_KeyDown);
            this.Load += new System.EventHandler(this.FrmDoctorMstr_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DCI.HRMS.Controls.Ucl_ActionControl uclAction;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgItems;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TextBox txtFName;
        private System.Windows.Forms.TextBox txtSName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPhone;
    }
}