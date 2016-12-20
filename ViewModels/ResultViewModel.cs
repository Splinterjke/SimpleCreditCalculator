using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace CreditCalc.ViewModels
{
	public class ResultViewModel : Screen
	{
		public ObservableCollection<Models.AnuResults> AnuResults { get; set; }

		public ResultViewModel(ObservableCollection<Models.AnuResults> anuResults)
		{
			DisplayName = "РЕЗУЛЬТАТ";
			this.AnuResults = anuResults;
		}
	}
}
