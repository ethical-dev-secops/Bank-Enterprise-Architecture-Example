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

namespace BankAccountManagementSystem.Application.Addresses.UpdateAddress
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand>
    {
        private readonly IAddressRepository _addressRepository;

        [IntentManaged(Mode.Merge)]
        public UpdateAddressCommandHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var existingAddress = await _addressRepository.FindByIdAsync(request.Id, cancellationToken);
            if (existingAddress is null)
            {
                throw new NotFoundException($"Could not find Address '{request.Id}'");
            }

            existingAddress.StreetName = request.StreetName;
            existingAddress.ErfNumber = request.ErfNumber;
            existingAddress.Suburb = request.Suburb;
            existingAddress.City = request.City;
            existingAddress.Country = request.Country;
        }
    }
}