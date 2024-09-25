using AutoMapper;
using Exe.Starot.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Order
{
    public static class OrderDTOMappingExtension
    {
        public static OrderDTO MapToOrderDTO(this OrderEntity orderEntity, IMapper mapper)
        {
            return mapper.Map<OrderDTO>(orderEntity);
        }

        public static List<OrderDTO> MapToOrderDTOList(this IEnumerable<OrderEntity> orders, IMapper mapper)
        {
            return orders.Select(order => order.MapToOrderDTO(mapper)).ToList();
        }
    }
}
