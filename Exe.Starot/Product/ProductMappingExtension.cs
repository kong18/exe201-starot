using AutoMapper;
using Exe.Starot.Application.TarotCard;
using Exe.Starot.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Product
{
    public static  class ProductMappingExtension
    {
        public static ProductDto MapToProductDto(this ProductEntity projectFrom, IMapper mapper)
       => mapper.Map<ProductDto>(projectFrom);

        public static List<ProductDto> MapToProductListDto(this IEnumerable<ProductEntity> projectFrom, IMapper mapper)
          => projectFrom.Select(x => x.MapToProductDto(mapper)).ToList();
    }
}
