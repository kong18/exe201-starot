using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Order.UpdateOrder
{
    public class UpdateOrderCommand : IRequest<string>
      {

        public UpdateOrderCommand(string? status)
        {
            Status = status;
          
        }
        public string ID { get; set; }
        public string? Status { get; set; }
       
    }
}
