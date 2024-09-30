using AutoMapper;
using Exe.Starot.Application.Common.Mappings;
using Exe.Starot.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Feedback
{
    public class FeedbackDto : IMapFrom<FeedbackEntity>
    {

        public string CustomerId { get; set; }
        public string ReaderId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<FeedbackEntity, FeedbackDto>()
               .ForMember(dto => dto.CustomerId, opt => opt.MapFrom(entity => entity.CustomerId))
               .ForMember(dto => dto.ReaderId, opt => opt.MapFrom(entity => entity.ReaderId))
               .ForMember(dto => dto.Rating, opt => opt.MapFrom(entity => entity.Rating))
               .ForMember(dto => dto.Comment, opt => opt.MapFrom(entity => entity.Comment))
               .ForMember(dto => dto.Date, opt => opt.MapFrom(entity => entity.Date));
        }
    }
}
