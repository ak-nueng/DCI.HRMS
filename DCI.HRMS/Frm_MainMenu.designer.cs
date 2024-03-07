namespace DCI.HRMS
{
    partial class Frm_MainMenu
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_MainMenu));
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lvMenu = new System.Windows.Forms.ListView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.btnExit = new System.Windows.Forms.Button();
            this.btnOpenMenu = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lvSubMenu = new System.Windows.Forms.ListView();
            this.colID = new System.Windows.Forms.ColumnHeader();
            this.colName = new System.Windows.Forms.ColumnHeader();
            this.colDescr = new System.Windows.Forms.ColumnHeader();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.DarkRed;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Franklin Gothic Medium", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(777, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = " Database System";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(777, 44);
            this.panel1.TabIndex = 1;
            // 
            // lvMenu
            // 
            this.lvMenu.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.lvMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMenu.FullRowSelect = true;
            this.lvMenu.GridLines = true;
            this.lvMenu.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvMenu.LargeImageList = this.imageList;
            this.lvMenu.Location = new System.Drawing.Point(0, 0);
            this.lvMenu.MultiSelect = false;
            this.lvMenu.Name = "lvMenu";
            this.lvMenu.Size = new System.Drawing.Size(164, 389);
            this.lvMenu.TabIndex = 0;
            this.lvMenu.UseCompatibleStateImageBehavior = false;
            this.lvMenu.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvMenu_MouseDoubleClick);
            this.lvMenu.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvMenu_MouseClick);
            this.lvMenu.SelectedIndexChanged += new System.EventHandler(this.lvMenu_SelectedIndexChanged);
            this.lvMenu.SizeChanged += new System.EventHandler(this.lvMenu_SizeChanged);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "file_manager.bmp");
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnExit.Location = new System.Drawing.Point(368, 12);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(116, 32);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "E&xit Program";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnOpenMenu
            // 
            this.btnOpenMenu.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnOpenMenu.Location = new System.Drawing.Point(248, 12);
            this.btnOpenMenu.Name = "btnOpenMenu";
            this.btnOpenMenu.Size = new System.Drawing.Size(116, 32);
            this.btnOpenMenu.TabIndex = 6;
            this.btnOpenMenu.Text = "&Open Menu";
            this.btnOpenMenu.UseVisualStyleBackColor = true;
            this.btnOpenMenu.Click += new System.EventHandler(this.btnOpenMenu_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnExit);
            this.panel4.Controls.Add(this.btnOpenMenu);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(122, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(489, 49);
            this.panel4.TabIndex = 8;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 44);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lvMenu);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Size = new System.Drawing.Size(777, 389);
            this.splitContainer1.SplitterDistance = 164;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 9;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lvSubMenu);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(611, 340);
            this.panel3.TabIndex = 10;
            // 
            // lvSubMenu
            // 
            this.lvSubMenu.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.lvSubMenu.AllowColumnReorder = true;
            this.lvSubMenu.BackColor = System.Drawing.Color.SeaShell;
            this.lvSubMenu.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colID,
            this.colName,
            this.colDescr});
            this.lvSubMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSubMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lvSubMenu.FullRowSelect = true;
            this.lvSubMenu.GridLines = true;
            this.lvSubMenu.Location = new System.Drawing.Point(0, 0);
            this.lvSubMenu.MultiSelect = false;
            this.lvSubMenu.Name = "lvSubMenu";
            this.lvSubMenu.Size = new System.Drawing.Size(611, 340);
            this.lvSubMenu.StateImageList = this.imageList1;
            this.lvSubMenu.TabIndex = 0;
            this.lvSubMenu.UseCompatibleStateImageBehavior = false;
            this.lvSubMenu.View = System.Windows.Forms.View.Details;
            this.lvSubMenu.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvSubMenu_MouseDoubleClick);
            // 
            // colID
            // 
            this.colID.Text = "ID";
            this.colID.Width = 90;
            // 
            // colName
            // 
            this.colName.Text = "Menu";
            this.colName.Width = 300;
            // 
            // colDescr
            // 
            this.colDescr.Text = "Description";
            this.colDescr.Width = 450;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "view_choose.bmp");
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 340);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(611, 49);
            this.panel2.TabIndex = 9;
            // 
            // Frm_MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 433);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Name = "Frm_MainMenu";
            this.Text = "Main Menu";
            this.Load += new System.EventHandler(this.Frm_MainMenu_Load);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnOpenMenu;
        private System.Windows.Forms.ListView lvMenu;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListView lvSubMenu;
        private System.Windows.Forms.ColumnHeader colID;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colDescr;
        private System.Windows.Forms.ImageList imageList1;
    }
}