using AutoMapper;
using Exe.Starot.Application.Common.Mappings;
using Exe.Starot.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Product
{
    public class ProductDto : IMapFrom<ProductEntity>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public void Mapping(Profile profile)
        {

            profile.CreateMap<ProductEntity, ProductDto>();
        }
    }
}
