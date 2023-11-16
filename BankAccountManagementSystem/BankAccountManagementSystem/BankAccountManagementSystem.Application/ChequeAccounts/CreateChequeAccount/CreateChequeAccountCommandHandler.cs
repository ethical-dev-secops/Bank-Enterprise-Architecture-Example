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

namespace BankAccountManagementSystem.Application.ChequeAccounts.CreateChequeAccount
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class CreateChequeAccountCommandHandler : IRequestHandler<CreateChequeAccountCommand, long>
    {
        private readonly IChequeAccountRepository _chequeAccountRepository;

        [IntentManaged(Mode.Merge)]
        public CreateChequeAccountCommandHandler(IChequeAccountRepository chequeAccountRepository)
        {
            _chequeAccountRepository = chequeAccountRepository;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<long> Handle(CreateChequeAccountCommand request, CancellationToken cancellationToken)
        {
            var newChequeAccount = new ChequeAccount
            {
                Type = request.Type,
                CurrencyIsoCode = request.CurrencyIsoCode,
                Balance = request.Balance,
                AccountHolderId = request.AccountHolderId,
            };

            _chequeAccountRepository.Add(newChequeAccount);
            await _chequeAccountRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return newChequeAccount.Id;
        }
    }
}