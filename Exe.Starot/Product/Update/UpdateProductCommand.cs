using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Product.Update
{
    public class UpdateProductCommand : IRequest<string>
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
        public string? Content { get; set; }
        public decimal? Price { get; set; }
        public IFormFile? ImageFile { get; set; }
        public UpdateProductCommand() { }
        public UpdateProductCommand(string id, string name, string description, string code, string content, decimal price, IFormFile imageFile)
        {

            Id = id; Name = name; Description = description; Code = code; Content = content; Price = price; ImageFile = imageFile;
        }
    }
}
