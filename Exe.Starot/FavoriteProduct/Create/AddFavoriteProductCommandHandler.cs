using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Base;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.FavoriteProduct.Create
{
    public class AddFavoriteProductCommandHandler : IRequestHandler<AddFavoriteProductCommand, string>
    {
        private readonly IFavoriteProductRepository _favoriteProductRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        public AddFavoriteProductCommandHandler(IFavoriteProductRepository favoriteProductRepository, IProductRepository productRepository, IUserRepository userRepository)
        {
            _favoriteProductRepository = favoriteProductRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(AddFavoriteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindAsync(p => p.ID == request.ProductId, cancellationToken);
            if (product == null)
            {
                throw new NotFoundException("Product not found.");
            }
            var user = await _userRepository.FindAsync(u => u.ID == request.UserId, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            //find favorite product
            var favoriteProduct = await _favoriteProductRepository.FindAsync(fp =>
             fp.ProductId == request.ProductId && fp.UserId == request.UserId, cancellationToken);

            if (request.IsFavorite)
            {
                // Add to favorites
                if (favoriteProduct != null)
                {
                    return "Product is already in the user's favorites.";
                }

                var newFavorite = new FavoriteProductEntity
                {
                    ProductId = request.ProductId,
                    UserId = request.UserId
                };

                _favoriteProductRepository.Add(newFavorite);
            }
            else
            {
                // Remove from favorites
                if (favoriteProduct == null)
                {
                    return "Product is not in the user's favorites.";
                }
                _favoriteProductRepository.Remove(favoriteProduct);

            }
            await _favoriteProductRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return request.IsFavorite ? "Product added to favorites." : "Product removed from favorites.";
        }
    }
}
