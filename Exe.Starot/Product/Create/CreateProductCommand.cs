using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Product.Create
{
    public class CreateProductCommand : IRequest<string>
    {

        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
       
        public string Content { get; set; }

        public IFormFile Url { get; set; }

    }
}
