using Exe.Starot.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Domain.Entities.Repositories
{
    public interface IUserRepository : IEFRepository<UserEntity, UserEntity>
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string passwordHash);
        string GeneratePassword();
        Task UpdateRefreshTokenAsync(UserEntity user, string newRefreshToken, DateTime expiryDate);

    }
}
