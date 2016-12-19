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
		public double creditSum, creditLength, monthFee, yearFee, totalMonthPay;

		public void TotalMonthPay()
		{
			totalMonthPay = Math.Round(creditSum * (YearFee() + YearFee() / (Math.Pow(1 + YearFee(), creditLength) - 1 )), 2);
		}

		public double YearFee()
		{
			return Math.Round(yearFee / 12, 2);
		}

		public double remCreditSum()
		{
			creditSum = creditSum - MonthPay();
			return Math.Round(creditSum, 2);
		}

		public double MonthFee()
		{
			return Math.Round(remCreditSum() * YearFee(), 2);
		}

		public double MonthPay()
		{
			return Math.Round(totalMonthpay - MonthFee(), 2);
		}

		public double ServiceFee(double creditSum, double creditLength, double monthFee)
		{
			return Math.Round(creditSum * monthFee * 0.01 * creditLength, 2);
		}

		

		
		
		
	}
}
