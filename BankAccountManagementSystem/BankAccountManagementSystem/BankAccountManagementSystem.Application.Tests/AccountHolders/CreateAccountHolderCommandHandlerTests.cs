using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using BankAccountManagementSystem.Application.AccountHolders.CreateAccountHolder;
using BankAccountManagementSystem.Application.Tests.Extensions;
using BankAccountManagementSystem.Domain.Entities;
using BankAccountManagementSystem.Domain.Repositories;
using FluentAssertions;
using Intent.RoslynWeaver.Attributes;
using NSubstitute;
using Xunit;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CRUD.Tests.Owner.CreateCommandHandlerTests", Version = "1.0")]

namespace BankAccountManagementSystem.Application.Tests.AccountHolders
{
    public class CreateAccountHolderCommandHandlerTests
    {
        public static IEnumerable<object[]> GetSuccessfulResultTestData()
        {
            var fixture = new Fixture();
            yield return new object[] { fixture.Create<CreateAccountHolderCommand>() };
        }

        [Theory]
        [MemberData(nameof(GetSuccessfulResultTestData))]
        public async Task Handle_WithValidCommand_AddsAccountHolderToRepository(CreateAccountHolderCommand testCommand)
        {
            // Arrange
            var accountHolderRepository = Substitute.For<IAccountHolderRepository>();
            var expectedAccountHolderId = new Fixture().Create<long>();
            AccountHolder addedAccountHolder = null;
            accountHolderRepository.OnAdd(ent => addedAccountHolder = ent);
            accountHolderRepository.UnitOfWork
                .When(async x => await x.SaveChangesAsync(CancellationToken.None))
                .Do(_ => addedAccountHolder.Id = expectedAccountHolderId);

            var sut = new CreateAccountHolderCommandHandler(accountHolderRepository);

            // Act
            var result = await sut.Handle(testCommand, CancellationToken.None);

            // Assert
            result.Should().Be(expectedAccountHolderId);
            await accountHolderRepository.UnitOfWork.Received(1).SaveChangesAsync();
            AccountHolderAssertions.AssertEquivalent(testCommand, addedAccountHolder);
        }
    }
}