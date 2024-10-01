using AutoMapper;
using Exe.Starot.Application.Common.Pagination;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Feedback.Filter
{
    public class FilterFeedbackQueryHandler : IRequestHandler<FilterFeedbackQuery, PagedResult<FeedbackDto>>
    {
        private readonly IFeedBackRepository _feedbackRepository;
        private readonly IMapper _mapper;

        public FilterFeedbackQueryHandler(IFeedBackRepository feedbackRepository, IMapper mapper)
        {
            _feedbackRepository = feedbackRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<FeedbackDto>> Handle(FilterFeedbackQuery request, CancellationToken cancellationToken)
        {
            var feedbacks = await _feedbackRepository.FindAllAsync(f =>
                (string.IsNullOrEmpty(request.ReaderId) || f.ReaderId == request.ReaderId) &&
                (!request.FromDate.HasValue || f.Date >= request.FromDate) &&
                (!request.ToDate.HasValue || f.Date <= request.ToDate),
                cancellationToken);

            var query = feedbacks.AsQueryable();

            var totalCount = query.Count();
            var items = query.Skip((request.PageNumber - 1) * request.PageSize)
                             .Take(request.PageSize)
                             .ToList();

            var feedbackDtos = _mapper.Map<List<FeedbackDto>>(items);

            return new PagedResult<FeedbackDto>
            {
                Data = feedbackDtos,
                TotalCount = totalCount,
                PageCount = (int)Math.Ceiling((double)totalCount / request.PageSize),
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}