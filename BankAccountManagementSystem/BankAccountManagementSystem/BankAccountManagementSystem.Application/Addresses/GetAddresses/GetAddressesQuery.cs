using System.Collections.Generic;
using BankAccountManagementSystem.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryModels", Version = "1.0")]

namespace BankAccountManagementSystem.Application.Addresses.GetAddresses
{
    public class GetAddressesQuery : IRequest<List<AddressDto>>, IQuery
    {
        public GetAddressesQuery()
        {
        }
    }
}