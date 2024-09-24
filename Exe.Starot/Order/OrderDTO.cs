using AutoMapper;
using Exe.Starot.Application.Common.Mappings;
using Exe.Starot.Domain.Entities.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Order
{
    public class OrderDTO : IMapFrom<OrderEntity>
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Code { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDay { get; set; }

        public List<ResponseItem> Products { get; set; } = new List<ResponseItem>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrderEntity, OrderDTO>()

                 .ForMember(dto => dto.Products, opt => opt.MapFrom(entity => entity.OrderDetails));


            profile.CreateMap<OrderDetailEntity, ResponseItem>()
                .ForMember(dto => dto.Name, opt => opt.MapFrom(entity => entity.Product.Name))
                .ForMember(dto => dto.UnitPrice, opt => opt.MapFrom(entity => entity.UnitPrice))
                .ForMember(dto => dto.Amount, opt => opt.MapFrom(entity => entity.Amount))
                .ForMember(dto => dto.Price, opt => opt.MapFrom(entity => entity.UnitPrice))
                .ForMember(dto => dto.ProductID, opt => opt.MapFrom(entity => entity.ProductId));
        

        }

    
        public class ResponseItem
        {
            public string ProductID { get; set; }

            public string Name { get; set; }

            public decimal UnitPrice { get; set; }

            public int Amount { get; set; }

            public decimal Price { get; set; }

        }
    }
}
