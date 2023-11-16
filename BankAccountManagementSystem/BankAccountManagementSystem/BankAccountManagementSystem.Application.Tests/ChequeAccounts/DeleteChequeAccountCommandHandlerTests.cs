using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using BankAccountManagementSystem.Application.ChequeAccounts.DeleteChequeAccount;
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

namespace BankAccountManagementSystem.Application.Tests.ChequeAccounts
{
    public class DeleteChequeAccountCommandHandlerTests
    {
        public static IEnumerable<object[]> GetSuccessfulResultTestData()
        {
            var fixture = new Fixture();
            fixture.Register<DomainEvent>(() => null!);
            var existingEntity = fixture.Create<ChequeAccount>();
            fixture.Customize<DeleteChequeAccountCommand>(comp => comp.With(x => x.Id, existingEntity.Id));
            var testCommand = fixture.Create<DeleteChequeAccountCommand>();
            yield return new object[] { testCommand, existingEntity };
        }

        [Theory]
        [MemberData(nameof(GetSuccessfulResultTestData))]
        public async Task Handle_WithValidCommand_DeletesChequeAccountFromRepository(
            DeleteChequeAccountCommand testCommand,
            ChequeAccount existingEntity)
        {
            // Arrange
            var chequeAccountRepository = Substitute.For<IChequeAccountRepository>();
            chequeAccountRepository.FindByIdAsync(testCommand.Id, CancellationToken.None)!.Returns(Task.FromResult(existingEntity));

            var sut = new DeleteChequeAccountCommandHandler(chequeAccountRepository);

            // Act
            await sut.Handle(testCommand, CancellationToken.None);

            // Assert
            chequeAccountRepository.Received(1).Remove(Arg.Is<ChequeAccount>(p => testCommand.Id == p.Id));
        }

        [Fact]
        public async Task Handle_WithInvalidChequeAccountId_ReturnsNotFound()
        {
            // Arrange
            var chequeAccountRepository = Substitute.For<IChequeAccountRepository>();
            var fixture = new Fixture();
            var testCommand = fixture.Create<DeleteChequeAccountCommand>();
            chequeAccountRepository.FindByIdAsync(testCommand.Id, CancellationToken.None)!.Returns(Task.FromResult<ChequeAccount>(default));


            var sut = new DeleteChequeAccountCommandHandler(chequeAccountRepository);

            // Act
            var act = async () => await sut.Handle(testCommand, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>();
        }
    }
}