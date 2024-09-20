using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Domain.Entities.Base
{
    public class ReaderEntity : Entity
    {
        public string UserId { get; set; }
        public int ExperienceYears { get; set; }
        public string Quote { get; set; }
        [Column(TypeName = "decimal(4,1)")]
        public decimal Rating { get; set; }
        public string Image { get; set; }

        public virtual UserEntity User { get; set; }
        public virtual ICollection<FeedbackEntity> Feedbacks { get; set; }
        public virtual ICollection<BookingEntity> Bookings { get; set; }
    }
}
