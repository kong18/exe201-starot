using AutoMapper;
using Exe.Starot.Application.Common.Pagination;
using Exe.Starot.Application.TarotCard;
using Exe.Starot.Application.TarotCard.Filter;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Product.Filter
{
    public class FilterProductQueryHandler : IRequestHandler<FilterProductQuery, PagedResult<ProductDto>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public FilterProductQueryHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PagedResult<ProductDto>> Handle(FilterProductQuery request, CancellationToken cancellationToken)
        {
            //find all products exlcute deleted product
            var products = await _repository.FindAllAsync(t => t.DeletedDay == null, cancellationToken);

            // Apply filters
            if (!string.IsNullOrEmpty(request.Name))
            {
                products = products.Where(s => s.Name.ToLower().Contains(request.Name.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(request.Code))
            {
                products = products.Where(s => s.Code.ToLower().Contains(request.Code.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(request.Description))
            {
                products = products.Where(s => s.Description.ToLower().Contains(request.Description.ToLower())).ToList();
            }

            // If no products are found, return an empty list
            if (!products.Any())
            {
                return new PagedResult<ProductDto>
                {
                    Data = new List<ProductDto>(),
                    TotalCount = 0,
                    PageCount = 0,
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize
                };
            }

            // Handle pagination
            var query = products.AsQueryable();
            var totalCount = query.Count();
            var items = query.Skip((request.PageNumber - 1) * request.PageSize)
                             .Take(request.PageSize)
                             .ToList();

            var pageCount = (int)Math.Ceiling((double)totalCount / request.PageSize);
            var dtos = _mapper.Map<List<ProductDto>>(items);

            // Return paged result
            return new PagedResult<ProductDto>
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
