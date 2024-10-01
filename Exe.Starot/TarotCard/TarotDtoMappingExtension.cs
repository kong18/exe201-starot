using AutoMapper;
using Exe.Starot.Application.Product;
using Exe.Starot.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.TarotCard
{
    public static  class TarotDtoMappingExtension
    {
        public static TarotCardDto MapToTarotCardDto(this TarotCardEntity projectFrom, IMapper mapper)
        => mapper.Map<TarotCardDto>(projectFrom);

        public static List<TarotCardDto> MapToListTarotCardDto(this IEnumerable<TarotCardEntity> projectFrom, IMapper mapper)
          => projectFrom.Select(x => x.MapToTarotCardDto(mapper)).ToList();
    }
}
