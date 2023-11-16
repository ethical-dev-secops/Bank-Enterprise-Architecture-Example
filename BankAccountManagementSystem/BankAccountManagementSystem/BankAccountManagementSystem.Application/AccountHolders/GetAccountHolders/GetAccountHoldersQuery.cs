using System.Collections.Generic;
using BankAccountManagementSystem.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryModels", Version = "1.0")]

namespace BankAccountManagementSystem.Application.AccountHolders.GetAccountHolders
{
    public class GetAccountHoldersQuery : IRequest<List<AccountHolderDto>>, IQuery
    {
        public GetAccountHoldersQuery()
        {
        }
    }
}