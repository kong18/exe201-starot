using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Domain.Entities.Base
{
    public class FeedbackEntity : Entity
    {
        public string CustomerId { get; set; }
        public string ReaderId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }

        public virtual CustomerEntity Customer { get; set; }
        public virtual ReaderEntity Reader { get; set; }
    }
}
