using Polyglot.DataAccess.Helpers;
using System;

namespace Polyglot.Common.DTOs.Chat
{
    public class ChatUserDTO
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public Role Role { get; set; }

        public DateTime LastSeen { get; set; }

        public bool IsOnline { get; set; }

    }
}
