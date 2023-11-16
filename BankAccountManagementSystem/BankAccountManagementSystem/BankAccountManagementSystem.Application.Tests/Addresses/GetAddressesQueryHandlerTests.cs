using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using BankAccountManagementSystem.Application.Addresses;
using BankAccountManagementSystem.Application.Addresses.GetAddresses;
using BankAccountManagementSystem.Domain.Common;
using BankAccountManagementSystem.Domain.Entities;
using BankAccountManagementSystem.Domain.Repositories;
using FluentAssertions;
using Intent.RoslynWeaver.Attributes;
using NSubstitute;
using Xunit;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CRUD.Tests.Owner.GetAllQueryHandlerTests", Version = "1.0")]

namespace BankAccountManagementSystem.Application.Tests.Addresses
{
    public class GetAddressesQueryHandlerTests
    {
        private readonly IMapper _mapper;

        public GetAddressesQueryHandlerTests()
        {
            var mapperConfiguration = new MapperConfiguration(
                config =>
                {
                    config.AddMaps(typeof(GetAddressesQueryHandler));
                });
            _mapper = mapperConfiguration.CreateMapper();
        }

        public static IEnumerable<object[]> GetSuccessfulResultTestData()
        {
            var fixture = new Fixture();
            fixture.Register<DomainEvent>(() => null!);
            yield return new object[] { fixture.CreateMany<Address>().ToList() };
            yield return new object[] { fixture.CreateMany<Address>(0).ToList() };
        }

        [Theory]
        [MemberData(nameof(GetSuccessfulResultTestData))]
        public async Task Handle_WithValidQuery_RetrievesAddresses(List<Address> testEntities)
        {
            // Arrange
            var fixture = new Fixture();
            var testQuery = fixture.Create<GetAddressesQuery>();
            var addressRepository = Substitute.For<IAddressRepository>();
            addressRepository.FindAllAsync(CancellationToken.None).Returns(Task.FromResult(testEntities));

            var sut = new GetAddressesQueryHandler(addressRepository, _mapper);

            // Act
            var results = await sut.Handle(testQuery, CancellationToken.None);

            // Assert
            AddressAssertions.AssertEquivalent(results, testEntities);
        }
    }
}