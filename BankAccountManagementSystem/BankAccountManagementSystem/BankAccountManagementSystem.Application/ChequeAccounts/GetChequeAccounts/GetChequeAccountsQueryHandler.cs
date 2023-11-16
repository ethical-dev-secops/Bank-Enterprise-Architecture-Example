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

namespace BankAccountManagementSystem.Application.ChequeAccounts.GetChequeAccounts
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class GetChequeAccountsQueryHandler : IRequestHandler<GetChequeAccountsQuery, List<ChequeAccountDto>>
    {
        private readonly IChequeAccountRepository _chequeAccountRepository;
        private readonly IMapper _mapper;

        [IntentManaged(Mode.Ignore)]
        public GetChequeAccountsQueryHandler(IChequeAccountRepository chequeAccountRepository, IMapper mapper)
        {
            _chequeAccountRepository = chequeAccountRepository;
            _mapper = mapper;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<List<ChequeAccountDto>> Handle(
            GetChequeAccountsQuery request,
            CancellationToken cancellationToken)
        {
            var chequeAccounts = await _chequeAccountRepository.FindAllAsync(cancellationToken);
            return chequeAccounts.MapToChequeAccountDtoList(_mapper);
        }
    }
}