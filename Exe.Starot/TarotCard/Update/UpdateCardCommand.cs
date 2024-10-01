using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.TarotCard.Update
{
    public class UpdateCardCommand : IRequest<string>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
         public string? Type {  get; set; }
        public string ? Content {  get; set; }
        public IFormFile? imagefile { get; set; }
    
            public UpdateCardCommand() { }  
 
    }
}
