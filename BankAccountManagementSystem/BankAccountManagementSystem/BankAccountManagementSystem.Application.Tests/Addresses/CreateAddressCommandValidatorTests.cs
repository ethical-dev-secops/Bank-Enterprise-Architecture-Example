using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using BankAccountManagementSystem.Application.Addresses.CreateAddress;
using BankAccountManagementSystem.Application.Common.Behaviours;
using FluentAssertions;
using FluentValidation;
using Intent.RoslynWeaver.Attributes;
using MediatR;
using NSubstitute;
using Xunit;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CRUD.Tests.FluentValidation.FluentValidationTest", Version = "1.0")]

namespace BankAccountManagementSystem.Application.Tests.Addresses
{
    public class CreateAddressCommandValidatorTests
    {
        public static IEnumerable<object[]> GetSuccessfulResultTestData()
        {
            var fixture = new Fixture();
            var testCommand = fixture.Create<CreateAddressCommand>();
            testCommand.StreetName = $"{string.Join(string.Empty, fixture.CreateMany<char>(30))}";
            testCommand.ErfNumber = $"{string.Join(string.Empty, fixture.CreateMany<char>(30))}";
            testCommand.Suburb = $"{string.Join(string.Empty, fixture.CreateMany<char>(30))}";
            testCommand.City = $"{string.Join(string.Empty, fixture.CreateMany<char>(30))}";
            testCommand.Country = $"{string.Join(string.Empty, fixture.CreateMany<char>(30))}";
            yield return new object[] { testCommand };
        }

        [Theory]
        [MemberData(nameof(GetSuccessfulResultTestData))]
        public async Task Validate_WithValidCommand_PassesValidation(CreateAddressCommand testCommand)
        {
            // Arrange
            var validator = GetValidationBehaviour();
            var expectedId = new Fixture().Create<long>();
            // Act
            var result = await validator.Handle(testCommand, () => Task.FromResult(expectedId), CancellationToken.None);

            // Assert
            result.Should().Be(expectedId);
        }

        public static IEnumerable<object[]> GetFailedResultTestData()
        {
            var fixture = new Fixture();
            fixture.Customize<CreateAddressCommand>(comp => comp
                .With(x => x.StreetName, () => default)
                .With(x => x.ErfNumber, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.Suburb, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.City, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.Country, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}"));
            var testCommand = fixture.Create<CreateAddressCommand>();
            yield return new object[] { testCommand, "StreetName", "not be empty" };

            fixture = new Fixture();
            fixture.Customize<CreateAddressCommand>(comp => comp
                .With(x => x.StreetName, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(31))}")
                .With(x => x.ErfNumber, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.Suburb, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.City, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.Country, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}"));
            testCommand = fixture.Create<CreateAddressCommand>();
            yield return new object[] { testCommand, "StreetName", "must be 30 characters or fewer" };

            fixture = new Fixture();
            fixture.Customize<CreateAddressCommand>(comp => comp
                .With(x => x.ErfNumber, () => default)
                .With(x => x.StreetName, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.Suburb, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.City, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.Country, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}"));
            testCommand = fixture.Create<CreateAddressCommand>();
            yield return new object[] { testCommand, "ErfNumber", "not be empty" };

            fixture = new Fixture();
            fixture.Customize<CreateAddressCommand>(comp => comp
                .With(x => x.ErfNumber, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(31))}")
                .With(x => x.StreetName, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.Suburb, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.City, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.Country, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}"));
            testCommand = fixture.Create<CreateAddressCommand>();
            yield return new object[] { testCommand, "ErfNumber", "must be 30 characters or fewer" };

            fixture = new Fixture();
            fixture.Customize<CreateAddressCommand>(comp => comp
                .With(x => x.Suburb, () => default)
                .With(x => x.StreetName, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.ErfNumber, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.City, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.Country, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}"));
            testCommand = fixture.Create<CreateAddressCommand>();
            yield return new object[] { testCommand, "Suburb", "not be empty" };

            fixture = new Fixture();
            fixture.Customize<CreateAddressCommand>(comp => comp
                .With(x => x.Suburb, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(31))}")
                .With(x => x.StreetName, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.ErfNumber, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.City, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.Country, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}"));
            testCommand = fixture.Create<CreateAddressCommand>();
            yield return new object[] { testCommand, "Suburb", "must be 30 characters or fewer" };

            fixture = new Fixture();
            fixture.Customize<CreateAddressCommand>(comp => comp
                .With(x => x.City, () => default)
                .With(x => x.StreetName, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.ErfNumber, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.Suburb, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.Country, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}"));
            testCommand = fixture.Create<CreateAddressCommand>();
            yield return new object[] { testCommand, "City", "not be empty" };

            fixture = new Fixture();
            fixture.Customize<CreateAddressCommand>(comp => comp
                .With(x => x.City, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(31))}")
                .With(x => x.StreetName, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.ErfNumber, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.Suburb, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.Country, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}"));
            testCommand = fixture.Create<CreateAddressCommand>();
            yield return new object[] { testCommand, "City", "must be 30 characters or fewer" };

            fixture = new Fixture();
            fixture.Customize<CreateAddressCommand>(comp => comp
                .With(x => x.Country, () => default)
                .With(x => x.StreetName, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.ErfNumber, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.Suburb, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.City, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}"));
            testCommand = fixture.Create<CreateAddressCommand>();
            yield return new object[] { testCommand, "Country", "not be empty" };

            fixture = new Fixture();
            fixture.Customize<CreateAddressCommand>(comp => comp
                .With(x => x.Country, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(31))}")
                .With(x => x.StreetName, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.ErfNumber, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.Suburb, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.City, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}"));
            testCommand = fixture.Create<CreateAddressCommand>();
            yield return new object[] { testCommand, "Country", "must be 30 characters or fewer" };
        }

        [Theory]
        [MemberData(nameof(GetFailedResultTestData))]
        public async Task Validate_WithInvalidCommand_FailsValidation(
            CreateAddressCommand testCommand,
            string expectedPropertyName,
            string expectedPhrase)
        {
            // Arrange
            var validator = GetValidationBehaviour();
            var expectedId = new Fixture().Create<long>();
            // Act
            var act = async () => await validator.Handle(testCommand, () => Task.FromResult(expectedId), CancellationToken.None);

            // Assert
            act.Should().ThrowAsync<ValidationException>().Result
            .Which.Errors.Should().Contain(x => x.PropertyName == expectedPropertyName && x.ErrorMessage.Contains(expectedPhrase));
        }

        private ValidationBehaviour<CreateAddressCommand, long> GetValidationBehaviour()
        {
            return new ValidationBehaviour<CreateAddressCommand, long>(new[] { new CreateAddressCommandValidator() });
        }
    }
}