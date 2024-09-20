using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Domain.Entities.Base
{
    public class UserAchievementEntity : Entity
    {
        public string UserId { get; set; }
        public int AchievementId { get; set; }
        public DateTime AchievedAt { get; set; }

        public virtual UserEntity User { get; set; }
        public virtual AchievementEntity Achievement { get; set; }
    }
}
