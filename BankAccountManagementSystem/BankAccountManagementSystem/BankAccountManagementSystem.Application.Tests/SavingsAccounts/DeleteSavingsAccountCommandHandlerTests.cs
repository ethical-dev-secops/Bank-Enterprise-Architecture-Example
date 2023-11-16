using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using BankAccountManagementSystem.Application.SavingsAccounts.DeleteSavingsAccount;
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

namespace BankAccountManagementSystem.Application.Tests.SavingsAccounts
{
    public class DeleteSavingsAccountCommandHandlerTests
    {
        public static IEnumerable<object[]> GetSuccessfulResultTestData()
        {
            var fixture = new Fixture();
            fixture.Register<DomainEvent>(() => null!);
            var existingEntity = fixture.Create<SavingsAccount>();
            fixture.Customize<DeleteSavingsAccountCommand>(comp => comp.With(x => x.Id, existingEntity.Id));
            var testCommand = fixture.Create<DeleteSavingsAccountCommand>();
            yield return new object[] { testCommand, existingEntity };
        }

        [Theory]
        [MemberData(nameof(GetSuccessfulResultTestData))]
        public async Task Handle_WithValidCommand_DeletesSavingsAccountFromRepository(
            DeleteSavingsAccountCommand testCommand,
            SavingsAccount existingEntity)
        {
            // Arrange
            var savingsAccountRepository = Substitute.For<ISavingsAccountRepository>();
            savingsAccountRepository.FindByIdAsync(testCommand.Id, CancellationToken.None)!.Returns(Task.FromResult(existingEntity));

            var sut = new DeleteSavingsAccountCommandHandler(savingsAccountRepository);

            // Act
            await sut.Handle(testCommand, CancellationToken.None);

            // Assert
            savingsAccountRepository.Received(1).Remove(Arg.Is<SavingsAccount>(p => testCommand.Id == p.Id));
        }

        [Fact]
        public async Task Handle_WithInvalidSavingsAccountId_ReturnsNotFound()
        {
            // Arrange
            var savingsAccountRepository = Substitute.For<ISavingsAccountRepository>();
            var fixture = new Fixture();
            var testCommand = fixture.Create<DeleteSavingsAccountCommand>();
            savingsAccountRepository.FindByIdAsync(testCommand.Id, CancellationToken.None)!.Returns(Task.FromResult<SavingsAccount>(default));


            var sut = new DeleteSavingsAccountCommandHandler(savingsAccountRepository);

            // Act
            var act = async () => await sut.Handle(testCommand, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>();
        }
    }
}