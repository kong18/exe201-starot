using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Domain.Entities.Base
{
    public class OrderEntity : Entity
    {
        public string Code { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual UserEntity User { get; set; }
        public virtual ICollection<OrderDetailEntity> OrderDetails { get; set; }
    }
}
