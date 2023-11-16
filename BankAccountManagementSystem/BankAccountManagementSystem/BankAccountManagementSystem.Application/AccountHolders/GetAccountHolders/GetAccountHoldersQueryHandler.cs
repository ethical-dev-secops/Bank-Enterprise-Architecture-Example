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

namespace BankAccountManagementSystem.Application.AccountHolders.GetAccountHolders
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class GetAccountHoldersQueryHandler : IRequestHandler<GetAccountHoldersQuery, List<AccountHolderDto>>
    {
        private readonly IAccountHolderRepository _accountHolderRepository;
        private readonly IMapper _mapper;

        [IntentManaged(Mode.Ignore)]
        public GetAccountHoldersQueryHandler(IAccountHolderRepository accountHolderRepository, IMapper mapper)
        {
            _accountHolderRepository = accountHolderRepository;
            _mapper = mapper;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<List<AccountHolderDto>> Handle(
            GetAccountHoldersQuery request,
            CancellationToken cancellationToken)
        {
            var accountHolders = await _accountHolderRepository.FindAllAsync(cancellationToken);
            return accountHolders.MapToAccountHolderDtoList(_mapper);
        }
    }
}