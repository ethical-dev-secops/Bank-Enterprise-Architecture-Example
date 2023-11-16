using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using BankAccountManagementSystem.Application.SavingsAccounts.UpdateSavingsAccount;
using BankAccountManagementSystem.Domain.Common;
using BankAccountManagementSystem.Domain.Common.Exceptions;
using BankAccountManagementSystem.Domain.Entities;
using BankAccountManagementSystem.Domain.Repositories;
using FluentAssertions;
using Intent.RoslynWeaver.Attributes;
using NSubstitute;
using Xunit;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CRUD.Tests.Owner.UpdateCommandHandlerTests", Version = "1.0")]

namespace BankAccountManagementSystem.Application.Tests.SavingsAccounts
{
    public class UpdateSavingsAccountCommandHandlerTests
    {
        public static IEnumerable<object[]> GetSuccessfulResultTestData()
        {
            var fixture = new Fixture();
            fixture.Register<DomainEvent>(() => null!);
            var existingEntity = fixture.Create<SavingsAccount>();
            fixture.Customize<UpdateSavingsAccountCommand>(comp => comp.With(x => x.Id, existingEntity.Id));
            var testCommand = fixture.Create<UpdateSavingsAccountCommand>();
            yield return new object[] { testCommand, existingEntity };
        }

        [Theory]
        [MemberData(nameof(GetSuccessfulResultTestData))]
        public async Task Handle_WithValidCommand_UpdatesExistingEntity(
            UpdateSavingsAccountCommand testCommand,
            SavingsAccount existingEntity)
        {
            // Arrange
            var savingsAccountRepository = Substitute.For<ISavingsAccountRepository>();
            savingsAccountRepository.FindByIdAsync(testCommand.Id, CancellationToken.None)!.Returns(Task.FromResult(existingEntity));

            var sut = new UpdateSavingsAccountCommandHandler(savingsAccountRepository);

            // Act
            await sut.Handle(testCommand, CancellationToken.None);

            // Assert
            SavingsAccountAssertions.AssertEquivalent(testCommand, existingEntity);
        }

        [Fact]
        public async Task Handle_WithInvalidIdCommand_ReturnsNotFound()
        {
            // Arrange
            var fixture = new Fixture();
            var testCommand = fixture.Create<UpdateSavingsAccountCommand>();
            var savingsAccountRepository = Substitute.For<ISavingsAccountRepository>();
            savingsAccountRepository.FindByIdAsync(testCommand.Id, CancellationToken.None)!.Returns(Task.FromResult<SavingsAccount>(default));


            var sut = new UpdateSavingsAccountCommandHandler(savingsAccountRepository);

            // Act
            var act = async () => await sut.Handle(testCommand, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>();
        }
    }
}