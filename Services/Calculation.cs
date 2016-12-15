using System;
using CreditCalc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCalc.Services
{
	class Calculation
	{
		public double MainPay(double creditSum, double creditLength)
		{
			return Math.Round(creditSum / creditLength, 2);
		}

		public double ServiceFee(double creditSum, double creditLength, double monthFee)
		{
			return Math.Round(creditSum * monthFee * 0.01 * creditLength, 2);
		}

		public double YearFee()
		{
			return 0;
		}
		
		public double TotalPay()
		{
			return 0;
		}
	}
}
