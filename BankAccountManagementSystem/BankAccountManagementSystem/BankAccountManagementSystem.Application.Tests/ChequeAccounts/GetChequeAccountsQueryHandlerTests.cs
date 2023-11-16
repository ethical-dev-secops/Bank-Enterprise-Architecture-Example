using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using BankAccountManagementSystem.Application.ChequeAccounts;
using BankAccountManagementSystem.Application.ChequeAccounts.GetChequeAccounts;
using BankAccountManagementSystem.Domain.Common;
using BankAccountManagementSystem.Domain.Entities;
using BankAccountManagementSystem.Domain.Repositories;
using FluentAssertions;
using Intent.RoslynWeaver.Attributes;
using NSubstitute;
using Xunit;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CRUD.Tests.Owner.GetAllQueryHandlerTests", Version = "1.0")]

namespace BankAccountManagementSystem.Application.Tests.ChequeAccounts
{
    public class GetChequeAccountsQueryHandlerTests
    {
        private readonly IMapper _mapper;

        public GetChequeAccountsQueryHandlerTests()
        {
            var mapperConfiguration = new MapperConfiguration(
                config =>
                {
                    config.AddMaps(typeof(GetChequeAccountsQueryHandler));
                });
            _mapper = mapperConfiguration.CreateMapper();
        }

        public static IEnumerable<object[]> GetSuccessfulResultTestData()
        {
            var fixture = new Fixture();
            fixture.Register<DomainEvent>(() => null!);
            yield return new object[] { fixture.CreateMany<ChequeAccount>().ToList() };
            yield return new object[] { fixture.CreateMany<ChequeAccount>(0).ToList() };
        }

        [Theory]
        [MemberData(nameof(GetSuccessfulResultTestData))]
        public async Task Handle_WithValidQuery_RetrievesChequeAccounts(List<ChequeAccount> testEntities)
        {
            // Arrange
            var fixture = new Fixture();
            var testQuery = fixture.Create<GetChequeAccountsQuery>();
            var chequeAccountRepository = Substitute.For<IChequeAccountRepository>();
            chequeAccountRepository.FindAllAsync(CancellationToken.None).Returns(Task.FromResult(testEntities));

            var sut = new GetChequeAccountsQueryHandler(chequeAccountRepository, _mapper);

            // Act
            var results = await sut.Handle(testQuery, CancellationToken.None);

            // Assert
            ChequeAccountAssertions.AssertEquivalent(results, testEntities);
        }
    }
}