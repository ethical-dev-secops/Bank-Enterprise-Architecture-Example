using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using BankAccountManagementSystem.Application.AccountHolders;
using BankAccountManagementSystem.Application.AccountHolders.GetAccountHolders;
using BankAccountManagementSystem.Domain.Common;
using BankAccountManagementSystem.Domain.Entities;
using BankAccountManagementSystem.Domain.Repositories;
using FluentAssertions;
using Intent.RoslynWeaver.Attributes;
using NSubstitute;
using Xunit;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CRUD.Tests.Owner.GetAllQueryHandlerTests", Version = "1.0")]

namespace BankAccountManagementSystem.Application.Tests.AccountHolders
{
    public class GetAccountHoldersQueryHandlerTests
    {
        private readonly IMapper _mapper;

        public GetAccountHoldersQueryHandlerTests()
        {
            var mapperConfiguration = new MapperConfiguration(
                config =>
                {
                    config.AddMaps(typeof(GetAccountHoldersQueryHandler));
                });
            _mapper = mapperConfiguration.CreateMapper();
        }

        public static IEnumerable<object[]> GetSuccessfulResultTestData()
        {
            var fixture = new Fixture();
            fixture.Register<DomainEvent>(() => null!);
            yield return new object[] { fixture.CreateMany<AccountHolder>().ToList() };
            yield return new object[] { fixture.CreateMany<AccountHolder>(0).ToList() };
        }

        [Theory]
        [MemberData(nameof(GetSuccessfulResultTestData))]
        public async Task Handle_WithValidQuery_RetrievesAccountHolders(List<AccountHolder> testEntities)
        {
            // Arrange
            var fixture = new Fixture();
            var testQuery = fixture.Create<GetAccountHoldersQuery>();
            var accountHolderRepository = Substitute.For<IAccountHolderRepository>();
            accountHolderRepository.FindAllAsync(CancellationToken.None).Returns(Task.FromResult(testEntities));

            var sut = new GetAccountHoldersQueryHandler(accountHolderRepository, _mapper);

            // Act
            var results = await sut.Handle(testQuery, CancellationToken.None);

            // Assert
            AccountHolderAssertions.AssertEquivalent(results, testEntities);
        }
    }
}