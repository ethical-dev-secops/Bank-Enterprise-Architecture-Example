using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BankAccountManagementSystem.Domain.Entities;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.AutoMapper.MappingExtensions", Version = "1.0")]

namespace BankAccountManagementSystem.Application.AccountHolders
{
    public static class AccountHolderDtoMappingExtensions
    {
        public static AccountHolderDto MapToAccountHolderDto(this AccountHolder projectFrom, IMapper mapper)
            => mapper.Map<AccountHolderDto>(projectFrom);

        public static List<AccountHolderDto> MapToAccountHolderDtoList(this IEnumerable<AccountHolder> projectFrom, IMapper mapper)
            => projectFrom.Select(x => x.MapToAccountHolderDto(mapper)).ToList();
    }
}