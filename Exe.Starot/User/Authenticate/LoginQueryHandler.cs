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

            var accessToken = _jwtService.CreateToken(user.Id, user.Email);
            var refreshToken = _jwtService.GenerateRefreshToken();

            await _userRepository.UpdateRefreshTokenAsync(user, refreshToken, DateTime.UtcNow.AddDays(30));

            var userLoginDto = _mapper.Map<UserLoginDTO>(user);
            userLoginDto.Token = accessToken;
            userLoginDto.RefreshToken = refreshToken;

            return userLoginDto;
        }
    }
}
