using Exe.Starot.Domain.Entities.Base;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.User.Register
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IReaderRepository _readerRepository; // Thêm ReaderRepository

        public RegisterUserCommandHandler(IUserRepository userRepository, ICustomerRepository customerRepository, IReaderRepository readerRepository)
        {
            _userRepository = userRepository;
            _customerRepository = customerRepository;
            _readerRepository = readerRepository;
        }

        public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.FindAsync(x => x.Email == request.Email, cancellationToken);
            if (existingUser != null)
            {
                throw new Exception("This Email has already been used");
            }
            if (request.Password != request.ConfirmPassword)
            {
                throw new Exception("Password and ConfirmPassword wrong");

            }

            var hashedPassword = _userRepository.HashPassword(request.Password);

            var user = new UserEntity
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                PasswordHash = hashedPassword,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                Wallet = 0, //default wallet
                Role = request.IsReader ? "Reader" : "Customer", // check role
                CreatedDate = DateTime.UtcNow
            };
            _userRepository.Add(user);

            if (request.IsReader)
            {
                // Create new Reader Entity if role is Reader
                var reader = new ReaderEntity
                {
                    User = user,
                    ExperienceYears = request.ExperienceYears.GetValueOrDefault(0),
                    Quote = request.Quote ?? string.Empty,
                    Rating = 0, // Default Rationg
                    Image = string.Empty // Update Later
                };

                // Thêm Reader vào repository
                _readerRepository.Add(reader);
            }
            else
            {
                //  Create new Customer Entity if role is Customer
                var customer = new CustomerEntity
                {
                    User = user,
                    Membership = 0 // Mặc định membership là 0
                };

                // Thêm Customer vào repository
                _customerRepository.Add(customer);
            }

            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            if (request.IsReader)
            {
                await _readerRepository.UnitOfWork.SaveChangesAsync(cancellationToken); // Lưu Reader
            }
            else
            {
                await _customerRepository.UnitOfWork.SaveChangesAsync(cancellationToken); // Lưu Customer
            }

            return "Register Sucessfully!";
        }
    }
}
