using System.Collections.Generic;

namespace Polyglot.Common.DTOs.Chat
{
    public class ChatContactsDTO
    {
        public int ChatUserId { get; set; }

        public IEnumerable<ChatUserDTO> ContactList { get; set; }

        public ChatContactsDTO()
        {
            ContactList = new List<ChatUserDTO>();
        }
    }
}
