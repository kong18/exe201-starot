using Exe.Starot.Domain.Entities.Base;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.User.Reader.CreateReader
{
    public class CreateReaderCommandHandler : IRequestHandler<CreateReaderCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IReaderRepository _readerRepository;
        public CreateReaderCommandHandler(IUserRepository userRepository, IReaderRepository readerRepository)
        {
            _userRepository = userRepository;
            _readerRepository = readerRepository;
        }
    
        public async Task<string> Handle(CreateReaderCommand request, CancellationToken cancellationToken)
        {
            var user = new UserEntity
            {
                Email = request.Email,
                PasswordHash = request.Password,
                Role = request.Role,
                Name = request.Name,    
                Phone = request.Phone,  
            };
            var reader = new ReaderEntity
            {
                User = user
            };
            _readerRepository.Add(reader);
            _userRepository.Add(user);
            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return user.Email;

        }
    }
}
