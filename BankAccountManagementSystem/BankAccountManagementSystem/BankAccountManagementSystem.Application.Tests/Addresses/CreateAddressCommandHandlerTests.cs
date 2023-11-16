using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using BankAccountManagementSystem.Application.Addresses.CreateAddress;
using BankAccountManagementSystem.Application.Tests.Extensions;
using BankAccountManagementSystem.Domain.Entities;
using BankAccountManagementSystem.Domain.Repositories;
using FluentAssertions;
using Intent.RoslynWeaver.Attributes;
using NSubstitute;
using Xunit;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CRUD.Tests.Owner.CreateCommandHandlerTests", Version = "1.0")]

namespace BankAccountManagementSystem.Application.Tests.Addresses
{
    public class CreateAddressCommandHandlerTests
    {
        public static IEnumerable<object[]> GetSuccessfulResultTestData()
        {
            var fixture = new Fixture();
            yield return new object[] { fixture.Create<CreateAddressCommand>() };
        }

        [Theory]
        [MemberData(nameof(GetSuccessfulResultTestData))]
        public async Task Handle_WithValidCommand_AddsAddressToRepository(CreateAddressCommand testCommand)
        {
            // Arrange
            var addressRepository = Substitute.For<IAddressRepository>();
            var expectedAddressId = new Fixture().Create<long>();
            Address addedAddress = null;
            addressRepository.OnAdd(ent => addedAddress = ent);
            addressRepository.UnitOfWork
                .When(async x => await x.SaveChangesAsync(CancellationToken.None))
                .Do(_ => addedAddress.Id = expectedAddressId);

            var sut = new CreateAddressCommandHandler(addressRepository);

            // Act
            var result = await sut.Handle(testCommand, CancellationToken.None);

            // Assert
            result.Should().Be(expectedAddressId);
            await addressRepository.UnitOfWork.Received(1).SaveChangesAsync();
            AddressAssertions.AssertEquivalent(testCommand, addedAddress);
        }
    }
}