using AutoMapper;
using Exe.Starot.Application.Product;
using Exe.Starot.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.FavoriteProduct
{
    public static class FavoriteProductMappingExtension
    {
        public static FavoriteProductDto MapToFavoriteProductDto(this FavoriteProductEntity projectFrom, IMapper mapper)
        => mapper.Map<FavoriteProductDto>(projectFrom);

        public static List<FavoriteProductDto> MapToFavoriteProductDtoList(this IEnumerable<FavoriteProductEntity> projectFrom, IMapper mapper)
          => projectFrom.Select(x => x.MapToFavoriteProductDto(mapper)).ToList();
    }
}
