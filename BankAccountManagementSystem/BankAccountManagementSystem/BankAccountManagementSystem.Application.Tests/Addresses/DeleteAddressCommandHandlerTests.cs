using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using BankAccountManagementSystem.Application.Addresses.DeleteAddress;
using BankAccountManagementSystem.Domain.Common;
using BankAccountManagementSystem.Domain.Common.Exceptions;
using BankAccountManagementSystem.Domain.Entities;
using BankAccountManagementSystem.Domain.Repositories;
using FluentAssertions;
using Intent.RoslynWeaver.Attributes;
using NSubstitute;
using Xunit;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CRUD.Tests.Owner.DeleteCommandHandlerTests", Version = "1.0")]

namespace BankAccountManagementSystem.Application.Tests.Addresses
{
    public class DeleteAddressCommandHandlerTests
    {
        public static IEnumerable<object[]> GetSuccessfulResultTestData()
        {
            var fixture = new Fixture();
            fixture.Register<DomainEvent>(() => null!);
            var existingEntity = fixture.Create<Address>();
            fixture.Customize<DeleteAddressCommand>(comp => comp.With(x => x.Id, existingEntity.Id));
            var testCommand = fixture.Create<DeleteAddressCommand>();
            yield return new object[] { testCommand, existingEntity };
        }

        [Theory]
        [MemberData(nameof(GetSuccessfulResultTestData))]
        public async Task Handle_WithValidCommand_DeletesAddressFromRepository(
            DeleteAddressCommand testCommand,
            Address existingEntity)
        {
            // Arrange
            var addressRepository = Substitute.For<IAddressRepository>();
            addressRepository.FindByIdAsync(testCommand.Id, CancellationToken.None)!.Returns(Task.FromResult(existingEntity));

            var sut = new DeleteAddressCommandHandler(addressRepository);

            // Act
            await sut.Handle(testCommand, CancellationToken.None);

            // Assert
            addressRepository.Received(1).Remove(Arg.Is<Address>(p => testCommand.Id == p.Id));
        }

        [Fact]
        public async Task Handle_WithInvalidAddressId_ReturnsNotFound()
        {
            // Arrange
            var addressRepository = Substitute.For<IAddressRepository>();
            var fixture = new Fixture();
            var testCommand = fixture.Create<DeleteAddressCommand>();
            addressRepository.FindByIdAsync(testCommand.Id, CancellationToken.None)!.Returns(Task.FromResult<Address>(default));


            var sut = new DeleteAddressCommandHandler(addressRepository);

            // Act
            var act = async () => await sut.Handle(testCommand, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>();
        }
    }
}