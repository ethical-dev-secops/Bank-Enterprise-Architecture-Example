using System;
using System.Threading;
using System.Threading.Tasks;
using BankAccountManagementSystem.Domain.Common.Exceptions;
using BankAccountManagementSystem.Domain.Repositories;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "2.0")]

namespace BankAccountManagementSystem.Application.ChequeAccounts.DeleteChequeAccount
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class DeleteChequeAccountCommandHandler : IRequestHandler<DeleteChequeAccountCommand>
    {
        private readonly IChequeAccountRepository _chequeAccountRepository;

        [IntentManaged(Mode.Merge)]
        public DeleteChequeAccountCommandHandler(IChequeAccountRepository chequeAccountRepository)
        {
            _chequeAccountRepository = chequeAccountRepository;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task Handle(DeleteChequeAccountCommand request, CancellationToken cancellationToken)
        {
            var existingChequeAccount = await _chequeAccountRepository.FindByIdAsync(request.Id, cancellationToken);
            if (existingChequeAccount is null)
            {
                throw new NotFoundException($"Could not find ChequeAccount '{request.Id}'");
            }

            _chequeAccountRepository.Remove(existingChequeAccount);
        }
    }
}