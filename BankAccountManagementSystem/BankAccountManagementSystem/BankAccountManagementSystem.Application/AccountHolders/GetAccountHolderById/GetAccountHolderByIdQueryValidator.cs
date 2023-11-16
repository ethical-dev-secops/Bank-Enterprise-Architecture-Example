using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.QueryValidator", Version = "2.0")]

namespace BankAccountManagementSystem.Application.AccountHolders.GetAccountHolderById
{
    public class GetAccountHolderByIdQueryValidator : AbstractValidator<GetAccountHolderByIdQuery>
    {
        [IntentManaged(Mode.Merge)]
        public GetAccountHolderByIdQueryValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
        }
    }
}