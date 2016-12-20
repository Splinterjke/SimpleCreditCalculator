using System;

namespace CreditCalc.Models
{
	public class AnuResults
	{
		public int CreditLength;

		private int _monthNum;
		public int MonthNum
		{
			get	{ return _monthNum;	}
			set
			{
				_monthNum = value;
			}
		}

		private DateTime _date;
		public DateTime Date
		{
			get { return _date; }
			set
			{
				_date = value;
			}
		}

		private double _remCreditSum;
		public double RemCreditSum
		{
			get { return _remCreditSum; }
			set
			{
				_remCreditSum = value - MonthPay;
			}
		}

		private double _monthPay;
		public double MonthPay
		{
			get { return _monthPay; }
			set { _monthPay = TotalMonthPay - MonthFee; }
		}

		private double _monthFee;
		public double MonthFee
		{
			get { return _monthFee; }
			set { _monthFee = RemCreditSum * YearFee; }
		}

		private double _yearFee;
		public double YearFee
		{
			get { return _yearFee; }
			set
			{
				_yearFee = value;
			}
		}

		public double serviceFee
		{
			get;
			set;
		}

		private double _totalMonthPay;		
		public double TotalMonthPay
		{
			get { return _totalMonthPay;  }
			set { _totalMonthPay = RemCreditSum *(YearFee + YearFee / (Math.Pow(1 + YearFee, CreditLength) - 1)); }
		}
	}
}
