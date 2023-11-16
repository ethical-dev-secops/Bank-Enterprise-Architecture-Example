using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BankAccountManagementSystem.Domain.Entities;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.AutoMapper.MappingExtensions", Version = "1.0")]

namespace BankAccountManagementSystem.Application.ChequeAccounts
{
    public static class ChequeAccountDtoMappingExtensions
    {
        public static ChequeAccountDto MapToChequeAccountDto(this ChequeAccount projectFrom, IMapper mapper)
            => mapper.Map<ChequeAccountDto>(projectFrom);

        public static List<ChequeAccountDto> MapToChequeAccountDtoList(this IEnumerable<ChequeAccount> projectFrom, IMapper mapper)
            => projectFrom.Select(x => x.MapToChequeAccountDto(mapper)).ToList();
    }
}