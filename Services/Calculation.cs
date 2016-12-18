using System;
using CreditCalc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCalc.Services
{
	class AnuCalculation
	{
		public double TotalMonthPay(double creditSum, double creditLength, double yearFee)
		{
			return Math.Round(creditSum * (yearFee +  yearFee / (Math.Pow(1 + yearFee, creditLength) - 1 )), 2);
		}

		public double ServiceFee(double creditSum, double creditLength, double monthFee)
		{
			return Math.Round(creditSum * monthFee * 0.01 * creditLength, 2);
		}

		public double MonthFee(double remCreditSum, double yearFee)
		{
			return Math.Round(remCreditSum * yearFee / 12, 2);
		}

		public double YearFee(double yearFee)
		{
			return Math.Round(yearFee/12, 2);
		}
		
		public double MonthPay(double totalMonthpay, double monthFee)
		{
			return Math.Round(totalMonthpay - monthFee, 2);
		}
	}
}
