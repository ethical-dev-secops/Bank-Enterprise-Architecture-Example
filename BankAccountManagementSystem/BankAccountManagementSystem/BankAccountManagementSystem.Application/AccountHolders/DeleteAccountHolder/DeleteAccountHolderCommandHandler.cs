using System;
using System.Threading;
using System.Threading.Tasks;
using BankAccountManagementSystem.Domain.Common.Exceptions;
using BankAccountManagementSystem.Domain.Repositories;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "2.0")]

namespace BankAccountManagementSystem.Application.AccountHolders.DeleteAccountHolder
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class DeleteAccountHolderCommandHandler : IRequestHandler<DeleteAccountHolderCommand>
    {
        private readonly IAccountHolderRepository _accountHolderRepository;

        [IntentManaged(Mode.Merge)]
        public DeleteAccountHolderCommandHandler(IAccountHolderRepository accountHolderRepository)
        {
            _accountHolderRepository = accountHolderRepository;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task Handle(DeleteAccountHolderCommand request, CancellationToken cancellationToken)
        {
            var existingAccountHolder = await _accountHolderRepository.FindByIdAsync(request.Id, cancellationToken);
            if (existingAccountHolder is null)
            {
                throw new NotFoundException($"Could not find AccountHolder '{request.Id}'");
            }

            _accountHolderRepository.Remove(existingAccountHolder);
        }
    }
}