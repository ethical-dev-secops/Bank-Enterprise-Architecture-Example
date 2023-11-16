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

namespace BankAccountManagementSystem.Application.AccountHolders.GetAccountHolderById
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class GetAccountHolderByIdQueryHandler : IRequestHandler<GetAccountHolderByIdQuery, AccountHolderDto>
    {
        private readonly IAccountHolderRepository _accountHolderRepository;
        private readonly IMapper _mapper;

        [IntentManaged(Mode.Ignore)]
        public GetAccountHolderByIdQueryHandler(IAccountHolderRepository accountHolderRepository, IMapper mapper)
        {
            _accountHolderRepository = accountHolderRepository;
            _mapper = mapper;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<AccountHolderDto> Handle(GetAccountHolderByIdQuery request, CancellationToken cancellationToken)
        {
            var accountHolder = await _accountHolderRepository.FindByIdAsync(request.Id, cancellationToken);
            if (accountHolder is null)
            {
                throw new NotFoundException($"Could not find AccountHolder '{request.Id}'");
            }

            return accountHolder.MapToAccountHolderDto(_mapper);
        }
    }
}