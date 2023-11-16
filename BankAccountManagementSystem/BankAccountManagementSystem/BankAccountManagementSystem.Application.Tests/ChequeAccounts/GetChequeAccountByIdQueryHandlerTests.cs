using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using BankAccountManagementSystem.Application.ChequeAccounts;
using BankAccountManagementSystem.Application.ChequeAccounts.GetChequeAccountById;
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

namespace BankAccountManagementSystem.Application.Tests.ChequeAccounts
{
    public class GetChequeAccountByIdQueryHandlerTests
    {
        private readonly IMapper _mapper;

        public GetChequeAccountByIdQueryHandlerTests()
        {
            var mapperConfiguration = new MapperConfiguration(
                config =>
                {
                    config.AddMaps(typeof(GetChequeAccountByIdQueryHandler));
                });
            _mapper = mapperConfiguration.CreateMapper();
        }

        public static IEnumerable<object[]> GetSuccessfulResultTestData()
        {
            var fixture = new Fixture();
            fixture.Register<DomainEvent>(() => null!);

            var existingEntity = fixture.Create<ChequeAccount>();
            fixture.Customize<GetChequeAccountByIdQuery>(comp => comp.With(x => x.Id, existingEntity.Id));
            var testQuery = fixture.Create<GetChequeAccountByIdQuery>();
            yield return new object[] { testQuery, existingEntity };
        }

        [Theory]
        [MemberData(nameof(GetSuccessfulResultTestData))]
        public async Task Handle_WithValidQuery_RetrievesChequeAccount(
            GetChequeAccountByIdQuery testQuery,
            ChequeAccount existingEntity)
        {
            // Arrange
            var chequeAccountRepository = Substitute.For<IChequeAccountRepository>();
            chequeAccountRepository.FindByIdAsync(testQuery.Id, CancellationToken.None)!.Returns(Task.FromResult(existingEntity));


            var sut = new GetChequeAccountByIdQueryHandler(chequeAccountRepository, _mapper);

            // Act
            var results = await sut.Handle(testQuery, CancellationToken.None);

            // Assert
            ChequeAccountAssertions.AssertEquivalent(results, existingEntity);
        }

        [Fact]
        public async Task Handle_WithInvalidIdQuery_ThrowsNotFoundException()
        {
            // Arrange
            var fixture = new Fixture();
            var query = fixture.Create<GetChequeAccountByIdQuery>();
            var chequeAccountRepository = Substitute.For<IChequeAccountRepository>();
            chequeAccountRepository.FindByIdAsync(query.Id, CancellationToken.None)!.Returns(Task.FromResult<ChequeAccount>(default));

            var sut = new GetChequeAccountByIdQueryHandler(chequeAccountRepository, _mapper);

            // Act
            var act = async () => await sut.Handle(query, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>();
        }
    }
}