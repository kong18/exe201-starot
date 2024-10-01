using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.FavoriteProduct.Create
{
    public class AddFavoriteProductCommand : IRequest<string>
    {
        public string ProductId { get; set; }
        public string UserId { get; set; }
        public bool IsFavorite { get; set; }  // true to add to favorite, false to remove
    }
}
