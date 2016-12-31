using CreditCalc.Models;
using Stylet;

namespace CreditCalc.ViewModels
{
	public class ResultViewModel : Screen, TabItem
	{
		public bool IsEnabled { get; set; }
		public BindableCollection<AnuResults> AnuResults { get; set; }

		public ResultViewModel()
		{
			DisplayName = "РЕЗУЛЬТАТ";
			IsEnabled = false;
		}

		protected override void OnActivate()
		{
			base.OnActivate();
			NotifyOfPropertyChange(() => AnuResults);
		}
	}
}
