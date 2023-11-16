using BankAccountManagementSystem.Domain.Entities;
using Intent.RoslynWeaver.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.EntityTypeConfiguration", Version = "1.0")]

namespace BankAccountManagementSystem.Infrastructure.Persistence.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.StreetName)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.ErfNumber)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.Suburb)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.Country)
                .IsRequired()
                .HasMaxLength(30);

            builder.Ignore(e => e.DomainEvents);
        }
    }
}