using AutoMapper;
using BankAccountManagementSystem.Application.Common.Mappings;
using BankAccountManagementSystem.Domain;
using BankAccountManagementSystem.Domain.Entities;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace BankAccountManagementSystem.Application.SavingsAccounts
{
    public class SavingsAccountDto : IMapFrom<SavingsAccount>
    {
        public SavingsAccountDto()
        {
            CurrencyIsoCode = null!;
        }

        public long Id { get; set; }
        public SavingsAccountType Type { get; set; }
        public string CurrencyIsoCode { get; set; }
        public decimal Balance { get; set; }
        public long AccountHolderId { get; set; }

        public static SavingsAccountDto Create(
            long id,
            SavingsAccountType type,
            string currencyIsoCode,
            decimal balance,
            long accountHolderId)
        {
            return new SavingsAccountDto
            {
                Id = id,
                Type = type,
                CurrencyIsoCode = currencyIsoCode,
                Balance = balance,
                AccountHolderId = accountHolderId
            };
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SavingsAccount, SavingsAccountDto>();
        }
    }
}