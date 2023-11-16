using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace BankAccountManagementSystem.Application.AccountHolders.CreateAccountHolder
{
    public class CreateAccountHolderCommandValidator : AbstractValidator<CreateAccountHolderCommand>
    {
        [IntentManaged(Mode.Merge)]
        public CreateAccountHolderCommandValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            RuleFor(v => v.FirstName)
                .NotNull()
                .MaximumLength(30);

            RuleFor(v => v.LastName)
                .NotNull()
                .MaximumLength(30);

            RuleFor(v => v.IdentityNumber)
                .NotNull()
                .MaximumLength(30);

            RuleFor(v => v.PassportNumber)
                .NotNull()
                .MaximumLength(30);
        }
    }
}