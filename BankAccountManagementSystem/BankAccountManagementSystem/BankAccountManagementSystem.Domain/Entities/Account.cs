using System.Collections.Generic;
using BankAccountManagementSystem.Domain.Common;
using Intent.RoslynWeaver.Attributes;

namespace BankAccountManagementSystem.Domain.Entities
{
    public class Account : IHasDomainEvent
    {
        public long Id { get; set; }

        public string CurrencyIsoCode { get; set; }

        public decimal Balance { get; set; }

        public long AccountHolderId { get; set; }

        public virtual AccountHolder AccountHolder { get; set; }

        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}