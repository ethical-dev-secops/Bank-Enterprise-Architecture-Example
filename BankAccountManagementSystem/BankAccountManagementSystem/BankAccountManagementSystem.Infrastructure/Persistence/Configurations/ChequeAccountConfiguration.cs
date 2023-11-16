using BankAccountManagementSystem.Domain.Entities;
using Intent.RoslynWeaver.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.EntityTypeConfiguration", Version = "1.0")]

namespace BankAccountManagementSystem.Infrastructure.Persistence.Configurations
{
    public class ChequeAccountConfiguration : IEntityTypeConfiguration<ChequeAccount>
    {
        public void Configure(EntityTypeBuilder<ChequeAccount> builder)
        {
            builder.HasBaseType<Account>();

            builder.Property(x => x.Type)
                .IsRequired();
        }
    }
}