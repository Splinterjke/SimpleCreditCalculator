using Stylet;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using System.ComponentModel;

namespace CreditCalc.ViewModels
{
	#region ViewModel
	public class InputDataViewModel : Screen
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
		public event System.EventHandler IsCalculated = delegate { };
		public event System.EventHandler IsReseted = delegate { };
		private List<string> properties;
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

		// Start calculation with inputed values
		public void Calculate()
		{
			if(DiffPay)
			{
				Services.AnuCalculation calculation = new Services.AnuCalculation();
				Models.Results result = new Models.Results();				
				System.Diagnostics.Debug.WriteLine("Считаю дифф. платежи");
				ResultViewModel r = new ResultViewModel();
				this.IsCalculated(this, new System.EventArgs());
				return;
			}

			if(AnnuPay)
			{
				System.Diagnostics.Debug.WriteLine("Считаю аннуит. платежи");
				return;
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
			this.IsReseted(this, new System.EventArgs());
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

	#region ValidationRules
	// Entity validator for ViewModel
	class InputDataViewModelValidator : AbstractValidator<InputDataViewModel>
	{
		public InputDataViewModelValidator()
		{
			CascadeMode = CascadeMode.StopOnFirstFailure;

			RuleFor(x => x.CreditSum).NotEmpty().WithMessage("{PropertyName} не может быть пустым").Matches(@"^\d+$").WithMessage("Разрешены только числовые значения");
			RuleFor(x => x.YearFee).NotEmpty().WithMessage("Поле не может быть пустым").Matches(@"^\d+(\.\d{1,2})?$").WithMessage("Разрешены только числовые значения");
			RuleFor(x => x.CreditLength).NotEmpty().WithMessage("Поле не может быть пустым").Matches(@"^\d+$").WithMessage("Разрешены только числовые значения");
			RuleFor(x => x.MonthFee).NotEmpty().WithMessage("Поле не может быть пустым").Matches(@"^\d+(\.\d{1,2})?$").WithMessage("Разрешены только числовые значения");
		}
	}
	#endregion
	
}
