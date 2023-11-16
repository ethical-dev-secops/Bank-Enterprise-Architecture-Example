using System.Collections.Generic;
using BankAccountManagementSystem.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryModels", Version = "1.0")]

namespace BankAccountManagementSystem.Application.SavingsAccounts.GetSavingsAccounts
{
    public class GetSavingsAccountsQuery : IRequest<List<SavingsAccountDto>>, IQuery
    {
        public GetSavingsAccountsQuery()
        {
        }
    }
}