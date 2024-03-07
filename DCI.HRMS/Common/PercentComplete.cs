using System.Collections;

namespace DCI.HRMS.Common
{
	/// <summary>
	/// Summary description for DataValue.
	/// </summary>
	public class PercentComplete
	{
		private double m_Num = 0.0;
		private ArrayList m_ObsCol = new ArrayList();

		public PercentComplete()
		{
		}

		public double Value
		{
			get { return this.m_Num; }
			set
			{
				this.m_Num = value;
				this.Notify();
			}
		}

		public override string ToString()
		{
			return this.m_Num.ToString() + "%";
		}

		public void Attach(IObserver o)
		{
			this.m_ObsCol.Add(o);
		}

		public void Detach(IObserver o)
		{
			this.m_ObsCol.Remove(o);
		}

		public void Notify()
		{
			foreach (IObserver o in this.m_ObsCol)
			{
				o.Update(this);
			}
		}
	}

	public class Percentage
	{
		private double val = 0.0;
		private string msg = string.Empty;
		private ArrayList obsClientList = new ArrayList();

		public Percentage()
		{
		}

		public double Value
		{
			get { return this.val; }
			set
			{
				this.val = value;
				this.Notify();
			}
		}

		public string Message
		{
			get{ return this.msg; }
			set{ this.msg = value; }
		}

		public override string ToString()
		{
			return this.val.ToString() + "%";
		}

		public void Attach(IObserver o)
		{
			this.obsClientList.Add(o);
		}

		public void Detach(IObserver o)
		{
			this.obsClientList.Remove(o);
		}

		public void Notify()
		{
			foreach (IObserver o in this.obsClientList)
			{
				o.Update(this);
			}
		}
	}
}