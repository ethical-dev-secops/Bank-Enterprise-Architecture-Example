using BankAccountManagementSystem.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace BankAccountManagementSystem.Application.AccountHolders.UpdateAccountHolder
{
    public class UpdateAccountHolderCommand : IRequest, ICommand
    {
        public UpdateAccountHolderCommand(long id,
            string firstName,
            string lastName,
            string identityNumber,
            string passportNumber,
            long addressId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            IdentityNumber = identityNumber;
            PassportNumber = passportNumber;
            AddressId = addressId;
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string PassportNumber { get; set; }
        public long AddressId { get; set; }
    }
}