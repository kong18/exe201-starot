using Exe.Starot.Application.Common.Pagination;
using Exe.Starot.Application.TarotCard;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Product.Filter
{
    public class FilterProductQuery : IRequest<PagedResult<ProductDto>>
    {
        public string ? Name {  get; set; }
        public string ? Description {  get; set; }
        public string ? Code { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}
