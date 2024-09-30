using AutoMapper;
using Exe.Starot.Domain.Entities.Base;
using Exe.Starot.Domain.Entities.Repositories;
using Exe.Starot.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Infrastructure.Repositories
{
    public class TarotCardRepository : RepositoryBase<TarotCardEntity, TarotCardEntity, ApplicationDbContext>, ITarotCardRepository
    {
        public TarotCardRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }
    }
}
