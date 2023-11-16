using BankAccountManagementSystem.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace BankAccountManagementSystem.Application.Addresses.UpdateAddress
{
    public class UpdateAddressCommand : IRequest, ICommand
    {
        public UpdateAddressCommand(long id, string streetName, string erfNumber, string suburb, string city, string country)
        {
            Id = id;
            StreetName = streetName;
            ErfNumber = erfNumber;
            Suburb = suburb;
            City = city;
            Country = country;
        }

        public long Id { get; set; }
        public string StreetName { get; set; }
        public string ErfNumber { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}