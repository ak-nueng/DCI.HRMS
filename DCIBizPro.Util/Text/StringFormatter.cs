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
	/// จัดรูปแบบในการแสดงผลของข้อมูลประเภทต่างๆ เช่น ตัวเลข , วันที่.
	/// </summary>
	public class StringFormatter
	{
		/// <summary>
		/// จัดรูปแบบการแสดงผลของวันที่
		/// </summary>
		/// <param name="date">วันที่</param>
		/// <param name="format">รูปแบบที่ต้องการ d = Day , M = month , y = year</param>
		/// <param name="culture">ประเภทของการแสดงผล</param>
		/// <returns>รูปแบบที่ถูกแปลงแล้ว</returns>
		public static string format(DateTime date , string format , Cultures culture)
		{
			string cultureInfo = "en-US";
			if(culture == Cultures.Thai){cultureInfo = "th-TH";}

			return date.ToString(format , new CultureInfo(cultureInfo));
		}

		/// <summary>
		/// จัดรูปแบบการแสดงผลของวันที่
		/// </summary>
		/// <param name="date">วันที่</param>
		/// <param name="format">รูปแบบที่ต้องการ d = Day , M = month , y = year</param>
		/// <param name="culture">ประเภทของการแสดงผล เช่น en-US , th-TH</param>
		/// <returns>รูปแบบที่ถูกแปลงแล้ว</returns>
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
