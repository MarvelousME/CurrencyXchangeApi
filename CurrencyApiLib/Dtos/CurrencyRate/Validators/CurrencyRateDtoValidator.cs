using FluentValidation;

namespace CurrencyApiLib.Dtos.CurrencyRate.Validators
{
    public class CurrencyRateDtoValidator : AbstractValidator<DeleteCurrencyRateDto>
    {
        public CurrencyRateDtoValidator()
        {
            RuleFor(x => x.Id).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
        }
    }
}
