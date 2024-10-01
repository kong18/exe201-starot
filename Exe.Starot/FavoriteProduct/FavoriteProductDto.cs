using AutoMapper;
using Exe.Starot.Application.Common.Mappings;
using Exe.Starot.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.FavoriteProduct
{
    public class FavoriteProductDto : IMapFrom<FavoriteProductEntity>
    {
        public string Id {  get; set; }
        public string ProductId {get; set; }
        public string UserId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FavoriteProductEntity, FavoriteProductDto>(); 
        }
    }
}
