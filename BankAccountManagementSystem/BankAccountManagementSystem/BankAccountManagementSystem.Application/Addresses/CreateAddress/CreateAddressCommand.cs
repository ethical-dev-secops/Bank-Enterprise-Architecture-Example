using BankAccountManagementSystem.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace BankAccountManagementSystem.Application.Addresses.CreateAddress
{
    public class CreateAddressCommand : IRequest<long>, ICommand
    {
        public CreateAddressCommand(string streetName, string erfNumber, string suburb, string city, string country)
        {
            StreetName = streetName;
            ErfNumber = erfNumber;
            Suburb = suburb;
            City = city;
            Country = country;
        }

        public string StreetName { get; set; }
        public string ErfNumber { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}