using Exe.Starot.Application.Common.Pagination;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.FavoriteProduct.Get
{
    public class GetFavoriteProductsByUserQuery : IRequest<IEnumerable<FavoriteProductDto>>
    {
        public string UserId { get; set; }
    }
}
