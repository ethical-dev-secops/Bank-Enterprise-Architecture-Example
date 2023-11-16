using BankAccountManagementSystem.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace BankAccountManagementSystem.Application.AccountHolders.DeleteAccountHolder
{
    public class DeleteAccountHolderCommand : IRequest, ICommand
    {
        public DeleteAccountHolderCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}