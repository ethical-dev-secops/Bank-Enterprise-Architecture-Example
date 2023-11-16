using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace BankAccountManagementSystem.Application.AccountHolders.DeleteAccountHolder
{
    public class DeleteAccountHolderCommandValidator : AbstractValidator<DeleteAccountHolderCommand>
    {
        [IntentManaged(Mode.Merge)]
        public DeleteAccountHolderCommandValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
        }
    }
}