using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace BankAccountManagementSystem.Application.Addresses.DeleteAddress
{
    public class DeleteAddressCommandValidator : AbstractValidator<DeleteAddressCommand>
    {
        [IntentManaged(Mode.Merge)]
        public DeleteAddressCommandValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
        }
    }
}