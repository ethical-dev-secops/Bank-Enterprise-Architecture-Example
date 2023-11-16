using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.QueryValidator", Version = "2.0")]

namespace BankAccountManagementSystem.Application.AccountHolders.GetAccountHolders
{
    public class GetAccountHoldersQueryValidator : AbstractValidator<GetAccountHoldersQuery>
    {
        [IntentManaged(Mode.Merge)]
        public GetAccountHoldersQueryValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
        }
    }
}