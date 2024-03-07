using System;
using System.Globalization;

namespace DCIBizPro.Util.Text
{
	public enum Cultures
	{
		Thai = 1 ,
		US = 2
	}
	/// <summary>
	/// �Ѵ�ٻẺ㹡���ʴ��Ţͧ�����Ż�������ҧ� �� ����Ţ , �ѹ���.
	/// </summary>
	public class StringFormatter
	{
		/// <summary>
		/// �Ѵ�ٻẺ����ʴ��Ţͧ�ѹ���
		/// </summary>
		/// <param name="date">�ѹ���</param>
		/// <param name="format">�ٻẺ����ͧ��� d = Day , M = month , y = year</param>
		/// <param name="culture">�������ͧ����ʴ���</param>
		/// <returns>�ٻẺ���١�ŧ����</returns>
		public static string format(DateTime date , string format , Cultures culture)
		{
			string cultureInfo = "en-US";
			if(culture == Cultures.Thai){cultureInfo = "th-TH";}

			return date.ToString(format , new CultureInfo(cultureInfo));
		}

		/// <summary>
		/// �Ѵ�ٻẺ����ʴ��Ţͧ�ѹ���
		/// </summary>
		/// <param name="date">�ѹ���</param>
		/// <param name="format">�ٻẺ����ͧ��� d = Day , M = month , y = year</param>
		/// <param name="culture">�������ͧ����ʴ��� �� en-US , th-TH</param>
		/// <returns>�ٻẺ���١�ŧ����</returns>
		public static string format(DateTime date , string format , string culture)
		{
			return date.ToString(format , new CultureInfo(culture));
		}

		public static string format(double n)
		{
			string fmt = "{0:###,##0.00}";
			return string.Format(fmt,n);
		}

		public static string format(int n)
		{
			string fmt = "{0:###,##0.##}";
			return string.Format(fmt,n);
		}

		public static string format(long n)
		{
			string fmt = "{0:###,##0.##}";
			return string.Format(fmt,n);
		}
	}
}
