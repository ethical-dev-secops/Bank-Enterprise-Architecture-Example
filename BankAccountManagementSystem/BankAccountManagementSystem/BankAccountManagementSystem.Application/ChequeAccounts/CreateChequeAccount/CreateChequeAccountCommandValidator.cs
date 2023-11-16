using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace BankAccountManagementSystem.Application.ChequeAccounts.CreateChequeAccount
{
    public class CreateChequeAccountCommandValidator : AbstractValidator<CreateChequeAccountCommand>
    {
        [IntentManaged(Mode.Merge)]
        public CreateChequeAccountCommandValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            RuleFor(v => v.Type)
                .NotNull()
                .IsInEnum();

            RuleFor(v => v.CurrencyIsoCode)
                .NotNull()
                .MaximumLength(3);
        }
    }
}