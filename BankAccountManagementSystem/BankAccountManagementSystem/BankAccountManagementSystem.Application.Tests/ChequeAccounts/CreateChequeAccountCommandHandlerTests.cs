using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using BankAccountManagementSystem.Application.ChequeAccounts.CreateChequeAccount;
using BankAccountManagementSystem.Application.Tests.Extensions;
using BankAccountManagementSystem.Domain.Entities;
using BankAccountManagementSystem.Domain.Repositories;
using FluentAssertions;
using Intent.RoslynWeaver.Attributes;
using NSubstitute;
using Xunit;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CRUD.Tests.Owner.CreateCommandHandlerTests", Version = "1.0")]

namespace BankAccountManagementSystem.Application.Tests.ChequeAccounts
{
    public class CreateChequeAccountCommandHandlerTests
    {
        public static IEnumerable<object[]> GetSuccessfulResultTestData()
        {
            var fixture = new Fixture();
            yield return new object[] { fixture.Create<CreateChequeAccountCommand>() };
        }

        [Theory]
        [MemberData(nameof(GetSuccessfulResultTestData))]
        public async Task Handle_WithValidCommand_AddsChequeAccountToRepository(CreateChequeAccountCommand testCommand)
        {
            // Arrange
            var chequeAccountRepository = Substitute.For<IChequeAccountRepository>();
            var expectedChequeAccountId = new Fixture().Create<long>();
            ChequeAccount addedChequeAccount = null;
            chequeAccountRepository.OnAdd(ent => addedChequeAccount = ent);
            chequeAccountRepository.UnitOfWork
                .When(async x => await x.SaveChangesAsync(CancellationToken.None))
                .Do(_ => addedChequeAccount.Id = expectedChequeAccountId);

            var sut = new CreateChequeAccountCommandHandler(chequeAccountRepository);

            // Act
            var result = await sut.Handle(testCommand, CancellationToken.None);

            // Assert
            result.Should().Be(expectedChequeAccountId);
            await chequeAccountRepository.UnitOfWork.Received(1).SaveChangesAsync();
            ChequeAccountAssertions.AssertEquivalent(testCommand, addedChequeAccount);
        }
    }
}