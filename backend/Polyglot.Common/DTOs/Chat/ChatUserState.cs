using System;

namespace Polyglot.Common.DTOs.Chat
{
    public class ChatUserStateDTO
    {
        public int ChatUserId { get; set; }

        public DateTime LastSeen { get; set; }
    }
}
