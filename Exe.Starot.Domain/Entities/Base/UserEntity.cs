using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Domain.Entities.Base
{
    public class UserEntity
    {
        [Key]
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string Role { get; set; }
      
    }
}
