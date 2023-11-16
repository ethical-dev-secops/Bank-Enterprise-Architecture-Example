using System.Collections.Generic;
using System.Linq;
using BankAccountManagementSystem.Application.Addresses;
using BankAccountManagementSystem.Application.Addresses.CreateAddress;
using BankAccountManagementSystem.Application.Addresses.UpdateAddress;
using BankAccountManagementSystem.Domain.Entities;
using FluentAssertions;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CRUD.Tests.Assertions.AssertionClass", Version = "1.0")]

namespace BankAccountManagementSystem.Application.Tests.Addresses
{
    public static class AddressAssertions
    {
        public static void AssertEquivalent(CreateAddressCommand expectedDto, Address actualEntity)
        {
            if (expectedDto == null)
            {
                actualEntity.Should().BeNull();
                return;
            }

            actualEntity.Should().NotBeNull();
            actualEntity.StreetName.Should().Be(expectedDto.StreetName);
            actualEntity.ErfNumber.Should().Be(expectedDto.ErfNumber);
            actualEntity.Suburb.Should().Be(expectedDto.Suburb);
            actualEntity.City.Should().Be(expectedDto.City);
            actualEntity.Country.Should().Be(expectedDto.Country);
        }

        public static void AssertEquivalent(IEnumerable<AddressDto> actualDtos, IEnumerable<Address> expectedEntities)
        {
            if (expectedEntities == null)
            {
                actualDtos.Should().BeNullOrEmpty();
                return;
            }

            actualDtos.Should().HaveSameCount(actualDtos);
            for (int i = 0; i < expectedEntities.Count(); i++)
            {
                var entity = expectedEntities.ElementAt(i);
                var dto = actualDtos.ElementAt(i);
                if (entity == null)
                {
                    dto.Should().BeNull();
                    continue;
                }

                dto.Should().NotBeNull();
                dto.Id.Should().Be(entity.Id);
                dto.StreetName.Should().Be(entity.StreetName);
                dto.ErfNumber.Should().Be(entity.ErfNumber);
                dto.Suburb.Should().Be(entity.Suburb);
                dto.City.Should().Be(entity.City);
                dto.Country.Should().Be(entity.Country);
            }
        }

        public static void AssertEquivalent(AddressDto actualDto, Address expectedEntity)
        {
            if (expectedEntity == null)
            {
                actualDto.Should().BeNull();
                return;
            }

            actualDto.Should().NotBeNull();
            actualDto.Id.Should().Be(expectedEntity.Id);
            actualDto.StreetName.Should().Be(expectedEntity.StreetName);
            actualDto.ErfNumber.Should().Be(expectedEntity.ErfNumber);
            actualDto.Suburb.Should().Be(expectedEntity.Suburb);
            actualDto.City.Should().Be(expectedEntity.City);
            actualDto.Country.Should().Be(expectedEntity.Country);
        }

        public static void AssertEquivalent(UpdateAddressCommand expectedDto, Address actualEntity)
        {
            if (expectedDto == null)
            {
                actualEntity.Should().BeNull();
                return;
            }

            actualEntity.Should().NotBeNull();
            actualEntity.StreetName.Should().Be(expectedDto.StreetName);
            actualEntity.ErfNumber.Should().Be(expectedDto.ErfNumber);
            actualEntity.Suburb.Should().Be(expectedDto.Suburb);
            actualEntity.City.Should().Be(expectedDto.City);
            actualEntity.Country.Should().Be(expectedDto.Country);
        }
    }
}