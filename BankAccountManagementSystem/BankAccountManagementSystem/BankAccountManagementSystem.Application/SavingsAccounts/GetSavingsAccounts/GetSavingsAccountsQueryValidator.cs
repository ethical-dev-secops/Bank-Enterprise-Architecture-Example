using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.QueryValidator", Version = "2.0")]

namespace BankAccountManagementSystem.Application.SavingsAccounts.GetSavingsAccounts
{
    public class GetSavingsAccountsQueryValidator : AbstractValidator<GetSavingsAccountsQuery>
    {
        [IntentManaged(Mode.Merge)]
        public GetSavingsAccountsQueryValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
        }
    }
}