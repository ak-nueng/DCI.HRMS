using System;

namespace DCIBizPro.Util
{
	/// <summary>
	/// แปลงค่าเวลาเป็นหน่วยนาที
	/// </summary>
	public class TimeConverter
	{
        private TimeConverter() { }
		public static double toMinutes(double num)
		{
			double min = 0.0;
			double hrs = 0.0;

			hrs = System.Math.Floor(num);
			min = num%1;
			
			if(min >= 60)
			{
				hrs = hrs + System.Math.Floor(min/60);
				min = min%60;
			}

			min = (hrs*60) + (min * 100);
			return min;
		}

		public static double toHours(int mins)
		{
			double hrs = 0.0;
			double num = 0.0;

			hrs = System.Math.Floor(Convert.ToDouble(mins/60));
			num = mins%60;

			hrs = hrs + (num/100);

			return hrs;
		}
	}
}
