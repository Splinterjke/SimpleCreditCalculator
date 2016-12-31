using Stylet;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using System.ComponentModel;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;

namespace CreditCalc.ViewModels
{
	#region ViewModel
	public class InputDataViewModel : Screen, TabItem
	{
		#region Properties
		[DisplayName("Сумма кредита")]
		public string CreditSum { get; set; }

		[DisplayName("Процентная ставка")]
		public string YearFee { get; set; }

		[DisplayName("Срок погашения")]
		public string CreditLength { get; set; }

		[DisplayName("Ежемесячная комиссия")]
		public string MonthFee { get; set; }

		public System.DateTime StartDate { get; set; }

		public bool DiffPay { get; set; } = true;
		public bool AnnuPay { get; set; }
		#endregion

		#region Variables
		private List<string> properties;
		private Conductor<IScreen>.Collection.OneActive Conductor;
		/// <summary>
		/// Mode 'One' for atleast one property, mode 'All' for all properties
		/// </summary>
		enum Mode { One, All };
		#endregion

		#region Methods
		public InputDataViewModel(IModelValidator<InputDataViewModel> validator) : base(validator)
		{
			DisplayName = "ДАННЫЕ";
			// Setting current date to avoid empty date property
			StartDate = System.DateTime.Today;
		}

		protected override void OnActivate()
		{
			this.ClearAllPropertyErrors();
			Conductor = (this.Parent as Conductor<IScreen>.Collection.OneActive);
			base.OnActivate();
		}

		protected override void OnValidationStateChanged(IEnumerable<string> changedProperties)
		{
			base.OnValidationStateChanged(changedProperties);
			// Manual rasing notifier for Stylet properties
			this.NotifyOfPropertyChange(() => this.CanCalculate);
			this.NotifyOfPropertyChange(() => this.CanReset);
		}

		// Guard property for Calculate method
		public bool CanCalculate
		{
			get { return !HasErrors && CheckFields(Mode.All); }
		}

		// Manual implementation of MetroMahapps dialog message
		public async Task<MessageDialogResult> ShowMessageAsync(string title, string message, MessageDialogStyle dialogStyle)
		{
			var metroWindow = (Application.Current.MainWindow as MetroWindow);
			metroWindow.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Theme;
			metroWindow.MetroDialogOptions.MaximumBodyHeight = 60;
			return await metroWindow.ShowMessageAsync(title, message, dialogStyle, metroWindow.MetroDialogOptions);
		}

		// Start calculation with inputed values
		public async void Calculate()
		{
			try
			{
				if (DiffPay)
				{
					
				}
				if (AnnuPay)
				{
					Models.AnuCalculation.CreditLength = int.Parse(CreditLength);
					Models.AnuCalculation.RemCreditSum = double.Parse(CreditSum);
					Models.AnuCalculation.YearFee = double.Parse(YearFee);
					Models.AnuCalculation.TotalMonthPay = double.Parse(CreditSum);
					BindableCollection<Models.AnuResults> results = new BindableCollection<Models.AnuResults>();
					for (int i = 0; i++ < Models.AnuCalculation.CreditLength;)
					{
						results.Add(new Models.AnuResults()
						{
							MonthNum = i,
							Date = StartDate.AddMonths(i),
							RemCreditSum = System.Math.Round(Models.AnuCalculation.RemCreditSum, 2),
							MonthPay = System.Math.Round(Models.AnuCalculation.MonthPay, 2),
							MonthFee = System.Math.Round(Models.AnuCalculation.MonthFee, 2),
							TotalMonthPay = System.Math.Round(Models.AnuCalculation.TotalMonthPay, 2)
						});
						Models.AnuCalculation.RemCreditSum = Models.AnuCalculation.RemCreditSum - Models.AnuCalculation.MonthPay;
					}
					var resultTab = Conductor.Items.ElementAt(Conductor.Items.IndexOf(this) + 1);
					(resultTab as ResultViewModel).IsEnabled = true;
					(resultTab as ResultViewModel).AnuResults = results;
					Conductor.ActiveItem = resultTab;
					return;
				}
			}
			catch (System.Exception ex)
			{
				await ShowMessageAsync("Error", ex.ToString(), MessageDialogStyle.Affirmative);
			}
		}

		// Guard property for Reset method
		public bool CanReset
		{
			get { return CheckFields(Mode.One); }
		}

		// Reset all properties
		public void Reset()
		{
			CreditSum = string.Empty;
			CreditLength = string.Empty;
			YearFee = string.Empty;
			MonthFee = string.Empty;
			StartDate = System.DateTime.Today;
			var resultTab = Conductor.Items.ElementAt(Conductor.Items.IndexOf(this) + 1);
			(resultTab as ResultViewModel).IsEnabled = false;
			this.ClearAllPropertyErrors();
		}

		/// <summary>
		/// Check properties for Null or Empty values
		/// </summary>
		/// <param name="mode"></param>
		private bool CheckFields(Mode mode)
		{
			properties = new List<string> { CreditSum, CreditLength, YearFee, MonthFee };
			if (mode == Mode.One)
				return properties.Any(x => !string.IsNullOrEmpty(x));
			if (mode == Mode.All)
				return properties.All(x => !string.IsNullOrEmpty(x));
			return false;
		}
		#endregion
	}
	#endregion

	#region Validator
	// Entity validator for ViewModel
	class InputDataViewModelValidator : AbstractValidator<InputDataViewModel>
	{
		public InputDataViewModelValidator()
		{
			CascadeMode = CascadeMode.StopOnFirstFailure;

			RuleFor(x => x.CreditSum).NotEmpty().WithMessage("{PropertyName} не может быть пустым").Matches(@"^\d+$").WithMessage("Разрешены только числовые значения");
			RuleFor(x => x.YearFee).NotEmpty().WithMessage("{PropertyName} не может быть пустым").Matches(@"^\d{1,2}(\.\d{1,2})?$").WithMessage("Только двухзначные нецелочисленные значения");
			RuleFor(x => x.CreditLength).NotEmpty().WithMessage("{PropertyName} не может быть пустым").Matches(@"^\d+$").WithMessage("Разрешены только числовые значения");
			RuleFor(x => x.MonthFee).NotEmpty().WithMessage("{PropertyName} не может быть пустым").Matches(@"^\d{1,2}(\.\d{1,2})?$").WithMessage("Только двухзначные нецелочисленные значения");
		}
	}
	#endregion

}
