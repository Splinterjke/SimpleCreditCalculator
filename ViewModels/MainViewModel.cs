using Stylet;

namespace CreditCalc.ViewModels
{
	public class MainViewModel : Conductor<IScreen>.Collection.OneActive
	{
		public MainViewModel(InputDataViewModel Tab1, ResultViewModel Tab2)
		{
			DisplayName = "simple credit calculator";
			this.Items.Add(Tab1);
			this.Items.Add(Tab2);
			this.ActiveItem = Tab1;
		}
	}	
}
