using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Repositories;
using Exe.Starot.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Product.Delete
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, string>
    {
        private readonly IProductRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        public DeleteProductCommandHandler(IProductRepository repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productExist = await _repository.FindAsync(x => x.ID == request.Id, cancellationToken);
            if (productExist == null || productExist.DeletedDay.HasValue)
            {
                throw new NotFoundException("Product is not existed or has been deleted");
            }
            productExist.DeletedDay = DateTime.UtcNow;
            productExist.DeletedBy = _currentUserService.UserId;
            _repository.Update(productExist);
            if (await _repository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0)
            {
                return "Product deleted successfully.";
            }
            else
            {
                return "Product deletion failed.";
            }

        }
    }
}
