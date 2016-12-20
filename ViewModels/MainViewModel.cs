using System.Windows.Input;
using Stylet;

namespace CreditCalc.ViewModels
{
	public class MainViewModel : Conductor<IScreen>.Collection.OneActive
	{
		private InputDataViewModel Tab1;
		private ResultViewModel Tab2;

		public MainViewModel(InputDataViewModel Tab1)
		{
			this.Tab1 = Tab1;
			//this.Tab2 = Tab2;
			DisplayName = "simple credit calculator";
			this.Items.Add(this.Tab1);
			//this.Items.Add(this.Tab2);
			this.ActiveItem = this.Tab1;
			this.Tab1.IsCalculated += Tab1_Calculated;
			this.Tab1.IsReseted += Tab1_IsReseted;
		}

		private void Tab1_IsReseted(object sender, System.EventArgs e)
		{
			
		}

		private void Tab1_Calculated(object sender, ResultEventArgs e)
		{
			this.Tab2 = new ResultViewModel(e.arg);
			this.Items.Add(this.Tab2);
			this.ActiveItem = this.Tab2;
		}
	}
}
