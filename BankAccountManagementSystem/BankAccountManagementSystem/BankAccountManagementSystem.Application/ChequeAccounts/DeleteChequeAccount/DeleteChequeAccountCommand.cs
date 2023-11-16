using BankAccountManagementSystem.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace BankAccountManagementSystem.Application.ChequeAccounts.DeleteChequeAccount
{
    public class DeleteChequeAccountCommand : IRequest, ICommand
    {
        public DeleteChequeAccountCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}