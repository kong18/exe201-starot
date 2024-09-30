using AutoMapper;
using Exe.Starot.Application.Product;
using Exe.Starot.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Feedback
{
    public static class FeedbackMappingExtension
    {
        public static FeedbackDto MapToFeedbackDto(this FeedbackEntity projectFrom, IMapper mapper)
         => mapper.Map<FeedbackDto>(projectFrom);

        public static List<FeedbackDto> MapToFeedbackDtoList(this IEnumerable<FeedbackEntity> projectFrom, IMapper mapper)
          => projectFrom.Select(x => x.MapToFeedbackDto(mapper)).ToList();
    }
}
