using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.FavoriteProduct.Delete
{
    public class DeleteFavoriteProductCommand : IRequest<string>
    {
        public string ProductId { get; set; }
        public string UserId { get; set; }
    }
}
