using Castle.Core.Resource;
using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Domain.Entities.Base;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Feedback.Create
{
    public class CreateFeedbackCommandHandler : IRequestHandler<CreateFeedbackCommand, string>
    {
        private readonly IFeedBackRepository _feedbackRepository;
        private readonly ICurrentUserService _currentUserService;

        public CreateFeedbackCommandHandler(IFeedBackRepository feedbackRepository, ICurrentUserService currentUserService)
        {
            _feedbackRepository = feedbackRepository;
            _currentUserService = currentUserService;
        }


        public async Task<string> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken)
        {
            string customerid = _currentUserService.UserId;
            Console.WriteLine($"Current user ID: {_currentUserService.UserId}");

            if (string.IsNullOrEmpty(customerid))
            {
                throw new UnauthorizedAccessException("User is not logged in.");
            }
            var feedback = new FeedbackEntity
            {
                CustomerId = customerid,
                ReaderId = request.ReaderId,
                Rating = request.Rating,
                Comment = request.Comment,
                Date = DateTime.UtcNow
            };

            _feedbackRepository.Add(feedback);

            if (await _feedbackRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0)
            {
                return "Feedback Created Successfully!";
            }
            else
            {
                return "Failed to create feedback.";
            }
        }
    }
}
