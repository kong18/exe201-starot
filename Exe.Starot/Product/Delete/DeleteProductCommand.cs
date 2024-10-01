using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Product.Delete
{
    public class DeleteProductCommand : IRequest<string>
    {
        public string Id {  get; set; }

    }
}
