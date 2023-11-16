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

namespace BankAccountManagementSystem.Application.Addresses.CreateAddress
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, long>
    {
        private readonly IAddressRepository _addressRepository;

        [IntentManaged(Mode.Merge)]
        public CreateAddressCommandHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<long> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            var newAddress = new Address
            {
                StreetName = request.StreetName,
                ErfNumber = request.ErfNumber,
                Suburb = request.Suburb,
                City = request.City,
                Country = request.Country,
            };

            _addressRepository.Add(newAddress);
            await _addressRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return newAddress.Id;
        }
    }
}