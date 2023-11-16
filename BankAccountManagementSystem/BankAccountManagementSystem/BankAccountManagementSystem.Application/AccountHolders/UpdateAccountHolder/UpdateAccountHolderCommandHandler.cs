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

namespace BankAccountManagementSystem.Application.AccountHolders.UpdateAccountHolder
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class UpdateAccountHolderCommandHandler : IRequestHandler<UpdateAccountHolderCommand>
    {
        private readonly IAccountHolderRepository _accountHolderRepository;

        [IntentManaged(Mode.Merge)]
        public UpdateAccountHolderCommandHandler(IAccountHolderRepository accountHolderRepository)
        {
            _accountHolderRepository = accountHolderRepository;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task Handle(UpdateAccountHolderCommand request, CancellationToken cancellationToken)
        {
            var existingAccountHolder = await _accountHolderRepository.FindByIdAsync(request.Id, cancellationToken);
            if (existingAccountHolder is null)
            {
                throw new NotFoundException($"Could not find AccountHolder '{request.Id}'");
            }

            existingAccountHolder.FirstName = request.FirstName;
            existingAccountHolder.LastName = request.LastName;
            existingAccountHolder.IdentityNumber = request.IdentityNumber;
            existingAccountHolder.PassportNumber = request.PassportNumber;
            existingAccountHolder.AddressId = request.AddressId;
        }
    }
}