using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using BankAccountManagementSystem.Application.Addresses.UpdateAddress;
using BankAccountManagementSystem.Domain.Common;
using BankAccountManagementSystem.Domain.Common.Exceptions;
using BankAccountManagementSystem.Domain.Entities;
using BankAccountManagementSystem.Domain.Repositories;
using FluentAssertions;
using Intent.RoslynWeaver.Attributes;
using NSubstitute;
using Xunit;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CRUD.Tests.Owner.UpdateCommandHandlerTests", Version = "1.0")]

namespace BankAccountManagementSystem.Application.Tests.Addresses
{
    public class UpdateAddressCommandHandlerTests
    {
        public static IEnumerable<object[]> GetSuccessfulResultTestData()
        {
            var fixture = new Fixture();
            fixture.Register<DomainEvent>(() => null!);
            var existingEntity = fixture.Create<Address>();
            fixture.Customize<UpdateAddressCommand>(comp => comp.With(x => x.Id, existingEntity.Id));
            var testCommand = fixture.Create<UpdateAddressCommand>();
            yield return new object[] { testCommand, existingEntity };
        }

        [Theory]
        [MemberData(nameof(GetSuccessfulResultTestData))]
        public async Task Handle_WithValidCommand_UpdatesExistingEntity(
            UpdateAddressCommand testCommand,
            Address existingEntity)
        {
            // Arrange
            var addressRepository = Substitute.For<IAddressRepository>();
            addressRepository.FindByIdAsync(testCommand.Id, CancellationToken.None)!.Returns(Task.FromResult(existingEntity));

            var sut = new UpdateAddressCommandHandler(addressRepository);

            // Act
            await sut.Handle(testCommand, CancellationToken.None);

            // Assert
            AddressAssertions.AssertEquivalent(testCommand, existingEntity);
        }

        [Fact]
        public async Task Handle_WithInvalidIdCommand_ReturnsNotFound()
        {
            // Arrange
            var fixture = new Fixture();
            var testCommand = fixture.Create<UpdateAddressCommand>();
            var addressRepository = Substitute.For<IAddressRepository>();
            addressRepository.FindByIdAsync(testCommand.Id, CancellationToken.None)!.Returns(Task.FromResult<Address>(default));


            var sut = new UpdateAddressCommandHandler(addressRepository);

            // Act
            var act = async () => await sut.Handle(testCommand, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>();
        }
    }
}