using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Application.FileUpload;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Repositories;
using Exe.Starot.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Product.Update
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, string>
    {
        private readonly IProductRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        private readonly FileUploadService _fileUploadService;
        public UpdateProductCommandHandler(IProductRepository repository, ICurrentUserService currentUserService, FileUploadService fileUploadService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
            _fileUploadService = fileUploadService;
        }

        public async Task<string> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productExist = await _repository.FindAsync(x => x.ID == request.Id, cancellationToken);
            if (productExist == null || productExist.DeletedDay.HasValue)
            {
                throw new NotFoundException("Product is not found or has been deleted");

            }
            string imageUrl = string.Empty;
            if (request.ImageFile != null)
            {
                using (var stream = request.ImageFile.OpenReadStream())
                {
                    imageUrl = await _fileUploadService.UploadFileAsync(stream, $"{Guid.NewGuid()}.jpg");
                }
            }

            productExist.Name = request.Name ?? productExist.Name;
            productExist.Code = request.Code ?? productExist.Code;
            productExist.Description = request.Description ?? productExist.Description;
            productExist.Content = request.Content ?? productExist.Content;
            productExist.Price = request.Price ?? productExist.Price;
            productExist.UpdatedBy = _currentUserService.UserId;
            productExist.LastUpdated = DateTime.Now;
            _repository.Update(productExist);
            if (await _repository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0)
            {
                return "Update Success!";
            }
            else
            {
                return "Update Fail!";
            }
        }
    }
}
