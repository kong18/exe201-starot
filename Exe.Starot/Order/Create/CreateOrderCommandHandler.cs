using AutoMapper;
using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Base;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Order.Create
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDTO>
    {
        private readonly OrderService _orderService;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;

        public CreateOrderCommandHandler(OrderService orderService,IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IProductRepository productRepository, IMapper mapper, ICurrentUserService currentUserService, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
            _orderService = orderService;   
        }

        public async Task<OrderDTO> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var user = _currentUserService.UserId;
            var userExist = await _userRepository.FindAsync(x => x.ID == user && x.DeletedDay == null, cancellationToken);
            if (userExist is null)
            {
                throw new NotFoundException("User does not exist");
            }
            foreach (var item in request.Products)
            {
                bool productExist = await _productRepository.AnyAsync(x => x.ID == item.ProductID && !x.DeletedDay.HasValue, cancellationToken);

                if (!productExist)
                {
                    throw new NotFoundException($"Product with ID {item.ProductID} is not found or deleted");
                }
            }

            decimal orderTotal = 0;

            var order = new OrderEntity
            {
                UserId = userExist.ID,
                Status = "Pending",
                Code = GenerateOrderCode(),
                CreatedBy = _currentUserService.UserId,
                OrderDate = DateTime.UtcNow.AddHours(7),
                CreatedDate = DateTime.UtcNow.AddHours(7),
                Total = 0 
            };

            _orderRepository.Add(order);
            await _orderRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            var orderID = order.ID;

            foreach (var item in request.Products)
            {
                var product = await _productRepository.FindAsync(x => x.ID == item.ProductID && !x.DeletedDay.HasValue, cancellationToken);

                if (product == null)
                {
                    throw new NotFoundException($"Product with ID {item.ProductID} is not found or deleted");
                }
                decimal productPrice = item.Quantity * product.Price;
                var orderDetail = new OrderDetailEntity
                {
                    OrderId = order.ID,
                    ProductId = item.ProductID,
                    Amount = item.Quantity,
                    UnitPrice = product.Price,
                    Price = productPrice,
                    CreatedDate = DateTime.UtcNow.AddHours(7),
                    Status = true
                };
                _orderDetailRepository.Add(orderDetail);
                orderTotal += productPrice; // Update total for the order
                await _orderDetailRepository.UnitOfWork.SaveChangesAsync(cancellationToken);


            }
            order.Total = orderTotal;
            await _orderRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            await _orderService.NotifyNewOrder(orderID.ToString());
            return order.MapToOrderDTO(_mapper);
        }

        private string GenerateOrderCode()
        {
            // Example format: ORD-20240925-XXXX
            // ORD: Fixed prefix indicating the order.
            // 20240925: Current date in YYYYMMDD format.
            // XXXX: Random alphanumeric string to ensure uniqueness.

            var datePart = DateTime.UtcNow.ToString("yyyyMMdd");
            var randomPart = Guid.NewGuid().ToString().Substring(0, 4).ToUpper(); // You can customize the length if needed.

            return $"ORD-{datePart}-{randomPart}";
        }
    }
}
