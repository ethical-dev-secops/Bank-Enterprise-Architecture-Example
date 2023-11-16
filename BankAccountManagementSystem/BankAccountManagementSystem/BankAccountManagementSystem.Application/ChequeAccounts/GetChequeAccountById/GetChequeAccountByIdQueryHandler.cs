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

namespace BankAccountManagementSystem.Application.ChequeAccounts.GetChequeAccountById
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class GetChequeAccountByIdQueryHandler : IRequestHandler<GetChequeAccountByIdQuery, ChequeAccountDto>
    {
        private readonly IChequeAccountRepository _chequeAccountRepository;
        private readonly IMapper _mapper;

        [IntentManaged(Mode.Ignore)]
        public GetChequeAccountByIdQueryHandler(IChequeAccountRepository chequeAccountRepository, IMapper mapper)
        {
            _chequeAccountRepository = chequeAccountRepository;
            _mapper = mapper;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<ChequeAccountDto> Handle(GetChequeAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var chequeAccount = await _chequeAccountRepository.FindByIdAsync(request.Id, cancellationToken);
            if (chequeAccount is null)
            {
                throw new NotFoundException($"Could not find ChequeAccount '{request.Id}'");
            }

            return chequeAccount.MapToChequeAccountDto(_mapper);
        }
    }
}