using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace Exe.Starot.Domain.Entities.Base
{
    public class UserEntity : IdentityUser
    {
        [Key]
        public  string? FirstName { get; set; }
        public  string? LastName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public DateTime RefreshTokenIssuedAt { get; set; }
        public void SetRefreshToken(string token, DateTime expiry)
        {
            RefreshToken = token;
            RefreshTokenExpiryTime = expiry;
            RefreshTokenIssuedAt = DateTime.UtcNow;
        }

        public bool IsRefreshTokenValid(string token)
        {
            return RefreshToken == token && RefreshTokenExpiryTime > DateTime.UtcNow;
        }
    }
}
