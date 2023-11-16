using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.QueryValidator", Version = "2.0")]

namespace BankAccountManagementSystem.Application.SavingsAccounts.GetSavingsAccountById
{
    public class GetSavingsAccountByIdQueryValidator : AbstractValidator<GetSavingsAccountByIdQuery>
    {
        [IntentManaged(Mode.Merge)]
        public GetSavingsAccountByIdQueryValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
        }
    }
}