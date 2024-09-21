using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Domain.Entities.Base
{
    public class BookingEntity : Entity
    {
        public int PackageId { get; set; }
        public string CustomerId { get; set; }
        public string ReaderId { get; set; }
        public DateTime StartDate { get; set; }
        public string Status { get; set; }
        public string LinkUrl { get; set; }

        public virtual CustomerEntity Customer { get; set; }
        public virtual ReaderEntity Reader { get; set; }
        public virtual PackageQuestionEntity Package { get; set; }
    }
}
