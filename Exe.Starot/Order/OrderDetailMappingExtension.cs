using AutoMapper;
using Exe.Starot.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Order
{
    public static class OrderDetailMappingExtension
    {
        public static OrderDetailDTO MapToOrderDetailDTO(this OrderDetailEntity orderDetailEntity, IMapper mapper)
        {
            return mapper.Map<OrderDetailDTO>(orderDetailEntity);
        }

        public static List<OrderDetailDTO> MapToOrderDetailDTOList(this IEnumerable<OrderDetailEntity> orders, IMapper mapper)
        {
            return orders.Select(order => order.MapToOrderDetailDTO(mapper)).ToList();
        }
    }
}
