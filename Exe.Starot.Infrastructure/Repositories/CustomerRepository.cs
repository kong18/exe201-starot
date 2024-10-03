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
    public class CustomerRepository : RepositoryBase<CustomerEntity, CustomerEntity, ApplicationDbContext>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
