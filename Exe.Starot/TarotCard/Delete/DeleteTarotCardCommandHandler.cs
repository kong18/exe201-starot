using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.TarotCard.Delete
{
    public class DeleteTarotCardCommandhandler : IRequestHandler<DeleteTarotCardCommand, string>
    {
        private readonly ITarotCardRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        public DeleteTarotCardCommandhandler(ITarotCardRepository repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(DeleteTarotCardCommand request, CancellationToken cancellationToken)
        {
            var tarotCard = await _repository.FindAsync(x => x.ID == request.Id, cancellationToken);
            if(tarotCard == null || tarotCard.DeletedDay.HasValue)
            {
                throw new NotFoundException("Tarot card not exist or has been deleted");
            }
            tarotCard.DeletedDay = DateTime.UtcNow;
            tarotCard.DeletedBy = _currentUserService.UserId;
            _repository.Update(tarotCard);
            if (await _repository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0)
            {
                return "TarotCard deleted successfully.";
            }
            else
            {
                return "TarotCard deletion failed.";
            }
        }
    }
}
