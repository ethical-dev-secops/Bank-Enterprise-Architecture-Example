using System;
using System.Threading;
using System.Threading.Tasks;
using BankAccountManagementSystem.Domain.Common.Exceptions;
using BankAccountManagementSystem.Domain.Repositories;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "2.0")]

namespace BankAccountManagementSystem.Application.SavingsAccounts.DeleteSavingsAccount
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class DeleteSavingsAccountCommandHandler : IRequestHandler<DeleteSavingsAccountCommand>
    {
        private readonly ISavingsAccountRepository _savingsAccountRepository;

        [IntentManaged(Mode.Merge)]
        public DeleteSavingsAccountCommandHandler(ISavingsAccountRepository savingsAccountRepository)
        {
            _savingsAccountRepository = savingsAccountRepository;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task Handle(DeleteSavingsAccountCommand request, CancellationToken cancellationToken)
        {
            var existingSavingsAccount = await _savingsAccountRepository.FindByIdAsync(request.Id, cancellationToken);
            if (existingSavingsAccount is null)
            {
                throw new NotFoundException($"Could not find SavingsAccount '{request.Id}'");
            }

            _savingsAccountRepository.Remove(existingSavingsAccount);
        }
    }
}