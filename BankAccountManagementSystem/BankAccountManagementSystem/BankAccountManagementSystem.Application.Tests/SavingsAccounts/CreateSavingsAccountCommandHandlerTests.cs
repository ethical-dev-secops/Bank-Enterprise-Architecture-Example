using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using BankAccountManagementSystem.Application.SavingsAccounts.CreateSavingsAccount;
using BankAccountManagementSystem.Application.Tests.Extensions;
using BankAccountManagementSystem.Domain.Entities;
using BankAccountManagementSystem.Domain.Repositories;
using FluentAssertions;
using Intent.RoslynWeaver.Attributes;
using NSubstitute;
using Xunit;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CRUD.Tests.Owner.CreateCommandHandlerTests", Version = "1.0")]

namespace BankAccountManagementSystem.Application.Tests.SavingsAccounts
{
    public class CreateSavingsAccountCommandHandlerTests
    {
        public static IEnumerable<object[]> GetSuccessfulResultTestData()
        {
            var fixture = new Fixture();
            yield return new object[] { fixture.Create<CreateSavingsAccountCommand>() };
        }

        [Theory]
        [MemberData(nameof(GetSuccessfulResultTestData))]
        public async Task Handle_WithValidCommand_AddsSavingsAccountToRepository(CreateSavingsAccountCommand testCommand)
        {
            // Arrange
            var savingsAccountRepository = Substitute.For<ISavingsAccountRepository>();
            var expectedSavingsAccountId = new Fixture().Create<long>();
            SavingsAccount addedSavingsAccount = null;
            savingsAccountRepository.OnAdd(ent => addedSavingsAccount = ent);
            savingsAccountRepository.UnitOfWork
                .When(async x => await x.SaveChangesAsync(CancellationToken.None))
                .Do(_ => addedSavingsAccount.Id = expectedSavingsAccountId);

            var sut = new CreateSavingsAccountCommandHandler(savingsAccountRepository);

            // Act
            var result = await sut.Handle(testCommand, CancellationToken.None);

            // Assert
            result.Should().Be(expectedSavingsAccountId);
            await savingsAccountRepository.UnitOfWork.Received(1).SaveChangesAsync();
            SavingsAccountAssertions.AssertEquivalent(testCommand, addedSavingsAccount);
        }
    }
}