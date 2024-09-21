using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace Exe.Starot.Domain.Entities.Base
{
    public class UserEntity : IdentityUser
    {
        public string Name { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public DateTime RefreshTokenIssuedAt { get; set; }
        public string Email { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Wallet { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }

        public virtual ICollection<OrderEntity> Orders { get; set; }
        public virtual ICollection<FavoriteProductEntity> FavoriteProducts { get; set; }
        public virtual ICollection<CustomerEntity> Customers { get; set; }
        public virtual ICollection<ReaderEntity> Readers { get; set; }
        public virtual ICollection<UserAchievementEntity> UserAchievements { get; set; }
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
