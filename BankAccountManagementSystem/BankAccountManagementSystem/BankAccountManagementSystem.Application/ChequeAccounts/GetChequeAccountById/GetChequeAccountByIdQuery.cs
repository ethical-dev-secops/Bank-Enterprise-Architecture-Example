using BankAccountManagementSystem.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryModels", Version = "1.0")]

namespace BankAccountManagementSystem.Application.ChequeAccounts.GetChequeAccountById
{
    public class GetChequeAccountByIdQuery : IRequest<ChequeAccountDto>, IQuery
    {
        public GetChequeAccountByIdQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}