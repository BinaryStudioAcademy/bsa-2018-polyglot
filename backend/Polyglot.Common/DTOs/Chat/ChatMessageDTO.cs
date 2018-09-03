using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.Common.DTOs.Chat
{
    public class ChatMessageDTO
    {
        public int Id { get; set; }

        public int SenderId { get; set; }

        public int RecipientId { get; set; }

        public string Body { get; set; }

        public DateTime ReceivedDate { get; set; }

        public bool IsRead { get; set; }
    }
}
