using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Application.FileUpload;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Base;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Product.Create
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, string>
    {
        private readonly IProductRepository _repository;
        private readonly ICurrentUserService _current;
        private readonly FileUploadService _fileUploadService;

        public CreateProductCommandHandler(IProductRepository repository, ICurrentUserService current, FileUploadService fileUploadService)
        {
            _repository = repository;
            _current = current;
            _fileUploadService = fileUploadService;
        }

        public async Task<string> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // Check for a product with the same name that is marked as deleted
            var existingProduct = await _repository.FindAsync(
                x => x.Name == request.ProductName && x.DeletedDay != null,
                cancellationToken);

            if (existingProduct != null)
            {
               
                existingProduct.DeletedDay = null; // Restore the product
                existingProduct.CreatedDate = DateTime.UtcNow; 

                // Update other fields if necessary
                existingProduct.Description = request.ProductDescription;
                existingProduct.Price = request.Price;
                existingProduct.Content = request.Content;

                // Save changes to the existing product
                _repository.Update(existingProduct);
            }
            else
            {
                // If not found, check for a product that is not deleted
                var productExist = await _repository.FindAsync(x => x.Name == request.ProductName, cancellationToken);
                if (productExist != null)
                {
                    throw new DuplicationException("A product with the same name already exists");
                }

                string imageUrl = string.Empty;
                if (request.Url != null)
                {
                    using (var stream = request.Url.OpenReadStream())
                    {
                        imageUrl = await _fileUploadService.UploadFileAsync(stream, $"{Guid.NewGuid()}.jpg");
                    }
                }

                string productCode = ProductCodeGenerator.GenerateProductCode();
                var newProduct = new ProductEntity
                {
                    Name = request.ProductName,
                    Code = productCode,
                    Description = request.ProductDescription,
                    Price = request.Price,
                    Image = imageUrl,
                    Content = request.Content,
                    CreatedBy = _current.UserId,
                    CreatedDate = DateTime.UtcNow,
                };

                _repository.Add(newProduct);
            }

            return await _repository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Create Success!" : "Create Fail!";
        }
    }
}
