using BankAccountManagementSystem.Domain.Enums;
using Intent.RoslynWeaver.Attributes;

namespace BankAccountManagementSystem.Domain.Entities
{
    public class SavingsAccount : Account
    {
        public SavingsAccountType Type { get; set; }
    }
}