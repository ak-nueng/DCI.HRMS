using System.ComponentModel;
using System.Windows.Forms;

namespace DCI.HRMS.Panes
{
	/// <summary>
	/// Summary description for BasePane.
	/// </summary>
	public class BasePanel : UserControl
    {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public BasePanel()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.SuspendLayout();
            // 
            // BasePanel
            // 
            this.Name = "BasePanel";
            this.Size = new System.Drawing.Size(450, 253);
            this.ResumeLayout(false);

		}

		#endregion
	}
}