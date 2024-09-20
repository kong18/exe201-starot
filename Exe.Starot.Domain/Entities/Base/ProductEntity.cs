using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Domain.Entities.Base
{
    public class ProductEntity : Entity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string Image { get; set; }

        public virtual ICollection<OrderDetailEntity> OrderDetails { get; set; }
        public virtual ICollection<FavoriteProductEntity> FavoriteProducts { get; set; }
    }
}
