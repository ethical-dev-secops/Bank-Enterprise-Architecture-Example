using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace BankAccountManagementSystem.Application.AccountHolders.UpdateAccountHolder
{
    public class UpdateAccountHolderCommandValidator : AbstractValidator<UpdateAccountHolderCommand>
    {
        [IntentManaged(Mode.Merge)]
        public UpdateAccountHolderCommandValidator()
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