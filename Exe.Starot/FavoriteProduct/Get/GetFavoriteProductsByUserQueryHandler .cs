using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Exe.Starot.Application.FavoriteProduct.Get
{
    public class GetFavoriteProductsByUserQueryHandler : IRequestHandler<GetFavoriteProductsByUserQuery, IEnumerable<FavoriteProductDto>>
    {
        private readonly IFavoriteProductRepository _repository;

        public GetFavoriteProductsByUserQueryHandler(IFavoriteProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<FavoriteProductDto>> Handle(GetFavoriteProductsByUserQuery request, CancellationToken cancellationToken)
        {
            // Fetch favorite products for the provided UserId
            var favoriteProducts = await _repository.FindAllAsync(f => f.UserId == request.UserId, cancellationToken);

            // If no favorite products found, return empty list
            if (favoriteProducts == null || !favoriteProducts.Any())
            {
                return new List<FavoriteProductDto>();
            }

            // Convert to DTO
            var favoriteProductDtos = favoriteProducts.Select(f => new FavoriteProductDto
            {   
                Id = f.ID,
                ProductId = f.ProductId,
                UserId = f.UserId,          
            }).ToList();

            return favoriteProductDtos;
        }
    }
}
