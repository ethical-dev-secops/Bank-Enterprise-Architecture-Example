using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BankAccountManagementSystem.Domain.Entities;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.AutoMapper.MappingExtensions", Version = "1.0")]

namespace BankAccountManagementSystem.Application.SavingsAccounts
{
    public static class SavingsAccountDtoMappingExtensions
    {
        public static SavingsAccountDto MapToSavingsAccountDto(this SavingsAccount projectFrom, IMapper mapper)
            => mapper.Map<SavingsAccountDto>(projectFrom);

        public static List<SavingsAccountDto> MapToSavingsAccountDtoList(this IEnumerable<SavingsAccount> projectFrom, IMapper mapper)
            => projectFrom.Select(x => x.MapToSavingsAccountDto(mapper)).ToList();
    }
}