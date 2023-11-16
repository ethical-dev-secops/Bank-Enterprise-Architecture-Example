using System.Collections.Generic;
using System.Linq;
using BankAccountManagementSystem.Application.AccountHolders;
using BankAccountManagementSystem.Application.AccountHolders.CreateAccountHolder;
using BankAccountManagementSystem.Application.AccountHolders.UpdateAccountHolder;
using BankAccountManagementSystem.Domain.Entities;
using FluentAssertions;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CRUD.Tests.Assertions.AssertionClass", Version = "1.0")]

namespace BankAccountManagementSystem.Application.Tests.AccountHolders
{
    public static class AccountHolderAssertions
    {
        public static void AssertEquivalent(CreateAccountHolderCommand expectedDto, AccountHolder actualEntity)
        {
            if (expectedDto == null)
            {
                actualEntity.Should().BeNull();
                return;
            }

            actualEntity.Should().NotBeNull();
            actualEntity.FirstName.Should().Be(expectedDto.FirstName);
            actualEntity.LastName.Should().Be(expectedDto.LastName);
            actualEntity.IdentityNumber.Should().Be(expectedDto.IdentityNumber);
            actualEntity.PassportNumber.Should().Be(expectedDto.PassportNumber);
            actualEntity.AddressId.Should().Be(expectedDto.AddressId);
        }

        public static void AssertEquivalent(
            IEnumerable<AccountHolderDto> actualDtos,
            IEnumerable<AccountHolder> expectedEntities)
        {
            if (expectedEntities == null)
            {
                actualDtos.Should().BeNullOrEmpty();
                return;
            }

            actualDtos.Should().HaveSameCount(actualDtos);
            for (int i = 0; i < expectedEntities.Count(); i++)
            {
                var entity = expectedEntities.ElementAt(i);
                var dto = actualDtos.ElementAt(i);
                if (entity == null)
                {
                    dto.Should().BeNull();
                    continue;
                }

                dto.Should().NotBeNull();
                dto.Id.Should().Be(entity.Id);
                dto.FirstName.Should().Be(entity.FirstName);
                dto.LastName.Should().Be(entity.LastName);
                dto.IdentityNumber.Should().Be(entity.IdentityNumber);
                dto.PassportNumber.Should().Be(entity.PassportNumber);
                dto.AddressId.Should().Be(entity.AddressId);
            }
        }

        public static void AssertEquivalent(AccountHolderDto actualDto, AccountHolder expectedEntity)
        {
            if (expectedEntity == null)
            {
                actualDto.Should().BeNull();
                return;
            }

            actualDto.Should().NotBeNull();
            actualDto.Id.Should().Be(expectedEntity.Id);
            actualDto.FirstName.Should().Be(expectedEntity.FirstName);
            actualDto.LastName.Should().Be(expectedEntity.LastName);
            actualDto.IdentityNumber.Should().Be(expectedEntity.IdentityNumber);
            actualDto.PassportNumber.Should().Be(expectedEntity.PassportNumber);
            actualDto.AddressId.Should().Be(expectedEntity.AddressId);
        }

        public static void AssertEquivalent(UpdateAccountHolderCommand expectedDto, AccountHolder actualEntity)
        {
            if (expectedDto == null)
            {
                actualEntity.Should().BeNull();
                return;
            }

            actualEntity.Should().NotBeNull();
            actualEntity.FirstName.Should().Be(expectedDto.FirstName);
            actualEntity.LastName.Should().Be(expectedDto.LastName);
            actualEntity.IdentityNumber.Should().Be(expectedDto.IdentityNumber);
            actualEntity.PassportNumber.Should().Be(expectedDto.PassportNumber);
            actualEntity.AddressId.Should().Be(expectedDto.AddressId);
        }
    }
}