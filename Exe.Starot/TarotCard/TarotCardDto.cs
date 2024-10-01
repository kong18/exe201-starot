using AutoMapper;
using Exe.Starot.Application.Common.Mappings;
using Exe.Starot.Domain.Entities.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.TarotCard
{
    public class TarotCardDto : IMapFrom<TarotCardEntity>
    {
        public int ID { get; set; }            // The ID of the card
        public string Name { get; set; }        // The name of the card
        public string Content { get; set; }     // Content or description of the card
        public string Url { get; set; }       // Image URL or path for the card
        public string Type { get; set; }        // Type (e.g., Major or Minor Arcana)

        public void Mapping(Profile profile)
        {
           profile.CreateMap<TarotCardEntity,TarotCardDto>();
        }
    }
}
