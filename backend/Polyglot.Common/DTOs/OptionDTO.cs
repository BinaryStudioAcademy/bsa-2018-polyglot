using Polyglot.DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.Common.DTOs
{
    public class OptionDTO
    {
        public int Id { get; set; }

        public int NotificationId { get; set; }
        public OptionDefinition OptionDefinition { get; set; }

        public OptionDTO()
        {
        }
    }
}
