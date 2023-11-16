using System.Collections.Generic;
using BankAccountManagementSystem.Domain.Common;
using Intent.RoslynWeaver.Attributes;

namespace BankAccountManagementSystem.Domain.Entities
{
    public class AccountHolder : IHasDomainEvent
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string IdentityNumber { get; set; }

        public string PassportNumber { get; set; }

        public long AddressId { get; set; }

        public virtual Address Address { get; set; }

        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}