using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Domain.Entities.Base
{
    public class TarotCardEntity : BangMaGocEntity
    {
        public string Content { get; set; }
        public string Image { get; set; }
        public string Type { get; set; }
    }
}
