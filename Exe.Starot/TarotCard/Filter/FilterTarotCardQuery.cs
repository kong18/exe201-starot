using Exe.Starot.Application.Common.Pagination;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.TarotCard.Filter
{
    public class FilterTarotCardQuery : IRequest<PagedResult<TarotCardDto>>
    {
        public int ? Id {  get; set; }  
        public string? Name { get; set; }
        public string? Type {  get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
