using System.Collections.Generic;
using System.Linq;
using BankAccountManagementSystem.Application.SavingsAccounts;
using BankAccountManagementSystem.Application.SavingsAccounts.CreateSavingsAccount;
using BankAccountManagementSystem.Application.SavingsAccounts.UpdateSavingsAccount;
using BankAccountManagementSystem.Domain.Entities;
using FluentAssertions;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CRUD.Tests.Assertions.AssertionClass", Version = "1.0")]

namespace BankAccountManagementSystem.Application.Tests.SavingsAccounts
{
    public static class SavingsAccountAssertions
    {
        public static void AssertEquivalent(CreateSavingsAccountCommand expectedDto, SavingsAccount actualEntity)
        {
            if (expectedDto == null)
            {
                actualEntity.Should().BeNull();
                return;
            }

            actualEntity.Should().NotBeNull();
            actualEntity.Type.Should().Be(expectedDto.Type);
            actualEntity.CurrencyIsoCode.Should().Be(expectedDto.CurrencyIsoCode);
            actualEntity.Balance.Should().Be(expectedDto.Balance);
            actualEntity.AccountHolderId.Should().Be(expectedDto.AccountHolderId);
        }

        public static void AssertEquivalent(
            IEnumerable<SavingsAccountDto> actualDtos,
            IEnumerable<SavingsAccount> expectedEntities)
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
                dto.Type.Should().Be(entity.Type);
                dto.CurrencyIsoCode.Should().Be(entity.CurrencyIsoCode);
                dto.Balance.Should().Be(entity.Balance);
                dto.AccountHolderId.Should().Be(entity.AccountHolderId);
            }
        }

        public static void AssertEquivalent(SavingsAccountDto actualDto, SavingsAccount expectedEntity)
        {
            if (expectedEntity == null)
            {
                actualDto.Should().BeNull();
                return;
            }

            actualDto.Should().NotBeNull();
            actualDto.Id.Should().Be(expectedEntity.Id);
            actualDto.Type.Should().Be(expectedEntity.Type);
            actualDto.CurrencyIsoCode.Should().Be(expectedEntity.CurrencyIsoCode);
            actualDto.Balance.Should().Be(expectedEntity.Balance);
            actualDto.AccountHolderId.Should().Be(expectedEntity.AccountHolderId);
        }

        public static void AssertEquivalent(UpdateSavingsAccountCommand expectedDto, SavingsAccount actualEntity)
        {
            if (expectedDto == null)
            {
                actualEntity.Should().BeNull();
                return;
            }

            actualEntity.Should().NotBeNull();
            actualEntity.Type.Should().Be(expectedDto.Type);
            actualEntity.CurrencyIsoCode.Should().Be(expectedDto.CurrencyIsoCode);
            actualEntity.Balance.Should().Be(expectedDto.Balance);
            actualEntity.AccountHolderId.Should().Be(expectedDto.AccountHolderId);
        }
    }
}