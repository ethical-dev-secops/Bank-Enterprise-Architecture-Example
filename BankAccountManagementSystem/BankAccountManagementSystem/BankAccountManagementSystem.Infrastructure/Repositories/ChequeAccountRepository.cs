using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BankAccountManagementSystem.Domain.Entities;
using BankAccountManagementSystem.Domain.Repositories;
using BankAccountManagementSystem.Infrastructure.Persistence;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.Repositories.Repository", Version = "1.0")]

namespace BankAccountManagementSystem.Infrastructure.Repositories
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class ChequeAccountRepository : RepositoryBase<ChequeAccount, ChequeAccount, ApplicationDbContext>, IChequeAccountRepository
    {
        public ChequeAccountRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ChequeAccount?> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await FindAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<List<ChequeAccount>> FindByIdsAsync(long[] ids, CancellationToken cancellationToken = default)
        {
            return await FindAllAsync(x => ids.Contains(x.Id), cancellationToken);
        }
    }
}