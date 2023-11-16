using BankAccountManagementSystem.Domain.Enums;
using Intent.RoslynWeaver.Attributes;

namespace BankAccountManagementSystem.Domain.Entities
{
    public class ChequeAccount : Account
    {
        public ChequeAccountType Type { get; set; }
    }
}