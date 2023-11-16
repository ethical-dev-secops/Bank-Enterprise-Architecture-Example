using System.Collections.Generic;
using System.Linq;
using BankAccountManagementSystem.Application.ChequeAccounts;
using BankAccountManagementSystem.Application.ChequeAccounts.CreateChequeAccount;
using BankAccountManagementSystem.Application.ChequeAccounts.UpdateChequeAccount;
using BankAccountManagementSystem.Domain.Entities;
using FluentAssertions;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CRUD.Tests.Assertions.AssertionClass", Version = "1.0")]

namespace BankAccountManagementSystem.Application.Tests.ChequeAccounts
{
    public static class ChequeAccountAssertions
    {
        public static void AssertEquivalent(CreateChequeAccountCommand expectedDto, ChequeAccount actualEntity)
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
            IEnumerable<ChequeAccountDto> actualDtos,
            IEnumerable<ChequeAccount> expectedEntities)
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

        public static void AssertEquivalent(ChequeAccountDto actualDto, ChequeAccount expectedEntity)
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

        public static void AssertEquivalent(UpdateChequeAccountCommand expectedDto, ChequeAccount actualEntity)
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