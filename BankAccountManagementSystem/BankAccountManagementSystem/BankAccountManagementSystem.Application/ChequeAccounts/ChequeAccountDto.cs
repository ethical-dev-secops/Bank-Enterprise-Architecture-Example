using AutoMapper;
using BankAccountManagementSystem.Application.Common.Mappings;
using BankAccountManagementSystem.Domain;
using BankAccountManagementSystem.Domain.Entities;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace BankAccountManagementSystem.Application.ChequeAccounts
{
    public class ChequeAccountDto : IMapFrom<ChequeAccount>
    {
        public ChequeAccountDto()
        {
            CurrencyIsoCode = null!;
        }

        public long Id { get; set; }
        public ChequeAccountType Type { get; set; }
        public string CurrencyIsoCode { get; set; }
        public decimal Balance { get; set; }
        public long AccountHolderId { get; set; }

        public static ChequeAccountDto Create(
            long id,
            ChequeAccountType type,
            string currencyIsoCode,
            decimal balance,
            long accountHolderId)
        {
            return new ChequeAccountDto
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
            profile.CreateMap<ChequeAccount, ChequeAccountDto>();
        }
    }
}