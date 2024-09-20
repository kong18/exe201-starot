using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Domain.Entities.Base
{
    public class AchievementEntity : BangMaGocEntity
    {
        public string Description { get; set; }
        public string Type { get; set; }

        public virtual ICollection<UserAchievementEntity> UserAchievements { get; set; }
    }
}
