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

namespace BankAccountManagementSystem.Application.AccountHolders.CreateAccountHolder
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class CreateAccountHolderCommandHandler : IRequestHandler<CreateAccountHolderCommand, long>
    {
        private readonly IAccountHolderRepository _accountHolderRepository;

        [IntentManaged(Mode.Merge)]
        public CreateAccountHolderCommandHandler(IAccountHolderRepository accountHolderRepository)
        {
            _accountHolderRepository = accountHolderRepository;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<long> Handle(CreateAccountHolderCommand request, CancellationToken cancellationToken)
        {
            var newAccountHolder = new AccountHolder
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                IdentityNumber = request.IdentityNumber,
                PassportNumber = request.PassportNumber,
                AddressId = request.AddressId,
            };

            _accountHolderRepository.Add(newAccountHolder);
            await _accountHolderRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return newAccountHolder.Id;
        }
    }
}