using BankAccountManagementSystem.Domain.Entities;
using Intent.RoslynWeaver.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.EntityTypeConfiguration", Version = "1.0")]

namespace BankAccountManagementSystem.Infrastructure.Persistence.Configurations
{
    public class AccountHolderConfiguration : IEntityTypeConfiguration<AccountHolder>
    {
        public void Configure(EntityTypeBuilder<AccountHolder> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.IdentityNumber)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.PassportNumber)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.AddressId)
                .IsRequired();

            builder.HasOne(x => x.Address)
                .WithMany()
                .HasForeignKey(x => x.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(e => e.DomainEvents);
        }
    }
}