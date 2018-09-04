using Polyglot.DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.Common.DTOs.Chat
{
    public class ChatDialogDTO
    {
        public int Id { get; set; }

        public IEnumerable<ChatUserDTO> Participants { get; set; }

        public int UnreadMessagesCount { get; set; }

        public string LastMessageText { get; set; }

        public ChatDialogDTO()
        {
            Participants = new List<ChatUserDTO>();
        }
    }
}
