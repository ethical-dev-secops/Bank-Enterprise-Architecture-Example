using System;
using System.Threading;
using System.Threading.Tasks;
using BankAccountManagementSystem.Domain.Common.Exceptions;
using BankAccountManagementSystem.Domain.Repositories;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "2.0")]

namespace BankAccountManagementSystem.Application.Addresses.DeleteAddress
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand>
    {
        private readonly IAddressRepository _addressRepository;

        [IntentManaged(Mode.Merge)]
        public DeleteAddressCommandHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var existingAddress = await _addressRepository.FindByIdAsync(request.Id, cancellationToken);
            if (existingAddress is null)
            {
                throw new NotFoundException($"Could not find Address '{request.Id}'");
            }

            _addressRepository.Remove(existingAddress);
        }
    }
}