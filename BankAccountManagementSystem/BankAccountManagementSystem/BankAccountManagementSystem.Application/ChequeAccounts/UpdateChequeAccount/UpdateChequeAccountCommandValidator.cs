using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace BankAccountManagementSystem.Application.ChequeAccounts.UpdateChequeAccount
{
    public class UpdateChequeAccountCommandValidator : AbstractValidator<UpdateChequeAccountCommand>
    {
        [IntentManaged(Mode.Merge)]
        public UpdateChequeAccountCommandValidator()
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