using Stylet;

namespace CreditCalc.ViewModels
{
	public class MainViewModel : Conductor<IScreen>.Collection.OneActive
	{
		public MainViewModel(System.Collections.Generic.List<TabItem> Tabs)
		{
			DisplayName = "simple credit calculator";
			Tabs.Reverse();
			Items.AddRange(Tabs);
		}
	}
}
