using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Application.FileUpload;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.TarotCard.Update
{
    public class UpdateCardCommandHandler : IRequestHandler<UpdateCardCommand, string>
    {
        private readonly ITarotCardRepository _repository;  
        private readonly ICurrentUserService _currentUserService;
        private readonly FileUploadService _fileUploadService;

        public UpdateCardCommandHandler(ITarotCardRepository repository, ICurrentUserService currentUserService, FileUploadService fileUploadService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
            _fileUploadService = fileUploadService;
        }
    
        public async Task<string> Handle(UpdateCardCommand request, CancellationToken cancellationToken)
        {
            var existTarotCard = await _repository.FindAsync(x => x.ID == request.Id,cancellationToken);
            if(existTarotCard is null || existTarotCard.DeletedDay.HasValue)
            {
                throw new NotFoundException("Card is not found or deleted");
            }
            var duplicateCard = await _repository.FindAsync(x => x.Name == request.Name && x.ID != request.Id, cancellationToken);
            if (duplicateCard != null)
            {
                throw new DuplicationException("A Tarot card with this name already exists.");
            }
            string imageUrl = string.Empty;
            if (request.imagefile != null)
            {
                using (var stream = request.imagefile.OpenReadStream())
                {
                    imageUrl = await _fileUploadService.UploadFileAsync(stream, $"{Guid.NewGuid()}.jpg");
                }
            }
            existTarotCard.Name = request.Name;
            existTarotCard.Content = request.Content;
            existTarotCard.Type = request.Type;
            existTarotCard.Image = !string.IsNullOrEmpty(imageUrl) ? imageUrl : existTarotCard.Image;
            existTarotCard.UpdatedBy = _currentUserService.UserId;
            existTarotCard.UpdatedDay = DateTime.Now;
           
            _repository.Update(existTarotCard);
            if(await _repository.UnitOfWork.SaveChangesAsync() > 0)
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
