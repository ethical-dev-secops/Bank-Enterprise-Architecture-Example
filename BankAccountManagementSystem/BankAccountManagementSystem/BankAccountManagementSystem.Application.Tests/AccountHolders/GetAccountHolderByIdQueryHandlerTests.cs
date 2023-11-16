using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using BankAccountManagementSystem.Application.AccountHolders;
using BankAccountManagementSystem.Application.AccountHolders.GetAccountHolderById;
using BankAccountManagementSystem.Domain.Common;
using BankAccountManagementSystem.Domain.Common.Exceptions;
using BankAccountManagementSystem.Domain.Entities;
using BankAccountManagementSystem.Domain.Repositories;
using FluentAssertions;
using Intent.RoslynWeaver.Attributes;
using NSubstitute;
using Xunit;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CRUD.Tests.Owner.GetByIdQueryHandlerTests", Version = "1.0")]

namespace BankAccountManagementSystem.Application.Tests.AccountHolders
{
    public class GetAccountHolderByIdQueryHandlerTests
    {
        private readonly IMapper _mapper;

        public GetAccountHolderByIdQueryHandlerTests()
        {
            var mapperConfiguration = new MapperConfiguration(
                config =>
                {
                    config.AddMaps(typeof(GetAccountHolderByIdQueryHandler));
                });
            _mapper = mapperConfiguration.CreateMapper();
        }

        public static IEnumerable<object[]> GetSuccessfulResultTestData()
        {
            var fixture = new Fixture();
            fixture.Register<DomainEvent>(() => null!);

            var existingEntity = fixture.Create<AccountHolder>();
            fixture.Customize<GetAccountHolderByIdQuery>(comp => comp.With(x => x.Id, existingEntity.Id));
            var testQuery = fixture.Create<GetAccountHolderByIdQuery>();
            yield return new object[] { testQuery, existingEntity };
        }

        [Theory]
        [MemberData(nameof(GetSuccessfulResultTestData))]
        public async Task Handle_WithValidQuery_RetrievesAccountHolder(
            GetAccountHolderByIdQuery testQuery,
            AccountHolder existingEntity)
        {
            // Arrange
            var accountHolderRepository = Substitute.For<IAccountHolderRepository>();
            accountHolderRepository.FindByIdAsync(testQuery.Id, CancellationToken.None)!.Returns(Task.FromResult(existingEntity));


            var sut = new GetAccountHolderByIdQueryHandler(accountHolderRepository, _mapper);

            // Act
            var results = await sut.Handle(testQuery, CancellationToken.None);

            // Assert
            AccountHolderAssertions.AssertEquivalent(results, existingEntity);
        }

        [Fact]
        public async Task Handle_WithInvalidIdQuery_ThrowsNotFoundException()
        {
            // Arrange
            var fixture = new Fixture();
            var query = fixture.Create<GetAccountHolderByIdQuery>();
            var accountHolderRepository = Substitute.For<IAccountHolderRepository>();
            accountHolderRepository.FindByIdAsync(query.Id, CancellationToken.None)!.Returns(Task.FromResult<AccountHolder>(default));

            var sut = new GetAccountHolderByIdQueryHandler(accountHolderRepository, _mapper);

            // Act
            var act = async () => await sut.Handle(query, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>();
        }
    }
}