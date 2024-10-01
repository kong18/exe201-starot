using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.FavoriteProduct.Delete
{
    public class DeleteFavoriteProductCommandHandler : IRequestHandler<DeleteFavoriteProductCommand, string>
    {
        private readonly IFavoriteProductRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        public async Task<string> Handle(DeleteFavoriteProductCommand request, CancellationToken cancellationToken)
        {
            var favoriteProduct = await _repository.FindAsync(f => f.ProductId == request.ProductId && f.UserId == _currentUserService.UserId, cancellationToken);

            if (favoriteProduct == null)
            {
                throw new NotFoundException("Favorite product not found.");
            }

            _repository.Remove(favoriteProduct);

            if (await _repository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0)
            {
                return "Favorite product deleted successfully.";
            }

            return "Favorite product deletion failed.";
        }
    
    }
}
