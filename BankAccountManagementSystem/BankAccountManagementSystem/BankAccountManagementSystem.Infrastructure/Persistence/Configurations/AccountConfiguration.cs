using BankAccountManagementSystem.Domain.Entities;
using Intent.RoslynWeaver.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.EntityTypeConfiguration", Version = "1.0")]

namespace BankAccountManagementSystem.Infrastructure.Persistence.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CurrencyIsoCode)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(x => x.Balance)
                .IsRequired();

            builder.Property(x => x.AccountHolderId)
                .IsRequired();

            builder.HasOne(x => x.AccountHolder)
                .WithMany()
                .HasForeignKey(x => x.AccountHolderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(e => e.DomainEvents);
        }
    }
}