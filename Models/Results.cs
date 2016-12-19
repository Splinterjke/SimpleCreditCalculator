using System;

namespace CreditCalc.Models
{
	public class Results
	{
		public int monthNum { get; set; }
		public DateTime date { get; set; }
		public double credit { get; set; }
		public double monthPay { get; set; }
		public double monthFee { get; set; }
		public double serviceFee { get; set; }
		public double totalMonthPay { get; set; }
	}
}
