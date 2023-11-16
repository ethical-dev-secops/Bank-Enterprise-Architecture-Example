using BankAccountManagementSystem.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace BankAccountManagementSystem.Application.AccountHolders.CreateAccountHolder
{
    public class CreateAccountHolderCommand : IRequest<long>, ICommand
    {
        public CreateAccountHolderCommand(string firstName,
            string lastName,
            string identityNumber,
            string passportNumber,
            long addressId)
        {
            FirstName = firstName;
            LastName = lastName;
            IdentityNumber = identityNumber;
            PassportNumber = passportNumber;
            AddressId = addressId;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string PassportNumber { get; set; }
        public long AddressId { get; set; }
    }
}