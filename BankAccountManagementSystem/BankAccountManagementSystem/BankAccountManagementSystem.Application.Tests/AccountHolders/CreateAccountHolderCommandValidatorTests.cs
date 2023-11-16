using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using BankAccountManagementSystem.Application.AccountHolders.CreateAccountHolder;
using BankAccountManagementSystem.Application.Common.Behaviours;
using FluentAssertions;
using FluentValidation;
using Intent.RoslynWeaver.Attributes;
using MediatR;
using NSubstitute;
using Xunit;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CRUD.Tests.FluentValidation.FluentValidationTest", Version = "1.0")]

namespace BankAccountManagementSystem.Application.Tests.AccountHolders
{
    public class CreateAccountHolderCommandValidatorTests
    {
        public static IEnumerable<object[]> GetSuccessfulResultTestData()
        {
            var fixture = new Fixture();
            var testCommand = fixture.Create<CreateAccountHolderCommand>();
            testCommand.FirstName = $"{string.Join(string.Empty, fixture.CreateMany<char>(30))}";
            testCommand.LastName = $"{string.Join(string.Empty, fixture.CreateMany<char>(30))}";
            testCommand.IdentityNumber = $"{string.Join(string.Empty, fixture.CreateMany<char>(30))}";
            testCommand.PassportNumber = $"{string.Join(string.Empty, fixture.CreateMany<char>(30))}";
            yield return new object[] { testCommand };
        }

        [Theory]
        [MemberData(nameof(GetSuccessfulResultTestData))]
        public async Task Validate_WithValidCommand_PassesValidation(CreateAccountHolderCommand testCommand)
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
            fixture.Customize<CreateAccountHolderCommand>(comp => comp
                .With(x => x.FirstName, () => default)
                .With(x => x.LastName, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.IdentityNumber, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.PassportNumber, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}"));
            var testCommand = fixture.Create<CreateAccountHolderCommand>();
            yield return new object[] { testCommand, "FirstName", "not be empty" };

            fixture = new Fixture();
            fixture.Customize<CreateAccountHolderCommand>(comp => comp
                .With(x => x.FirstName, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(31))}")
                .With(x => x.LastName, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.IdentityNumber, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.PassportNumber, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}"));
            testCommand = fixture.Create<CreateAccountHolderCommand>();
            yield return new object[] { testCommand, "FirstName", "must be 30 characters or fewer" };

            fixture = new Fixture();
            fixture.Customize<CreateAccountHolderCommand>(comp => comp
                .With(x => x.LastName, () => default)
                .With(x => x.FirstName, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.IdentityNumber, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.PassportNumber, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}"));
            testCommand = fixture.Create<CreateAccountHolderCommand>();
            yield return new object[] { testCommand, "LastName", "not be empty" };

            fixture = new Fixture();
            fixture.Customize<CreateAccountHolderCommand>(comp => comp
                .With(x => x.LastName, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(31))}")
                .With(x => x.FirstName, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.IdentityNumber, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.PassportNumber, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}"));
            testCommand = fixture.Create<CreateAccountHolderCommand>();
            yield return new object[] { testCommand, "LastName", "must be 30 characters or fewer" };

            fixture = new Fixture();
            fixture.Customize<CreateAccountHolderCommand>(comp => comp
                .With(x => x.IdentityNumber, () => default)
                .With(x => x.FirstName, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.LastName, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.PassportNumber, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}"));
            testCommand = fixture.Create<CreateAccountHolderCommand>();
            yield return new object[] { testCommand, "IdentityNumber", "not be empty" };

            fixture = new Fixture();
            fixture.Customize<CreateAccountHolderCommand>(comp => comp
                .With(x => x.IdentityNumber, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(31))}")
                .With(x => x.FirstName, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.LastName, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.PassportNumber, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}"));
            testCommand = fixture.Create<CreateAccountHolderCommand>();
            yield return new object[] { testCommand, "IdentityNumber", "must be 30 characters or fewer" };

            fixture = new Fixture();
            fixture.Customize<CreateAccountHolderCommand>(comp => comp
                .With(x => x.PassportNumber, () => default)
                .With(x => x.FirstName, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.LastName, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.IdentityNumber, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}"));
            testCommand = fixture.Create<CreateAccountHolderCommand>();
            yield return new object[] { testCommand, "PassportNumber", "not be empty" };

            fixture = new Fixture();
            fixture.Customize<CreateAccountHolderCommand>(comp => comp
                .With(x => x.PassportNumber, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(31))}")
                .With(x => x.FirstName, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.LastName, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}")
                .With(x => x.IdentityNumber, () => $"{string.Join(string.Empty, fixture.CreateMany<char>(29))}"));
            testCommand = fixture.Create<CreateAccountHolderCommand>();
            yield return new object[] { testCommand, "PassportNumber", "must be 30 characters or fewer" };
        }

        [Theory]
        [MemberData(nameof(GetFailedResultTestData))]
        public async Task Validate_WithInvalidCommand_FailsValidation(
            CreateAccountHolderCommand testCommand,
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

        private ValidationBehaviour<CreateAccountHolderCommand, long> GetValidationBehaviour()
        {
            return new ValidationBehaviour<CreateAccountHolderCommand, long>(new[] { new CreateAccountHolderCommandValidator() });
        }
    }
}