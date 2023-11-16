using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Entities.DomainEnum", Version = "1.0")]

namespace BankAccountManagementSystem.Domain.Enums
{
    public enum ChequeAccountType
    {
        CommercialChequeAccount,
        EverydayTransactionalAccount,
        TravellersChequeAccount
    }
}