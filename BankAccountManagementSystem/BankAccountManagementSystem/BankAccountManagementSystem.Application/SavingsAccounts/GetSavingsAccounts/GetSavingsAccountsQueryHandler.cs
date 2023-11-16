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

namespace BankAccountManagementSystem.Application.SavingsAccounts.GetSavingsAccounts
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class GetSavingsAccountsQueryHandler : IRequestHandler<GetSavingsAccountsQuery, List<SavingsAccountDto>>
    {
        private readonly ISavingsAccountRepository _savingsAccountRepository;
        private readonly IMapper _mapper;

        [IntentManaged(Mode.Ignore)]
        public GetSavingsAccountsQueryHandler(ISavingsAccountRepository savingsAccountRepository, IMapper mapper)
        {
            _savingsAccountRepository = savingsAccountRepository;
            _mapper = mapper;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<List<SavingsAccountDto>> Handle(
            GetSavingsAccountsQuery request,
            CancellationToken cancellationToken)
        {
            var savingsAccounts = await _savingsAccountRepository.FindAllAsync(cancellationToken);
            return savingsAccounts.MapToSavingsAccountDtoList(_mapper);
        }
    }
}