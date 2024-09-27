using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.User.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUserService;

        public ChangePasswordCommandHandler(IUserRepository userRepository, ICurrentUserService currentUserService)
        {
            _userRepository = userRepository;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var userEmail = _currentUserService.UserEmail;
            var user = await _userRepository.FindAsync(x => x.Email == userEmail);

            if (user == null)
            {
                throw new NotFoundException($"User not found");
            }

            if (!_userRepository.VerifyPassword(request.OldPassword, user.PasswordHash))
            {
                throw new NotFoundException("Old password is incorrect");
            }

            user.PasswordHash = _userRepository.HashPassword(request.NewPassword);
            _userRepository.Update(user);

            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return "Password changed successfully";
        }
    }
}
