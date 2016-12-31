using System;

namespace CreditCalc.Models
{
	public struct AnuResults
	{
		public int MonthNum { get; set; }

		public DateTime Date { get; set; }

		public double TotalMonthPay { get; set; }

		public double MonthPay { get; set; }

		public double RemCreditSum { get; set; }

		public double MonthFee { get; set; }

		public double ServiceFee { get; set; }
	}

	public static class AnuCalculation
	{
		public static int CreditLength { get; set; }

		public static int CreditSum { get; set; }

		private static double _totalMonthPay;
		public static double TotalMonthPay
		{
			get { return _totalMonthPay; }
			set
			{
				_totalMonthPay = value * (YearFee + YearFee / (Math.Pow(1 + YearFee, CreditLength) - 1));
			}
		}

		public static double RemCreditSum { get; set; }

		private static double _yearFee;
		public static double YearFee
		{
			get { return _yearFee; }
			set { _yearFee = value / 100 / 12; }
		}

		public static double MonthPay
		{
			get { return TotalMonthPay - MonthFee; }
		}		

		public static double MonthFee
		{
			get { return RemCreditSum * YearFee; }
		}
	}
}
