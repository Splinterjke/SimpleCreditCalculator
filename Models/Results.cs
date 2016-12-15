using System;

namespace CreditCalc.Models
{
	public class Results
	{
		public int monthNum { get; set; }
		public DateTime date { get; set; }
		public double credit { get; set; }
		public double mainPay { get; set; }
		public double yearFee { get; set; }
		public double serviceFee { get; set; }
		public double totalPay { get; set; }
	}
}
