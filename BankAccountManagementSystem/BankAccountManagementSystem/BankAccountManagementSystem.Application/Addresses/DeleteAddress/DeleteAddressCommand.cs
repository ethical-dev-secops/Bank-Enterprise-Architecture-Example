using BankAccountManagementSystem.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace BankAccountManagementSystem.Application.Addresses.DeleteAddress
{
    public class DeleteAddressCommand : IRequest, ICommand
    {
        public DeleteAddressCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}