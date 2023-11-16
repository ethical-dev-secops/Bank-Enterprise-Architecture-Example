using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using BankAccountManagementSystem.Application.AccountHolders.DeleteAccountHolder;
using BankAccountManagementSystem.Domain.Common;
using BankAccountManagementSystem.Domain.Common.Exceptions;
using BankAccountManagementSystem.Domain.Entities;
using BankAccountManagementSystem.Domain.Repositories;
using FluentAssertions;
using Intent.RoslynWeaver.Attributes;
using NSubstitute;
using Xunit;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CRUD.Tests.Owner.DeleteCommandHandlerTests", Version = "1.0")]

namespace BankAccountManagementSystem.Application.Tests.AccountHolders
{
    public class DeleteAccountHolderCommandHandlerTests
    {
        public static IEnumerable<object[]> GetSuccessfulResultTestData()
        {
            var fixture = new Fixture();
            fixture.Register<DomainEvent>(() => null!);
            var existingEntity = fixture.Create<AccountHolder>();
            fixture.Customize<DeleteAccountHolderCommand>(comp => comp.With(x => x.Id, existingEntity.Id));
            var testCommand = fixture.Create<DeleteAccountHolderCommand>();
            yield return new object[] { testCommand, existingEntity };
        }

        [Theory]
        [MemberData(nameof(GetSuccessfulResultTestData))]
        public async Task Handle_WithValidCommand_DeletesAccountHolderFromRepository(
            DeleteAccountHolderCommand testCommand,
            AccountHolder existingEntity)
        {
            // Arrange
            var accountHolderRepository = Substitute.For<IAccountHolderRepository>();
            accountHolderRepository.FindByIdAsync(testCommand.Id, CancellationToken.None)!.Returns(Task.FromResult(existingEntity));

            var sut = new DeleteAccountHolderCommandHandler(accountHolderRepository);

            // Act
            await sut.Handle(testCommand, CancellationToken.None);

            // Assert
            accountHolderRepository.Received(1).Remove(Arg.Is<AccountHolder>(p => testCommand.Id == p.Id));
        }

        [Fact]
        public async Task Handle_WithInvalidAccountHolderId_ReturnsNotFound()
        {
            // Arrange
            var accountHolderRepository = Substitute.For<IAccountHolderRepository>();
            var fixture = new Fixture();
            var testCommand = fixture.Create<DeleteAccountHolderCommand>();
            accountHolderRepository.FindByIdAsync(testCommand.Id, CancellationToken.None)!.Returns(Task.FromResult<AccountHolder>(default));


            var sut = new DeleteAccountHolderCommandHandler(accountHolderRepository);

            // Act
            var act = async () => await sut.Handle(testCommand, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>();
        }
    }
}