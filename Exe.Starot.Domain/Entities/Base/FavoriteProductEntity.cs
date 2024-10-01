using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Domain.Entities.Base
{
    public class FavoriteProductEntity : Entity
    {
        public string ProductId { get; set; }
        public string UserId { get; set; }
        public bool IsFavorite { get; set; } = true;

        public virtual ProductEntity Product { get; set; }
        public virtual UserEntity User { get; set; }
    }
}
