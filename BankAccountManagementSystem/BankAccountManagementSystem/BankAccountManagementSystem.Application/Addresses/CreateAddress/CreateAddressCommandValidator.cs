using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace BankAccountManagementSystem.Application.Addresses.CreateAddress
{
    public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
    {
        [IntentManaged(Mode.Merge)]
        public CreateAddressCommandValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            RuleFor(v => v.StreetName)
                .NotNull()
                .MaximumLength(30);

            RuleFor(v => v.ErfNumber)
                .NotNull()
                .MaximumLength(30);

            RuleFor(v => v.Suburb)
                .NotNull()
                .MaximumLength(30);

            RuleFor(v => v.City)
                .NotNull()
                .MaximumLength(30);

            RuleFor(v => v.Country)
                .NotNull()
                .MaximumLength(30);
        }
    }
}