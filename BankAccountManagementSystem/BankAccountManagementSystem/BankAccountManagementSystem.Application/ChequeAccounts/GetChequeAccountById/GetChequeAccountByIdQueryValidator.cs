using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.QueryValidator", Version = "2.0")]

namespace BankAccountManagementSystem.Application.ChequeAccounts.GetChequeAccountById
{
    public class GetChequeAccountByIdQueryValidator : AbstractValidator<GetChequeAccountByIdQuery>
    {
        [IntentManaged(Mode.Merge)]
        public GetChequeAccountByIdQueryValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
        }
    }
}