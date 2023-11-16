using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BankAccountManagementSystem.Domain.Entities;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Entities.Repositories.Api.EntityRepositoryInterface", Version = "1.0")]

namespace BankAccountManagementSystem.Domain.Repositories
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public interface IChequeAccountRepository : IEFRepository<ChequeAccount, ChequeAccount>
    {
        [IntentManaged(Mode.Fully)]
        Task<ChequeAccount?> FindByIdAsync(long id, CancellationToken cancellationToken = default);
        [IntentManaged(Mode.Fully)]
        Task<List<ChequeAccount>> FindByIdsAsync(long[] ids, CancellationToken cancellationToken = default);
    }
}