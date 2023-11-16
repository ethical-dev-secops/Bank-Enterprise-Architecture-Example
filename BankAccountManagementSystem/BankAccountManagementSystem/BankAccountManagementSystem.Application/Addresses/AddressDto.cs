using AutoMapper;
using BankAccountManagementSystem.Application.Common.Mappings;
using BankAccountManagementSystem.Domain.Entities;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace BankAccountManagementSystem.Application.Addresses
{
    public class AddressDto : IMapFrom<Address>
    {
        public AddressDto()
        {
            StreetName = null!;
            ErfNumber = null!;
            Suburb = null!;
            City = null!;
            Country = null!;
        }

        public long Id { get; set; }
        public string StreetName { get; set; }
        public string ErfNumber { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public static AddressDto Create(long id, string streetName, string erfNumber, string suburb, string city, string country)
        {
            return new AddressDto
            {
                Id = id,
                StreetName = streetName,
                ErfNumber = erfNumber,
                Suburb = suburb,
                City = city,
                Country = country
            };
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Address, AddressDto>();
        }
    }
}