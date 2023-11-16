using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using BankAccountManagementSystem.Application.Addresses;
using BankAccountManagementSystem.Application.Addresses.GetAddressById;
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

namespace BankAccountManagementSystem.Application.Tests.Addresses
{
    public class GetAddressByIdQueryHandlerTests
    {
        private readonly IMapper _mapper;

        public GetAddressByIdQueryHandlerTests()
        {
            var mapperConfiguration = new MapperConfiguration(
                config =>
                {
                    config.AddMaps(typeof(GetAddressByIdQueryHandler));
                });
            _mapper = mapperConfiguration.CreateMapper();
        }

        public static IEnumerable<object[]> GetSuccessfulResultTestData()
        {
            var fixture = new Fixture();
            fixture.Register<DomainEvent>(() => null!);

            var existingEntity = fixture.Create<Address>();
            fixture.Customize<GetAddressByIdQuery>(comp => comp.With(x => x.Id, existingEntity.Id));
            var testQuery = fixture.Create<GetAddressByIdQuery>();
            yield return new object[] { testQuery, existingEntity };
        }

        [Theory]
        [MemberData(nameof(GetSuccessfulResultTestData))]
        public async Task Handle_WithValidQuery_RetrievesAddress(GetAddressByIdQuery testQuery, Address existingEntity)
        {
            // Arrange
            var addressRepository = Substitute.For<IAddressRepository>();
            addressRepository.FindByIdAsync(testQuery.Id, CancellationToken.None)!.Returns(Task.FromResult(existingEntity));


            var sut = new GetAddressByIdQueryHandler(addressRepository, _mapper);

            // Act
            var results = await sut.Handle(testQuery, CancellationToken.None);

            // Assert
            AddressAssertions.AssertEquivalent(results, existingEntity);
        }

        [Fact]
        public async Task Handle_WithInvalidIdQuery_ThrowsNotFoundException()
        {
            // Arrange
            var fixture = new Fixture();
            var query = fixture.Create<GetAddressByIdQuery>();
            var addressRepository = Substitute.For<IAddressRepository>();
            addressRepository.FindByIdAsync(query.Id, CancellationToken.None)!.Returns(Task.FromResult<Address>(default));

            var sut = new GetAddressByIdQueryHandler(addressRepository, _mapper);

            // Act
            var act = async () => await sut.Handle(query, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>();
        }
    }
}