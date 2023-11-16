using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BankAccountManagementSystem.Domain.Common.Exceptions;
using BankAccountManagementSystem.Domain.Repositories;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "2.0")]

namespace BankAccountManagementSystem.Application.SavingsAccounts.UpdateSavingsAccount
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class UpdateSavingsAccountCommandHandler : IRequestHandler<UpdateSavingsAccountCommand>
    {
        private readonly ISavingsAccountRepository _savingsAccountRepository;

        [IntentManaged(Mode.Merge)]
        public UpdateSavingsAccountCommandHandler(ISavingsAccountRepository savingsAccountRepository)
        {
            _savingsAccountRepository = savingsAccountRepository;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task Handle(UpdateSavingsAccountCommand request, CancellationToken cancellationToken)
        {
            var existingSavingsAccount = await _savingsAccountRepository.FindByIdAsync(request.Id, cancellationToken);
            if (existingSavingsAccount is null)
            {
                throw new NotFoundException($"Could not find SavingsAccount '{request.Id}'");
            }

            existingSavingsAccount.Type = request.Type;
            existingSavingsAccount.CurrencyIsoCode = request.CurrencyIsoCode;
            existingSavingsAccount.Balance = request.Balance;
            existingSavingsAccount.AccountHolderId = request.AccountHolderId;
        }
    }
}