using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.QueryValidator", Version = "2.0")]

namespace BankAccountManagementSystem.Application.ChequeAccounts.GetChequeAccounts
{
    public class GetChequeAccountsQueryValidator : AbstractValidator<GetChequeAccountsQuery>
    {
        [IntentManaged(Mode.Merge)]
        public GetChequeAccountsQueryValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
        }
    }
}