using System.Collections.Generic;
using BankAccountManagementSystem.Domain.Common;
using Intent.RoslynWeaver.Attributes;

namespace BankAccountManagementSystem.Domain.Entities
{
    public class Address : IHasDomainEvent
    {
        public long Id { get; set; }

        public string StreetName { get; set; }

        public string ErfNumber { get; set; }

        public string Suburb { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}