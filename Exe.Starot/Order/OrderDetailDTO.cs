using AutoMapper;
using Exe.Starot.Application.Common.Mappings;
using Exe.Starot.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Order
{
    public class OrderDetailDTO : IMapFrom<OrderDetailEntity>
    {
        public string ID {  get; set; }
        public string ProductID {  get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Price { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrderDetailEntity, OrderDetailDTO>();
        }
    }
}
