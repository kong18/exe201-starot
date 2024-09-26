using AutoMapper;
using Exe.Starot.Application.Common.Mappings;
using Exe.Starot.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.User
{
    public class UserDTO : IMapFrom<UserEntity>
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserEntity, UserDTO>();
                    
        }
    }
}
