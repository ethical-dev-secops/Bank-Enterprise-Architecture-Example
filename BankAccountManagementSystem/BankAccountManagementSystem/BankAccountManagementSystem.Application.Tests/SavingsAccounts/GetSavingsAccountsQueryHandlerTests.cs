using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using BankAccountManagementSystem.Application.SavingsAccounts;
using BankAccountManagementSystem.Application.SavingsAccounts.GetSavingsAccounts;
using BankAccountManagementSystem.Domain.Common;
using BankAccountManagementSystem.Domain.Entities;
using BankAccountManagementSystem.Domain.Repositories;
using FluentAssertions;
using Intent.RoslynWeaver.Attributes;
using NSubstitute;
using Xunit;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CRUD.Tests.Owner.GetAllQueryHandlerTests", Version = "1.0")]

namespace BankAccountManagementSystem.Application.Tests.SavingsAccounts
{
    public class GetSavingsAccountsQueryHandlerTests
    {
        private readonly IMapper _mapper;

        public GetSavingsAccountsQueryHandlerTests()
        {
            var mapperConfiguration = new MapperConfiguration(
                config =>
                {
                    config.AddMaps(typeof(GetSavingsAccountsQueryHandler));
                });
            _mapper = mapperConfiguration.CreateMapper();
        }

        public static IEnumerable<object[]> GetSuccessfulResultTestData()
        {
            var fixture = new Fixture();
            fixture.Register<DomainEvent>(() => null!);
            yield return new object[] { fixture.CreateMany<SavingsAccount>().ToList() };
            yield return new object[] { fixture.CreateMany<SavingsAccount>(0).ToList() };
        }

        [Theory]
        [MemberData(nameof(GetSuccessfulResultTestData))]
        public async Task Handle_WithValidQuery_RetrievesSavingsAccounts(List<SavingsAccount> testEntities)
        {
            // Arrange
            var fixture = new Fixture();
            var testQuery = fixture.Create<GetSavingsAccountsQuery>();
            var savingsAccountRepository = Substitute.For<ISavingsAccountRepository>();
            savingsAccountRepository.FindAllAsync(CancellationToken.None).Returns(Task.FromResult(testEntities));

            var sut = new GetSavingsAccountsQueryHandler(savingsAccountRepository, _mapper);

            // Act
            var results = await sut.Handle(testQuery, CancellationToken.None);

            // Assert
            SavingsAccountAssertions.AssertEquivalent(results, testEntities);
        }
    }
}