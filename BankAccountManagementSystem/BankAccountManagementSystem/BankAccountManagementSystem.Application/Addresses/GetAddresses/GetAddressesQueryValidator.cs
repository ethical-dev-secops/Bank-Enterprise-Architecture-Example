using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.QueryValidator", Version = "2.0")]

namespace BankAccountManagementSystem.Application.Addresses.GetAddresses
{
    public class GetAddressesQueryValidator : AbstractValidator<GetAddressesQuery>
    {
        [IntentManaged(Mode.Merge)]
        public GetAddressesQueryValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
        }
    }
}