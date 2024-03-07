using System;
using System.Collections;

namespace DCIBizPro.Util.Data
{
	public interface IMonitor
	{
		void update(object obj);
	}

	public class ProgressMeter
	{
		private int cur = 0;
		private int max = 0;
		private ArrayList monitors = new ArrayList();

		public ProgressMeter()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public int Value
		{
			get{ return this.cur; }
			set
			{
				this.cur = value;
				foreach(IMonitor monitor in monitors)
				{
					monitor.update(this);
				}
			}
		}
		public int Maximum
		{
			get{ return this.max; }
			set{ this.max = value; }
		}
		
		public void Attach(IMonitor o)
		{
			this.monitors.Add(o);
		}

		public void Detach(IMonitor o)
		{
			this.monitors.Remove(o);
		}

		public int PercentComplete
		{
			get
			{
				return Convert.ToInt32((this.cur/this.max)*100);
			}
		}

		public override string ToString()
		{
			return this.PercentComplete.ToString("##0%");
		}

	}
}
