using Stylet;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using System.ComponentModel;

namespace CreditCalc.ViewModels
{
	public class InputDataViewModel : Screen
	{
		public string CreditSum { get; set; }
		public string YearFee { get; set; }
		public string CreditLength { get; set; }
		public string MonthFee { get; set; }
		public System.DateTime StartDate { get; set; }
		private List<string> strings;

		public InputDataViewModel(IModelValidator<InputDataViewModel> validator) : base(validator)
		{
			DisplayName = "ДАННЫЕ";
			StartDate = System.DateTime.Today;			
		}

		protected override void OnValidationStateChanged(IEnumerable<string> changedProperties)
		{	
			base.OnValidationStateChanged(changedProperties);
			// Fody can't weave other assemblies, so we have to manually raise this
			this.NotifyOfPropertyChange(() => this.CanCalculate);
			this.NotifyOfPropertyChange(() => this.CanReset);
		}

		public bool CanCalculate
		{
			get { return !this.HasErrors && checkFields("all"); }
		}

		public void Calculate()
		{

		}

		public bool CanReset
		{
			get {return checkFields("one");}			
		}

		public void Reset()
		{
			
		}

		private bool checkFields(string mode)
		{
			strings = new List<string> { CreditSum, YearFee, CreditLength, MonthFee };
			if (mode == "one")
				return strings.Exists(x => !string.IsNullOrEmpty(x));
			if (mode == "all")
				return strings.All(x => !string.IsNullOrEmpty(x));
			return false;
		}
	}

	public class InputDataViewModelValidator : AbstractValidator<InputDataViewModel>
	{
		public InputDataViewModelValidator()
		{
			RuleFor(x => x.CreditSum).NotEmpty().WithMessage("Поле не может быть пустым").Matches(@"^\d+$").WithMessage("Разрешены только числовые значения");
			RuleFor(x => x.YearFee).NotEmpty().WithMessage("Поле не может быть пустым").Matches(@"^\d+$").WithMessage("Разрешены только числовые значения").Length(2).WithMessage("Разрешены только двухзначные значения");
			RuleFor(x => x.CreditLength).NotEmpty().WithMessage("Поле не может быть пустым").Matches(@"^\d+$").Length(2).WithMessage("Сумма кредита must contain a number");
			RuleFor(x => x.MonthFee).NotEmpty().Matches(@"^\d+$").WithMessage("{PropertyName} must be a valid number");			
		}
	}
}
