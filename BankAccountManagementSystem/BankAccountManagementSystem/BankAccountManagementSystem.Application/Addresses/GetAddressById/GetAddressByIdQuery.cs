using BankAccountManagementSystem.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryModels", Version = "1.0")]

namespace BankAccountManagementSystem.Application.Addresses.GetAddressById
{
    public class GetAddressByIdQuery : IRequest<AddressDto>, IQuery
    {
        public GetAddressByIdQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}