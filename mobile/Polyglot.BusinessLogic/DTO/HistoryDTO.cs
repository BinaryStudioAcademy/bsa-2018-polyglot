using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.BusinessLogic.DTO
{
    public class HistoryDTO
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string AvatarUrl { get; set; }

        public string Action { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public DateTime When { get; set; }
    }
}
