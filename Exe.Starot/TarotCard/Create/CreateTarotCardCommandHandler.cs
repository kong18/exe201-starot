using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Application.FileUpload;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Base;
using Exe.Starot.Domain.Entities.Repositories;
using Exe.Starot.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.TarotCard.Create
{
    public class CreateTarotCardCommandHandler : IRequestHandler<CreateTarotCardCommand, string>
    {
        private readonly ITarotCardRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        private readonly FileUploadService _fileUploadService;

        public CreateTarotCardCommandHandler(ITarotCardRepository repository, ICurrentUserService currentUserService, FileUploadService fileUploadService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
            _fileUploadService = fileUploadService;
        }

        public async Task<string> Handle(CreateTarotCardCommand request, CancellationToken cancellationToken)
        {
            // 1. Check if a card with the same name already exists
            var duplicateCard = await _repository.FindAsync(x => x.Name == request.Name, cancellationToken);
            if (duplicateCard != null)
            {
                throw new DuplicationException("A Tarot card with this name already exists.");
            }

            // 2. Handle image upload, if an image is provided
            string imageUrl = string.Empty;
            if (request.Image != null)
            {
                using (var stream = request.Image.OpenReadStream())
                {
                    imageUrl = await _fileUploadService.UploadFileAsync(stream, $"{Guid.NewGuid()}.jpg");
                }
            }

            var newTarotCard = new TarotCardEntity
            {
                Name = request.Name,
                Content = request.Content,
                Type = request.Type,
                Image = imageUrl,
                CreatedBy = _currentUserService.UserId,
                CreatedDate = DateTime.Now
            };

            // 4. Save the new Tarot card to the repository
          _repository.Add (newTarotCard);
            return await _repository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Create Success!" : "Create Fail!";

         
        }
    }
}
