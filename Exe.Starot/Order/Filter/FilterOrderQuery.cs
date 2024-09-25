using Exe.Starot.Application.Common.Pagination;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Order.Filter
{
    public class FilterOrderQuery : IRequest<PagedResult<OrderDTO>>
    {
        public string? UserID { get; set; }
        public bool? SortOrder { get; set; }
        public string? Status { get; set; }
  
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
