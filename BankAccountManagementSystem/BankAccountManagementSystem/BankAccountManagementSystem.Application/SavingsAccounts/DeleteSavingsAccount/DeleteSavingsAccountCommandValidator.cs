using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace BankAccountManagementSystem.Application.SavingsAccounts.DeleteSavingsAccount
{
    public class DeleteSavingsAccountCommandValidator : AbstractValidator<DeleteSavingsAccountCommand>
    {
        [IntentManaged(Mode.Merge)]
        public DeleteSavingsAccountCommandValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
        }
    }
}