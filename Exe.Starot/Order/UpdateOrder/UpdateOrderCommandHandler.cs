using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Order.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, string>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICurrentUserService _currentUserService;
        public UpdateOrderCommandHandler(IOrderRepository orderRepository, ICurrentUserService currentUserService)
        {
            _orderRepository = orderRepository;
            
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderExist = await _orderRepository.FindAsync(x => x.ID == request.ID && !x.DeletedDay.HasValue, cancellationToken);

            if (orderExist is null)
            {
                return "Order does not exist";
            }


            orderExist.Status = request.Status ?? orderExist.Status;
           

            orderExist.UpdatedBy = _currentUserService.UserId;
            orderExist.LastUpdated = DateTime.UtcNow.AddHours(7);
            _orderRepository.Update(orderExist);

            return await _orderRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Update Success!" : "Update Fail!";
        }
    }
}
