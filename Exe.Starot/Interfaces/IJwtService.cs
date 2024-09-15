using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Interfaces
{
    public interface IJwtService
    {
        string CreateToken(string ID, string roles, string email);
        string CreateToken(string email, string roles);
        string CreateToken(string subject, string role, int expiryDays);
    }
}
