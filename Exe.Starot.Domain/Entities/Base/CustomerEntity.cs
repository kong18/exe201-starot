using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Domain.Entities.Base
{
    public class CustomerEntity : Entity
    {
        public string UserId { get; set; }
        public int Membership { get; set; }

        public virtual UserEntity User { get; set; }
        public virtual ICollection<FeedbackEntity> Feedbacks { get; set; }
        public virtual ICollection<BookingEntity> Bookings { get; set; }
    }
}
