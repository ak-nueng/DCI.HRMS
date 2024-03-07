namespace DCI.HRMS.PSN
{
    partial class FrmRptFirstAidSummary
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
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cboSection = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.crtViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.FAR_TotalService1 = new DCI.HRMS.Reports.PSN.FAR_Total_Service();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.dtTo);
            this.panel1.Controls.Add(this.dtFrom);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cboSection);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(782, 104);
            this.panel1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(24, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 15);
            this.label3.TabIndex = 20;
            this.label3.Text = "ถึง";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(668, 64);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 28);
            this.btnSearch.TabIndex = 19;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dtTo
            // 
            this.dtTo.Location = new System.Drawing.Point(132, 68);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(128, 20);
            this.dtTo.TabIndex = 18;
            // 
            // dtFrom
            // 
            this.dtFrom.Location = new System.Drawing.Point(132, 44);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(128, 20);
            this.dtFrom.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(24, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 16;
            this.label2.Text = "วันที่:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboSection
            // 
            this.cboSection.FormattingEnabled = true;
            this.cboSection.Location = new System.Drawing.Point(132, 20);
            this.cboSection.Name = "cboSection";
            this.cboSection.Size = new System.Drawing.Size(270, 21);
            this.cboSection.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(25, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "หน่วยงาน:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // crtViewer
            // 
            this.crtViewer.ActiveViewIndex = -1;
            this.crtViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crtViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crtViewer.Location = new System.Drawing.Point(0, 104);
            this.crtViewer.Name = "crtViewer";
            this.crtViewer.Size = new System.Drawing.Size(782, 442);
            this.crtViewer.TabIndex = 2;
            // 
            // FrmRptFirstAidSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 546);
            this.Controls.Add(this.crtViewer);
            this.Controls.Add(this.panel1);
            this.Name = "FrmRptFirstAidSummary";
            this.Text = "FrmRptFirstAidSummary";
            this.Load += new System.EventHandler(this.FrmRptFirstAidSummary_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crtViewer;
        private DCI.HRMS.Reports.PSN.FAR_Total_Service FAR_TotalService1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboSection;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label3;
    }
}