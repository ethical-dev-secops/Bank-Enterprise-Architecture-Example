using AutoMapper;
using BankAccountManagementSystem.Application.Common.Mappings;
using BankAccountManagementSystem.Domain.Entities;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace BankAccountManagementSystem.Application.AccountHolders
{
    public class AccountHolderDto : IMapFrom<AccountHolder>
    {
        public AccountHolderDto()
        {
            FirstName = null!;
            LastName = null!;
            IdentityNumber = null!;
            PassportNumber = null!;
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string PassportNumber { get; set; }
        public long AddressId { get; set; }

        public static AccountHolderDto Create(
            long id,
            string firstName,
            string lastName,
            string identityNumber,
            string passportNumber,
            long addressId)
        {
            return new AccountHolderDto
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                IdentityNumber = identityNumber,
                PassportNumber = passportNumber,
                AddressId = addressId
            };
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AccountHolder, AccountHolderDto>();
        }
    }
}