using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Order.Create
{
    public class CreateOrderCommand : IRequest<OrderDTO>
    {
        public CreateOrderCommand(List<RequestItem> products)
        {
            Products = products;
           
        }

     
        public decimal Total { get; set; } = 0;
        public List<RequestItem> Products { get; set; }
    }

    public class RequestItem
    {
        public string ProductID { get; set; }
        public int Quantity { get; set; }
      
    }

}
