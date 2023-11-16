using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BankAccountManagementSystem.Domain.Entities;
using BankAccountManagementSystem.Domain.Repositories;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "2.0")]

namespace BankAccountManagementSystem.Application.SavingsAccounts.CreateSavingsAccount
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class CreateSavingsAccountCommandHandler : IRequestHandler<CreateSavingsAccountCommand, long>
    {
        private readonly ISavingsAccountRepository _savingsAccountRepository;

        [IntentManaged(Mode.Merge)]
        public CreateSavingsAccountCommandHandler(ISavingsAccountRepository savingsAccountRepository)
        {
            _savingsAccountRepository = savingsAccountRepository;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<long> Handle(CreateSavingsAccountCommand request, CancellationToken cancellationToken)
        {
            var newSavingsAccount = new SavingsAccount
            {
                Type = request.Type,
                CurrencyIsoCode = request.CurrencyIsoCode,
                Balance = request.Balance,
                AccountHolderId = request.AccountHolderId,
            };

            _savingsAccountRepository.Add(newSavingsAccount);
            await _savingsAccountRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return newSavingsAccount.Id;
        }
    }
}