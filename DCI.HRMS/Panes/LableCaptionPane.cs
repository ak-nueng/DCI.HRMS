using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace DCI.HRMS.Panes
{
	///<summary>
	/// Custom control that draws the caption for each pane. Contains an active 
	/// state and draws the caption different for each state. Caption is drawn
	/// with a gradient fill and antialias font.
	///</summary>
	public class LabelCaptionPane : UserControl
	{
		//Consist Value
		private class Consts
		{
			public const int DefaultHeight = 20;
			public const string DefaultFontName = "arial";
			public const int DefaultFontSize = 9;
			public const int PosOffset = 4;
		}

		# region Internal Memebers

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		private bool _active = false;
		private bool _antiAlias = false;
		private bool _allowActive = false;

		private Color _colorActiveText = Color.Black;
		private Color _colorInactiveText = Color.White;

		private Color _colorActiveLow = Color.FromArgb(255, 165, 78);
		private Color _colorActiveHigh = Color.FromArgb(255, 225, 155);
		private Color _colorInactiveLow = Color.FromArgb(3, 55, 145);
		private Color _colorInactiveHigh = Color.FromArgb(90, 135, 215);

		// gdi objects
		private SolidBrush _brushActiveText;
		private SolidBrush _brushInactiveText;
		private LinearGradientBrush _brushActive;
		private LinearGradientBrush _brushInactive;
		private StringFormat _format;

		# endregion

		# region Public Properties

		public string Caption
		{
			get { return base.Text; }
			set
			{
				base.Text = value;
				this.Invalidate();
			}
		}

		new public string Text
		{
			get { return base.Text; }
			set
			{
				base.Text = value;
				this.Invalidate();
			}
		}

		public bool Active
		{
			get { return this._active; }
			set
			{
				this._active = value;
				this.Invalidate();
			}
		}

		public bool AllowActive
		{
			get { return this._allowActive; }
			set
			{
				this._allowActive = value;
				this.Invalidate();
			}
		}

		public bool AntiAlias
		{
			get { return this._antiAlias; }
			set
			{
				this._antiAlias = value;
				this.Invalidate();
			}
		}

		# endregion

		public LabelCaptionPane() : base()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// set double buffer styles
			this.SetStyle(ControlStyles.DoubleBuffer, true);

			// init the height
			this.Height = Consts.DefaultHeight;

			// format used when drawing the text
			this._format = new StringFormat();
			this._format.FormatFlags = StringFormatFlags.NoWrap;
			this._format.LineAlignment = StringAlignment.Center;
			this._format.Trimming = StringTrimming.EllipsisCharacter;

			this.Font = new Font(Consts.DefaultFontName, Consts.DefaultFontSize, FontStyle.Bold);

			// create gdi objects
			this.ActiveTextColor = this._colorActiveText;
			this.InactiveTextColor = this._colorInactiveText;

			// setting the height above actually does this, but leave
			// in incase change the code (and forget to init the 
			// gradient brushes)
			this.CreateGradientBrushes();
		}

		# region Color Properties

		public Color ActiveTextColor
		{
			get { return this._colorActiveText; }
			set
			{
				if (value.Equals(Color.Empty))
				{
					value = Color.Black;
				}
				this._colorActiveText = value;
				this._brushActiveText = new SolidBrush(this._colorActiveText);
				this.Invalidate();
			}
		}

		public Color InactiveTextColor
		{
			get { return this._colorInactiveText; }
			set
			{
				if (value.Equals(Color.Empty))
				{
					value = Color.Black;
				}
				this._colorInactiveText = value;
				this._brushInactiveText = new SolidBrush(this._colorInactiveText);
				this.Invalidate();
			}
		}


		public Color ActiveGradientLowColor
		{
			get { return this._colorActiveLow; }
			set
			{
				if (value.Equals(Color.Empty))
				{
					value = Color.FromArgb(255, 165, 78);
				}
				this._colorActiveLow = value;
				this.CreateGradientBrushes();
				this.Invalidate();
			}
		}

		public Color ActiveGradientHighColor
		{
			get { return this._colorActiveHigh; }
			set
			{
				if (value.Equals(Color.Empty))
				{
					value = Color.FromArgb(255, 225, 155);
				}
				this._colorActiveHigh = value;
				this.CreateGradientBrushes();
				this.Invalidate();
			}
		}

		public Color InactiveGradientLowColor
		{
			get { return this._colorInactiveLow; }
			set
			{
				if (value.Equals(Color.Empty))
				{
					value = Color.FromArgb(3, 55, 145);
				}
				this._colorInactiveLow = value;
				this.CreateGradientBrushes();
				this.Invalidate();
			}
		}

		public Color InactiveGradientHighColor
		{
			get { return this._colorInactiveHigh; }
			set
			{
				if (value.Equals(Color.Empty))
				{
					value = Color.FromArgb(90, 135, 215);
				}
				this._colorInactiveHigh = value;
				this.CreateGradientBrushes();
				this.Invalidate();
			}
		}

		public SolidBrush TextBrush
		{
			get
			{
				if (this.Active && this.AllowActive)
				{
					return this._brushActiveText;
				}
				else
				{
					return this._brushInactiveText;
				}
			}
		}

		public LinearGradientBrush BackBrush
		{
			get
			{
				if (this.Active && this.AllowActive)
				{
					return this._brushActive;
				}
				else
				{
					return this._brushInactive;
				}
			}
		}


		private void CreateGradientBrushes()
		{
			//can only create brushes when have a width and height
			if (this.Width > 0 && this.Height > 0)
			{
				if (this._brushActive != null)
				{
					this._brushActive.Dispose();
				}
				this._brushActive = new LinearGradientBrush(this.DisplayRectangle
				                                            , this._colorActiveHigh
				                                            , this._colorActiveLow
				                                            , LinearGradientMode.Vertical);

				if (this._brushInactive != null)
				{
					this._brushInactive.Dispose();
				}
				this._brushInactive = new LinearGradientBrush(this.DisplayRectangle
				                                              , this._colorInactiveHigh
				                                              , this._colorInactiveLow
				                                              , LinearGradientMode.Vertical);
			}
		}

		# endregion

		# region Internal Method

		protected override void OnPaint(PaintEventArgs e)
		{
			this.DrawCaption(e.Graphics);
			base.OnPaint(e);
		}

		private void DrawCaption(Graphics g)
		{
			g.FillRectangle(this.BackBrush, this.DisplayRectangle);

			if (this._antiAlias)
			{
				g.TextRenderingHint = TextRenderingHint.AntiAlias;
			}

			RectangleF bounds = new RectangleF(Consts.PosOffset, 0
			                                   , this.DisplayRectangle.Width - Consts.PosOffset
			                                   , this.DisplayRectangle.Height);

			g.DrawString(this.Text, this.Font, this.TextBrush, bounds, this._format);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (this.AllowActive)
			{
				this.Focus();
			}
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			//create the gradient brushes based on the new size
			this.CreateGradientBrushes();
		}

		# endregion

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
			// 
			// PaneCaption
			// 
			this.Name = "PaneCaption";
			this.Size = new System.Drawing.Size(150, 32);
			this.Load += new System.EventHandler(this.PaneCaption_Load);

		}

		#endregion

		private void PaneCaption_Load(object sender, EventArgs e)
		{
		}
	}
}