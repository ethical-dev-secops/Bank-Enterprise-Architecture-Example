using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace BankAccountManagementSystem.Application.ChequeAccounts.DeleteChequeAccount
{
    public class DeleteChequeAccountCommandValidator : AbstractValidator<DeleteChequeAccountCommand>
    {
        [IntentManaged(Mode.Merge)]
        public DeleteChequeAccountCommandValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
        }
    }
}