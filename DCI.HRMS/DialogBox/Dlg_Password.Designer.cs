namespace DCI.HRMS.DialogBox
{
    partial class Dlg_Password
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
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonTextBox1 = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButton2 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.SuspendLayout();
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel1.Location = new System.Drawing.Point(7, 12);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel1.Size = new System.Drawing.Size(125, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Text = "กรุณาป้อน Password :";
            this.kryptonLabel1.Values.ExtraText = "";
            this.kryptonLabel1.Values.Image = null;
            this.kryptonLabel1.Values.Text = "กรุณาป้อน Password :";
            // 
            // kryptonTextBox1
            // 
            this.kryptonTextBox1.InputControlStyle = ComponentFactory.Krypton.Toolkit.InputControlStyle.Standalone;
            this.kryptonTextBox1.Location = new System.Drawing.Point(128, 12);
            this.kryptonTextBox1.Name = "kryptonTextBox1";
            this.kryptonTextBox1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonTextBox1.PasswordChar = '*';
            this.kryptonTextBox1.Size = new System.Drawing.Size(172, 23);
            this.kryptonTextBox1.TabIndex = 1;
            this.kryptonTextBox1.TextChanged += new System.EventHandler(this.kryptonTextBox1_TextChanged);
            this.kryptonTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.kryptonTextBox1_KeyDown);
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Standalone;
            this.kryptonButton1.Enabled = false;
            this.kryptonButton1.Location = new System.Drawing.Point(155, 40);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonButton1.Size = new System.Drawing.Size(71, 25);
            this.kryptonButton1.TabIndex = 2;
            this.kryptonButton1.Text = "OK";
            this.kryptonButton1.Values.ExtraText = "";
            this.kryptonButton1.Values.Image = null;
            this.kryptonButton1.Values.ImageStates.ImageCheckedNormal = null;
            this.kryptonButton1.Values.ImageStates.ImageCheckedPressed = null;
            this.kryptonButton1.Values.ImageStates.ImageCheckedTracking = null;
            this.kryptonButton1.Values.Text = "OK";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // kryptonButton2
            // 
            this.kryptonButton2.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Standalone;
            this.kryptonButton2.Location = new System.Drawing.Point(232, 40);
            this.kryptonButton2.Name = "kryptonButton2";
            this.kryptonButton2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonButton2.Size = new System.Drawing.Size(68, 25);
            this.kryptonButton2.TabIndex = 2;
            this.kryptonButton2.Text = "Cancel";
            this.kryptonButton2.Values.ExtraText = "";
            this.kryptonButton2.Values.Image = null;
            this.kryptonButton2.Values.ImageStates.ImageCheckedNormal = null;
            this.kryptonButton2.Values.ImageStates.ImageCheckedPressed = null;
            this.kryptonButton2.Values.ImageStates.ImageCheckedTracking = null;
            this.kryptonButton2.Values.Text = "Cancel";
            this.kryptonButton2.Click += new System.EventHandler(this.kryptonButton2_Click);
            // 
            // Dlg_Password
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 74);
            this.ControlBox = false;
            this.Controls.Add(this.kryptonButton2);
            this.Controls.Add(this.kryptonButton1);
            this.Controls.Add(this.kryptonTextBox1);
            this.Controls.Add(this.kryptonLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Dlg_Password";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dlg_Password";
            this.Load += new System.EventHandler(this.Dlg_Password_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton2;
        public ComponentFactory.Krypton.Toolkit.KryptonTextBox kryptonTextBox1;
    }
}