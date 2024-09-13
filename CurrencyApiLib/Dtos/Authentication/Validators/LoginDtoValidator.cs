using CurrencyApiInfrastructure.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CurrencyApiLib.Dtos.Authentication.Validators
{
    /// <summary>
    /// The login data transfer object validator.
    /// </summary>
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginDtoValidator"/> class.
        /// </summary>
        /// <param name="localizer">The localizer.</param>
        public LoginDtoValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.UserName).Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage(_ => localizer["User_Name_Required"])
                .NotEmpty()
                .WithMessage(_ => localizer["User_Name_Required"]);
            RuleFor(x => x.Password).Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage(_ => localizer["Password_Required"])
                .NotEmpty()
                .WithMessage(_ => localizer["Password_Required"]);
        }
    }
}
