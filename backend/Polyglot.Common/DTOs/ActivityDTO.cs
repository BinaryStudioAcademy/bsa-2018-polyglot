using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.Common.DTOs
{
    public class ActivityDTO
    {
        public string Message { get; set; }
        public DateTime? DateTime { get; set; }
        public UserProfileDTO User { get; set; }
    }
}
