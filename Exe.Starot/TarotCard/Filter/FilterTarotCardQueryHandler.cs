using AutoMapper;
using Exe.Starot.Application.Common.Pagination;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Exe.Starot.Application.TarotCard.Filter
{
    public class FilterTarotCardQueryHandler : IRequestHandler<FilterTarotCardQuery, PagedResult<TarotCardDto>>
    {
        private readonly ITarotCardRepository _repository;
        private readonly IMapper _mapper;

        public FilterTarotCardQueryHandler(ITarotCardRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedResult<TarotCardDto>> Handle(FilterTarotCardQuery request, CancellationToken cancellationToken)
        {
            var tarotCard = await _repository.FindAllAsync(t => t.DeletedDay == null, cancellationToken);

            // Apply filters
            if (!string.IsNullOrEmpty(request.Name))
            {
                tarotCard = tarotCard.Where(s => s.Name.ToLower().Contains(request.Name.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(request.Type))
            {
                tarotCard = tarotCard.Where(s => s.Type == request.Type).ToList();
            }

            if (request.Id.HasValue)
            {
                tarotCard = tarotCard.Where(s => s.ID == request.Id.Value).ToList();
            }

            // If no Tarot cards are found, return an empty list
            if (!tarotCard.Any())
            {
                return new PagedResult<TarotCardDto>
                {
                    Data = new List<TarotCardDto>(),
                    TotalCount = 0,
                    PageCount = 0,
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize
                };
            }

            // Handle pagination
            var query = tarotCard.AsQueryable();
            var totalCount = query.Count();
            var items = query.Skip((request.PageNumber - 1) * request.PageSize)
                             .Take(request.PageSize)
                             .ToList();

            var pageCount = (int)Math.Ceiling((double)totalCount / request.PageSize);
            var dtos = _mapper.Map<List<TarotCardDto>>(items);

            // Return paged result
            return new PagedResult<TarotCardDto>
            {
                Data = dtos,
                TotalCount = totalCount,
                PageCount = pageCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
