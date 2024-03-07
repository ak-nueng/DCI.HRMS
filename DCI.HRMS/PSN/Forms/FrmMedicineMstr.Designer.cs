namespace DCI.HRMS.PSN
{
    partial class FrmMedicineMstr
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgItems = new System.Windows.Forms.DataGridView();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.uclAction = new DCI.HRMS.Controls.Ucl_ActionControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgItems);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(674, 331);
            this.panel1.TabIndex = 2;
            // 
            // dgItems
            // 
            this.dgItems.AllowUserToAddRows = false;
            this.dgItems.AllowUserToDeleteRows = false;
            this.dgItems.AllowUserToOrderColumns = true;
            this.dgItems.AllowUserToResizeRows = false;
            this.dgItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgItems.Location = new System.Drawing.Point(0, 0);
            this.dgItems.MultiSelect = false;
            this.dgItems.Name = "dgItems";
            this.dgItems.RowHeadersWidth = 25;
            this.dgItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgItems.Size = new System.Drawing.Size(674, 331);
            this.dgItems.TabIndex = 0;
            this.dgItems.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMedicines_CellClick);
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtCode.Location = new System.Drawing.Point(144, 384);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(152, 22);
            this.txtCode.TabIndex = 3;
            this.txtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCode_KeyDown);
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtName.Location = new System.Drawing.Point(144, 412);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(320, 22);
            this.txtName.TabIndex = 4;
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyEnter);
            // 
            // txtUnit
            // 
            this.txtUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtUnit.Location = new System.Drawing.Point(144, 440);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(92, 22);
            this.txtUnit.TabIndex = 5;
            this.txtUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(24, 388);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "รหัสยา";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(24, 416);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "ชื่อยา";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(24, 441);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "หน่วยนับ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uclAction
            // 
            this.uclAction.Dock = System.Windows.Forms.DockStyle.Top;
            this.uclAction.Location = new System.Drawing.Point(0, 0);
            this.uclAction.Name = "uclAction";
            this.uclAction.Size = new System.Drawing.Size(674, 45);
            this.uclAction.TabIndex = 1;
            // 
            // FrmMedicineMstr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 477);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUnit);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.uclAction);
            this.KeyPreview = true;
            this.Name = "FrmMedicineMstr";
            this.Text = "ลงทะเบียนข้อมูลยารักษาโรค";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMedicineMstr_KeyDown);
            this.Load += new System.EventHandler(this.FrmMedicineMstr_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DCI.HRMS.Controls.Ucl_ActionControl uclAction;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgItems;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}