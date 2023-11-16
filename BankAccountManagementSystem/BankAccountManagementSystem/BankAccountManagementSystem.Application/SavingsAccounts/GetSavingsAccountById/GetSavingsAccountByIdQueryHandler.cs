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

namespace BankAccountManagementSystem.Application.SavingsAccounts.GetSavingsAccountById
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class GetSavingsAccountByIdQueryHandler : IRequestHandler<GetSavingsAccountByIdQuery, SavingsAccountDto>
    {
        private readonly ISavingsAccountRepository _savingsAccountRepository;
        private readonly IMapper _mapper;

        [IntentManaged(Mode.Ignore)]
        public GetSavingsAccountByIdQueryHandler(ISavingsAccountRepository savingsAccountRepository, IMapper mapper)
        {
            _savingsAccountRepository = savingsAccountRepository;
            _mapper = mapper;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<SavingsAccountDto> Handle(
            GetSavingsAccountByIdQuery request,
            CancellationToken cancellationToken)
        {
            var savingsAccount = await _savingsAccountRepository.FindByIdAsync(request.Id, cancellationToken);
            if (savingsAccount is null)
            {
                throw new NotFoundException($"Could not find SavingsAccount '{request.Id}'");
            }

            return savingsAccount.MapToSavingsAccountDto(_mapper);
        }
    }
}