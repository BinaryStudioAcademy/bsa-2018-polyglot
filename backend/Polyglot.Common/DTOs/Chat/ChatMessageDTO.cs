using System;

namespace Polyglot.Common.DTOs.Chat
{
    public class ChatMessageDTO
    {
        public int Id { get; set; }

        public long ClientId { get; set; }

        public int SenderId { get; set; }

        public string Body { get; set; }

        public DateTime ReceivedDate { get; set; }

        public bool IsRead { get; set; }

        public int DialogId { get; set; }

        public bool IsRecieved { get; set; }

        public bool IsRecieving { get; set; }
    }
}
