using Exe.Starot.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Domain.Entities.Repositories
{
    public interface IOrderRepository : IEFRepository<OrderEntity, OrderEntity>
    {
    }
}
