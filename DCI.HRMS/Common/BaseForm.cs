using System;
using System.Drawing;
using System.Windows.Forms;
using DCI.Security.Model;
//using DCI.DTO.SM;

namespace DCI.HRMS.Common
{
	/// <summary>
	/// Summary description for BaseForm.
	/// </summary>
	public class BaseForm : Form
	{
		private string _GUID;
		private string _Type;
		private ActionStatus _actionStatus = ActionStatus.None;
		private PermissionInfo _perms = new PermissionInfo();
		private bool _isOpened = false;

		public BaseForm()
		{
		}

		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseForm));
            this.SuspendLayout();
            // 
            // BaseForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BaseForm";
            this.Load += new System.EventHandler(this.BaseForm_Load);
            this.ResumeLayout(false);

		}

		private void BaseForm_Load(object sender, EventArgs e)
		{
		}

		public string Key
		{
			get { return this._GUID; }
			set { this._GUID = value; }
		}

		public string FormType
		{
			get { return this._Type; }
			set { this._Type = value; }
		}

		public ActionStatus ActionStatus
		{
			get { return this._actionStatus; }
			set
			{
				this._actionStatus = value;
				try
				{
					if (this.MdiParent is IFormParentAction)
					{
						IFormParentAction frm = (IFormParentAction) this.MdiParent;
						frm.controlActionFlow(this);
					}
				}
				catch
				{
				}
			}
		}

		public PermissionInfo PermissionInfo
		{
			get { return this._perms; }
			set { this._perms = value; }
		}

		public bool IsOpened
		{
			get { return this._isOpened; }
			set { this._isOpened = value; }
		}
	}
}