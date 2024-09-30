using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.TarotCard.Create
{
    public class CreateTarotCardCommand : IRequest<string>
    {
        public CreateTarotCardCommand() { }
        public CreateTarotCardCommand(string name,string content, string type, IFormFile imageFile) {
            Name = name;
            Content = content;
            Type = type;
            Image = imageFile;

        }
        public string Name { get; set; }         // Name of the Tarot card
        public string Content { get; set; }      // Description or content
        public string Type { get; set; }         // Type (e.g., Major or Minor Arcana)
        public IFormFile? Image { get; set; }    // Optional: Image file for the Tarot card
    }
}
