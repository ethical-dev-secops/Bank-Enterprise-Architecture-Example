using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BankAccountManagementSystem.Domain.Repositories;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryHandler", Version = "1.0")]

namespace BankAccountManagementSystem.Application.Addresses.GetAddresses
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class GetAddressesQueryHandler : IRequestHandler<GetAddressesQuery, List<AddressDto>>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        [IntentManaged(Mode.Ignore)]
        public GetAddressesQueryHandler(IAddressRepository addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<List<AddressDto>> Handle(GetAddressesQuery request, CancellationToken cancellationToken)
        {
            var addresses = await _addressRepository.FindAllAsync(cancellationToken);
            return addresses.MapToAddressDtoList(_mapper);
        }
    }
}