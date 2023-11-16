using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using BankAccountManagementSystem.Application.SavingsAccounts;
using BankAccountManagementSystem.Application.SavingsAccounts.GetSavingsAccountById;
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

namespace BankAccountManagementSystem.Application.Tests.SavingsAccounts
{
    public class GetSavingsAccountByIdQueryHandlerTests
    {
        private readonly IMapper _mapper;

        public GetSavingsAccountByIdQueryHandlerTests()
        {
            var mapperConfiguration = new MapperConfiguration(
                config =>
                {
                    config.AddMaps(typeof(GetSavingsAccountByIdQueryHandler));
                });
            _mapper = mapperConfiguration.CreateMapper();
        }

        public static IEnumerable<object[]> GetSuccessfulResultTestData()
        {
            var fixture = new Fixture();
            fixture.Register<DomainEvent>(() => null!);

            var existingEntity = fixture.Create<SavingsAccount>();
            fixture.Customize<GetSavingsAccountByIdQuery>(comp => comp.With(x => x.Id, existingEntity.Id));
            var testQuery = fixture.Create<GetSavingsAccountByIdQuery>();
            yield return new object[] { testQuery, existingEntity };
        }

        [Theory]
        [MemberData(nameof(GetSuccessfulResultTestData))]
        public async Task Handle_WithValidQuery_RetrievesSavingsAccount(
            GetSavingsAccountByIdQuery testQuery,
            SavingsAccount existingEntity)
        {
            // Arrange
            var savingsAccountRepository = Substitute.For<ISavingsAccountRepository>();
            savingsAccountRepository.FindByIdAsync(testQuery.Id, CancellationToken.None)!.Returns(Task.FromResult(existingEntity));


            var sut = new GetSavingsAccountByIdQueryHandler(savingsAccountRepository, _mapper);

            // Act
            var results = await sut.Handle(testQuery, CancellationToken.None);

            // Assert
            SavingsAccountAssertions.AssertEquivalent(results, existingEntity);
        }

        [Fact]
        public async Task Handle_WithInvalidIdQuery_ThrowsNotFoundException()
        {
            // Arrange
            var fixture = new Fixture();
            var query = fixture.Create<GetSavingsAccountByIdQuery>();
            var savingsAccountRepository = Substitute.For<ISavingsAccountRepository>();
            savingsAccountRepository.FindByIdAsync(query.Id, CancellationToken.None)!.Returns(Task.FromResult<SavingsAccount>(default));

            var sut = new GetSavingsAccountByIdQueryHandler(savingsAccountRepository, _mapper);

            // Act
            var act = async () => await sut.Handle(query, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>();
        }
    }
}