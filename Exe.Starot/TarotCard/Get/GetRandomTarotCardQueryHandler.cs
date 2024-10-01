using AutoMapper;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.TarotCard.Get
{
    public class GetRandomTarotCardQueryHandler : IRequestHandler<GetRandomTarotCardQuery, TarotCardDto>
    {
        private readonly IMapper _mapper;
        private readonly ITarotCardRepository _repository;

        public GetRandomTarotCardQueryHandler(IMapper mapper, ITarotCardRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<TarotCardDto> Handle(GetRandomTarotCardQuery request, CancellationToken cancellationToken)
        {
            // Retrieve all Tarot cards from the repository
            var tarotCards = await _repository.FindAllAsync(cancellationToken);

            // Get a random Tarot card
            var randomCard = tarotCards.OrderBy(c => Guid.NewGuid()).FirstOrDefault();

            // Return null if no card is found
            if (randomCard == null || randomCard.DeletedDay.HasValue)
            {
                throw new NotFoundException("Card is not found or deleted");
            }

            
            return randomCard.MapToTarotCardDto(_mapper);
        }
    }
}
