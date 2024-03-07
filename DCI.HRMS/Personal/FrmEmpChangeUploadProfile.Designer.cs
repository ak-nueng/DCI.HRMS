namespace DCI.HRMS.Personal
{
    partial class FrmEmpChangeUploadProfile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEmpChangeUploadProfile));
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.pnData = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.dgvData = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.kryptonHeaderGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.btnLoad = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnChoseFile = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.lblBrowser = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.ucl_ActionControl1 = new DCI.HRMS.Controls.Ucl_ActionControl();
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnData.Panel)).BeginInit();
            this.pnData.Panel.SuspendLayout();
            this.pnData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).BeginInit();
            this.kryptonHeaderGroup1.Panel.SuspendLayout();
            this.kryptonHeaderGroup1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.pnData);
            this.kryptonPanel.Controls.Add(this.kryptonHeaderGroup1);
            this.kryptonPanel.Controls.Add(this.ucl_ActionControl1);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonPanel.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.kryptonPanel.Size = new System.Drawing.Size(1023, 531);
            this.kryptonPanel.TabIndex = 0;
            // 
            // pnData
            // 
            this.pnData.CollapseTarget = ComponentFactory.Krypton.Toolkit.HeaderGroupCollapsedTarget.CollapsedToPrimary;
            this.pnData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnData.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.pnData.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.pnData.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Primary;
            this.pnData.HeaderStyleSecondary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.pnData.HeaderVisiblePrimary = false;
            this.pnData.Location = new System.Drawing.Point(0, 201);
            this.pnData.Name = "pnData";
            this.pnData.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            // 
            // pnData.Panel
            // 
            this.pnData.Panel.Controls.Add(this.dgvData);
            this.pnData.Size = new System.Drawing.Size(1023, 330);
            this.pnData.TabIndex = 3;
            this.pnData.Text = "Heading";
            this.pnData.ValuesPrimary.Description = "";
            this.pnData.ValuesPrimary.Heading = "Heading";
            this.pnData.ValuesPrimary.Image = ((System.Drawing.Image)(resources.GetObject("pnData.ValuesPrimary.Image")));
            this.pnData.ValuesSecondary.Description = "";
            this.pnData.ValuesSecondary.Heading = "Description";
            this.pnData.ValuesSecondary.Image = null;
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.GridStyles.Style = ComponentFactory.Krypton.Toolkit.DataGridViewStyle.List;
            this.dgvData.GridStyles.StyleBackground = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundList;
            this.dgvData.GridStyles.StyleColumn = ComponentFactory.Krypton.Toolkit.GridStyle.List;
            this.dgvData.GridStyles.StyleDataCells = ComponentFactory.Krypton.Toolkit.GridStyle.List;
            this.dgvData.GridStyles.StyleRow = ComponentFactory.Krypton.Toolkit.GridStyle.List;
            this.dgvData.Location = new System.Drawing.Point(0, 0);
            this.dgvData.Name = "dgvData";
            this.dgvData.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.dgvData.Size = new System.Drawing.Size(1021, 307);
            this.dgvData.StateCommon.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundList;
            this.dgvData.TabIndex = 5;
            // 
            // kryptonHeaderGroup1
            // 
            this.kryptonHeaderGroup1.CollapseTarget = ComponentFactory.Krypton.Toolkit.HeaderGroupCollapsedTarget.CollapsedToPrimary;
            this.kryptonHeaderGroup1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeaderGroup1.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonHeaderGroup1.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.kryptonHeaderGroup1.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Primary;
            this.kryptonHeaderGroup1.HeaderStyleSecondary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.kryptonHeaderGroup1.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup1.Location = new System.Drawing.Point(0, 45);
            this.kryptonHeaderGroup1.Name = "kryptonHeaderGroup1";
            this.kryptonHeaderGroup1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            // 
            // kryptonHeaderGroup1.Panel
            // 
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.btnLoad);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.btnChoseFile);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.lblBrowser);
            this.kryptonHeaderGroup1.Size = new System.Drawing.Size(1023, 156);
            this.kryptonHeaderGroup1.TabIndex = 2;
            this.kryptonHeaderGroup1.Text = "Heading";
            this.kryptonHeaderGroup1.ValuesPrimary.Description = "";
            this.kryptonHeaderGroup1.ValuesPrimary.Heading = "Heading";
            this.kryptonHeaderGroup1.ValuesPrimary.Image = ((System.Drawing.Image)(resources.GetObject("kryptonHeaderGroup1.ValuesPrimary.Image")));
            this.kryptonHeaderGroup1.ValuesSecondary.Description = "";
            this.kryptonHeaderGroup1.ValuesSecondary.Heading = "Description";
            this.kryptonHeaderGroup1.ValuesSecondary.Image = null;
            // 
            // btnLoad
            // 
            this.btnLoad.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Standalone;
            this.btnLoad.Location = new System.Drawing.Point(23, 73);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.btnLoad.Size = new System.Drawing.Size(192, 25);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Load";
            this.btnLoad.Values.ExtraText = "";
            this.btnLoad.Values.Image = null;
            this.btnLoad.Values.ImageStates.ImageCheckedNormal = null;
            this.btnLoad.Values.ImageStates.ImageCheckedPressed = null;
            this.btnLoad.Values.ImageStates.ImageCheckedTracking = null;
            this.btnLoad.Values.Text = "Load";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnChoseFile
            // 
            this.btnChoseFile.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Standalone;
            this.btnChoseFile.Location = new System.Drawing.Point(23, 15);
            this.btnChoseFile.Name = "btnChoseFile";
            this.btnChoseFile.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.btnChoseFile.Size = new System.Drawing.Size(192, 35);
            this.btnChoseFile.TabIndex = 1;
            this.btnChoseFile.Text = "‡≈◊Õ°‰ø≈Ï ”À√—∫Õ—æ‚À≈¥";
            this.btnChoseFile.Values.ExtraText = "";
            this.btnChoseFile.Values.Image = global::DCI.HRMS.Properties.Resources.up1;
            this.btnChoseFile.Values.ImageStates.ImageCheckedNormal = null;
            this.btnChoseFile.Values.ImageStates.ImageCheckedPressed = null;
            this.btnChoseFile.Values.ImageStates.ImageCheckedTracking = null;
            this.btnChoseFile.Values.Text = "‡≈◊Õ°‰ø≈Ï ”À√—∫Õ—æ‚À≈¥";
            this.btnChoseFile.Click += new System.EventHandler(this.btnChoseFile_Click);
            // 
            // lblBrowser
            // 
            this.lblBrowser.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.lblBrowser.Location = new System.Drawing.Point(247, 21);
            this.lblBrowser.Name = "lblBrowser";
            this.lblBrowser.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.lblBrowser.Size = new System.Drawing.Size(15, 20);
            this.lblBrowser.TabIndex = 0;
            this.lblBrowser.Text = "-";
            this.lblBrowser.Values.ExtraText = "";
            this.lblBrowser.Values.Image = null;
            this.lblBrowser.Values.Text = "-";
            // 
            // ucl_ActionControl1
            // 
            this.ucl_ActionControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucl_ActionControl1.Location = new System.Drawing.Point(0, 0);
            this.ucl_ActionControl1.Name = "ucl_ActionControl1";
            this.ucl_ActionControl1.Size = new System.Drawing.Size(1023, 45);
            this.ucl_ActionControl1.TabIndex = 1;
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.FileName = "openFileDialog1";
            // 
            // FrmEmpChangeUploadProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 531);
            this.Controls.Add(this.kryptonPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmEmpChangeUploadProfile";
            this.Text = "FrmEmpChangeUploadProfile";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnData.Panel)).EndInit();
            this.pnData.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnData)).EndInit();
            this.pnData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).EndInit();
            this.kryptonHeaderGroup1.Panel.ResumeLayout(false);
            this.kryptonHeaderGroup1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).EndInit();
            this.kryptonHeaderGroup1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;
        private DCI.HRMS.Controls.Ucl_ActionControl ucl_ActionControl1;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnChoseFile;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblBrowser;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnLoad;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog1;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup pnData;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgvData;
    }
}

