using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BankAccountManagementSystem.Domain.Common.Exceptions;
using BankAccountManagementSystem.Domain.Repositories;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryHandler", Version = "1.0")]

namespace BankAccountManagementSystem.Application.Addresses.GetAddressById
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class GetAddressByIdQueryHandler : IRequestHandler<GetAddressByIdQuery, AddressDto>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        [IntentManaged(Mode.Ignore)]
        public GetAddressByIdQueryHandler(IAddressRepository addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<AddressDto> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
        {
            var address = await _addressRepository.FindByIdAsync(request.Id, cancellationToken);
            if (address is null)
            {
                throw new NotFoundException($"Could not find Address '{request.Id}'");
            }

            return address.MapToAddressDto(_mapper);
        }
    }
}