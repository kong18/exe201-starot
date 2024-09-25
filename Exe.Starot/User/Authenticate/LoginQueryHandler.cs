using AutoMapper;
using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.User.Authenticate
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, UserLoginDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public LoginQueryHandler(IUserRepository userRepository, IJwtService jwtService, IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        public async Task<UserLoginDTO> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(u => u.Email == request.Email);

            if (user == null || !_userRepository.VerifyPassword(request.Password, user.PasswordHash))
            {
                throw new NotFoundException("Invalid email or password.");
            }
            Console.WriteLine($"User ID: {user.ID}"); // Log user.ID for debugging

            var accessToken = _jwtService.CreateToken(user.ID, user.Email, user.Role);
            var refreshToken = _jwtService.GenerateRefreshToken();

            await _userRepository.UpdateRefreshTokenAsync(user, refreshToken, DateTime.UtcNow.AddDays(30));

            var userLoginDto = _mapper.Map<UserLoginDTO>(user);
            userLoginDto.EntityId = user.ID;
            userLoginDto.Token = accessToken;
            userLoginDto.RefreshToken = refreshToken;
            //if (user.Customers != null)
            //{
            //    userLoginDto.EntityId = user.ID;
            //    userLoginDto.Role = "Customer";
            //}
            //if (user.Readers != null)
            //{
            //    userLoginDto.EntityId = user.ID;
            //    userLoginDto.Role = "Reader";
            //}
            //else
            //{
            //    throw new NotFoundException($"No associated entity found for user - {request.Email}");
            //}


            return userLoginDto;
        }
    }
}
