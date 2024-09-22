using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.User.Authenticate
{
    public class LoginQuery : IRequest<UserLoginDTO>
    {
        public LoginQuery() { }

        public LoginQuery(string email, string password)
        {
            Email = email;
            Password = password;
        }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

