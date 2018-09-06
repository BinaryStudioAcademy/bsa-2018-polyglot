using System;

namespace Polyglot.DataAccess.Entities.Chat
{
    public class UserState : Entity
    {
        public int ChatUserId { get; set; }

        public virtual UserProfile ChatUser { get; set; }

        public DateTime LastSeen { get; set; }

        public bool IsOnline { get; set; }
    }
}
