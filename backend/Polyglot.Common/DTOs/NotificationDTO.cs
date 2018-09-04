using Polyglot.DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.Common.DTOs
{
    public class NotificationDTO
    {
        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public UserProfileDTO Receiver { get; set; }

        public UserProfileDTO Sender { get; set; }

        public string Message { get; set; }

        public IEnumerable<OptionDTO> Options { get; set; }

    }
}
