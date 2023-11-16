using BankAccountManagementSystem.Application.Common.Interfaces;
using BankAccountManagementSystem.Domain;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace BankAccountManagementSystem.Application.SavingsAccounts.CreateSavingsAccount
{
    public class CreateSavingsAccountCommand : IRequest<long>, ICommand
    {
        public CreateSavingsAccountCommand(SavingsAccountType type,
            string currencyIsoCode,
            decimal balance,
            long accountHolderId)
        {
            Type = type;
            CurrencyIsoCode = currencyIsoCode;
            Balance = balance;
            AccountHolderId = accountHolderId;
        }

        public SavingsAccountType Type { get; set; }
        public string CurrencyIsoCode { get; set; }
        public decimal Balance { get; set; }
        public long AccountHolderId { get; set; }
    }
}