using System;

namespace Polyglot.Common.DTOs.NoSQL
{
    public class HistoryDTO
    {
        public string UserName { get; set; }

        public string AvatarUrl { get; set; }

        public string Action { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public DateTime When { get; set; }
    }
}
